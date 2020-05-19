using System;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class FloatScriptableEventReceiver : BaseScriptableEventReceiver<float>
    {
        [Serializable]
        public class ReceiverEventHandlerCallback : UnityEvent<float>, ICallback
        {
            public void Call(float value) => Invoke(value);
        }

        public new ReceiverEventHandlerCallback Event = new ReceiverEventHandlerCallback();
        protected override ICallback Callback => Event;
    }
}