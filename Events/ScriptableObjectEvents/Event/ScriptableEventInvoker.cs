using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class ScriptableEventInvoker : BaseScriptableEventInvoker
    {
        [SerializeField] private ScriptableEvent m_event = null;

        public void Invoke() => m_event?.Invoke();
    }
}