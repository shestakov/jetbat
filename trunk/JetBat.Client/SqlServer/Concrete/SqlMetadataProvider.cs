namespace JetBat.Client.SqlServer.Concrete
{
	public class SqlMetadataProvider : Common.SqlMetadataProvider
	{
		public SqlMetadataProvider(string connectionString) : this(connectionString, false)
		{
		}

		public SqlMetadataProvider(string connectionString, bool enableCaching)
			: base(connectionString, enableCaching)
		{
			loaderList.Add(new PlainObjectDefinitionSqlLoader(connectionString, enableCaching));
			loaderList.Add(new PlainObjectListViewDefinitionSqlLoader(connectionString, enableCaching));
			loaderList.Add(new DocumentDefinitionSqlLoader(connectionString, enableCaching));
			loaderList.Add(new DocumentListViewDefinitionSqlLoader(connectionString, enableCaching));
			loaderList.Add(new StoredQueryDefinitionSqlLoader(connectionString, enableCaching));
		}
	}
}