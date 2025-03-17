using UnityEngine;

namespace Utilities.General.Events
{
    [CreateAssetMenu(menuName = "Utilities/Events/IntEvent", fileName = "IntEvent")]   
    public class IntEvent : ParameterizedEvent<int>
    {
    }
}