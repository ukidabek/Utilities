using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class IntScriptableEventReceiver : BaseScriptableEventReceiver<int>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<int>, ICallback
        {
            public void Call(int value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}