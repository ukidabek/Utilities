using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities.General.SpawnPoints
{
	public class SpawnPointsManager : MonoBehaviour, IEnumerable<SpawnPoint>
	{
		internal static SpawnPointsManager Instance { get; private set; }

		private ObservableCollection<SpawnPoint> m_spawnPoints = new ObservableCollection<SpawnPoint>();
		public ReadOnlyObservableCollection<SpawnPoint> SpawnPoints { get; private set; }

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				SpawnPoints = new ReadOnlyObservableCollection<SpawnPoint>(m_spawnPoints);
			}
			else
				Destroy(gameObject);
		}

		public void RegisterSawnPoint(SpawnPoint point)
		{
			if (m_spawnPoints.Contains(point)) return;
			m_spawnPoints.Add(point);
		}

		public void UnregisterSpawnPint(SpawnPoint point)
		{
			if (!m_spawnPoints.Contains(point)) return;
			m_spawnPoints.Remove(point);
		}

		private void Reset()
		{
			name = GetType().Name;
		}

		public IEnumerator<SpawnPoint> GetEnumerator() => m_spawnPoints.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}