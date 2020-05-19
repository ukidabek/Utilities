using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.CallbackHandlers
{
    public class OnStartWithCoroutineHandler : MonoBehaviour
    {
        public UnityEvent OnStartBeforeCoroutine = new UnityEvent();
        public UnityEvent OnStartAfterCoroutine = new UnityEvent();

        protected virtual IEnumerator OnStartCoroutine()
        {
            yield return null;
        }
        
        private IEnumerator Start()
        {
            OnStartBeforeCoroutine.Invoke();
            yield return OnStartCoroutine();
            OnStartAfterCoroutine.Invoke();
        }
    }
}