using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General
{
    public class EventBoolSwitch : MonoBehaviour
    {
        [SerializeField] private bool m_status = false;
        public bool Status { get => m_status; set => m_status = value; }

        public UnityEvent InvokeOnTrue = new UnityEvent();
        public UnityEvent InvokeOnFalse = new UnityEvent();

        public void Invoke()
        {
            if(Status)
                InvokeOnTrue.Invoke();
            else
                InvokeOnFalse.Invoke();
        }
    }
}