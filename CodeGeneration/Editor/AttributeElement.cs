namespace Utilities.Editor
{
	public abstract class AttributeElement : CSharpFileElement
	{
		protected AttributeElement(string name) : base(name) { }

		public virtual string Parameters { get => string.Empty; }

		public override string GenerateCode(int tabulatorsCount)
		{
			string parameters = string.IsNullOrEmpty(Parameters) ? string.Empty : string.Format("({0})", Parameters);
			return string.Format("[{0}{1}]\r\n", Name, parameters);

		}
	}
}
