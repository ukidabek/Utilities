using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class FireEventOnStart : MonoBehaviour
    {
        public UnityEvent OnStartEvent = new UnityEvent();

        private void Start()
        {
            OnStartEvent.Invoke();
        }
    }
}