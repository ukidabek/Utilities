namespace Utilities.General.Events
{
    public abstract class ParameterizedEvent<T> : Event<IEventListener<T>>
    {
        public sealed override void Invoke() => Invoke(default);
        
        public void Invoke(T eventArgument)
        {
            LogEventInvoke();
            InvokeListeners(m_listeners, listener => listener.Invoke(eventArgument));
        }
    }
}