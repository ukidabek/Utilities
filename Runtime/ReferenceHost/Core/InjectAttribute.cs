using System;

namespace Utilities.General.Reference.Core
{
	public class InjectAttribute : Attribute
    {
		public string ID { get; private set; } = string.Empty;

		public InjectAttribute()
		{
		}

		public InjectAttribute(string iD)
		{
			ID = iD;
		}
	}
}