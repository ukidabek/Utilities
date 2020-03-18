using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public class Vector3ScriptableEventInvoker : BaseScriptableEventInvoker<Vector3>
    {
        [SerializeField] private Vector3ScriptableEvent m_event = null;

        public override void Invoke(Vector3 value) => m_event?.Invoke(value);
    }
}