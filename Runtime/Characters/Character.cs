using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.General.Characters
{
	public abstract class Character : MonoBehaviour
	{
		[SerializeField] private CharacterCollection m_characterPool = null;
		public CharacterCollection CharacterPool => m_characterPool;

		[ReferenceList, SerializeReference] private ICharacterResource[] m_characterResources = null;
		public IEnumerable<ICharacterResource> Resources => m_characterResources;

		private IIsDeadStatusProvider m_isDeadStatusProvider = null;
		public bool IsDead => ChandleStatusProvider(m_isDeadStatusProvider);

		protected virtual void Awake()
		{
			m_isDeadStatusProvider = GetComponent<IIsDeadStatusProvider>();
			foreach (var resource in m_characterResources)
			{
				resource.Reset();
			}
		}

		protected virtual void OnEnable() => m_characterPool?.AddCharacter(this);

		protected virtual void OnDisable() => m_characterPool?.RemoveCharacter(this);
		
		public IEnumerable<T> GetResourcesOfType<T>() where T : CharacterResource => m_characterResources.OfType<T>();

		protected bool ChandleStatusProvider<T>(T statusProvider) where T : IStatusProvider
		{
			if (statusProvider == null)
			{

				Debug.LogWarning($"Status provider of type <b>{typeof(T).Name}</b> is null.");
				return false;
			}

			return statusProvider.Status;
		}
	}
}