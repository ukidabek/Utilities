using UnityEngine.Events;
using Utilities.General.Events.Core;

namespace Utilities.General.Events
{
    public class VoidEventListener : EventListener
    {
        public UnityEvent Callback = new UnityEvent();
        public override void Invoke() => Callback.Invoke();
    }
}