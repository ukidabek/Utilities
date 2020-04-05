using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    [CreateAssetMenu(fileName = "ScriptableEvent", menuName = "ScriptableEvent/New ScriptableEvent")]
    public class ScriptableEvent : BaseScriptableEvent
    {
        private List<ScriptableEventReceiver> m_receivers = new List<ScriptableEventReceiver>();

        public void Invoke()
        {
            if (m_enableLogs)
            {
                var colorHexValue = ColorUtility.ToHtmlStringRGB(m_logColor);  
                Debug.Log($"Event <color=#{colorHexValue}><b>{name}</b></color> invoked!.", this);
            }
            m_receivers.ForEach(receiver => receiver.Invoke());
        }

        public override void RegisterReceiver(BaseScriptableEventReceiver receiver)
        {
            if (receiver != null && receiver is ScriptableEventReceiver handlerReceiver &&
                !m_receivers.Contains(handlerReceiver))
                m_receivers.Add(handlerReceiver);
        }

        public override void UnregisterReceiver(BaseScriptableEventReceiver receiver)
        {
            if (receiver != null && receiver is ScriptableEventReceiver handlerReceiver &&
                m_receivers.Contains(handlerReceiver))
                m_receivers.Remove(handlerReceiver);
        }
    }
}