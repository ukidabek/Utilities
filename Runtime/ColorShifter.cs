using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
	public class ColorShifter : MonoBehaviour
	{
		[Serializable]
		public class UpdateColorCallback : UnityEvent<Color>
		{
		}

		public Color ColorA = new Color();
		public Color ColorB = new Color();
		public AnimationCurve ShiftCharacteristic = new AnimationCurve();
		[Space] public UpdateColorCallback UpdateColor = new UpdateColorCallback();
		private float counter = 0;
		private Coroutine shiftCoroutine = null;

		public void Shift()
		{
			if (shiftCoroutine != null) StopCoroutine(shiftCoroutine);
			shiftCoroutine = StartCoroutine(ShiftCoroutine());
		}

		private IEnumerator ShiftCoroutine()
		{
			counter = 0;
			UpdateColor.Invoke(ColorA);
			while (counter < ShiftCharacteristic.keys[ShiftCharacteristic.keys.Length - 1].time)
			{
				counter += Time.deltaTime;
				float value = ShiftCharacteristic.Evaluate(counter);
				UpdateColor.Invoke(Color.Lerp(ColorA, ColorB, value));
				yield return null;
			}
		}
	}
}