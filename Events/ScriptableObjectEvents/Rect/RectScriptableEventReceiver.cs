using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class RectScriptableEventReceiver : BaseScriptableEventReceiver<Rect>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<Rect>, ICallback
        {
            public void Call(Rect value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;    }
}
