using System;
using UnityEngine;

namespace Utilities.Events
{
    public abstract class BaseScriptableEventInvoker : MonoBehaviour
    {
        [SerializeField] private bool m_invokeOnStart = false;

        protected virtual void Start()
        {
            if (m_invokeOnStart)
                InvokeOnAwake();
        }

        protected virtual void InvokeOnAwake()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseScriptableEventInvoker<T> : BaseScriptableEventInvoker
    {
        [SerializeField] private T m_defaultValue = default(T);
        public T DefaultValue
        {
            get => m_defaultValue;
            set => m_defaultValue = value;
        }

        protected override void InvokeOnAwake() => Invoke();

        public virtual void Invoke() => Invoke(m_defaultValue);

        public virtual void Invoke(object @object)
        {
            if (@object is T value)
                Invoke(value);
            else
                throw new ArgumentException();
        }
        
        public abstract void Invoke(T value);
    }
}