using System.Collections.Generic;

namespace JetBat.DatabaseSchema
{
	public class TableSchema : INamedObject
	{
		public readonly NamedObjectReadOnlyCollection<TableColumnSchema> Columns;
		public readonly string Description;
		public readonly NamedObjectReadOnlyCollection<ForeignKeySchema> IncomingForeignKeys;
		private readonly string name;
		public readonly NamedObjectReadOnlyCollection<ForeignKeySchema> OutgoingForeignKeys;
		public readonly NamedObjectReadOnlyCollection<TableColumnSchema> PrimaryKey;

		public TableSchema(string name, string description, IList<TableColumnSchema> columns,
						   IEnumerable<ForeignKeySchema> foreignKeys)
		{
			this.name = name;
			Description = description;

			#region Creating column collection

			Columns = new NamedObjectReadOnlyCollection<TableColumnSchema>(columns);
			foreach (TableColumnSchema column in columns)
			{
				column.Table = this;
			}

			#endregion

			#region Creating primary key column collection

			var primaryKeyColumns = new List<TableColumnSchema>();
			foreach (TableColumnSchema column in columns)
			{
				if (column.IsPrimaryKeyMember)
					primaryKeyColumns.Add(column);
			}
			PrimaryKey = new NamedObjectReadOnlyCollection<TableColumnSchema>(primaryKeyColumns);

			#endregion

			#region Creatiog foreign key collection

			var incomingForeignKeys = new List<ForeignKeySchema>();
			foreach (ForeignKeySchema foreignKey in foreignKeys)
			{
				if (foreignKey.ParentTableName == this.name)
				{
					incomingForeignKeys.Add(foreignKey);
					foreignKey.ParentTable = this;
				}
			}
			IncomingForeignKeys = new NamedObjectReadOnlyCollection<ForeignKeySchema>(incomingForeignKeys);

			var outgoingForeignKeys = new List<ForeignKeySchema>();
			foreach (ForeignKeySchema foreignKey in foreignKeys)
			{
				if (foreignKey.ChildTableName == this.name)
				{
					outgoingForeignKeys.Add(foreignKey);
					foreignKey.ChildTable = this;
				}
			}
			OutgoingForeignKeys = new NamedObjectReadOnlyCollection<ForeignKeySchema>(outgoingForeignKeys);

			#endregion
		}

		public TableColumnSchema this[string columnName]
		{
			get { return Columns[columnName]; }
		}

		#region INamedObject Members

		public string Name
		{
			get { return name; }
		}

		#endregion
	}
}