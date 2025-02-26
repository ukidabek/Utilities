namespace Utilities.General.Characters
{
	public interface IStatusProvider
	{
		bool Status { get; }
		void Initialize(Character character);
	}
}