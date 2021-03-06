﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class BoolScriptableEventReceiver : BaseScriptableEventReceiver<bool, BoolScriptableEvent>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<bool>, ICallback
        {
            public void Call(bool value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}
