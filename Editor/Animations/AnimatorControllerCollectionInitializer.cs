using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities.General.Animation
{
    public static class AnimatorControllerCollectionInitializer
    {
        internal const string Manu_Path = "Assets/Create/";
        internal const string Parameters_Collection_Manu_Path = Manu_Path + "AnimatorControllerParametersCollection";
        internal const string State_Collection_Manu_Path = Manu_Path + "AnimatorControllerStatesCollection";

        
        internal const BindingFlags BindingsFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
        
        internal struct ConnectionFieldInfo
        {
            public FieldInfo DefinitionsFieldInfo;
            public FieldInfo HandledAnimatorControllerFieldInfo;
        }
        
        internal static Dictionary<Type, ConnectionFieldInfo> FieldInfoDictionary = new Dictionary<Type, ConnectionFieldInfo>();
        
        [InitializeOnLoadMethod]
        private static void GetTypeInfo()
        {
            var derivedTypes = TypeCache.GetTypesDerivedFrom(typeof(AnimatorControllerCollection<>));
            foreach (var type in derivedTypes)
            {
                FieldInfoDictionary.Add(type, new ConnectionFieldInfo()
                {
                    HandledAnimatorControllerFieldInfo = type.GetField("m_animatorController", BindingsFlags),
                    DefinitionsFieldInfo = type.GetField("m_definitions", BindingsFlags)
                });
            }
        }
        
        [MenuItem(Parameters_Collection_Manu_Path, true)]
        [MenuItem(State_Collection_Manu_Path, true)]
        private static bool IsSelectionValid()
        {
            var activeObject = Selection.activeObject;
            return activeObject is AnimatorController;
        }

        [MenuItem(Parameters_Collection_Manu_Path)]
        private static void CreateAnimatorControllerHandler()
        {
            var animatorController = Selection.activeObject as AnimatorController;
            CreateAnimatorControllerCollection<AnimatorControllerParametersCollection, AnimatorControllerParameterDefinition>(
                animatorController, 
                (controller, collection) =>
                controller.parameters
                    .Select(parameter =>
                    {
                        var definition = ScriptableObject.CreateInstance<AnimatorControllerParameterDefinition>()
                            .Initialize(controller, (parameter.name, parameter.nameHash, (int)parameter.type));
                        AssetDatabase.AddObjectToAsset(definition, collection);
                        return definition;
                    }));
        }
        
        [MenuItem(State_Collection_Manu_Path)]
        private static void CreateAnimatorControllerStatesCollection()
        {
            var animatorController = Selection.activeObject as AnimatorController;
            var index = 0;
            CreateAnimatorControllerCollection<AnimatorControllerStatesCollection, AnimatorControllerStateDefinition>(
                animatorController, (controller, collection) =>
                    controller.layers.SelectMany(layer =>
                        {
                            var currentIndex = index++;
                            return layer.stateMachine.states.Select(state => (
                                layername: layer.name,
                                layerIndex: currentIndex,
                                stateName: state.state.name,
                                nameHash: state.state.nameHash));
                        })
                        .Select(stateInfo =>
                        {
                            var definition = ScriptableObject.CreateInstance<AnimatorControllerStateDefinition>()
                                .Initialize(animatorController, stateInfo);
                            AssetDatabase.AddObjectToAsset(definition, collection);
                            return definition;
                        }));
        }

        private static void CreateAnimatorControllerCollection<T, T1>(
            AnimatorController animatorController,
            Func<AnimatorController, T, IEnumerable<T1>> createDefinitions)
            where T : AnimatorControllerCollection<T1>
            where T1 : AnimatorControllerDefinition
        {
            var type = typeof(T);
            if(!FieldInfoDictionary.TryGetValue(type, out var fieldInfo)) return;
            
            var savePath = AssetDatabase.GetAssetPath(animatorController);
            savePath = Path.GetDirectoryName(savePath);
            savePath = Path.Join(savePath, $"{animatorController.name}_{type.Name}.asset");
			
            var collection = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(collection, savePath);

            fieldInfo.HandledAnimatorControllerFieldInfo.SetValue(collection, animatorController);
            
            var parameterDefinitions = createDefinitions(animatorController, collection).ToArray();

            fieldInfo.DefinitionsFieldInfo.SetValue(collection, parameterDefinitions);
            
            collection.ApplyChanges();
        }
        
        public static void ApplyChanges(this Object instance)
        {
            EditorUtility.SetDirty(instance);
            AssetDatabase.SaveAssetIfDirty(instance);
            AssetDatabase.Refresh();
        }
    }
}