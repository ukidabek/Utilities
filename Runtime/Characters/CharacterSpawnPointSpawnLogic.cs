using System.Collections;
using UnityEngine;

namespace Utilities.General.Characters
{
	public abstract class CharacterSpawnPointSpawnLogic : MonoBehaviour
	{
		public abstract IEnumerator SpawnCoroutine();
	}
}