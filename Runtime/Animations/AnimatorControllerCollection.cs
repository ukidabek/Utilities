using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities.General.Animation
{
    public abstract class AnimatorControllerCollection<T> : ScriptableObject where T : AnimatorControllerDefinition
    {
        [FormerlySerializedAs("m_handledAnimatorController")] 
        [SerializeField] protected RuntimeAnimatorController m_animatorController = null;

        public RuntimeAnimatorController AnimatorController => m_animatorController;
        
        [FormerlySerializedAs("m_parameterDefinitions")] 
        [SerializeField] protected T[] m_definitions = null;
        public T[] Definitions => m_definitions;
    }
}