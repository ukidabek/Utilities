using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities.General.Animation
{
    public class AnimatorControllerDefinition : ScriptableObject
    {
        [FormerlySerializedAs("_hash")] 
        [SerializeField] protected int m_hash = 0;
        public int Hash => m_hash;

        protected void Initialize(string name)
        {
            this.name = name;
            m_hash = Animator.StringToHash(name);
        }
    }
}