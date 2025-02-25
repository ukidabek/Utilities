using System.Linq;
using UnityEngine;

namespace Utilities.General.Characters
{
    [RequireComponent(typeof(Character))]
    public class CharacterResourceUpdater : MonoBehaviour
    {
        [SerializeField] private Character m_character = null;

        private IUpdatableCharacterResource[] m_updatableResources = null;
		
        private void Awake()
        {
            m_updatableResources = m_character.Resources
                .OfType<IUpdatableCharacterResource>()
                .ToArray();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            foreach (var updatableResource in m_updatableResources) 
                updatableResource.Update(deltaTime);
        }

        private void Reset() => m_character = GetComponent<Character>();
    }
}