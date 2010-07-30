using System.Collections.Generic;
using System.Xml.Serialization;

namespace JetBat.Client.Metadata.Misc
{
	[XmlInclude(typeof(System.DBNull))]
	public class ObjectMethodResult
	{
		public bool AuthenticationRequired { get; set; }
		public string Exception { get; set; }
		public List<ErrorMessage> ErrorMessages { get; set; }
		public NamedObjectCollection<NameValue> Parameters { get; set; }
		public List<List<NameValue>> Recordset { get; set; }
	}
}