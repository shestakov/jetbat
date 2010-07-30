using System.Collections.Generic;
using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Simple
{
	public class ObjectComplexAttribute : INamedObject
	{
		[XmlAttribute]
		public int ID { get; set; }

		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public string FriendlyName { get; set; }

		public List<ComplexAttributeColumnPair> MemberColumns { get; set; }
		public string UILabel { get; set; }
		public int UIPreferredIndex { get; set; }
		public string Description { get; set; }
	}
}