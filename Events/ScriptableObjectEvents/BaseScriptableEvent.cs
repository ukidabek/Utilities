using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utilities.Events
{
    public abstract class BaseScriptableEvent : ScriptableObject
    {
        [SerializeField] protected bool m_enabled = true;
        public bool Enabled
        {
            get => m_enabled;
            set => m_enabled = value;
        }

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

    public abstract class BaseScriptableEvent<T> : BaseScriptableEvent
    {
        public class ReceiverHolder
        {
            public readonly BaseScriptableEventReceiver Receiver = null;
            public readonly MethodInfo MethodInfo = null;
            public string Name => Receiver.name;
            
            public ReceiverHolder(BaseScriptableEventReceiver receiver, MethodInfo methodInfo)
            {
                Receiver = receiver;
                MethodInfo = methodInfo;
            }

            public void Invoke(object data) => MethodInfo.Invoke(Receiver, new[] {data});
        }
        
        protected List<ReceiverHolder> m_receiverHolders = new List<ReceiverHolder>();
        
        protected List<BaseScriptableEventReceiver<T, BaseScriptableEvent<T>>> m_receivers = new List<BaseScriptableEventReceiver<T, BaseScriptableEvent<T>>>();

        public override List<BaseScriptableEventReceiver> Receivers =>
            new List<BaseScriptableEventReceiver>(m_receivers);

        public override void RegisterReceiver(BaseScriptableEventReceiver receiver)
        {
            var methodInfo = receiver
                .GetType()
                .GetMethods()
                .FirstOrDefault(info =>
                {
                    var parameters = info.GetParameters()
                        .Any(parameterInfo => parameterInfo.ParameterType == typeof(T));
                    return info.Name == "Invoke" && parameters;
                });

            if(methodInfo != null && m_receiverHolders.All(holder => holder.Receiver != receiver))
                m_receiverHolders.Add(new ReceiverHolder(receiver, methodInfo));
        }

        public override void UnregisterReceiver(BaseScriptableEventReceiver receiver)
        {
            var receiverHolder = m_receiverHolders.FirstOrDefault(holder => holder.Receiver == receiver);
            m_receiverHolders.Remove(receiverHolder);
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
                if(!Enabled)
                    Debug.Log($"Event <color=#{colorHexValue}><b>{name}</b></color> is disabled!</color>.", this);
            }
            
            if(!Enabled) return;
            
            m_receiverHolders.ForEach(receiverHolder =>
            {
                if (m_enableLogs && m_enableDetailedLogs)
                    Debug.Log(
                        $"Invoke event on receiver attached to <color=#{colorHexValue}><b>{receiverHolder.Name}</b></color>.");
                receiverHolder.Invoke(value);
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