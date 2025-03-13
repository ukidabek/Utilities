using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using AnimatorController = UnityEditor.Animations.AnimatorController;

namespace Utilities.General.Animation
{
    public abstract class AnimatorControllerCollectionEditor : Editor
    {
        protected struct Data
        {
            public string name;
            public int hahs;
            public object[] info;
        }
        
        protected enum RefreshStatus
        {
            None = 0,
            Initialize = 1,
            Validation = 2,
            Apply = 3,
        }
        
        protected readonly List<AnimatorControllerDefinition> m_validParameters = new List<AnimatorControllerDefinition>(30);
        protected readonly List<AnimatorControllerDefinition> m_parametersToAdd = new List<AnimatorControllerDefinition>(30);
        protected readonly List<AnimatorControllerDefinition> m_parametersToRemove = new List<AnimatorControllerDefinition>(30);
        protected readonly List<bool> m_overrideDefinition = new List<bool>(30);
        protected AnimatorController m_animatorController = null;
        protected AnimatorControllerDefinition[] m_parameterDefinitions = null;
        protected ReorderableList m_parametersToAddList;
        protected ReorderableList m_parametersToRemoveList;
        internal AnimatorControllerCollectionInitializer.ConnectionFieldInfo FieldInfo = default;
        protected RefreshStatus m_refreshStatus = RefreshStatus.None;

        protected IEnumerable<IList> ParametersList
        {
            get
            {
                yield return m_parametersToAdd;
                yield return m_parametersToRemove;
                yield return m_validParameters;
                yield return m_overrideDefinition;
            }
        }

        protected virtual void OnEnable()
        {
            FieldInfo = AnimatorControllerCollectionInitializer.FieldInfoDictionary[target.GetType()];
            
            m_animatorController = FieldInfo.HandledAnimatorControllerFieldInfo.GetValue(target) as AnimatorController;
            m_parameterDefinitions = FieldInfo.DefinitionsFieldInfo.GetValue(target) as AnimatorControllerParameterDefinition[];

            m_parametersToAddList = new ReorderableList(
                m_parametersToAdd, 
                typeof(AnimatorControllerParameterDefinition), 
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
                typeof(AnimatorControllerParameterDefinition), 
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

        protected void GetRefreshInformation()
        {
            if (!GUILayout.Button("Refresh")) return;
            
            foreach (var list in ParametersList)
                list.Clear();
        
            var parameters = GetData();
            
            var length = parameters.Length;
            for (var i = 0; i < length; i++)
            {
                var parameter = parameters[i];
                var selectedDefinition = m_parameterDefinitions.FirstOrDefault(definition => definition.Hash == parameter.hahs);
                
                if (selectedDefinition is not null)
                {
                    m_validParameters.Add(selectedDefinition);
                    continue;
                }
                
                m_parametersToAdd.Add(CreateDefinition(parameter));
            }
            
            m_parametersToRemove.AddRange(m_parameterDefinitions.Except(m_validParameters));
            m_overrideDefinition.AddRange(new bool[m_parametersToRemove.Count]);
            
            m_refreshStatus = RefreshStatus.Validation;
        }

        protected abstract AnimatorControllerDefinition CreateDefinition(Data parameter);

        protected abstract Data[] GetData();

        private void ApplyChanges()
        {
            var lenght = m_parametersToRemove.Count - 1;
            for (var i = lenght; i >= 0; i--)
            {
                if (!m_overrideDefinition[i]) continue;
                var processedDefinition = m_parametersToRemove[i];
                OverrideDefinition(processedDefinition, m_parametersToAdd[i]);
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
            FieldInfo.DefinitionsFieldInfo.SetValue(target, ConvertParameterDefinition(m_parameterDefinitions));
            
            serializedObject.ApplyModifiedProperties();
            target.ApplyChanges();
            
            m_refreshStatus = RefreshStatus.None;
        }

        protected abstract object ConvertParameterDefinition(AnimatorControllerDefinition[] parameterDefinitions);
        
        protected abstract void OverrideDefinition(AnimatorControllerDefinition destination, AnimatorControllerDefinition animatorControllerDefinition);

        private void Initialize()
        {
            if (!GUILayout.Button("Initialize")) return;
            
            m_parameterDefinitions = GetDefinitions().ToArray();

            FieldInfo.DefinitionsFieldInfo.SetValue(target, m_parameterDefinitions);
            serializedObject.ApplyModifiedProperties();
            m_refreshStatus = RefreshStatus.None;
        }

        protected abstract IEnumerable<AnimatorControllerDefinition> GetDefinitions();

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
    }
}