using UnityEngine;
using Utilities.General.Characters;
using Utilities.General.Reset;

namespace Utilities.General.Pool
{
	[CreateAssetMenu(menuName = "Utilities/Character/CharacterPool")]
	public class CharacterPool : ObjectPool<Character>
	{
		protected override void OnPoolGet(Character character)
		{
			if (character.TryGetComponent<ToCharacterPoolBinder>(out var binder))
			{
				binder.Pool = this;
			}

			character.gameObject.SetActive(true);
		}

		protected override void OnPoolRelease(Character character)
		{
			var resetLogic = character.GetComponent<IResetLogic>();
			resetLogic.ResetObject();
			character.gameObject.SetActive(false);
		}
	}
}