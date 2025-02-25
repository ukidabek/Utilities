using System;

namespace Utilities.General.Characters
{
	public interface ICharacterResource
	{
		public Key KeyA { get; }
		float Value { get; set; }
		event Action<float> OnValueChanged;
		void Initialize();
		void Reset();
	}

	public interface IUpdatableCharacterResource
	{
		void Update(float deltaTime);
	}
}