using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableEvent", menuName = "ScriptableEvent/New ScriptableEvent")]
public class ScriptableEvent : BaseScriptableEvent
{
    private List<ScriptableEventReceiver> m_receivers = new List<ScriptableEventReceiver>();

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