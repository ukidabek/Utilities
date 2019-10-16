using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Utilities.Editor
{
	public partial class CSharpFile
	{
		public List<UsingElement> Usings = new List<UsingElement>();

		public List<CSharpFileElement> cSharpFileElements = new List<CSharpFileElement>();

		public static string WriteObjects<T>(List<T> objects, int tabulatorsCount) where T : CSharpFileElement
		{
			string content = string.Empty;
			foreach (var item in objects)
				content += item.GenerateCode(tabulatorsCount);
			return content;
		}

		public const string BeginBody = "{\r\n";
		public const string EndBody = "}\r\n";
		public const string SimpleBody = "{ }\r\n";
		public const string NewLine = "\r\n";

		private string Generate()
		{
			string content = string.Empty; ;
			content += WriteObjects(cSharpFileElements, 0);
			return content;
		}

		public virtual void Save(string path)
		{
			File.WriteAllText(string.Format("{0}/{1}.cs", path, "Test"), Generate());
		}
	}
}
