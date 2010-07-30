namespace JetBat.Metadata
{
	public abstract class BusinessObjectOnTableAndViewConstructor : BusinessObjectConstructor
	{
		protected DatabaseTable databaseTable;
		protected DatabaseView databaseView;

		protected BusinessObjectOnTableAndViewConstructor(MetadataContainer context) : base(context)
		{
		}

		protected DatabaseTableColumn getTableColumn(string columnName)
		{
			foreach (DatabaseTableColumn column in databaseTable.Columns)
			{
				if (column.Name == columnName) //TODO: Mark this as a data structure rule
				{
					return column;
				}
			}
			return null;
		}

		protected DatabaseViewColumn getViewColumn(string columnName)
		{
			foreach (var column in databaseView.Columns)
			{
				if (column.Name == columnName) //TODO: Mark this as a data structure rule
				{
					return column;
				}
			}
			return null;
		}

		protected abstract bool columnAllowsNull(string columnName);
	}
}