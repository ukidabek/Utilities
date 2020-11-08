using System.Dynamic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General
{
   public class OnObjectDisabledEvent : MonoBehaviour
   {
      public UnityEvent OnObjectDisabled = new UnityEvent();

      private void OnDisable() => OnObjectDisabled.Invoke();
   }
}