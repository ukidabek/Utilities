using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Component = UnityEngine.Component;
using Object = UnityEngine.Object;

namespace Utilities.General.Events
{
    [CustomEditor(typeof(Event<>), true)]
    public class EventEditor : Editor
    {
        private IEnumerable m_enumerable;
        private Type m_objetType = typeof(Object);
        private Vector2 m_scrollPosition;
        
        private void OnEnable()
        {
            var type = target.GetType();
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            var memberInfos = type.GetField("m_listeners", bindingFlags); 
            var hasSet = memberInfos.GetValue(target);
            if (hasSet is IEnumerable enumerable)
                m_enumerable = enumerable;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            
            GUILayout.Label("Subscribed listeners:");
            m_scrollPosition =  EditorGUILayout.BeginScrollView(m_scrollPosition, GUILayout.MaxHeight(400));
            foreach (var listener in m_enumerable)
            {
                if(listener is not MonoBehaviour current) continue;
                EditorGUILayout.ObjectField(current, m_objetType);
                GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            }
            EditorGUILayout.EndScrollView();
        }
    }
}