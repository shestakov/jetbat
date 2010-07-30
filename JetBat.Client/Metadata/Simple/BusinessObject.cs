using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Simple
{
	public class BusinessObject
	{
		[XmlAttribute]
		public int ObjectType { get; set; }

		[XmlAttribute]
		public string ObjectNamespace { get; set; }

		[XmlAttribute]
		public string ObjectName { get; set; }

		[XmlAttribute]
		public string FriendlyName { get; set; }

		public NamedObjectCollection<ObjectAttribute> Attributes { get; set; }
		public NamedObjectCollection<ObjectComplexAttribute> ComplexAttributes { get; set; }
		public NamedObjectCollection<ObjectMethod> Methods { get; set; }
		public NamedObjectCollection<ObjectAction> Actions { get; set; }
		public string Description { get; set; }
	}
}