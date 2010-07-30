using System.Collections.Generic;
using JetBat.Client.Metadata.Definitions;

namespace JetBat.Client.SqlServer.Concrete
{
	internal class IntermediateMethodDefinition
	{
		public readonly string FriendlyName;
		public readonly string Name;

		public readonly List<ObjectMethodParameterDefinition> ParameterDefinitionList =
			new List<ObjectMethodParameterDefinition>();

		public readonly bool ReturnsXmlErrorList;
		public readonly string StoredProcedureName;

		internal IntermediateMethodDefinition(string name, string friendlyName, string storedProcedureName,
		                                      bool returnsXmlErrorList)
		{
			Name = name;
			FriendlyName = friendlyName;
			StoredProcedureName = storedProcedureName;
			ReturnsXmlErrorList = returnsXmlErrorList;
		}
	}
}