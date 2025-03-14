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
            var name = string.Join("/", controller.name, info.layerName, info.stateName);
            return Initialize(name, info.nameHash) as AnimatorControllerStateDefinition;
        }

        public bool IsInState(Animator animator)
        {
            var currentState = animator.GetCurrentAnimatorStateInfo(m_layerIndex);
            return currentState.shortNameHash == m_hash;
        }

        public void Initialize(AnimatorControllerStateDefinition source)
        {
            m_layerName = source.m_layerName;
            m_layerIndex = source.m_layerIndex;
            base.Initialize(source.name, source.Hash);
        }
    }
}