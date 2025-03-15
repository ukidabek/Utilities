using System;

namespace Utilities.General.Characters
{
	public interface IResource
	{
		public Key Key { get; }
		float Value { get; set; }
		float MaximumValue { get; set; }
		event Action<float> OnValueChanged;
		void Initialize();
		void Reset();
	}

	public interface IUpdatableCharacterResource
	{
		void Update(float deltaTime);
	}
}