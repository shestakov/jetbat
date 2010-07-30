using System.Collections.Generic;

namespace JetBat.DatabaseSchema.SqlServer
{
	internal class StoredProcedureDescriptor
	{
		public readonly string Descrtiption;
		public readonly string Name;
		public readonly List<StoredProcedureParameterDescriptor> Parameters = new List<StoredProcedureParameterDescriptor>();

		public StoredProcedureDescriptor(string name, string descrtiption)
		{
			Name = name;
			Descrtiption = descrtiption;
		}
	}
}