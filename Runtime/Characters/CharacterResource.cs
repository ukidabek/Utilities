using System;
using UnityEngine;

namespace Utilities.General.Characters
{
    [Serializable]
    public class CharacterResource : ICharacterResource
    {
        [SerializeField] private Key m_keyA = null;
        public Key KeyA => m_keyA;
        
        public event Action<float> OnValueChanged = null;

        [SerializeField, ReadOnly] private float m_value = 0;

        public virtual float Value
        {
            get => m_value;
            set
            {
                if (m_value == value) return;
                m_value = Mathf.Clamp(value, 0f, m_maximumValue);
                OnValueChanged?.Invoke(m_value);
            }
        }

        [SerializeField] protected float m_maximumValue = 100;

        public virtual void Initialize() => m_maximumValue = m_value;

        public virtual void Reset() => m_value = m_maximumValue;
    }
}