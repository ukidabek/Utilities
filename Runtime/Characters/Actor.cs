using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities.General.Characters
{
    public abstract class Actor : MonoBehaviour
    {
        [FormerlySerializedAs("m_characterPool")] 
        [SerializeField] protected ActorsCollection m_actorsPool = null;
        public ActorsCollection ActorsPool => m_actorsPool;
        
        [FormerlySerializedAs("m_characterResources")] 
        [ReferenceList, SerializeReference] protected IResource[] m_resources = null;
        protected Dictionary<Key, IResource> m_resourcesDictionary = new Dictionary<Key, IResource>();
        public IEnumerable<IResource> Resources => m_resources;

        [FormerlySerializedAs("m_statusProviders")] [ReferenceList, SerializeReference] protected IStatus[] m_statuses = null;
        protected virtual IEnumerable<IStatus> Statuses => m_statuses;

        public IEnumerable<T> GetResourcesOfType<T>() where T : Resource => m_resources.OfType<T>();

        public void ResetResources()
        {
            foreach (var resource in m_resources) 
                resource.Reset();
        }

        public bool TryGetResource(Key key, out IResource resource) => m_resourcesDictionary.TryGetValue(key, out resource);
        
        public bool HaveResource(Key key) => m_resourcesDictionary.ContainsKey(key);

        protected bool HandleStatusProvider<T>(T statusProvider) where T : IStatus
        {
            if (statusProvider is not null) return statusProvider.Status;
            Debug.LogWarning($"Status provider of type <b>{typeof(T).Name}</b> is null.");
            return false;
        }

        public bool TryGetStatus<T>(out T status) where T : IStatus
        {
             status = m_statuses.OfType<T>().FirstOrDefault();
             return status is not null;
        }

        protected virtual void OnEnable() => m_actorsPool?.RegisterActor(this);

        protected virtual void Awake()
        {
            foreach (var resource in m_resources)
            {
                m_resourcesDictionary.Add(resource.Key, resource);
                resource.Initialize();
            }
			
            foreach (var statusProvider in Statuses) 
                statusProvider.Initialize(this);
        }
        
        protected virtual void OnDisable() => m_actorsPool?.RemoveActor(this);
    }
}