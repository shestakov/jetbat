using System.Collections.Generic;

namespace JetBat.DatabaseSchema
{
	public class DatabaseSchema : INamedObject
	{
		private readonly string name;
		public NamedObjectReadOnlyCollection<ForeignKeySchema> ForeignKeys;
		public NamedObjectReadOnlyCollection<StoredProcedureSchema> StoredProcedures;
		public NamedObjectReadOnlyCollection<TableSchema> Tables;
		public NamedObjectReadOnlyCollection<ViewSchema> Views;

		public DatabaseSchema(string name, IList<TableSchema> tables, IList<ViewSchema> views,
							  IList<StoredProcedureSchema> storedProcedures, IList<ForeignKeySchema> foreignKeys)
		{
			this.name = name;
			Tables = new NamedObjectReadOnlyCollection<TableSchema>(tables);
			Views = new NamedObjectReadOnlyCollection<ViewSchema>(views);
			StoredProcedures = new NamedObjectReadOnlyCollection<StoredProcedureSchema>(storedProcedures);
			ForeignKeys = new NamedObjectReadOnlyCollection<ForeignKeySchema>(foreignKeys);
		}

		#region INamedObject Members

		public string Name
		{
			get { return name; }
		}

		#endregion
	}
}