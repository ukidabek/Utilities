using UnityEngine;

namespace Utilities.Events
{
    public abstract class BaseScriptableEventInvoker : MonoBehaviour
    {
    }
    
    public abstract class BaseScriptableEventInvoker<T> : BaseScriptableEventInvoker
    {
        public abstract void Invoke(T value);
    }
}