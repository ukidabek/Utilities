using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities.General.Animation
{
    public abstract class AnimatorControllerCollection<T> : ScriptableObject where T : AnimatorControllerDefinition
    {
        [FormerlySerializedAs("m_handledAnimatorController")] 
        [SerializeField] protected AnimatorController m_animatorController = null;

        public AnimatorController AnimatorController => m_animatorController;
        
        [FormerlySerializedAs("m_parameterDefinitions")] 
        [SerializeField] protected T[] m_definitions = null;
        public T[] Definitions => m_definitions;
    }
}