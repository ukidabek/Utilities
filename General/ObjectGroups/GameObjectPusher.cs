using System;
using UnityEngine;

namespace Utilities.General.ObjectGroups
{
    public class GameObjectPusher : ObjectGroupWorker
    {
        [SerializeField] private string m_key = String.Empty;
        public string Key
        {
            get => string.IsNullOrEmpty(m_key) ? ObjectToPush.name : m_key;
            set
            {
                if (m_key != value)
                {
                    m_key = value;
                    m_objectGroup?.PushObject(this);
                }
            }
        } 

        [SerializeField] private GameObject m_objectToPush = null;
        public GameObject ObjectToPush => m_objectToPush != null ? m_objectToPush : gameObject;

        [SerializeField] private bool m_overrideObject = true;
        public bool OverrideObject => m_overrideObject;

        [SerializeField] private bool m_useObjectInstanceIdAsKey = false;

        protected override void Awake()
        {
            m_key = m_useObjectInstanceIdAsKey ? GetInstanceID().ToString() : m_key;
            Push();
        }

        private void OnEnable() => Push();

        private void OnDestroy()
        {
            m_objectGroup?.ClearObject(this);
        }

        public void Push() => m_objectGroup?.PushObject(this);
    }
}