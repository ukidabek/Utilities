namespace Utilities.General.Characters
{
	public class CharacterCollectionChangedEventArgs
	{
		public CharacterCollectionChangedAction Type { get; internal set; }
		public Character Character { get; internal set; }
	}
}