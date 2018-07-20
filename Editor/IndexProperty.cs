using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BaseGameLogic.Utilities
{
    [CustomPropertyDrawer(typeof(Index))]
    public class IndexProperty : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var index = property.FindPropertyRelative("_current");
            var count = property.FindPropertyRelative("Count");
            float width = position.width / 3;
            var rect = new Rect(position.position, new Vector2(width, EditorGUIUtility.singleLineHeight));
            EditorGUI.LabelField(rect, new GUIContent(property.displayName));
            rect.x += width;
            float separatorWeight = 10;
            rect.width -= separatorWeight / 2;
            EditorGUI.PropertyField(rect, index, new GUIContent(""));
            rect.x += width;
            EditorGUI.LabelField(new Rect(new Vector2(rect.x - separatorWeight / 2, rect.y), new Vector2(separatorWeight, EditorGUIUtility.singleLineHeight)), new GUIContent("/"));
            rect.x += separatorWeight / 2;
            EditorGUI.PropertyField(rect, count, new GUIContent(""));
        }
    }
}
