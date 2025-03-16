using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.Events
{
    public class EventListener<T, T1> : EventListener<T1> where T : Event<T1>
    {
        [SerializeField] private T m_event;
        protected override Event<T1> Event => m_event;
    }

    public abstract class EventListener<T> : MonoBehaviour
    {
        public UnityEvent<T> Callback =  new UnityEvent<T>();
        protected abstract Event<T> Event { get; }
        private void OnEnable() => Event?.Subscribe(this);

        private void OnDisable() => Event?.Unsubscribe(this);
        
        public void Invoke(T eventArgument) => Callback.Invoke(eventArgument);
    }
}