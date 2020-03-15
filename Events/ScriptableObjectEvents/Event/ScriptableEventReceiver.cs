using UnityEngine.Events;

public class ScriptableEventReceiver : BaseScriptableEventReceiver
{
    public UnityEvent Callback = new UnityEvent();

    public void Invoke() => Callback.Invoke();
}