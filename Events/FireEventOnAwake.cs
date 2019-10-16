using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class FireEventOnAwake : MonoBehaviour
    {
        public UnityEvent OnAwakeEvent = new UnityEvent();

        private void Awake()
        {
            OnAwakeEvent.Invoke();
        }
    }
}