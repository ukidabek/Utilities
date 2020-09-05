using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.ObjectGroups
{
    [Serializable]
    public class OnObjectPulledEvent : UnityEvent<GameObject>
    {
    }

    public class GameObjectPuller : ObjectGroupWorker
    {
        public OnObjectPulledEvent OnObjectPulled = new OnObjectPulledEvent();

        [SerializeField] private string m_key = string.Empty;
        public string Key => m_key;

        public void Set(GameObject gameObject) => OnObjectPulled.Invoke(gameObject);

        protected override void Awake()
        {
            m_objectGroup?.PullObject(this);
        }
    }
}