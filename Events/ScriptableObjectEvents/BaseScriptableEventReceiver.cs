using UnityEngine;

public abstract class BaseScriptableEventReceiver : MonoBehaviour
{
    [SerializeField] private BaseScriptableEvent m_event = null;
    
    private void OnEnable() => Register();

    private void OnDisable() => Unregister();

    private void OnDestroy() => Unregister();

    private void Register() => m_event?.RegisterReceiver(this);

    private void Unregister() => m_event?.UnregisterReceiver(this);
}