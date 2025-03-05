using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Utilities.General.Animation
{
    public class AnimatorControllerHandler : ScriptableObject
    {
        private const string Manu_Path = "Assets/Create/AnimatorControllerHandler";

        [MenuItem(Manu_Path, true)]
        private static bool IsSelectionValid()
        {
            var activeObject = Selection.activeObject;
            return activeObject is AnimatorController;
        }

        [MenuItem(Manu_Path)]
        private static void CreateAnimatorControllerHandler()
        {
            var animatorController = Selection.activeObject as AnimatorController;

            var savePath = AssetDatabase.GetAssetPath(animatorController);
            savePath = Path.GetDirectoryName(savePath);
            savePath = Path.Join(savePath, $"{animatorController.name}_{nameof(AnimatorControllerHandler)}.asset");
			
            var instance = CreateInstance<AnimatorControllerHandler>();
            AssetDatabase.CreateAsset(instance, savePath);

            instance.Initialize(animatorController);

            RefreshAsset(instance);
        }

        private static void RefreshAsset(AnimatorControllerHandler instance)
        {
            EditorUtility.SetDirty(instance);
            AssetDatabase.SaveAssetIfDirty(instance);
            AssetDatabase.Refresh();
        }

        [SerializeField] private AnimatorController m_handledAnimatorController = null;
        [SerializeField] private AnimatorParameterDefinition[] m_parameterDefinitions = null;
        
        private void Initialize(AnimatorController animatorController)
        {
            m_handledAnimatorController = animatorController;
            m_parameterDefinitions = m_handledAnimatorController.parameters
                .Select(CreateDefinition)
                .ToArray();
        }

        private AnimatorParameterDefinition CreateDefinition(AnimatorControllerParameter parameter)
        {
            var definition = CreateInstance<AnimatorParameterDefinition>();
            definition.Initialize(parameter);
            AssetDatabase.AddObjectToAsset(definition, this);
            return definition;
        }

        [ContextMenu("Refresh")]
        private void Refresh()
        {
            var validParameters = new List<AnimatorParameterDefinition>(30);
            var parametersToAdd = new List<AnimatorParameterDefinition>(30);
            var parameters = m_handledAnimatorController.parameters;
            var length = parameters.Length;
            var definitionsLength = m_parameterDefinitions.Length;
            
            for (var i = 0; i < length; i++)
            {
                var parameter = parameters[i];
                var hash = Animator.StringToHash(parameter.name);
                var selectedDefinition = m_parameterDefinitions.FirstOrDefault(definition => definition.Hash == hash);
                if (selectedDefinition is not null)
                {
                    validParameters.Add(selectedDefinition);
                    continue;
                }

                if (i < definitionsLength)
                {
                    selectedDefinition = m_parameterDefinitions[i];
                    if (selectedDefinition.Type == parameter.type)
                    {
                        selectedDefinition.Initialize(parameter);
                        validParameters.Add(selectedDefinition);
                        continue;
                    }
                }

                parametersToAdd.Add(CreateDefinition(parameter));
            }
            
            var itemsToRemoves = m_parameterDefinitions.Except(validParameters);
            foreach (var parameter in itemsToRemoves)
            {
                AssetDatabase.RemoveObjectFromAsset(parameter);
            }
            
            m_parameterDefinitions = validParameters.Concat(parametersToAdd).ToArray();
            
            RefreshAsset(this);
        }
    }
}