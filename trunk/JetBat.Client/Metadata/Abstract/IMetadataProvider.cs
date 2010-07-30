using System.Collections.Generic;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Metadata.Abstract
{
	public interface IMetadataProvider
	{
		BusinessObject LoadBusinessObject(QualifiedName qualifiedName, int objectTypeID);
		Dictionary<QualifiedName, ObjectDefinition> LoadObjectList(Dictionary<QualifiedName, int> nameAndTypeList);
		Dictionary<QualifiedName, int> LoadNameList();
		int GetObjectType(string objectNamespace, string objectName);
	}
}