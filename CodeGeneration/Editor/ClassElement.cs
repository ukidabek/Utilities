using System;

namespace Utilities.Editor
{
	public class InheritanceElement : CSharpFileElement
	{
		private Type type = null;

		protected InheritanceElement(string name) : base(name) { }
		public InheritanceElement(Type type) : this(type.Name)
		{
			this.type = type;
		}

		public override string GenerateCode(int tabulatorsCount)
		{
			return string.Format(" : {0}", Name);
		}
	}

	public class ClassElement : WithInterfacesObjectElement
	{
		public override ObjectType Type => ObjectType.Class;
		public InheritanceElement DerivedFrom = null;
		public bool IsAbstract = false;
		public ClassElement(string name) : base(name) { }

		public override string GenerateCode(int tabulatorsCount)
		{
			string content = string.Empty;
			string tabulators = GenerateTabulations(tabulatorsCount);
			content += tabulators + CSharpFile.WriteObjects(ClassAttributes, tabulatorsCount);
			content += tabulators + string.Format(
				"{0} {1} {2} {3}",
				AccessModifier.ToString().ToLower(),
				IsAbstract ? "abstract" : string.Empty,
				Type.ToString().ToString().ToLower(),
				Name);

			if (DerivedFrom != null)
				content += DerivedFrom.GenerateCode(0);

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
