using UnityEngine;

namespace Utilities.Events
{

    public class IntScriptableEventInvoker  : BaseScriptableEventInvoker<int>
    {
        [SerializeField] private IntScriptableEvent m_event = null;

        public override void Invoke(int value) => m_event?.Invoke(value);
    }
}