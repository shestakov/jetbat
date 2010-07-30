using System;

namespace JetBat.Metadata
{
	[Serializable]
	public class StoredProcedureParameterDefinition
	{
		public string Name { get; set; }
		public string DataTypeName { get; set; }
		public bool IsOutput { get; set; }
		public int MaxLength { get; set; }
		public int Precision { get; set; }
		public int Scale { get; set; }
		public string Description { get; set; }

		public StoredProcedureParameterDefinition()
		{
		}

		public StoredProcedureParameterDefinition(string name, string dataTypeName, bool isOutput, int maxLength, int precision, int scale, string description)
		{
			Name = name;
			DataTypeName = dataTypeName;
			IsOutput = isOutput;
			MaxLength = maxLength;
			Precision = precision;
			Scale = scale;
			Description = description;
		}

		public override string ToString()
		{
			return string.Format("[{0}] - {1} ({2}) ({3}, {4})", Name, DataTypeName, MaxLength, Precision, Scale);
		}
	}
}