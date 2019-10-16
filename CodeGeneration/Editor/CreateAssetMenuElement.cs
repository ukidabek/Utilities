namespace Utilities.Editor
{
	public class CreateAssetMenuElement : AttributeElement
	{
		public override string Parameters => string.Format("fileName = \"{0}.asset\", menuName = \"Custom Type/{0}\"", FileName);
		public string FileName { get; private set; }

		public CreateAssetMenuElement(string fileName) : base("CreateAssetMenu")
		{
			FileName = fileName;
		}
	}
}