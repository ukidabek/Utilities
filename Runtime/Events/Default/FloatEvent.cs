using UnityEngine;
using Utilities.General.Events.Core;

namespace Utilities.General.Events
{
    [CreateAssetMenu(menuName = "Utilities/Events/FloatEvent", fileName = "FloatEvent")]   
    public class FloatEvent : ParameterizedEvent<float>
    {
    }
}