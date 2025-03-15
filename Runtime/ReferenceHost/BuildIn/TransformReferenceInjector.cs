using UnityEngine;
using Utilities.General.Reference.Core;

namespace Utilities.General.Reference.BuildIn
{
    [AddComponentMenu("References/Transform/TransformReferenceInjector")]
	public class TransformReferenceInjector : ReferenceHostInjector<TransformReferenceHost, Transform> 
	{
	}
}