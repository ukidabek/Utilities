namespace Utilities.General.Characters
{
	public interface IStatus
	{
		bool Status { get; }
		void Initialize(Actor actor);
	}
}