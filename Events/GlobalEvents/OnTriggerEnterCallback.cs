using UnityEngine;

namespace Utilities.Events
{
	public class OnTriggerEnterCallback : OnTriggerCallback
	{
		private void OnTriggerEnter(Collider other)
		{
#if UNITY_EDITOR
			Debug.LogFormat("GameObject <color=#008000ff>{0}</color> enter trigger.", other.gameObject.name);
#endif
			OnColliderCallback.Invoke(other);
			OnGameObjectCallback.Invoke(other.gameObject);
		}
	}
}