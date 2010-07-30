using System.Collections.Generic;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public delegate void GetPlainObjectListQualifiedNamespaceDelegate(
		string objectNamespace, string objectName, string complexAttributeName, out string plainObjectListNamespace,
		out string plainObjectListName, out string displayMemberPath, out Dictionary<string, string> migrtaedAttributes);

	public delegate void GetDocumentListQualifiedNamespaceDelegate(
		string objectNamespace, string objectName, string complexAttributeName, out string documentListNamespace,
		out string documentListName, out string displayMemberPath, out Dictionary<string, string> migrtaedAttributes);
}