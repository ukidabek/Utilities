using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public class BoundsScriptableEventInvoker : BaseScriptableEventInvoker<Bounds>
    {
        [SerializeField] private BoundsScriptableEvent m_event = null;

        public override void Invoke(Bounds value) => m_event?.Invoke(value);
    }
}