using System.Collections.Generic;

namespace JetBat.DatabaseSchema
{
	public class ViewSchema : INamedObject
	{
		public readonly string Description;
		private readonly string name;
		public NamedObjectReadOnlyCollection<ViewColumnSchema> Columns;

		public ViewSchema(string name, string description, IList<ViewColumnSchema> columns)
		{
			Columns = new NamedObjectReadOnlyCollection<ViewColumnSchema>(columns);
			foreach (ViewColumnSchema column in columns)
			{
				column.View = this;
			}
			this.name = name;
			Description = description;
		}

		#region INamedObject Members

		public string Name
		{
			get { return name; }
		}

		#endregion
	}
}