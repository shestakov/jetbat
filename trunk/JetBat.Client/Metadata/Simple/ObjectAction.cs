using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Simple
{
	public class ObjectAction : INamedObject
	{
		public string Description { get; set; }

		[XmlAttribute]
		public bool Enabled { get; set; }

		[XmlAttribute]
		public string FriendlyName { get; set; }

		[XmlAttribute]
		public string Name { get; set; }

		public string UIBriefText { get; set; }
		public string UIFullText { get; set; }
	}
}