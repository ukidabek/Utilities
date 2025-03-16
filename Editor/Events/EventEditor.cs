using System;
using System.Collections;
using System.Linq;
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
        private Type m_objetType = typeof(Object);
        private Type m_genericType = null;
        private FieldInfo m_colorFieldInfo = null;
        private MethodInfo m_invokeMethodInfo = null;
        
        private IEnumerable m_enumerable;
        
        private Vector2 m_scrollPosition;
        
        private void OnEnable()
        {
            var type = target.GetType();
            var baseType = type.BaseType;
            m_genericType = baseType.GetGenericArguments().First();

            var bindingFlags = EventEditorUtilities.Binding_Flags;
            m_invokeMethodInfo = baseType.GetMethod(EventEditorUtilities.Invoke_Method_Name, bindingFlags);
            
            var listenersFieldInfo = baseType.GetField(EventEditorUtilities.Listeners_Field_Name, bindingFlags); 
            var listeners = listenersFieldInfo.GetValue(target);
            if (listeners is IEnumerable enumerable)
                m_enumerable = enumerable;
            
            m_colorFieldInfo = baseType.GetField(EventEditorUtilities.Color_Field_Name, bindingFlags);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var oldColor = GUI.color;
            GUI.color = (Color)m_colorFieldInfo.GetValue(target);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.color = oldColor;
            if (GUILayout.Button(EventEditorUtilities.Invoke_Method_Name) && EditorApplication.isPlaying)
            {
                var defaultValue = Activator.CreateInstance(m_genericType);
                m_invokeMethodInfo.Invoke(target, new[] { defaultValue });
            }
                
            GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            GUILayout.Label("Subscribed listeners:");
            m_scrollPosition =  EditorGUILayout.BeginScrollView(m_scrollPosition,GUILayout.MaxHeight(400));
            foreach (var listener in m_enumerable)
            {
                if(listener is not MonoBehaviour current) continue;
                EditorGUILayout.ObjectField(current, m_objetType);
                GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
    }
}