using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.Events.Core
{
    public class EventListener<T, T1> : EventListener<T1> where T : ParameterizedEvent<T1>
    {
        [SerializeField] private T m_event;
        protected override ParameterizedEvent<T1> Event => m_event;
    }

    public abstract class EventListener<T> : EventListener, IEventListener<T>
    {
        public UnityEvent<T> Callback =  new UnityEvent<T>();
        protected abstract ParameterizedEvent<T> Event { get; }
        private void OnEnable() => Event?.Subscribe(this);

        private void OnDisable() => Event?.Unsubscribe(this);

        public override void Invoke() => Invoke(default);
        
        public void Invoke(T eventArgument) => Callback.Invoke(eventArgument);
        
    }

    public abstract class EventListener : MonoBehaviour, IEventListener
    {
        public abstract void Invoke();
    }

    public interface IEventListener
    {
        void Invoke();
    }

    public interface IEventListener<in T> : IEventListener
    {
        void Invoke(T eventArgument);
    }
}