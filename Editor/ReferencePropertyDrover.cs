using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Utilities.General
{
    [CustomPropertyDrawer(typeof(ReferenceAttribute))]    
    public class ReferencePropertyDrover : PropertyDrawer
    {
        private TypeProvider m_typeProvider = null;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (m_typeProvider == null)
            {
                m_typeProvider = ScriptableObject.CreateInstance<TypeProvider>();
                m_typeProvider.GenerateTreeEntries(fieldInfo.FieldType);
            }

            var height = base.GetPropertyHeight(property, label);
            var propertyHeight = EditorGUI.GetPropertyHeight(property, label, true);

            if (property.managedReferenceValue != null)
            {
                propertyHeight = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
            }
            
            return height + propertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var labelPosition = position;
            labelPosition.width = EditorGUIUtility.labelWidth;
            GUI.Label(labelPosition, label);
            var valuePosition = position;
            valuePosition.x = labelPosition.width + 19;
            valuePosition.width -= EditorGUIUtility.labelWidth;
            valuePosition.height = EditorGUIUtility.singleLineHeight;

            if (property.managedReferenceValue != null)
            {
                var typeName = property.managedReferenceValue.GetType().Name;
                EditorGUI.PropertyField(valuePosition, property, new GUIContent(typeName), property.isExpanded);
                valuePosition.y += EditorGUI.GetPropertyHeight(property, label);
            }

            if (GUI.Button(valuePosition, "Select Type"))
            {
                var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                var context = new SearchWindowContext(mousePosition);
                m_typeProvider.Property = property;
                SearchWindow.Open(context, m_typeProvider);
            }
        }
    }
}