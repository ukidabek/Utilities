using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public class StringScriptableEventInvoker : BaseScriptableEventInvoker<string>
    {
        [SerializeField] private StringScriptableEvent m_event = null;

        public override void Invoke(string value) => m_event?.Invoke(value);
    }
}