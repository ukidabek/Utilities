using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public abstract class BaseScriptableEvent : ScriptableObject
    {
        [Header("Logging")] [SerializeField] protected bool m_enableLogs = true;
        [SerializeField] protected bool m_enableDetailedLogs = false;
        [SerializeField] protected Color m_logColor = Color.white;
        public Color LogColor => m_logColor;
#if UNITY_EDITOR
        [Space, SerializeField, TextArea(5, 10)]
        private string m_Description = string.Empty;
#endif
        public abstract List<BaseScriptableEventReceiver> Receivers { get; }

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
        protected List<BaseScriptableEventReceiver<T>> m_receivers = new List<BaseScriptableEventReceiver<T>>();

        public override List<BaseScriptableEventReceiver> Receivers =>
            new List<BaseScriptableEventReceiver>(m_receivers);

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

        public void Invoke(T value)
        {
            var colorHexValue = string.Empty;
            if (m_enableLogs)
            {
                colorHexValue = ColorUtility.ToHtmlStringRGB(m_logColor);
                Debug.Log(
                    $"Event <color=#{colorHexValue}><b>{name}</b></color> invoked! Value used is: <color=#{colorHexValue}>{ValueToString(value)}</color>.",
                    this);
            }

            m_receivers.ForEach(receiver =>
            {
                if (m_enableLogs && m_enableDetailedLogs)
                    Debug.Log(
                        $"Invoke event on receiver attached to <color=#{colorHexValue}><b>{receiver.name}</b></color>.");
                receiver.Invoke(value);
            });
        }

        private static string ValueToString(T value)
        {
            var valueToDisplay = string.Empty;
            try
            {
                valueToDisplay = value.ToString();
            }
            catch (NullReferenceException)
            {
                valueToDisplay = "null";
            }

            return valueToDisplay;
        }
    }
}