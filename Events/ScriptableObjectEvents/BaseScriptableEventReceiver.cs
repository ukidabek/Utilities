using UnityEngine;

namespace Utilities.Events
{
    public abstract class BaseScriptableEventReceiver : MonoBehaviour
    {
        public abstract BaseScriptableEvent Event { get; }

        private string EventName => Event ? Event.name : "null";

        [SerializeField] private bool m_registerOnEnable = true;
        [SerializeField] private bool m_unregisterOnDisable = true;

        private void OnEnable()
        {
            if(!m_registerOnEnable) return;
            Register();
        }

        private void OnDisable()
        {
            if(!m_unregisterOnDisable) return;
            Unregister();
        }

        private void OnDestroy() => Unregister();

        public void Register()
        {
            Event?.RegisterReceiver(this);
            Debug.Log(
                $"Receiver attached to <b>{name}</b> game object registered to event <b>{EventName}</b>");
        }


        public void Unregister()
        {
            Event?.UnregisterReceiver(this);
            Debug.Log(
                $"Receiver attached to <b>{name}</b> game object unregistered from event <b>{EventName}</b>");
        }
    }


    public abstract class BaseScriptableEventReceiver<T, T1> : BaseScriptableEventReceiver where T1 : BaseScriptableEvent<T>
    {
        [SerializeField] private T1 m_event = default;

        public override BaseScriptableEvent Event => m_event;

        protected interface ICallback
        {
            void Call(T value);
        }
        
        [General.ReadOnly, SerializeField] private T m_value = default(T);

        protected abstract ICallback Callback { get; }

        public void Invoke(T value) => Callback.Call(m_value = value);
    }
}