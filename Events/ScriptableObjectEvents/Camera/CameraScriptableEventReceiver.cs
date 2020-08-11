using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class CameraScriptableEventReceiver : BaseScriptableEventReceiver<Camera, CameraScriptableEvent>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<Camera>, ICallback
        {
            public void Call(Camera value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}