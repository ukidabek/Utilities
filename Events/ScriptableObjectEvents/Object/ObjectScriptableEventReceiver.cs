using System;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class ObjectScriptableEventReceiver : BaseScriptableEventReceiver<object, ObjectScriptableEvent>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<object>, ICallback
        {
            public void Call(object value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}