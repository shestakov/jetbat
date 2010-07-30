using System;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Metadata.Abstract
{
	public interface IObjectDefinitionSqlLoader
	{
		Type TargetObjectType { get; }
		int TargetObjectID { get; }

		ObjectDefinition LoadImmutable(string objectNamespace, string objectName);
		BusinessObject Load(string objectNamespace, string objectName);
	}
}