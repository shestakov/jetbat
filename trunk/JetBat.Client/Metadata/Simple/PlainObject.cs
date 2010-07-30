using System.Xml.Serialization;

namespace JetBat.Client.Metadata.Simple
{
	public class PlainObject : InstancedObject
	{
		[XmlAttribute]
		public string UIEditorName { get; set; }
	}
}