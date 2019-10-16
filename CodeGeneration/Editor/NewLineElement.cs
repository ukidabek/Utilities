namespace Utilities.Editor
{
	public class NewLineElement : CSharpFileElement
	{
		public NewLineElement() : base(string.Empty) { }

		public override string GenerateCode(int tabulatorsCount)
		{
			return CSharpFile.NewLine;
		}
	}
}
