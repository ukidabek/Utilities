using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.ObjectGroups
{
    public class GameObjectsPuller : ObjectGroupWorker
    {
        [Serializable]
        public class OnObjectsPulledEvent : UnityEvent<List<GameObject>>
        {
        }
        
        public OnObjectsPulledEvent OnObjectsPulled = new OnObjectsPulledEvent();

        public void Set(List<GameObject> list) => OnObjectsPulled.Invoke(list);
        
        protected override void Awake() => PullObjects();

        private void PullObjects() => m_objectGroup?.PullObjects(this);

        protected virtual void OnEnable() => PullObjects();
    }
}