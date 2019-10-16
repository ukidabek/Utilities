using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
	[Serializable] public class CollisionCallback : UnityEvent<Collision> { }
}