using System;

namespace Utilities.General.Characters
{
	public interface ICharacterResource
	{
		float Value { get; set; }

		event Action<float> OnValueChanged;

		void Reset();
	}
}