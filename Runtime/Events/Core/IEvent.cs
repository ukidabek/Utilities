namespace Utilities.General.Events.Core
{
    public interface IEvent<in T> : IEvent
    {
        void Invoke(T argument);
    }

    public interface IEvent
    {
        void Invoke();
    }
}