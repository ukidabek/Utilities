using UnityEditor.Animations;
using UnityEngine;

namespace Utilities.General.Animation
{
    public class AnimatorControllerHandler : ScriptableObject
    {
        [SerializeField] private AnimatorController m_handledAnimatorController = null;
        public AnimatorController MHandledAnimatorController => m_handledAnimatorController;
        
        [SerializeField] private AnimatorParameterDefinition[] m_parameterDefinitions = null;
        public AnimatorParameterDefinition[] MParameterDefinitions => m_parameterDefinitions;
    }
}