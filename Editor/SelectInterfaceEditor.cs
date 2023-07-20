using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Utilities.General
{
    public class SelectInterfaceEditor : EditorWindow
    {
        private Vector2 scrollPosition = Vector2.zero;

        private SerializedProperty property;
        private ClassConstructor classsConstructor;

        private string serchQuire = string.Empty;
        private IEnumerable<Type> searchResult;
        private FieldInfo field = null;

        public SelectInterfaceEditor(Rect position, SerializedProperty property)
        {
            this.position = position;
            this.property = property;

            field = property.serializedObject.targetObject.GetType().GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Instance);
            classsConstructor = field.GetValue(property.serializedObject.targetObject) as ClassConstructor;
        }

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            {
                serchQuire = EditorGUILayout.TextField("Search", serchQuire);
            }
            if(EditorGUI.EndChangeCheck())
                searchResult = classsConstructor.GetType().Assembly.GetTypes().Where(t => t.Name.Contains(serchQuire));

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            {
                if(searchResult != null && searchResult.Count() > 0)
                {
                    foreach (var item in searchResult)
                    {
                        if(GUILayout.Button(item.Name))
                        {
                            field.SetValue(property.serializedObject.targetObject, new ClassConstructor(item));
                            property.serializedObject.ApplyModifiedProperties();
                            this.Close();
                            break;
                        }
                    }
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}