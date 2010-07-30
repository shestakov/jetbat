using System;
using System.Collections.Generic;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Metadata.Definitions
{
	[Serializable]
	public class ObjectMethodDefinition : INamedObject
	{
		#region Атрибуты

		public string FriendlyName { get; private set; }
		public string Name { get; private set; }
		public bool ReturnsXmlErrorList { get; private set; }
		public string StoredProcedureName { get; private set; }
		public NamedObjectReadOnlyCollection<ObjectMethodParameterDefinition> ParameterDefinitions { get; private set; }

		#endregion

		#region Initialization

		public ObjectMethodDefinition(ObjectMethod method)
		{
			IList<ObjectMethodParameterDefinition> list = new List<ObjectMethodParameterDefinition>();
			foreach (ObjectMethodParameter parameter in method.ParameterDefinitions)
				list.Add(new ObjectMethodParameterDefinition(parameter));
			ParameterDefinitions = new NamedObjectReadOnlyCollection<ObjectMethodParameterDefinition>(list);
			Name = method.Name;
			FriendlyName = method.FriendlyName;
			StoredProcedureName = method.StoredProcedureName;
			ReturnsXmlErrorList = method.ReturnsXmlErrorList;
		}

		#endregion

		#region Misc

		public override string ToString()
		{
			return FriendlyName;
		}

		#endregion
	}
}