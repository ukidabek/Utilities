using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Utilities.General.Animation
{
    public static class AnimatorControllerHandlerInitializer
    {
        internal static FieldInfo ParameterDefinitionsFieldInfo;
        internal static FieldInfo HandledAnimatorControllerFieldInfo;
        private const string Manu_Path = "Assets/Create/AnimatorControllerHandler";

        [InitializeOnLoadMethod]
        private static void GetTypeInfo()
        {
            var type = typeof(AnimatorControllerHandler);
            var bindingsFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            HandledAnimatorControllerFieldInfo = type.GetField("m_handledAnimatorController", bindingsFlags);
            ParameterDefinitionsFieldInfo = type.GetField("m_parameterDefinitions", bindingsFlags);
        }
        
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
			
            var instance = ScriptableObject.CreateInstance<AnimatorControllerHandler>();
            AssetDatabase.CreateAsset(instance, savePath);

            HandledAnimatorControllerFieldInfo.SetValue(instance, animatorController);
            
            var parameterDefinitions = animatorController.parameters
                .Select(parameter =>
                {
                    var definition = ScriptableObject.CreateInstance<AnimatorParameterDefinition>().Initialize(parameter);
                    AssetDatabase.AddObjectToAsset(definition, instance);
                    return definition;
                })
                .ToArray();

            ParameterDefinitionsFieldInfo.SetValue(instance, parameterDefinitions);
            
            instance.ApplyChanges();
        }

        public static void ApplyChanges(this AnimatorControllerHandler instance)
        {
            EditorUtility.SetDirty(instance);
            AssetDatabase.SaveAssetIfDirty(instance);
            AssetDatabase.Refresh();
        }
    }
}