using UnityEngine;

namespace Utilities.General.Pool
{
	[CreateAssetMenu(fileName = "GameObjectPool", menuName = "Utilities/Pool/GameObjectPool")]
	public class GameObjectPool : ObjectPool<GameObject>
	{
		protected override void OnPoolGet(GameObject character) =>  character.SetActive(true);

		protected override void OnPoolRelease(GameObject character) => character.SetActive(false);
	}
}