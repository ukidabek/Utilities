using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.General.Events
{
    public abstract class Event<T> : ScriptableObject
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        private const string Invoke_Log_Format = "[<color=#{0}>Event</color>] {1} invoked! Value: {2}";

        [SerializeField] private bool m_enableLogging = false;
        [SerializeField] private Color m_color = new Color(0f, 0f, 0f, 1f);
#endif
        
        private string m_colorHexValue = string.Empty;

        private HashSet<EventListener<T>> m_listeners = new HashSet<EventListener<T>>(30);

        private void OnEnable()
        {
            m_colorHexValue = ColorUtility.ToHtmlStringRGB(m_color);
        }

        public void Subscribe(EventListener<T> listener) => m_listeners.Add(listener);

        public void Unsubscribe(EventListener<T> listener) => m_listeners.Remove(listener);

        public void Invoke(T eventArgument)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (m_enableLogging)
            {
                var valuesString = eventArgument is null ? "null" : eventArgument.ToString();
                Debug.LogFormat(Invoke_Log_Format, m_colorHexValue, name, valuesString);
            }
#endif
            foreach (var listener in m_listeners)
            {
                try
                {
                    listener.Invoke(eventArgument);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }
        }
    }
}