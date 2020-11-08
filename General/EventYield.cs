using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General
{
    public class EventYield : MonoBehaviour
    {
        public interface IYieldInterface
        {
            IEnumerator YieldOperation();
        }
      
        public UnityEvent Event = new UnityEvent();

        private IEnumerator YieldCoroutine()
        {
            IYieldInterface operation = GetComponent<IYieldInterface>();
            yield return operation?.YieldOperation();
            Event.Invoke();
        }
      
        public void Invoke()
        {
            StartCoroutine(YieldCoroutine());
        }
    }
}