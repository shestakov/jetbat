using System;
using System.Collections.Generic;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Silverlight.UI
{
	public class ExecuteMethodCompleteEventArgs : EventArgs
	{
		public string MethodName { get; set; }
		public string Exception { get; set; }
		public List<ErrorMessage> ErrorMessages { get; set; }
		public Exception CallException { get; set; }
		public NamedObjectCollection<NameValue> Parameters { get; set; }
		public NamedObjectCollection<NameValue> InvariantParameters { get; set; }
	}
}