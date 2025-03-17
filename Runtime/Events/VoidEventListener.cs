using UnityEngine.Events;

namespace Utilities.General.Events
{
    public class VoidEventListener : EventListener
    {
        public UnityEvent Callback = new UnityEvent();
        public override void Invoke() => Callback.Invoke();
    }
}