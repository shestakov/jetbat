namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public interface IObjectAttributeEditor
	{
		void SetNull();
		string AttributeName { get; }
		bool IsNull { get; }
		object Value { get; set; }
		string UILabel { get; }
		bool IsReadOnly { get; set; }
	}
}