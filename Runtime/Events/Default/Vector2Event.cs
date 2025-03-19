using UnityEngine;
using Utilities.General.Events.Core;

namespace Utilities.General.Events
{
    [CreateAssetMenu(menuName = "Utilities/Events/Vector2Event", fileName = "Vector2Event")]
    public class Vector2Event : ParameterizedEvent<Vector2>
    {
    }
}