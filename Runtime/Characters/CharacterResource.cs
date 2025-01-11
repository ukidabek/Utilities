using System;
using UnityEngine;

namespace Utilities.General.Characters
{
	public class CharacterResource : MonoBehaviour, ICharacterResource
	{
		public event Action<float> OnValueChanged = null;

		[SerializeField] private float m_value = 100;
		public virtual float Value
		{
			get => m_value;
			set
			{
				if (m_value == value) return;
				m_value = value;
				OnValueChanged?.Invoke(m_value);
			}
		}

		private float m_initialValue = 0;

		protected virtual void Awake() 
		{
			m_initialValue = m_value;
		}

		public virtual void Reset()
		{
			m_value = m_initialValue;
		}
	}
}