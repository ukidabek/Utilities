namespace Utilities.Editor.CodeGeneration
{
	public abstract class CSharpFileElement
	{
		public string Name = "CSharpFile";

		protected CSharpFileElement(string name)
		{
			Name = name;
		}

		public abstract string GenerateCode(int tabulatorsCount);

		protected string GenerateTabulations(int count)
		{
			string tabulators = string.Empty;
			for (int i = 0; i < count; i++)
			{
				tabulators += "\t";
			}
			return tabulators;
		}
	}
}
