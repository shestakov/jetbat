using JetBat.Client.Metadata.Simple;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public delegate IComplexAttributeEditor CreateComplexAttributeEditorDelegate(
		string objectNamespace, string objectName, ObjectComplexAttribute complexAttribute, out bool hideFromUser);
}