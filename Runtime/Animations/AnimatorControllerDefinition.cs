using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities.General.Animation
{
    public class AnimatorControllerDefinition : ScriptableObject
    {
        [FormerlySerializedAs("_hash")] 
        [SerializeField] protected int m_hash = 0;
        public int Hash => m_hash;

        protected AnimatorControllerDefinition Initialize(string name) 
            => Initialize(name,  Animator.StringToHash(name));

        protected AnimatorControllerDefinition Initialize(string name, int hash)
        {
            this.name = name;
            this.m_hash = hash;
            return this;
        }
    }
}