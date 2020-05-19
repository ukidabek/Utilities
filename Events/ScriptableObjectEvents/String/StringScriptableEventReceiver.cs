using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class StringScriptableEventReceiver : BaseScriptableEventReceiver<string>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<string>, ICallback
        {
            public void Call(string value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}
