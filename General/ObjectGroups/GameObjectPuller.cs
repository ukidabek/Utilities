using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.ObjectGroups
{
    public class GameObjectPuller : ObjectGroupWorker
    {
        [Serializable]
        public class OnObjectPulledEvent : UnityEvent<GameObject>
        {
        }
        
        public OnObjectPulledEvent OnObjectPulled = new OnObjectPulledEvent();

        [SerializeField] private string m_key = string.Empty;
        public string Key => m_key;

        [SerializeField] private GameObject m_gameObject = null;
        public GameObject GameObject => m_gameObject;

        public void Set(GameObject gameObject) => OnObjectPulled.Invoke(m_gameObject = gameObject);

        protected override void Awake()
        {
            PullObject();
        }

        private void PullObject() => m_objectGroup?.PullObject(this);
    }
}