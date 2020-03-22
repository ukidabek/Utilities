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

        private GUIStyle m_popupStyle = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            m_reference = property.FindPropertyRelative("m_reference");
            m_value = property.FindPropertyRelative("m_value");
            m_useReference = property.FindPropertyRelative("m_useReference");

            m_popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            m_popupStyle.imagePosition = ImagePosition.ImageOnly;

            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            Rect buttonRect = new Rect(position);
            buttonRect.yMin += m_popupStyle.margin.top;
            buttonRect.width = m_popupStyle.fixedWidth + m_popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            int result = m_useReference.boolValue ? 0 : 1;
            result = EditorGUI.Popup(buttonRect, result, m_popupOptions, m_popupStyle);
            m_useReference.boolValue = result == 0;

            EditorGUI.PropertyField(position,
                m_useReference.boolValue ? m_reference : m_value,
                GUIContent.none);

            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }
    }
}