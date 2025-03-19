using UnityEngine;
using Utilities.General.Events.Core;

namespace Utilities.General.Events
{
    [CreateAssetMenu(menuName = "Utilities/Events/BoolEvent", fileName = "BoolEvent")]
    public class BoolEvent : ParameterizedEvent<bool>
    {
    }
}