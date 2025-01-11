using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utilities.General.UI
{
	public abstract class Display : MonoBehaviour
	{
		[SerializeField] protected GameObject m_panel = null;
		[SerializeField] protected bool m_disableOnAwake = false;
		protected virtual void Show() => (m_panel ?? gameObject).SetActive(true);

		protected virtual void Hide() => (m_panel ?? gameObject).SetActive(false);

		protected virtual void Awake()
		{
			if (!m_disableOnAwake) return;
			Hide();
		}

		protected abstract void OnEnable();

		protected abstract void OnDisable();

		protected abstract void OnDestroy();
	}

	public abstract class Display<TerminalType> : Display where TerminalType : DisplayTerminal
	{
		[SerializeField] protected TerminalType m_terminal = null;

		protected override void OnEnable() => ConnectToTerminal(m_terminal);

		protected override void OnDisable() => DisconnectFromTerminal(m_terminal);

		protected override void OnDestroy() => DisconnectFromTerminal(m_terminal);

		protected virtual void ConnectToTerminal(TerminalType eventBus) => m_terminal.OnStatusChanged += OnStateChanged;

		protected virtual void DisconnectFromTerminal(TerminalType eventBus) => m_terminal.OnStatusChanged -= OnStateChanged;

		private void OnStateChanged(bool status)
		{
			if (status)
				Show();
			else
				Hide();
		}

		protected void GenerateItemsDisplay<DataType, SubDisplayType>(
			IReadOnlyList<DataType> itemsToDisplay, 
			IList<SubDisplayType> displayList, 
			Pool.ObjectPool<SubDisplayType> pool, 
			Transform parent)
			where DataType : Object
			where SubDisplayType : SubDisplay<TerminalType, DataType>
		{
			foreach (var item in itemsToDisplay)
			{
				var instance = pool.Get();
				instance.transform.SetParent(parent);
				instance.Initialize(item);
				displayList.Add(instance);
			}
		}

		protected void ClearItemsDisplay<DataType, SubDisplayType>(IList<SubDisplayType> displayList, Pool.ObjectPool<SubDisplayType> pool)
			where SubDisplayType : SubDisplay<TerminalType, DataType>
		{
			foreach(var item in displayList)
			{
				pool.Release(item);
			}
			displayList.Clear();
		}
	}

	public abstract class SubDisplay<TerminalType, DataType> : Selectable, ISubmitHandler
		where TerminalType : DisplayTerminal
	{
		[SerializeField] protected TerminalType m_terminal = null;

		public abstract void Initialize(DataType dataType);

		public virtual void OnSubmit(BaseEventData eventData)
		{
			// if we get set disabled during the press
			// don't run the coroutine.
			if (!IsActive() || !IsInteractable())
				return;

			DoStateTransition(SelectionState.Pressed, false);
			StartCoroutine(OnFinishSubmit());
		}

		private IEnumerator OnFinishSubmit()
		{
			var fadeTime = colors.fadeDuration;
			var elapsedTime = 0f;

			while (elapsedTime < fadeTime)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}

			DoStateTransition(currentSelectionState, false);
		}
	}
}