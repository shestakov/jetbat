namespace JetBat.DatabaseSchema.SqlServer
{
	internal class ForeignKeyColumnPairDescriptor
	{
		public readonly string ForeignKeyColumnName;
		public readonly string ForeignKeyName;
		public readonly string PrimaryKeyColumnName;

		public ForeignKeyColumnPairDescriptor(string foreignKeyName, string primaryKeyColumnName, string foreignKeyColumnName)
		{
			ForeignKeyName = foreignKeyName;
			PrimaryKeyColumnName = primaryKeyColumnName;
			ForeignKeyColumnName = foreignKeyColumnName;
		}
	}
}