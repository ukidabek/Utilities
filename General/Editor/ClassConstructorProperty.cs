using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BaseGameLogic.Utilities
{
    [CustomPropertyDrawer(typeof(ClassConstructor))]
    public class ClassInfoProperty : PropertyDrawer
    {
        private List<string> constructorNames = new List<string>();
        private List<ClassConstructor> _stateInfoList = new List<ClassConstructor>();

        private ClassConstructor classsConstructor = null;
        private FieldInfo field = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (classsConstructor == null)
            {
                field = property.serializedObject.targetObject.GetType().GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Instance);
                classsConstructor = field.GetValue(property.serializedObject.targetObject) as ClassConstructor;
            }

            if (constructorNames.Count == 0)
            {
                var types = GetTypes();
                if(types != null && types.Length > 0)
                {
                    foreach (var type in GetTypes())
                    {
                        foreach (var constructor in type.GetConstructors())
                        {
                            _stateInfoList.Add(new ClassConstructor(constructor, classsConstructor.BaseType));
                            constructorNames.Add(_stateInfoList[_stateInfoList.Count - 1].Name);
                        }
                    }

                    if (string.IsNullOrEmpty(classsConstructor.Type.FullName) && string.IsNullOrEmpty(classsConstructor.Type.AssemblFullName))
                        classsConstructor = _stateInfoList[0];
                }
            }

            return EditorGUIUtility.singleLineHeight * (classsConstructor.Parameters == null ? 1 : classsConstructor.Parameters.Length + 2);
        }

        private Type[] GetTypes()
        {
            Type type = (Type)classsConstructor.BaseType;
            Func<Type, bool> quiry = (Type arg) => { return type.IsAssignableFrom(arg) && arg.BaseType == typeof(System.Object) && !arg.IsInterface; };
            var typesEnumerator = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(quiry);
            if (typesEnumerator.Count() == 0)
                return null;
            return typesEnumerator == null ? null : typesEnumerator.ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int index = _stateInfoList.IndexOf(classsConstructor);
            if (index < 0)
            {
                classsConstructor = _stateInfoList[index = 0];
                field.SetValue(property.serializedObject.targetObject, classsConstructor);
            }

            if(_stateInfoList.Count < 0)
            {
                EditorGUI.LabelField(position, new GUIContent(string.Format("There is no classes that extends or implement {0}.", ((Type)classsConstructor.BaseType).Name)));
                return;
            }

            Rect rect = position;
            rect.height = EditorGUIUtility.singleLineHeight;
            GUI.Label(rect, new GUIContent(property.displayName));
            rect.y += EditorGUIUtility.singleLineHeight;
            index = EditorGUI.Popup(rect, index, constructorNames.ToArray());
            for (int i = 0; i < classsConstructor.Parameters.Length; i++)
            {
                rect.y += EditorGUIUtility.singleLineHeight;
                Type parametersType = classsConstructor.Parameters[i].Type;
                GUIContent parameterLabel = new GUIContent(classsConstructor.Parameters[i].ParameterName);
                switch (parametersType.Name)
                {
                    case "Int32":
                        classsConstructor.Parameters[i].IntValue = EditorGUI.IntField(rect, parameterLabel, classsConstructor.Parameters[i].IntValue);
                        break;
                    case "Single":
                        classsConstructor.Parameters[i].FloatValue = EditorGUI.FloatField(rect, parameterLabel, classsConstructor.Parameters[i].FloatValue);
                        break;
                    case "Boolean":
                        classsConstructor.Parameters[i].BoolValue = EditorGUI.Toggle(rect, parameterLabel, classsConstructor.Parameters[i].BoolValue);
                        break;
                    case "String":
                        classsConstructor.Parameters[i].StringValue = EditorGUI.TextField(rect, parameterLabel, classsConstructor.Parameters[i].StringValue);
                        break;
                    default:
                        classsConstructor.Parameters[i].ObjectValue = EditorGUI.ObjectField(rect, parameterLabel, classsConstructor.Parameters[i].ObjectValue, parametersType, true);
                        break;
                }
            }

            if (classsConstructor != _stateInfoList[index])
            {
                classsConstructor = _stateInfoList[index];
                field.SetValue(property.serializedObject.targetObject, classsConstructor);
            }

            property.serializedObject.ApplyModifiedProperties();
        }
    }
}