using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Simple
{
	public class ObjectMethod : INamedObject
	{
		public NamedObjectCollection<ObjectMethodParameter> ParameterDefinitions { get; set; }

		[XmlAttribute]
		public string FriendlyName { get; set; }

		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public bool ReturnsXmlErrorList { get; set; }

		public string StoredProcedureName { get; set; }
	}
}