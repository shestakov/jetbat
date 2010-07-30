using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Simple
{
	public class ObjectAttribute : INamedObject
	{
		[XmlAttribute]
		public string DataType { get; set; }

		[XmlAttribute]
		public short DateTimeFormatID { get; set; }

		public object DefaultValue { get; set; }
		public string FriendlyName { get; set; }

		[XmlAttribute]
		public bool IsExternal { get; set; }

		[XmlAttribute]
		public bool IsNullable { get; set; }

		[XmlAttribute]
		public bool IsPrimaryKeyMember { get; set; }

		[XmlAttribute]
		public bool IsReadOnly { get; set; }

		[XmlAttribute]
		public bool IsUserVisible { get; set; }

		[XmlAttribute]
		public int MaxLength { get; set; }

		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public int Precision { get; set; }

		[XmlAttribute]
		public int Scale { get; set; }

		[XmlAttribute]
		public string SqlDbType { get; set; }

		public string UILabel { get; set; }

		[XmlAttribute]
		public int UIPreferredWidth { get; set; }

		[XmlAttribute]
		public int UIPreferredIndex { get; set; }

		[XmlAttribute]
		public bool UIAllowMultilineText { get; set; }
	}
}