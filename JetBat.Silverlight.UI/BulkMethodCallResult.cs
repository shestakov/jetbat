using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Silverlight.UI
{
	[XmlInclude(typeof(DBNull))]
	public class BulkMethodCallResult
	{
		public bool Success { get; set; }
		public int FailedMethodIndex { get; set; }
		public string Exception { get; set; }
		public List<ErrorMessage> FailedMethodCallErrors { get; set; }
		public List<ObjectMethodResult> MethodResults { get; set; }
	}
}