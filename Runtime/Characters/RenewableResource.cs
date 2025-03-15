using System;
using UnityEngine;

namespace Utilities.General.Characters
{
    [Serializable]
    public class RenewableResource : UpdatableResource
    {
        [SerializeField] protected float m_regenerationSpeed = 1f;
        public float RegenerationSpeed
        {
            get => m_regenerationSpeed;
            set => m_regenerationSpeed = value;
        }

        [SerializeField] protected bool m_constraintRegeneration = false;
        public bool ConstraintRegeneration
        {
            get => m_constraintRegeneration;
            set => m_constraintRegeneration = value;
        }

        [SerializeField] protected float m_maxRegenerationValue = 30f;
        public float MaxRegenerationValue
        {
            get => m_maxRegenerationValue;
            set => m_maxRegenerationValue = value;
        }

        public override void Update(float deltaTime)
        {
            if (ConstraintRegeneration && Value >= MaxRegenerationValue) return;
            if (Value >= MaximumValue) return;
            Value += RegenerationSpeed * deltaTime;
        }
    }
}