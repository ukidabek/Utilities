using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
	public class Toggler : MonoBehaviour
	{
		[Serializable]
		public class OnStatusChangeEvent : UnityEvent<bool>
		{
		}
		
		[SerializeField] private bool status = false;
		public UnityEvent OnTrue = new UnityEvent();
		public OnStatusChangeEvent OnStatusChange = new OnStatusChangeEvent();
		public UnityEvent OnFalse = new UnityEvent();

		public bool Status
		{
			get => status;
			set
			{
				status = value;
				if (status)
					OnTrue.Invoke();
				else
					OnFalse.Invoke();
			}
		}

		public void Toggle()
		{
			Status = !Status;
		}
	}
}