using UnityEditor.Animations;
using UnityEngine;

namespace Utilities.General.Animation
{
    public class AnimatorControllerStateDefinition : AnimatorControllerDefinition
    {
        [SerializeField] private string m_layerName;
        public string LayerName => m_layerName;
        
        [SerializeField] private int m_layerIndex;
        public int LayerIndex => m_layerIndex;
        
        public AnimatorControllerStateDefinition Initialize(AnimatorController controller, 
            (string layerName, int layerIndex, string stateName, int nameHash) info)
        {
            m_layerName = info.layerName;
            m_layerIndex = info.layerIndex;
            return base.Initialize(string.Join("/", controller.name, info.layerName, info.stateName), info.nameHash) 
                as AnimatorControllerStateDefinition;
        }

        public bool IsInState(Animator animator)
        {
            var currentState = animator.GetCurrentAnimatorStateInfo(m_layerIndex);
            return currentState.shortNameHash == m_hash;
        }
    }
}