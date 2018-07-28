using UnityEngine;
using UnityEngine.Events;

namespace BaseGameLogic.Utilities.UI
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