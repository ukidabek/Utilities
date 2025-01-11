#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Utilities.General.Animation
{
    public class AnimatorDefinitionBase : ScriptableObject
    {
#if UNITY_EDITOR
        private const string Animator_Warning = "Provider animator is different then used to initialize this definition.";

        [SerializeField, HideInInspector] protected AnimatorController _animator = null;
#endif
        [Conditional("UNITY_EDITOR")]
        protected void ValidateAnimator(Animator animator)
        {
#if UNITY_EDITOR
            if (animator.runtimeAnimatorController != _animator)
                Debug.LogWarning(Animator_Warning, this);
#endif
        }
    }
}