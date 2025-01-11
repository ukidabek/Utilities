using UnityEngine;

namespace Utilities.General.SpawnPoints
{
	public class SpawnPoint : MonoBehaviour
	{
		private void OnEnable() => SpawnPointsManager.Instance?.RegisterSawnPoint(this);

		private void OnDisable() => SpawnPointsManager.Instance?.UnregisterSpawnPint(this);

		private void OnDestroy() => SpawnPointsManager.Instance?.UnregisterSpawnPint(this);

		private void Reset() => name = GetType().Name;

		public virtual void MoveTo(GameObject gameObject, bool setPosition = true, bool setRotation = true)
		{
			var transform = gameObject.transform;
			if (setPosition)
				transform.position = this.transform.position;
			if (setRotation)
				transform.rotation = this.transform.rotation;
		}
	}
}