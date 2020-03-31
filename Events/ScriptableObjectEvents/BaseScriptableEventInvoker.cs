using System;
using UnityEngine;

namespace Utilities.Events
{
    public abstract class BaseScriptableEventInvoker : MonoBehaviour
    {
        [SerializeField] private bool m_invokeOnAwake = false;

        private void Awake()
        {
            if (m_invokeOnAwake)
                InvokeOnAwake();
        }

        protected virtual void InvokeOnAwake()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseScriptableEventInvoker<T> : BaseScriptableEventInvoker
    {
        public abstract void Invoke(T value);
    }
}