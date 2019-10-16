using System.Collections.Generic;

namespace Utilities.Editor
{
	public abstract class ObjectElement : CSharpFileElement
	{
		public FileAccessModifier AccessModifier = FileAccessModifier.Public;
		public abstract ObjectType Type { get; }

		public List<CSharpFileElement> cSharpFileElements = new List<CSharpFileElement>();

		public List<AttributeElement> ClassAttributes = new List<AttributeElement>();

		protected ObjectElement(string name) : base(name) { }
	}
}
