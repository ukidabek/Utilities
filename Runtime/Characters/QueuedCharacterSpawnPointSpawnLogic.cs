using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.General.Pool;

namespace Utilities.General.Characters
{
	public class QueuedCharacterSpawnPointSpawnLogic : CharacterSpawnPointSpawnLogic
	{
		[Serializable]
		public class CharacterSpawn
		{
			[SerializeField] protected CharacterPool m_pool;
			[SerializeField] private float m_awaitTime;

			private Transform m_transform;
			private WaitForSeconds m_waitForSeconds;
			public WaitForSeconds WaitForSeconds => m_waitForSeconds;

			public void Initialize(Transform transform)
			{
				m_transform = transform;
				m_waitForSeconds = new WaitForSeconds(m_awaitTime);
			}

			public void Spawn()
			{
				var character = m_pool.Get();
				var characterTransform = character.transform;
				characterTransform.position = m_transform.position;
				characterTransform.rotation = m_transform.rotation;
			}
		}

		[SerializeField] private List<CharacterSpawn> m_list = new List<CharacterSpawn>();

		private void Start()
		{
			foreach (var item in m_list)
				item.Initialize(transform);
		}

		public override IEnumerator SpawnCoroutine()
		{
			foreach (var item in m_list)
			{
				yield return item.WaitForSeconds;
				item.Spawn();
			}
		}
	}
}