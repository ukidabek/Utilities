namespace Utilities.General.Characters
{
	public abstract class Character : Actor
	{
		private IIsDeadStatus m_isDeadStatus = null;
		
		public bool IsDead => HandleStatusProvider(m_isDeadStatus);
		
		public bool IsAlive => !IsDead;

		protected override void Awake()
		{
			base.Awake();
			TryGetStatus(out m_isDeadStatus);
		}
	}
}