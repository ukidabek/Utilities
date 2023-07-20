using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
	public class DontDestroyOnLoadMarker : MonoBehaviour
	{
		private void OnEnable()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}