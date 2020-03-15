using UnityEngine;
using Utilities.Events;

namespace Utilities.Events
{
	public abstract class OnTriggerCallback : MonoBehaviour
	{
		public OnCollider OnColliderCallback = new OnCollider();
		public OnGameObject OnGameObjectCallback = new OnGameObject();
	}
}