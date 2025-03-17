using UnityEngine;

namespace Utilities.General.Events
{
    [CreateAssetMenu(menuName = "Utilities/Events/VoidEvent", fileName = "VoidEvent")]
    public class Event : Event<IEventListener>
    {
    }
}