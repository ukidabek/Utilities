using UnityEngine;
using UnityEngine.Events;

namespace BaseGameLogic.Utilities.UI
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