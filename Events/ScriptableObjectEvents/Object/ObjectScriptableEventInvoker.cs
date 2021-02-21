using UnityEngine;

namespace Utilities.Events
{
    public class ObjectScriptableEventInvoker : BaseScriptableEventInvoker
    {
        [SerializeField] private ObjectScriptableEvent m_event = null;

        public void Invoke(object value)
        {
            m_event.Invoke(value);
        }
    }
}