using System.Xml.Serialization;

namespace JetBat.Client.Metadata.Misc
{
	public class NameValue : INamedObject
	{
		//[XmlAttribute("Name")]
		public string Name { get; set; }
		//[XmlText]
		public object Value { get; set; }
	}
}