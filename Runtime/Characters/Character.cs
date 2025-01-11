using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.General.Characters
{
	public abstract class Character : MonoBehaviour
	{
		[SerializeField] private CharacterCollection m_characterPool = null;
		public CharacterCollection CharacterPool => m_characterPool;

		[SerializeField] private Object[] m_characterResources = null;
		public IEnumerable<ICharacterResource> Resources { get; private set; }

		private IIsDeadStatusProvider m_isDeadStatisProvider = null;
		public bool IsDead => ChandleStatusProvider(m_isDeadStatisProvider);

		protected virtual void Awake()
		{
			Resources = m_characterResources
				.OfType<ICharacterResource>()
				.ToArray();
			m_isDeadStatisProvider = GetComponent<IIsDeadStatusProvider>();
		}

		protected virtual void OnEnable() => m_characterPool?.AddCharacter(this);

		protected virtual void OnDisable() => m_characterPool?.RemoveCharacter(this);

		protected virtual void OnDestroy() => m_characterPool?.RemoveCharacter(this);

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

		[ContextMenu("Collect all resources")]
		private void CollectAllResources()
		{
			var rootGameObject = transform.root.gameObject;
			var resources = rootGameObject.GetComponentsInChildren<ICharacterResource>();

			m_characterResources = m_characterResources
				.OfType<ICharacterResource>()
				.Concat(resources)
				.Distinct()
				.OfType<Object>()
				.ToArray();
		}
	}
}