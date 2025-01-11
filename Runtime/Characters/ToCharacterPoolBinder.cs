using UnityEngine;
using Utilities.General.Pool;

namespace Utilities.General.Characters
{
	[RequireComponent (typeof (Character))]
	public class ToCharacterPoolBinder : MonoBehaviour
	{
		[SerializeField] private Character m_character = null;
		[field: SerializeField] public CharacterPool Pool
		{
			get;
			internal set;
		}

		public void Release() => Pool.Release(m_character);

		private void Reset() => m_character = GetComponent<Character>();
	}
}