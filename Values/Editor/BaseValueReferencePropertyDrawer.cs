using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Utilities.Values
{
    [CustomPropertyDrawer(typeof(BaseValueReference), true)]
    public class BaseValueReferencePropertyDrawer : PropertyDrawer
    {
        private readonly string[] m_popupOptions =
            {"Use reference", "Use value"};

        private SerializedProperty m_reference = null;
        private SerializedProperty m_value = null;
        private SerializedProperty m_useReference = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            m_reference = property.FindPropertyRelative("m_reference");
            m_value = property.FindPropertyRelative("m_value");
            m_useReference = property.FindPropertyRelative("m_useReference");

            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            position.x -= 15 * EditorGUI.indentLevel;

            Rect buttonRect = new Rect(position) {width = 135};

            position.x = buttonRect.x + buttonRect.width - 15 * EditorGUI.indentLevel;
            position.width -= buttonRect.width - 15 * EditorGUI.indentLevel;

            int result = m_useReference.boolValue ? 0 : 1;
            result = EditorGUI.Popup(buttonRect, result, m_popupOptions);
            m_useReference.boolValue = result == 0;

            EditorGUI.PropertyField(position,
                m_useReference.boolValue ? m_reference : m_value,
                GUIContent.none);

            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }
    }
}