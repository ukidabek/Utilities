namespace Utilities.Editor
{
	public class ClassElement : WithInterfacesObjectElement
	{
		public override ObjectType Type => ObjectType.Class;
		public string DerivedFrom = string.Empty;

		public ClassElement(string name) : base(name) { }

		public override string GenerateCode(int tabulatorsCount)
		{
			string content = string.Empty;
			string tabulators = GenerateTabulations(tabulatorsCount);
			content += tabulators + CSharpFile.WriteObjects(ClassAttributes, tabulatorsCount);
			content += CSharpFile.NewLine;
			content += tabulators + string.Format(
				"{0} {1} {2}",
				AccessModifier.ToString().ToLower(),
				Type.ToString().ToString().ToLower(),
				Name);

			if (!string.IsNullOrEmpty(DerivedFrom))
				content += string.Format(" : {0}", DerivedFrom);

			content += CSharpFile.NewLine;
			content += tabulators + CSharpFile.BeginBody;
			if (cSharpFileElements.Count > 0)
			{
				content += CSharpFile.NewLine;
				content += tabulators + CSharpFile.WriteObjects(cSharpFileElements, tabulatorsCount + 1);
			}
			content += tabulators + CSharpFile.EndBody;

			return content;
		}

	}
}
