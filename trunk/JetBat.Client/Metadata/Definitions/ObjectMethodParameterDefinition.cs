using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Definitions
{
	public class ObjectMethodParameterDefinition : INamedObject
	{
		#region Атрибуты

		public string AlternativeName { get; private set; }
		public string AttributeName { get; private set; }
		public SqlParameterDirection Direction { get; private set; }
		public int MaxLength { get; private set; }
		public string Name { get; private set; }
		public byte Precision { get; private set; }
		public byte Scale { get; private set; }
		public string SqlDbType { get; private set; }

		#endregion

		#region Инициализация

		public ObjectMethodParameterDefinition(Simple.ObjectMethodParameter parameter)
		{
			Name = parameter.Name;
			SqlDbType = parameter.SqlDbType;
			Direction = parameter.Direction;
			MaxLength = parameter.MaxLength;
			Precision = parameter.Precision;
			Scale = parameter.Scale;
			AttributeName = parameter.AttributeName;
			AlternativeName = parameter.AlternativeName;
		}

		#endregion

		#region Свойства

		public string ActualName
		{
			get { return AttributeName != string.Empty ? AttributeName : AlternativeName; }
		}

		#endregion
	}
}