using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BaseGameLogic.CustomTypes
{

    [CustomPropertyDrawer(typeof(CustomType))]
    public class CustomTypePropertyDrawer : PropertyDrawer
    {
        private class Popup
        {
            List<string> values = null;
            private SerializedProperty property = null;
            private int index = -1;

            public string Value { get => property.stringValue; }

            public Popup(SerializedProperty property, string name, List<string> groups)
            {
                this.values = groups;
                this.property = property.FindPropertyRelative(name);
                index = groups.IndexOf(this.property.stringValue);
                index = index < 0 ? 0 : index;
            }

            public void DrawPopup(Rect position)
            {
                index = EditorGUI.Popup(position, index, values.ToArray());
                property.stringValue = values[index];
            }
        }

        private Popup group = null;
        private Popup type = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            group = new Popup(property, "groupName", TypesManager.Instance.GetGroups());
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect currentPosition = position;
            currentPosition.width = position.width / 2;
            group.DrawPopup(currentPosition);
            currentPosition.x += currentPosition.width;
            type = new Popup(property, "typeName", TypesManager.Instance.GetTypes(group.Value));
            type.DrawPopup(currentPosition);
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}