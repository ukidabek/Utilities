using UnityEngine;

namespace Utilities.General.Animation
{
	public class AnimatorControllerParameterDefinition : AnimatorControllerDefinition
	{
		[SerializeField] private AnimatorControllerParameterType m_type;
		public AnimatorControllerParameterType Type => m_type;

		[Header("Set float settings")]
		[SerializeField] private bool m_useDampTime = false;
		[SerializeField] private float m_dampTime = 1.0f;
		
		public AnimatorControllerParameterDefinition Initialize(RuntimeAnimatorController controller, (string name, int hash, int type) parameter)
		{
			Initialize($"{controller.name}/{parameter.name}", parameter.hash);
			m_type = (AnimatorControllerParameterType)parameter.type;
			return this;
		}
		
		public AnimatorControllerParameterDefinition Initialize(AnimatorControllerParameterDefinition parameter)
		{
			Initialize(parameter.name, parameter.m_hash);
			m_type = parameter.m_type;
			return this;
		}
		public void SetInt(Animator animator, int value) => animator.SetInteger(m_hash, value);

		public int GetInt(Animator animator) => animator.GetInteger(m_hash);

		public void SetBool(Animator animator, bool value) => animator.SetBool(m_hash, value);

		public bool GetBool(Animator animator) => animator.GetBool(m_hash);

		public void SetFloat(Animator animator, float value) => SetFloat(animator, value, Time.deltaTime);
		
		public void SetFloat(Animator animator, float value, float deltaTime)
		{
			if (m_useDampTime)
				animator.SetFloat(m_hash, value, m_dampTime, deltaTime);
			else
				animator.SetFloat(m_hash, value);
		}

		public float GetFloat(Animator animator) => animator.GetFloat(m_hash);

		public void SetTrigger(Animator animator) => animator.SetTrigger(m_hash);

		public void ResetTrigger(Animator animator) => animator.ResetTrigger(m_hash);
	}
}