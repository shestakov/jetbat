namespace JetBat.DatabaseSchema
{
	public class TableColumnSchema : INamedObject
	{
		public readonly bool AllowNull;
		public readonly string DataTypeName;
		public readonly string Description;
		public readonly bool IsForeignKeyMember;
		public readonly bool IsIdentity;
		public readonly bool IsPrimaryKeyMember;
		public readonly int MaxLength;
		private readonly string name;
		public readonly int Precision;
		public readonly int Scale;

		public TableColumnSchema(string name, string dataTypeName, bool allowNull, bool isPrimaryKeyMember,
								 bool isForeignKeyMember, bool isIdentity, int maxLength, int precision, int scale,
								 string description)
		{
			this.name = name;
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

		public TableSchema Table { get; internal set; }

		#region INamedObject Members

		public string Name
		{
			get { return name; }
		}

		#endregion
	}
}