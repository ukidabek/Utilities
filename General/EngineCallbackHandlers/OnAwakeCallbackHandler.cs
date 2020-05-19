using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.CallbackHandlers
{
    public class OnAwakeCallbackHandler : MonoBehaviour
    {
        public UnityEvent OnAwake = new UnityEvent();
        private void Awake() => OnAwake.Invoke();
    }
}