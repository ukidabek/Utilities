using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public class FloatScriptableEventInvoker : BaseScriptableEventInvoker<float>
    {
        [SerializeField] private FloatScriptableEvent m_event = null;

        public override void Invoke(float value) => m_event?.Invoke(value);
    }
}