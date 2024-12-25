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
            
            var propertyHeight = EditorGUI.GetPropertyHeight(property, label, true);

            if (property.managedReferenceValue == null) return propertyHeight;
            
            propertyHeight = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
            propertyHeight += base.GetPropertyHeight(property, label);

            return propertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            EditorGUI.BeginProperty(position, label, property);
            
            var controlPosition = EditorGUI.PrefixLabel(position, label);

            if (property.managedReferenceValue != null)
            {
                var typeName = property.managedReferenceValue.GetType().Name;
                var propertyLabel = new GUIContent(typeName);
                EditorGUIUtility.labelWidth = controlPosition.width * .4f;
                EditorGUI.indentLevel = 1;
                EditorGUI.PropertyField(controlPosition, property, propertyLabel, property.isExpanded);
                controlPosition.y += EditorGUI.GetPropertyHeight(property, label);
                controlPosition.height = EditorGUIUtility.singleLineHeight;
            }

            EditorGUI.indentLevel = 0;
            controlPosition.width /= 2f;
            if (GUI.Button(controlPosition, "Select Type"))
            {
                var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                var context = new SearchWindowContext(mousePosition);
                m_typeProvider.Property = property;
                SearchWindow.Open(context, m_typeProvider);
            }
            
            controlPosition.x += controlPosition.width;
            if (GUI.Button(controlPosition, "Clear"))
            {
                property.managedReferenceValue = null;
                property.serializedObject.ApplyModifiedProperties();
            }
            
            EditorGUI.EndProperty();
        }
    }
}