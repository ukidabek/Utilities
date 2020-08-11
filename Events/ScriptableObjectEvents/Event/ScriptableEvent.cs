using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Events
{
    [CreateAssetMenu(fileName = "ScriptableEvent", menuName = "ScriptableEvent/New ScriptableEvent")]
    public class ScriptableEvent : BaseScriptableEvent
    {
        private List<ScriptableEventReceiver> m_receivers = new List<ScriptableEventReceiver>();

        public override List<BaseScriptableEventReceiver> Receivers =>
            new List<BaseScriptableEventReceiver>(m_receivers);

        public void Invoke()
        {
            if(!Enabled) return;
            
            var colorHexValue = string.Empty;
            if (m_enableLogs)
            {
                colorHexValue = ColorUtility.ToHtmlStringRGB(m_logColor);
                Debug.Log($"Event <color=#{colorHexValue}><b>{name}</b></color> invoked!.", this);
            }

            m_receivers.ForEach(receiver =>
            {
                if (m_enableLogs && m_enableDetailedLogs)
                    Debug.Log(
                        $"Invoke event on receiver attached to <color=#{colorHexValue}><b>{receiver.name}</b></color>.");
                receiver.Invoke();
            });
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