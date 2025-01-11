using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.Characters
{
	[CreateAssetMenu(menuName = "Character/CharacterCollection")]
	public class CharacterCollection : ScriptableObject, IEnumerable<Character>
	{
		public UnityEvent<CharacterCollectionChangedEventArgs> OnCharacterListChanged = new UnityEvent<CharacterCollectionChangedEventArgs>();

		private List<Character> m_characters = new List<Character>();
		public ReadOnlyCollection<Character> CharacterList { get; private set; }

		private CharacterCollectionChangedEventArgs m_event = new CharacterCollectionChangedEventArgs();

		private void OnEnable()
		{
			m_characters.Clear();
			CharacterList = new ReadOnlyCollection<Character>(m_characters);
		}

		public void AddCharacter(Character character)
		{
			if (m_characters.Contains(character)) return;

			m_characters.Add(character);

			m_event.Type = CharacterCollectionChangedAction.Added;
			m_event.Character = character;
			OnCharacterListChanged.Invoke(m_event);
		}

		public void RemoveCharacter(Character character)
		{
			if (!m_characters.Contains(character)) return;

			m_characters.Add(character);

			m_event.Type = CharacterCollectionChangedAction.Removed;
			m_event.Character = character;
			OnCharacterListChanged.Invoke(m_event);
		}

		public void Clear() => m_characters.Clear();

		public IEnumerator<Character> GetEnumerator() => m_characters.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => m_characters.GetEnumerator();
	}
}