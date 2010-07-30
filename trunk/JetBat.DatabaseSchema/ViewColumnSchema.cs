namespace JetBat.DatabaseSchema
{
	public class ViewColumnSchema : INamedObject
	{
		public readonly string DataTypeName;
		public readonly string Description;
		public readonly int MaxLength;
		private readonly string name;
		public readonly int Precision;
		public readonly int Scale;

		public ViewColumnSchema(string name, string dataTypeName, int maxLength, int precision, int scale, string description)
		{
			this.name = name;
			DataTypeName = dataTypeName;
			MaxLength = maxLength;
			Precision = precision;
			Scale = scale;
			Description = description;
		}

		public ViewSchema View { get; internal set; }

		#region INamedObject Members

		public string Name
		{
			get { return name; }
		}

		#endregion
	}
}