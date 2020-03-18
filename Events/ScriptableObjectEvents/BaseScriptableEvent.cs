using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public abstract class BaseScriptableEvent : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField, TextArea(5, 10)] private string m_Description = string.Empty;
#endif
        public abstract void RegisterReceiver(BaseScriptableEventReceiver receiver);

        public abstract void UnregisterReceiver(BaseScriptableEventReceiver receiver);
    }

    public abstract class BaseScriptableEventReceiver<T> : BaseScriptableEventReceiver
    {
        protected interface ICallback
        {
            void Call(T value);
        }
        
       
        protected abstract ICallback Callback { get; }

        public void Invoke(T value) => Callback.Call(value);
    }
    
    public abstract class BaseScriptableEvent<T> : BaseScriptableEvent
    {

        [SerializeField] List<BaseScriptableEventReceiver<T>> m_receivers = new List<BaseScriptableEventReceiver<T>>();

        public override void RegisterReceiver(BaseScriptableEventReceiver receiver)
        {
            if (receiver is BaseScriptableEventReceiver<T> usableReceiver && !m_receivers.Contains(usableReceiver))
                m_receivers.Add(usableReceiver);
        }

        public override void UnregisterReceiver(BaseScriptableEventReceiver receiver)
        {
            if (receiver is BaseScriptableEventReceiver<T> usableReceiver && m_receivers.Contains(usableReceiver))
                m_receivers.Remove(usableReceiver);
        }

        public void Invoke(T value) => m_receivers.ForEach(receiver => receiver.Invoke(value));
    }
}