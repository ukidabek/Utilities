using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class ScriptableEventReceiver : BaseScriptableEventReceiver
    {
        [SerializeField] private ScriptableEvent m_event = default;
        public override BaseScriptableEvent Event => m_event;
        
        public UnityEvent Callback = new UnityEvent();

        public void Invoke() => Callback.Invoke();
        
    }
}