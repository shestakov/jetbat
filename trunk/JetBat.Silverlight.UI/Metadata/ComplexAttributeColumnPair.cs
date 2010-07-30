using System.Xml.Serialization;

namespace JetBat.Client.Metadata.Simple
{
	public class ComplexAttributeColumnPair
	{
		[XmlAttribute]
		public string ForeignKeyColumnName { get; set; }

		[XmlAttribute]
		public string PrimaryKeyColumnName { get; set; }
	}
}