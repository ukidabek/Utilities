using UnityEngine;
using Utilities.General.Events.Core;

namespace Utilities.General.Events
{
    [CreateAssetMenu(menuName = "Utilities/Events/Vector3Event", fileName = "Vector3Event")]
    public class Vector3Event : ParameterizedEvent<Vector3>
    {
    }
}