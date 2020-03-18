using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public class Vector2ScriptableEventInvoker : BaseScriptableEventInvoker<Vector2>
    {
        [SerializeField] private Vector2ScriptableEvent m_event = null;

        public override void Invoke(Vector2 value) => m_event?.Invoke(value);
    }
}