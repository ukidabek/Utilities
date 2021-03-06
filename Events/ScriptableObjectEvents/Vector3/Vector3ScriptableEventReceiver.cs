﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class Vector3ScriptableEventReceiver : BaseScriptableEventReceiver<Vector3, Vector3ScriptableEvent>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<Vector3>, ICallback
        {
            public void Call(Vector3 value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}