using System;

namespace Utilities.Editor.CodeGeneration
{
	public class UsingElement : CSharpFileElement
	{
		public UsingElement(string usingPath) : base(usingPath) { }

		public UsingElement(Type type) : base(type.Namespace) { }

		public override string GenerateCode(int tabulatorsCount)
		{
			return string.Format("using {0};\r\n", Name);
		}
	}
}