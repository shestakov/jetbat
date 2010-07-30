using System.Collections.Generic;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public interface IComplexAttributeEditor
	{
		string ComplexAttributeName { get; }
		bool IsReadOnly { get; set; }
		NamedObjectCollection<NameValue> SelectedObject { get; }
		Dictionary<string, string> MigratedAttributes { get; }
		void Load(NamedObjectCollection<NameValue> actualParameters);
		void SelectItem(NamedObjectCollection<NameValue> complexAttributeValue);
	}
}