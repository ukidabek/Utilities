using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Utilities.General.Events.Core
{
    public abstract class Event<T> : ScriptableObject, IEvent where T : IEventListener
    {
        protected const string Invoke_Log_Format = "[<color=#{0}>Event</color>] {1} invoked!";

        [SerializeField] protected bool m_enableLogging = false;
        [SerializeField] protected Color m_color = new Color(0f, 0f, 0f, 1f);
        
        protected HashSet<T> m_listeners = new HashSet<T>(30);
        
        public virtual void Invoke()
        {
            LogEventInvoke();
            InvokeListeners(m_listeners, listener => listener.Invoke());
        }

        protected static void InvokeListeners(HashSet<T> listeners, Action<T> invoker)
        {
            foreach (var listener in listeners)
            {
                try
                {
                    invoker(listener);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }
        }

        public void Subscribe(T listener) => m_listeners.Add(listener);

        public void Unsubscribe(T listener) => m_listeners.Remove(listener);

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        protected void LogEventInvoke()
        {
            if (!m_enableLogging) return;
            var colorHexValue = ColorUtility.ToHtmlStringRGB(m_color);
            Debug.LogFormat(Invoke_Log_Format, colorHexValue, name);

        }
    }
}