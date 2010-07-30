namespace JetBat.DatabaseSchema.SqlServer
{
	internal class TableColumnDescriptor
	{
		public readonly bool AllowNull;
		public readonly string DataTypeName;
		public readonly string Description;
		public readonly bool IsForeignKeyMember;
		public readonly bool IsIdentity;
		public readonly bool IsPrimaryKeyMember;
		public readonly int MaxLength;
		public readonly string Name;
		public readonly int Precision;
		public readonly int Scale;
		public readonly string TableName;

		public TableColumnDescriptor(string tableName, string name, string dataTypeName, bool allowNull,
		                             bool isPrimaryKeyMember, bool isForeignKeyMember, bool isIdentity, int maxLength,
		                             int precision, int scale, string description)
		{
			TableName = tableName;
			Name = name;
			DataTypeName = dataTypeName;
			AllowNull = allowNull;
			IsPrimaryKeyMember = isPrimaryKeyMember;
			IsForeignKeyMember = isForeignKeyMember;
			IsIdentity = isIdentity;
			MaxLength = maxLength;
			Precision = precision;
			Scale = scale;
			Description = description;
		}
	}
}