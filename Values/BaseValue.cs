using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Values
{
    public abstract class BaseValue : ScriptableObject
    {
        public abstract Type ValueType { get; }
        public abstract object Value { get; set; }
    }

    public class BaseValue<T> : BaseValue
    {
        public event Action<T> OnValueChange = null;

        [SerializeField] private T m_value;

        public override Type ValueType => typeof(T);

        public override object Value
        {
            get => m_value;
            set
            {
                if (!(value is T)) return;
                ;

                if (!m_value.Equals(value))
                    OnValueChange?.Invoke((T) value);
                m_value = (T) value;
            }
        }

        public static implicit operator T(BaseValue<T> value) => value.m_value;
    }
}