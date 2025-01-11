using System.Linq;
using UnityEngine;

namespace Utilities.General
{
	/// <summary>
	/// This object detach transforms form list form its parent.
	///	If GameObject have any child object hosing a components with 
	///	transform won't be moved in any way. Those object should be 
	///	detached form performance reason.
	/// </summary>
	public class ChildDetacher : MonoBehaviour
	{
		[SerializeField] private Transform[] m_childsToDetatch;

		private void Awake()
		{
			foreach (var item in m_childsToDetatch)
				item.SetParent(null);
		}

		private void OnDestroy()
		{
			foreach (var item in m_childsToDetatch)
				Destroy(item.gameObject);
		}

		[ContextMenu("Collect static objects")]
		private void CollectStaticObjects()
		{
			var rootGameObject = transform.root.gameObject;

			m_childsToDetatch = m_childsToDetatch
				.Concat(transform.GetComponentsInChildren<Transform>().Where(child => child.gameObject.isStatic))
				.Distinct()
				.ToArray();
		}
	}
}