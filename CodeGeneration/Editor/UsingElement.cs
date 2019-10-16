namespace Utilities.Editor
{
	public class UsingElement : CSharpFileElement
	{
		public UsingElement(string usingPath) : base(usingPath) { }

		public override string GenerateCode(int tabulatorsCount)
		{
			return string.Format("using {0};\r\n", Name);
		}
	}
}
