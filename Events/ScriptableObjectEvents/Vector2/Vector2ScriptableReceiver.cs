using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class Vector2ScriptableReceiver : BaseScriptableEventReceiver<Vector2>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<Vector2>, ICallback
        {
            public void Call(Vector2 value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}