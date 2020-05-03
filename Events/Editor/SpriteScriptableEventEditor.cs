using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utilities.Events
{
    [CustomEditor(typeof(BaseScriptableEvent), true)]
    public class SpriteScriptableEventEditor : UnityEditor.Editor
    {
        private BaseScriptableEvent m_scriptableEvent = null;
        private void Awake()
        {
         m_scriptableEvent = target as BaseScriptableEvent;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(m_scriptableEvent == null) return;
            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
            EditorGUILayout.LabelField("Receivers list:");
            foreach (var receiver in m_scriptableEvent.Receivers)
                EditorGUILayout.ObjectField("", receiver, typeof(BaseScriptableEventReceiver));
        }
    }
}