using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.ObjectGroups
{
    public class OnObjectPulledEvent : UnityEvent<GameObject>
    {
    }

    public class GameObjectPuller : ObjectGroupWorker
    {
        public OnObjectPulledEvent OnObjectPulled = new OnObjectPulledEvent();
        public string Key { get; set; }

        public void Set(GameObject gameObject) => OnObjectPulled.Invoke(gameObject);

        protected override void Awake()
        {
            m_objectGroup?.PullObject(this);
        }
    }
}