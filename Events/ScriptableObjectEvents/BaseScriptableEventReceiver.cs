using UnityEngine;

namespace Utilities.Events
{
    public abstract class BaseScriptableEventReceiver : MonoBehaviour
    {
        [SerializeField] private BaseScriptableEvent m_event = null;
        private string EventName => (m_event ? m_event.name : "null");

        private void OnEnable() => Register();

        private void OnDisable() => Unregister();

        private void OnDestroy() => Unregister();

        private void Register()
        {
            m_event?.RegisterReceiver(this);
            Debug.Log(
                $"Receiver attached to <b>{name}</b> game object registered to event <b>{EventName}</b>");
        }


        private void Unregister()
        {
            m_event?.UnregisterReceiver(this);
            Debug.Log(
                $"Receiver attached to <b>{name}</b> game object unregistered from event <b>{EventName}</b>");
        }
    }
}