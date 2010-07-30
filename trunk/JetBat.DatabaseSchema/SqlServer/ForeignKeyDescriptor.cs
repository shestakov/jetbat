using System.Collections.Generic;

namespace JetBat.DatabaseSchema.SqlServer
{
	internal class ForeignKeyDescriptor
	{
		public readonly List<ForeignKeyColumnPairDescriptor> ColumnPairs = new List<ForeignKeyColumnPairDescriptor>();
		public readonly string Descrtiption;
		public readonly string ForeignTableName;
		public readonly string Name;
		public readonly string PrimaryTableName;

		public ForeignKeyDescriptor(string name, string primaryTableName, string foreignTableName, string descrtiption)
		{
			Name = name;
			PrimaryTableName = primaryTableName;
			ForeignTableName = foreignTableName;
			Descrtiption = descrtiption;
		}
	}
}