using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Utilities.General
{
    [CustomPropertyDrawer(typeof(ClassConstructor))]
    public class ClassInfoProperty : PropertyDrawer
    {
        private List<string> constructorNames = new List<string>();
        private List<ClassConstructor> _constructorsList = new List<ClassConstructor>();

        private ClassConstructor classsConstructor = null;
        private FieldInfo field = null;
        private Type baseType;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Initialize(property);
            return EditorGUIUtility.singleLineHeight * (classsConstructor.Parameters == null ? 1 : classsConstructor.Parameters.Length + 3);
        }

        private void Initialize(SerializedProperty property)
        {
            field = property.serializedObject.targetObject.GetType().GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Instance);
            classsConstructor = field.GetValue(property.serializedObject.targetObject) as ClassConstructor;

            if (_constructorsList.Count == 0 || (_constructorsList.Count > 0 && (Type)_constructorsList[0].BaseType != (Type)classsConstructor.BaseType))
            {
                constructorNames.Clear();
                _constructorsList.Clear();
                var types = GetTypes();
                if (types != null && types.Length > 0)
                {
                    foreach (var type in types)
                    {
                        foreach (var constructor in type.GetConstructors())
                        {
                            _constructorsList.Add(new ClassConstructor(constructor, classsConstructor.BaseType));
                            constructorNames.Add(_constructorsList[_constructorsList.Count - 1].Name);
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(classsConstructor.Type.FullName) && string.IsNullOrEmpty(classsConstructor.Type.AssemblFullName) && _constructorsList.Count > 0)
                classsConstructor = _constructorsList[0];
        }

        private Type[] GetTypes()
        {
            baseType = classsConstructor.BaseType;
            if (baseType == null)
                return null;

            Func<Type, bool> quiry = (Type arg) => { return baseType.IsAssignableFrom(arg) && arg.BaseType == typeof(System.Object) && !arg.IsInterface; };
            var typesEnumerator = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(quiry);
            if (typesEnumerator.Count() == 0)
                return null;

            return typesEnumerator == null ? null : typesEnumerator.ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect interfaceSelect = position;
            interfaceSelect.height = EditorGUIUtility.singleLineHeight;

            GUI.Label(interfaceSelect, new GUIContent(property.displayName));
            interfaceSelect.y += EditorGUIUtility.singleLineHeight;

            Type baseType = classsConstructor.BaseType;

            if (GUI.Button(interfaceSelect, new GUIContent(baseType == null ? "No interface selected." : baseType.Name)))
                new SelectInterfaceEditor(position, property).Show();

            int index = _constructorsList.IndexOf(classsConstructor);
            interfaceSelect.y += EditorGUIUtility.singleLineHeight;

            if (_constructorsList.Count <= 0)
            {
                if (baseType == null)
                    EditorGUI.LabelField(interfaceSelect, new GUIContent("No interface selected."));
                else
                    EditorGUI.LabelField(interfaceSelect, new GUIContent(string.Format("There is no classes that extends or implement {0}.", ((Type)classsConstructor.BaseType).Name)));
                return;
            }

            Rect rect = interfaceSelect;
            rect.height = EditorGUIUtility.singleLineHeight;
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
                    case "Color":
                        classsConstructor.Parameters[i].ColorValue = EditorGUI.ColorField(rect, parameterLabel, classsConstructor.Parameters[i].ColorValue);
                        break;
                    default:
                        classsConstructor.Parameters[i].ObjectValue = EditorGUI.ObjectField(rect, parameterLabel, classsConstructor.Parameters[i].ObjectValue, parametersType, true);
                        break;
                }
            }

            if (classsConstructor != _constructorsList[index])
            {
                classsConstructor = _constructorsList[index];
                field.SetValue(property.serializedObject.targetObject, classsConstructor);
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}