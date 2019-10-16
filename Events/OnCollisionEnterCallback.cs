using UnityEngine;

namespace Utilities.Events
{
	public class OnCollisionEnterCallback : MonoBehaviour
	{
		public CollisionCallback Callback = new CollisionCallback();

		private void OnCollisionEnter(Collision collision)
		{
			Debug.Log(collision.gameObject.name, collision.gameObject);
			Callback.Invoke(collision);
		}
	}
}