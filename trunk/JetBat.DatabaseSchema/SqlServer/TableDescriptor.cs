using System.Collections.Generic;

namespace JetBat.DatabaseSchema.SqlServer
{
	internal class TableDescriptor
	{
		public readonly List<TableColumnDescriptor> Columns = new List<TableColumnDescriptor>();
		public readonly string Descrtiption;
		public readonly string Name;

		public TableDescriptor(string name, string descrtiption)
		{
			Name = name;
			Descrtiption = descrtiption;
		}
	}
}