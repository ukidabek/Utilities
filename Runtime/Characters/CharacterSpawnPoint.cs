using UnityEngine;

namespace Utilities.General.Characters
{
	public class CharacterSpawnPoint : MonoBehaviour
	{
		[SerializeField] private CharacterSpawnPointSpawnLogic m_spawnLogic = null;
		protected Coroutine m_spawnCoroutine = null;

		protected uint m_chacactersInRange = 0;

		private void OnTriggerEnter(Collider other)
		{
			if(!other.TryGetComponent<Character>(out var character)) return;
			m_chacactersInRange++;
			if (m_spawnCoroutine != null) return;
			m_spawnCoroutine = StartCoroutine(m_spawnLogic.SpawnCoroutine());
		}

		private void OnTriggerExit(Collider other)
		{
			if (!other.TryGetComponent<Character>(out var character)) return;
			m_chacactersInRange--;
			if (m_chacactersInRange > 0) return;
			StopCoroutine(m_spawnCoroutine);
			m_spawnCoroutine = null;
		}
	}
}