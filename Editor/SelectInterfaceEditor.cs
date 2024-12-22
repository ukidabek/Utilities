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
        private Vector2 m_scrollPosition = Vector2.zero;

        private SerializedProperty property;
        private ClassConstructor m_classConstructor;

        private string m_searchQuire = string.Empty;
        private IEnumerable<Type> m_searchResult;
        private FieldInfo m_field = null;

        public SelectInterfaceEditor(Rect position, SerializedProperty property)
        {
            this.position = position;
            this.property = property;

            m_field = property.serializedObject.targetObject.GetType().GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Instance);
            m_classConstructor = m_field.GetValue(property.serializedObject.targetObject) as ClassConstructor;
        }

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            {
                m_searchQuire = EditorGUILayout.TextField("Search", m_searchQuire);
            }
            if(EditorGUI.EndChangeCheck())
                m_searchResult = m_classConstructor.GetType().Assembly.GetTypes().Where(t => t.Name.Contains(m_searchQuire));

            m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);
            {
                if(m_searchResult != null && m_searchResult.Any())
                {
                    foreach (var item in m_searchResult)
                    {
                        if (!GUILayout.Button(item.Name)) continue;
                        m_field.SetValue(property.serializedObject.targetObject, new ClassConstructor(item));
                        property.serializedObject.ApplyModifiedProperties();
                        Close();
                        break;
                    }
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}