using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class BoundsScriptableEventReceiver :  BaseScriptableEventReceiver<Bounds>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<Bounds>, ICallback
        {
            public void Call(Bounds value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}