using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        protected override void InvokeOnAwake() => Invoke();

        public virtual void Invoke() => Invoke(m_defaultValue);

        public abstract void Invoke(T value);
    }
}