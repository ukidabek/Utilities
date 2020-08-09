using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public class CameraScriptableEventInvoker : BaseScriptableEventInvoker<Camera>
    {
        [SerializeField] private CameraScriptableEvent m_event = null;

        public override void Invoke(Camera value) => m_event?.Invoke(value);
    }
}