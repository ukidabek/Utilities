using UnityEngine;
using Utilities.General.Events.Core;

namespace Utilities.General.Events
{
    [CreateAssetMenu(menuName = "Utilities/Events/IntEvent", fileName = "IntEvent")]   
    public class IntEvent : ParameterizedEvent<int>
    {
    }
}