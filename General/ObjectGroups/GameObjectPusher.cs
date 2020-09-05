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

        protected override void Awake()
        {
            m_objectGroup?.PushObject(this);
        }
    }
}