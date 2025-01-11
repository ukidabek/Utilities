using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities.General.UI
{
	public class SelectorScript : MonoBehaviour
	{
		[SerializeField] private GameObject m_objectToSelect = null;

		private void OnEnable()
		{
			EventSystem.current.SetSelectedGameObject(m_objectToSelect);
		}
	}
}