using UnityEngine;

namespace Utilities.Events
{
    [CreateAssetMenu(fileName = "ObjectListScriptableEvent", menuName = "ScriptableEvent/New ObjectListScriptableEvent")]
    public class ObjectListScriptableEvent : BaseScriptableEvent<object[]>
    {
    }
}