using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Utilities.General.Characters
{
    [Serializable, Preserve]
    public class ResourceDepletedIsDeadStatusProvider : IIsDeadStatusProvider
    {
        [SerializeField] private Character m_character;	
        [SerializeField] private Key m_resourceKey;

        public bool Status
        {
            get
            {
                if (!m_character.TryGetResource(m_resourceKey, out var resource)) return false;
                return resource.Value == 0f;
            }
        }
    }
}