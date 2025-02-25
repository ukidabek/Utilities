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
		
		private Dictionary<Key, ICharacterResource> m_resourcesDictionary = new Dictionary<Key, ICharacterResource>();

		[Reference, SerializeReference] private IIsDeadStatusProvider m_isDeadStatusProvider = null;
		public bool IsDead => HandleStatusProvider(m_isDeadStatusProvider);
		public bool IsAlive => !IsDead;
		
		protected virtual void Awake()
		{
			m_isDeadStatusProvider = GetComponent<IIsDeadStatusProvider>();
			foreach (var resource in m_characterResources)
			{
				m_resourcesDictionary.Add(resource.Key, resource);
				resource.Initialize();
			}
		}
		
		protected virtual void OnEnable() => m_characterPool?.AddCharacter(this);

		protected virtual void OnDisable() => m_characterPool?.RemoveCharacter(this);
		
		public IEnumerable<T> GetResourcesOfType<T>() where T : CharacterResource => m_characterResources.OfType<T>();

		public void ResetResources()
		{
			foreach (var resource in m_characterResources) 
				resource.Reset();
		}
		
		public bool TryGetResource(Key key, out ICharacterResource resource) => m_resourcesDictionary.TryGetValue(key, out resource);
		
		public bool HaveResource(Key key) => m_resourcesDictionary.ContainsKey(key);
		
		public ICharacterResource this[Key key] => m_resourcesDictionary[key]; 
		
		public bool HandleStatusProvider<T>(T statusProvider) where T : IStatusProvider
		{
			if (statusProvider != null) return statusProvider.Status;
			Debug.LogWarning($"Status provider of type <b>{typeof(T).Name}</b> is null.");
			return false;
		}
	}
}