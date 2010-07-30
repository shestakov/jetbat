namespace JetBat.DatabaseSchema.SqlServer
{
	internal class ViewColumnDescriptor
	{
		public readonly string DataTypeName;
		public readonly string Description;
		public readonly int MaxLength;
		public readonly string Name;
		public readonly int Precision;
		public readonly int Scale;
		public readonly string ViewName;

		public ViewColumnDescriptor(string viewName, string name, string dataTypeName, int maxLength, int precision, int scale,
		                            string description)
		{
			ViewName = viewName;
			Name = name;
			DataTypeName = dataTypeName;
			MaxLength = maxLength;
			Precision = precision;
			Scale = scale;
			Description = description;
		}
	}
}