using System.Collections.Generic;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Silverlight.UI
{
	public class BulkMethodCallArguments
	{
		public string ObjectNamespace { get; set; }
		public string ObjectName { get; set; }
		public string MethodName { get; set; }
		public List<NameValue> Parameters { get; set; }
	}
}