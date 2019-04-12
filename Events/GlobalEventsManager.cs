using BaseGameLogic.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BaseGameLogic.Utilities
{
    public class GlobalEventsManager : MonoBehaviour
    {
        public static GlobalEventsManager Instance { get; private set; }

        [SerializeField] private List<GlobalEvent> _eventList = new List<GlobalEvent>();
        public List<GlobalEvent> EventList { get { return _eventList; } }
        private Dictionary<string, GlobalEvent> _eventsDictionary = new Dictionary<string, GlobalEvent>();

        protected void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            foreach (var item in _eventList)
                _eventsDictionary.Add(item.Name, item);
        }

        [Serializable] public class GlobalEvent
        {
            [Serializable] private class EventWithParams : UnityEvent<object[]> {}
            [SerializeField] private string _name = string.Empty;
            public string Name { get { return _name; } }

            [SerializeField] private EventWithParams @event = new EventWithParams();

            public void ActivateEvent(params object[] data)
            {
                @event.Invoke(data);
            }
        }

        public void TriggerEvent(string name, params object[] data)
        {
            GlobalEvent @event = null;
            if (_eventsDictionary.TryGetValue(name, out @event))
            {
                @event.ActivateEvent(data);
            }
            else
                Debug.LogFormat("There is no event name of {0}.", name);
        }
    }
}