using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.General.Events
{
    public abstract class Event<T> : ScriptableObject
    {
        protected HashSet<EventListener<T>> m_listeners = new HashSet<EventListener<T>>(30);

        public void Subscribe(EventListener<T> listener) => m_listeners.Add(listener);

        public void Unsubscribe(EventListener<T> listener) => m_listeners.Remove(listener);
        
        public void Invoke(T eventArgument)
        {
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
