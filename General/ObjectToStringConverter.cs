using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.General.Reflection;

namespace Utilities.General
{
    public class ObjectToStringConverter : MonoBehaviour
    {
        public abstract class ToStringConverter
        {
            public abstract bool AcceptObject(object obj);
            public abstract string ConvertObject(object obj);
        }

        public abstract class ToStringConverter<T> : ToStringConverter
        {
            public override bool AcceptObject(object obj) => obj is T;
            protected T GetObject(object obj) => (T) obj;
        }

        private static ToStringConverter[] m_toStringConverters = null;

        [SerializeField] private string m_prefix = string.Empty;
        [SerializeField] private string m_suffix = string.Empty;
        [Space] [SerializeField] private Text m_text = null;
        [SerializeField] private TextMeshProUGUI m_textMeshPro = null;

        private void Awake()
        {
            InitializeConverters();
        }

        private static void InitializeConverters()
        {
            if (m_toStringConverters != null) return;
            var types = ReflectionHelper.GetAllDerivativeTypesFrom(typeof(ToStringConverter));
            m_toStringConverters = ReflectionHelper.CreateInstance<ToStringConverter>(types);
        }

        public void Convert(object obj)
        {
            var converter = m_toStringConverters.FirstOrDefault(stringConverter => stringConverter.AcceptObject(obj));
            if (converter == null) return;
            var value = $"{m_prefix}{converter.ConvertObject(obj)}{m_suffix}";
            if (m_text != null)
                m_text.text = value;
            if (m_textMeshPro != null)
                m_textMeshPro.text = value;
        }
    }
}