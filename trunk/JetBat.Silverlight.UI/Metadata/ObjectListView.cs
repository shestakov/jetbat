using System.Xml.Serialization;

namespace JetBat.Client.Metadata.Simple
{
	public class ObjectListView : BusinessObject
	{
		[XmlAttribute]
		public string BasicObjectName { get; set; }

		[XmlAttribute]
		public string BasicObjectNamespace { get; set; }

		[XmlAttribute]
		public string ObjectActionNameLoadList { get; set; }

		[XmlAttribute]
		public string ObjectMethodNameLoadList { get; set; }

		[XmlAttribute]
		public string UIListCaption { get; set; }
	}
}