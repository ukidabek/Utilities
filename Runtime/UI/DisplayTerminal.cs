using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.UI
{
	public abstract class DisplayTerminal : ScriptableObject
	{
		[SerializeField] protected UnityEvent<bool> m_onStatusChanged = new UnityEvent<bool>();
		public event UnityAction<bool> OnStatusChanged
		{
			add => m_onStatusChanged.AddListener(value);
			remove => m_onStatusChanged.RemoveListener(value);
		}

		protected virtual void OnEnable() {}

		public virtual void Show() => m_onStatusChanged?.Invoke(true);

		public virtual void Hide() => m_onStatusChanged?.Invoke(false);
	}
}