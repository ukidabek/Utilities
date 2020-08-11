using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Utilities.Events
{
    public abstract class BaseScriptableEventReceiver : MonoBehaviour
    {
        //[SerializeField] private BaseScriptableEvent m_event = null;
        public abstract BaseScriptableEvent Event { get; }

        private string EventName => (Event ? Event.name : "null");

        private void OnEnable() => Register();

        private void OnDisable() => Unregister();

        private void OnDestroy() => Unregister();

        private void Register()
        {
            Event?.RegisterReceiver(this);
            Debug.Log(
                $"Receiver attached to <b>{name}</b> game object registered to event <b>{EventName}</b>");
        }


        private void Unregister()
        {
            Event?.UnregisterReceiver(this);
            Debug.Log(
                $"Receiver attached to <b>{name}</b> game object unregistered from event <b>{EventName}</b>");
        }
    }


    public abstract class BaseScriptableEventReceiver<T, T1> : BaseScriptableEventReceiver where T1 : BaseScriptableEvent<T>
    {
        [SerializeField] private T1 m_event = default;

        public override BaseScriptableEvent Event => m_event;

        protected interface ICallback
        {
            void Call(T value);
        }
        
        [General.ReadOnly, SerializeField] private T m_value = default(T);

        protected abstract ICallback Callback { get; }

        public void Invoke(T value) => Callback.Call(m_value = value);
    }
}