using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class SpriteScriptableEventReceiver : BaseScriptableEventReceiver<Sprite, SpriteScriptableEvent>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<Sprite>, ICallback
        {
            public void Call(Sprite value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}