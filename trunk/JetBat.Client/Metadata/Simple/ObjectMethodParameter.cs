using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Simple
{
	public class ObjectMethodParameter : INamedObject
	{
		[XmlAttribute]
		public string AlternativeName { get; set; }

		[XmlAttribute]
		public string AttributeName { get; set; }

		[XmlAttribute]
		public SqlParameterDirection Direction { get; set; }

		[XmlAttribute]
		public int MaxLength { get; set; }

		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public byte Precision { get; set; }

		[XmlAttribute]
		public byte Scale { get; set; }

		[XmlAttribute]
		public string SqlDbType { get; set; }
	}
}