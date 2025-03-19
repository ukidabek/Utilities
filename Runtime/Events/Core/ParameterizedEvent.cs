namespace Utilities.General.Events.Core
{
    public abstract class ParameterizedEvent<T> : Event<IEventListener<T>>, IEvent<T>
    {
        public sealed override void Invoke() => Invoke(default);
        
        public void Invoke(T eventArgument)
        {
            LogEventInvoke();
            InvokeListeners(m_listeners, listener => listener.Invoke(eventArgument));
        }
    }
}