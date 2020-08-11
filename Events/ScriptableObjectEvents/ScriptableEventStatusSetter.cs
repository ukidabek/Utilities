using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public class ScriptableEventStatusSetter : MonoBehaviour
    {
        private static readonly Dictionary<BaseScriptableEvent, bool> InitialStatusDictionary = new Dictionary<BaseScriptableEvent, bool>();
        
        [SerializeField] private BaseScriptableEvent m_event = null;

        [SerializeField] private bool m_status = default;
        public bool Status
        {
            get => m_event.Enabled;
            set => m_status = m_event.Enabled = value;
        }

        private void Awake()
        {
            if (m_event == null) return;
            m_status = m_event.Enabled;
            if (!InitialStatusDictionary.ContainsKey(m_event))
                InitialStatusDictionary.Add(m_event, m_event.Enabled);
        }

        private void OnValidate()
        {
            if (m_event == null) return;
            m_status = m_event.Enabled;
        }

        private void OnDestroy()
        {
            if(InitialStatusDictionary == null) return;

            if (InitialStatusDictionary.TryGetValue(m_event, out bool initialStatus))
                m_event.Enabled = initialStatus;
        }
    }
}