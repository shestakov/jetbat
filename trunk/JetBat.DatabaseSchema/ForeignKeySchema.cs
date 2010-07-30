using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JetBat.DatabaseSchema
{
	public class ForeignKeySchema : INamedObject
	{
		public readonly string ChildTableName;
		public readonly string Description;
		private readonly string name;
		public readonly string ParentTableName;
		public ReadOnlyCollection<ForeignKeyColumnPairSchema> ColumnPairs;

		public ForeignKeySchema(string name, string parentTableName, string childTableName, string description,
								IList<ForeignKeyColumnPairSchema> columnPairs)
		{
			//TODO: Добавить таки каждой колонке пары ссылку на реальный столбец таблицы!
			ColumnPairs = new ReadOnlyCollection<ForeignKeyColumnPairSchema>(columnPairs);
			this.name = name;
			ParentTableName = parentTableName;
			ChildTableName = childTableName;
			Description = description;
		}

		public TableSchema ParentTable { get; internal set; }

		public TableSchema ChildTable { get; internal set; }

		#region INamedObject Members

		public string Name
		{
			get { return name; }
		}

		#endregion
	}
}