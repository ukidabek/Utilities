using UnityEngine;

namespace Utilities.Values
{
    public abstract class BaseValueReference
    {
    }
    
    public abstract class BaseValueReference<T, T1> : BaseValueReference where T : BaseValue
    {
        [SerializeField] private bool m_useReference = false;
        [SerializeField] private T m_reference = default;
        [SerializeField] private T1 m_value = default;
        
        public T1 Value
        {
            get
            {
                if (m_useReference && m_reference != null && m_reference.Value is T1 value)
                    return value;

                return m_value;
            }
            set
            {
                if (m_reference && m_reference != null)
                    m_reference.Value = value;
                else
                    m_value = value;
            }
        }

        public static implicit operator T1(BaseValueReference<T, T1> value) => value.Value;
    }
}