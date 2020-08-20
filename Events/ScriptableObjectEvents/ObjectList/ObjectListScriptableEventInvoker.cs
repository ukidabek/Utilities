using UnityEngine;

namespace Utilities.Events
{
    public class ObjectListScriptableEventInvoker : BaseScriptableEventInvoker<object[]>
    {
        
        [SerializeField] private ObjectListScriptableEvent m_event = null;

        public override void Invoke(object[] value) => m_event?.Invoke(value);
    }
}