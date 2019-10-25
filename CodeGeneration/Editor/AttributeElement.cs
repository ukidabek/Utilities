using System;

namespace Utilities.Editor.CodeGeneration
{
	public class AttributeElement : CSharpFileElement
	{
		protected AttributeElement(string name) : base(name) { }

		public AttributeElement(Type type) : this(type.Name) { }

		public virtual string Parameters { get => string.Empty; }

		public override string GenerateCode(int tabulatorsCount)
		{
			string parameters = string.IsNullOrEmpty(Parameters) ? string.Empty : string.Format("({0})", Parameters);
			return string.Format("[{0}{1}]\r\n", Name, parameters);

		}
	}
}
