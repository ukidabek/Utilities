using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

namespace Utilities.General
{
#if UNITY_2023_1_OR_NEWER
    [CustomPropertyDrawer(typeof(ReferenceListAttribute))]
#endif
    public class ReferenceListPropertyDrover : PropertyDrawer
    {
        private ReorderableList m_reorderableList = null;
        private TypeProvider m_typeProvider = null;
        private int m_activeIndex = -1;
        private bool? m_isTypeInvalid;
        private Type m_baseType = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (m_isTypeInvalid.Value)
            {
                EditorGUI.LabelField(position, $"Field {label} is invalid!");
                return;
            }

            m_reorderableList.DoList(position);
            m_reorderableList.serializedProperty.serializedObject.UpdateIfRequiredOrScript();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            m_baseType = null;
            if (!m_isTypeInvalid.HasValue)
            {
                m_isTypeInvalid = !property.isArray;
                if (m_isTypeInvalid.Value)
                    return base.GetPropertyHeight(property, label);

                var genericArguments = fieldInfo.FieldType.GenericTypeArguments;
                if (genericArguments != null && genericArguments.Any())
                {
                    m_isTypeInvalid = genericArguments.Length > 1;
                    if (m_isTypeInvalid.Value)
                        return base.GetPropertyHeight(property, label);
                    m_baseType = genericArguments.First();
                }
                else
                    m_baseType = fieldInfo.FieldType.GetElementType();

                m_isTypeInvalid = m_baseType.IsSubclassOf(typeof(UnityEngine.Object));
                if (m_isTypeInvalid.Value)
                    return base.GetPropertyHeight(property, label);
                
                /*TODO Add additional validation if property have SerializeReference assigned*/
                
                // var type = Type.GetType(property.type);
                // var attributes = type.GetCustomAttributes(true);
                // m_isTypeInvalid = !attributes.OfType<SerializeReference>().Any();
            }
            
            if(m_isTypeInvalid.Value)
                return base.GetPropertyHeight(property, label);

            if (m_typeProvider == null)
            {
                m_typeProvider = ScriptableObject.CreateInstance<TypeProvider>();
                m_typeProvider.GenerateTreeEntries(m_baseType);
            }
            
            m_reorderableList ??= new ReorderableList(
                property.serializedObject, 
                property, 
                true, 
                true, 
                true, 
                true)
            {
                onAddCallback = OnAddCallback,
                onRemoveCallback = OnRemoveCallback,
                drawHeaderCallback = DrawHeaderCallback, 
                drawElementCallback = DrawElementCallback,
                elementHeightCallback = ElementHeightCallback,
            };
            
            return m_reorderableList.GetHeight();
        }

        private void DrawHeaderCallback(Rect rect)
        {
            var serializedPropertyDisplayName = m_reorderableList.serializedProperty.displayName;
            EditorGUI.LabelField(rect, serializedPropertyDisplayName);
        }

        private void OnRemoveCallback(ReorderableList list)
        {
            if (m_activeIndex < 0) return;
            var listSerializedProperty = list.serializedProperty;
            listSerializedProperty.DeleteArrayElementAtIndex(m_activeIndex);
            listSerializedProperty.serializedObject.ApplyModifiedProperties();
        }

        private float ElementHeightCallback(int index)
        {
            var element = m_reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            var elementHeight = EditorGUI.GetPropertyHeight(element, element.isExpanded);
            return elementHeight;
        }

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (isActive) m_activeIndex = index;
            var element = m_reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            var elementManagedReferenceValue = element.managedReferenceValue;
            
            if (elementManagedReferenceValue != null)
            {
                var margin = ReferencePropertyDroverHelper.Margin;
                rect.x += margin;
                rect.width -= margin;
                var elementType = elementManagedReferenceValue.GetType();
                EditorGUI.PropertyField(rect, element, new GUIContent(elementType.Name), element.isExpanded);
                var elementSerializedObject = element.serializedObject;
                elementSerializedObject.ApplyModifiedProperties();
                elementSerializedObject.UpdateIfRequiredOrScript();
                return;
            }
           
            var warningStyle = new GUIStyle(EditorStyles.label)
            {
                normal =
                {
                    textColor = Color.red
                },
                fontStyle = FontStyle.Bold
            };
            EditorGUI.LabelField(rect, "This class is null! Maybe the class name was changed. Use the [MovedFrom] attribute!", warningStyle);
        }

        private void OnAddCallback(ReorderableList list)
        {
            var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            var context = new SearchWindowContext(mousePosition);
            m_typeProvider.Property = list.serializedProperty;
            SearchWindow.Open(context, m_typeProvider);
        }
    }
}