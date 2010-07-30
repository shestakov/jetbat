namespace JetBat.DatabaseSchema.SqlServer
{
	internal class StoredProcedureParameterDescriptor
	{
		public readonly string DataTypeName;
		public readonly string Description;
		public readonly bool IsOutput;
		public readonly int MaxLength;
		public readonly string Name;
		public readonly int Precision;
		public readonly int Scale;
		public readonly string StoredProcedureName;

		public StoredProcedureParameterDescriptor(string storedProcedureName, string name, string dataTypeName, bool isOutput,
		                                          int maxLength, int precision, int scale, string description)
		{
			StoredProcedureName = storedProcedureName;
			Name = name;
			DataTypeName = dataTypeName;
			IsOutput = isOutput;
			MaxLength = maxLength;
			Precision = precision;
			Scale = scale;
			Description = description;
		}
	}
}