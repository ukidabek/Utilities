using UnityEngine;
using UnityEngine.Events;


namespace Utilities.General.GamesState
{
	public class GameStateHost : MonoBehaviour
	{
		[SerializeField] private UnityEvent<GameStateInfo, GameStateInfo> m_onGameStateChanged = null;
		public event UnityAction<GameStateInfo, GameStateInfo> OnGameStateChanged
		{
			add => m_onGameStateChanged.AddListener(value);
			remove => m_onGameStateChanged.RemoveListener(value);
		}

		[SerializeField] private GameStateInfo m_currentState = null;
		public GameStateInfo CurrentState
		{
			get => m_currentState;
			set
			{
				if (m_currentState == value) return;
				var previousState = m_currentState;
				m_currentState = value;
				m_onGameStateChanged.Invoke(previousState, m_currentState);
			}
		}

		private void Reset() => name = GetType().Name;
	}
}