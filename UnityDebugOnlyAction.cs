using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
	public class UnityDebugOnlyAction : MonoBehaviour
	{
		public UnityEvent Event = new UnityEvent();

		public void Invoke()
		{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
			Event.Invoke();
#endif
		}
	}
}