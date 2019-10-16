using UnityEngine;

namespace Utilities.Events
{
	public class OnTriggerExitCallback : OnTriggerCallback
	{
		private void OnTriggerExit(Collider other)
		{
#if UNITY_EDITOR
			Debug.LogFormat("GameObject <color=#ff0000ff>{0}</color> exit trigger.", other.gameObject.name);
#endif
			OnColliderCallback.Invoke(other);
			OnGameObjectCallback.Invoke(other.gameObject);
		}
	}
}