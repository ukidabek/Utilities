using UnityEngine;

namespace Utilities.General.Animation
{
	[CreateAssetMenu(fileName = "AnimationParameter", menuName = "Utilities/Animation/AnimationParameter")]
	public class AnimatorParameterDefinition : AnimatorDefinitionBase
	{
		[SerializeField, HideInInspector] private string _name = string.Empty;
		[SerializeField, HideInInspector] private int _hash = 0;
		[Header("Set float settings")]
		[SerializeField] private bool m_useDampTime = false;
		[SerializeField] private float m_dampTime = 1.0f;

		public void SetInt(Animator animator, int value)
		{
			ValidateAnimator(animator);
			animator.SetInteger(_hash, value);
		}

		public int GetInt(Animator animator)
		{
			ValidateAnimator(animator);
			return animator.GetInteger(_hash);
		}

		public void SetBool(Animator animator, bool value)
		{
			ValidateAnimator(animator);
			animator.SetBool(_hash, value);
		}

		public bool GetBool(Animator animator)
		{
			ValidateAnimator(animator);
			return animator.GetBool(_hash);
		}

		public void SetFloat(Animator animator, float value)
		{
			ValidateAnimator(animator);
			if (m_useDampTime)
				animator.SetFloat(_hash, value, m_dampTime, Time.deltaTime);
			else
				animator.SetFloat(_hash, value);
		}

		public float GetFloat(Animator animator)
		{
			ValidateAnimator(animator);
			return animator.GetFloat(_hash);
		}

		public void SetTrigger(Animator animator)
		{
			ValidateAnimator(animator);
			animator.SetTrigger(_hash);
		}

		public void ResetTrigger(Animator animator)
		{
			ValidateAnimator(animator);
			animator.ResetTrigger(_hash);
		}
	}
}