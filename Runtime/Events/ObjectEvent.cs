using UnityEngine;

namespace Utilities.General.Events
{
    [CreateAssetMenu(menuName = "Utilities/Events/ObjectEvent", fileName = "ObjectEvent")]
    public class ObjectEvent : Event<UnityEngine.Object>
    {
    }
}