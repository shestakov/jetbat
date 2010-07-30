namespace JetBat.DatabaseSchema
{
	public class ForeignKeyColumnPairSchema
	{
		public readonly string ForeignKeyColumnName;
		public readonly string PrimaryKeyColumnName;

		public ForeignKeyColumnPairSchema(string primaryKeyColumnName, string foreignKeyColumnName)
		{
			PrimaryKeyColumnName = primaryKeyColumnName;
			ForeignKeyColumnName = foreignKeyColumnName;
		}
	}
}