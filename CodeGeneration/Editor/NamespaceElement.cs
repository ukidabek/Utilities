using System.Collections.Generic;

namespace Utilities.Editor
{
	public class NamespaceElement : CSharpFileElement
	{
		public List<CSharpFileElement> cSharpFileElements = new List<CSharpFileElement>();

		public NamespaceElement(string name) : base(name) { }

		public override string GenerateCode(int tabulatorsCount)
		{
			string content = string.Empty;
			content += string.Format("namespace {0}", Name);
			content += CSharpFile.NewLine;
			content += CSharpFile.BeginBody;
			content += CSharpFile.WriteObjects(cSharpFileElements, tabulatorsCount + 1);
			content += CSharpFile.EndBody;

			return content;
		}
	}
}
