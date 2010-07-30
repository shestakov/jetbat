using System.Collections.Generic;

namespace JetBat.DatabaseSchema.SqlServer
{
	internal class ViewDescriptor
	{
		public readonly List<ViewColumnDescriptor> Columns = new List<ViewColumnDescriptor>();
		public readonly string Descrtiption;
		public readonly string Name;

		public ViewDescriptor(string name, string descrtiption)
		{
			Name = name;
			Descrtiption = descrtiption;
		}
	}
}