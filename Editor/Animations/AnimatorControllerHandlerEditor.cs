using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using AnimatorController = UnityEditor.Animations.AnimatorController;

namespace Utilities.General.Animation
{
    [CustomEditor(typeof(AnimatorControllerHandler))]
    public class AnimatorControllerHandlerEditor : Editor
    {
        private enum RefreshStatus
        {
            None = 0,
            Initialize = 1,
            Validation = 2,
            Apply = 3,
        }
        
        private List<AnimatorParameterDefinition> m_validParameters = new List<AnimatorParameterDefinition>(30);
        private List<AnimatorParameterDefinition> m_parametersToAdd = new List<AnimatorParameterDefinition>(30);
        private List<AnimatorParameterDefinition> m_parametersToRemove = new List<AnimatorParameterDefinition>(30);
        private List<bool> m_overrideDefinition = new List<bool>(30);
            
        private IEnumerable<IList> ParametersList
        {
            get
            {
                yield return m_parametersToAdd;
                yield return m_parametersToRemove;
                yield return m_validParameters;
                yield return m_overrideDefinition;
            }
        }
    
        private AnimatorController m_handledAnimatorController = null;
        private AnimatorParameterDefinition[] m_parameterDefinitions = null;

        private RefreshStatus m_refreshStatus = RefreshStatus.None;

        private ReorderableList m_parametersToAddList;
        private ReorderableList m_parametersToRemoveList;
        
        private void OnEnable()
        {
            m_handledAnimatorController = AnimatorControllerHandlerInitializer.HandledAnimatorControllerFieldInfo.GetValue(target) as AnimatorController;
            m_parameterDefinitions = AnimatorControllerHandlerInitializer.ParameterDefinitionsFieldInfo.GetValue(target) as AnimatorParameterDefinition[];

            m_parametersToAddList = new ReorderableList(
                m_parametersToAdd, 
                typeof(AnimatorParameterDefinition), 
                true,
                true, 
                false, 
                false)
            {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, "To add:"),
                drawElementCallback = (rect, index, _, _) => EditorGUI.LabelField(rect, m_parametersToAdd[index].name)
            };
            
            m_parametersToRemoveList = new ReorderableList(
                m_parametersToRemove, 
                typeof(AnimatorParameterDefinition), 
                true,
                true, 
                false, 
                false)
            {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, "To remove:"),
                drawElementCallback = (rect, index, _, _) => EditorGUI.LabelField(rect, m_parametersToRemove[index].name)
            };
            
            if (m_parameterDefinitions == null || m_parameterDefinitions.Length == 0)
                m_refreshStatus = RefreshStatus.Initialize;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            switch (m_refreshStatus)
            {
                case RefreshStatus.None:
                    GetRefreshInformation();
                    break;
                case RefreshStatus.Initialize:
                    Initialize();
                    break;
                case RefreshStatus.Validation:
                    ValidateChanges();
                    break;
                case RefreshStatus.Apply:
                    ApplyChanges();
                    break;
            }
        }

        private void ApplyChanges()
        {
            var lenght = m_parametersToRemove.Count - 1;
            for (var i = lenght; i >= 0; i--)
            {
                if (!m_overrideDefinition[i]) continue;
                var processedDefinition = m_parametersToRemove[i];
                processedDefinition.Initialize(m_parametersToAdd[i]);
                EditorUtility.SetDirty(processedDefinition);
                m_validParameters.Add(processedDefinition);
                m_parametersToAdd.RemoveAt(i);
                m_parametersToRemove.RemoveAt(i);
            }

            foreach (var itemToAdd in m_parametersToAdd) 
                AssetDatabase.AddObjectToAsset(itemToAdd,target);
            
            foreach (var itemToRemove in m_parametersToRemove) 
                AssetDatabase.RemoveObjectFromAsset(itemToRemove);

            m_parameterDefinitions = m_validParameters.Concat(m_parametersToAdd).ToArray();
            AnimatorControllerHandlerInitializer.ParameterDefinitionsFieldInfo.SetValue(target, m_parameterDefinitions);
            
            serializedObject.ApplyModifiedProperties();
            (target as AnimatorControllerHandler).ApplyChanges();
            
            m_refreshStatus = RefreshStatus.None;
        }

        private void Initialize()
        {
            if (!GUILayout.Button("Initialize")) return;
            m_parameterDefinitions = m_handledAnimatorController.parameters
                .Select(CreateInstance<AnimatorParameterDefinition>().Initialize)
                .Select(parameter =>
                {
                    AssetDatabase.AddObjectToAsset(parameter, target);
                    return parameter;
                })
                .ToArray();

            AnimatorControllerHandlerInitializer.ParameterDefinitionsFieldInfo.SetValue(target, m_parameterDefinitions);
            serializedObject.ApplyModifiedProperties();
            m_refreshStatus = RefreshStatus.None;
        }
        
        private void ValidateChanges()
        {
            if (m_parametersToAdd.Count == 0 && m_parametersToRemove.Count == 0)
            {
                m_refreshStatus = RefreshStatus.None;
                return;
            }

            EditorGUILayout.BeginHorizontal();
            m_parametersToAddList.DoLayoutList();

            var lenght = m_overrideDefinition.Count;
            GUILayout.BeginVertical(GUILayout.Width(15f));
            
            GUILayout.Space(23);
            for (var i = 0; i < lenght; i++)
            {
                m_overrideDefinition[i] = EditorGUILayout.Toggle(
                    m_overrideDefinition[i],  
                    GUILayout.Height(23),
                    GUILayout.Width(15f));
            }

            GUILayout.EndVertical();
            m_parametersToRemoveList.DoLayoutList();
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Apply"))
                m_refreshStatus = RefreshStatus.Apply;

            if (GUILayout.Button("Cancel"))
                m_refreshStatus = RefreshStatus.None;
            
            EditorGUILayout.EndHorizontal();
        }

        private void GetRefreshInformation()
        {
            if (!GUILayout.Button("Refresh")) return;
            foreach (var list in ParametersList)
                list.Clear();
        
            var parameters = m_handledAnimatorController.parameters;
            var length = parameters.Length;
            
            for (var i = 0; i < length; i++)
            {
                var parameter = parameters[i];
                var hash = Animator.StringToHash(parameter.name);
                var selectedDefinition = m_parameterDefinitions.FirstOrDefault(definition => definition.Hash == hash);
                
                if (selectedDefinition is not null)
                {
                    m_validParameters.Add(selectedDefinition);
                    continue;
                }
                
                m_parametersToAdd.Add(CreateInstance<AnimatorParameterDefinition>().Initialize(parameter));
            }
            
            m_parametersToRemove.AddRange(m_parameterDefinitions.Except(m_validParameters));
            m_overrideDefinition.AddRange(new bool[m_parametersToRemove.Count]);
            
            m_refreshStatus = RefreshStatus.Validation;
        }
    }
}
