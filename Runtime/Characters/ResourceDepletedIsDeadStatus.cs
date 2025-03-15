using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Utilities.General.Characters
{
    [Serializable, Preserve]
    public class ResourceDepletedIsDeadStatus : IIsDeadStatus
    {
        [SerializeField] private Key m_resourceKey;

        protected IResource m_resource = null;
        
        public bool Status => m_resource?.Value == 0f;

        public void Initialize(Actor actor)
        {
            if (m_resourceKey == null)
            {
                Debug.LogError($"{nameof(Key)} is is null!");
                return;
            }
            
            if (!actor.TryGetResource(m_resourceKey, out var resource))
            {
                Debug.LogError($"There is no resource with key {m_resourceKey.name}!");
                return;
            }
            
            m_resource = resource;
        }
    }
}