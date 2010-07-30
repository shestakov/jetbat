using System.Data;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Abstract
{
	public interface IObjectFactory
	{
		DataTable LoadObjectListView(string objectNamespace, string objectName, AttributeValueSet parameters);
		DataTable LoadObjectListView(string objectNamespace, string objectName, AttributeValueSet parameters, out ObjectListViewDefinition objectListViewDefinition);
		ObjectInstance New<T>(string entityNamespace, string entityName) where T : InstancedObjectDefinition;
	}
}