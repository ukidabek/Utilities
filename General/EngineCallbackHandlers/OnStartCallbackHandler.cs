using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.CallbackHandlers
{
    public class OnStartCallbackHandler : MonoBehaviour
    {
        public UnityEvent OnStart = new UnityEvent();
        private void Start() => OnStart.Invoke();
    }
}