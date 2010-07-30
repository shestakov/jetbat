using System.Xml.Serialization;

namespace JetBat.Client.Metadata.Misc
{
	public class ErrorMessage
	{
		[XmlAttribute]
		public string AttributeName { get; set; }

		[XmlAttribute]
		public int Severity { get; set; }

		public string Text { get; set; }
	}
}