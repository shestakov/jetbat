namespace JetBat.DatabaseSchema
{
	public class StoredProcedureParameterSchema : INamedObject
	{
		public readonly string DataTypeName;
		public readonly string Description;
		public readonly bool IsOutput;
		public readonly int MaxLength;
		private readonly string name;
		public readonly int Precision;
		public readonly int Scale;

		public StoredProcedureParameterSchema(string name, string dataTypeName, bool isOutput, int maxLength, int precision,
		                                      int scale, string description)
		{
			this.name = name;
			DataTypeName = dataTypeName;
			IsOutput = isOutput;
			MaxLength = maxLength;
			Precision = precision;
			Scale = scale;
			Description = description;
		}

		public StoredProcedureSchema StoredProcedure { get; internal set; }

		#region INamedObject Members

		public string Name
		{
			get { return name; }
		}

		#endregion
	}
}