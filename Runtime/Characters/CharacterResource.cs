using System;
using UnityEngine;

namespace Utilities.General.Characters
{
    [Serializable]
    public class CharacterResource : ICharacterResource
    {
        [SerializeField] private Key m_key = null;
        public Key Key => m_key;
        
        public event Action<float> OnValueChanged = null;

        [SerializeField] private float m_value = 0;

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
        public float MaximumValue
        {
            get => m_maximumValue;
            set => m_maximumValue = value;
        }
        
        public virtual void Initialize() => Value = MaximumValue;

        public virtual void Reset() => Initialize();
    }
}