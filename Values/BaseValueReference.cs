using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Utilities.Events;

namespace Utilities.Values
{
    public abstract class BaseValueReference
    {
        
    }
    
    public abstract class BaseValueReference<T, T1> : BaseValueReference where T : BaseValue
    {
        [SerializeField] private bool m_useReference = false;
        [SerializeField] private T m_reference;
        [SerializeField] private T1 m_value;

        public T1 Value
        {
            get
            {
                if (m_useReference && m_reference != null && m_reference.Value is T1 value)
                    return value;

                return m_value;
            }
        }

        public static implicit operator T1(BaseValueReference<T, T1> value) => value.Value;
    }
}