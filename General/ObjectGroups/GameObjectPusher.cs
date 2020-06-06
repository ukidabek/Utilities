using System;
using UnityEngine;

namespace Utilities.General.ObjectGroups
{
    public class GameObjectPusher : ObjectGroupWorker
    {
        [SerializeField] private string m_key = String.Empty;
        public string Key => string.IsNullOrEmpty(m_key) ? m_objectToPush.name : m_key;

        [SerializeField] private GameObject m_objectToPush = null;
        public GameObject ObjectToPush => m_objectToPush;

        protected override void Awake()
        {
            m_objectGroup?.PushObject(this);
        }
    }
}