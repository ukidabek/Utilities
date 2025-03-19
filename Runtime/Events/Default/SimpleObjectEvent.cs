using UnityEngine;
using Utilities.General.Events.Core;

namespace Utilities.General.Events
{
    [CreateAssetMenu(menuName = "Utilities/Events/SimpleObjectEvent", fileName = "SimpleObjectEvent")]
    public class SimpleObjectEvent : ParameterizedEvent<object>
    {
    }
}