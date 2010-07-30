using System.Collections.Generic;
using System.Text;
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

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(string.Format("Exception: [{0}]", Exception));
			builder.AppendLine();

			builder.AppendLine("Error messages:");
			if (ErrorMessages != null)
				foreach (ErrorMessage message in ErrorMessages)
					builder.AppendLine(string.Format("Error message: [{0}]", message.Text));
			builder.AppendLine();

			builder.AppendLine("Parameters:");
			if (Parameters != null)
				foreach (NameValue parameter in Parameters)
					builder.AppendLine(string.Format("[{0}] = [{1}]", parameter.Name, parameter.Value));
			builder.AppendLine();

			builder.AppendLine("RecordSet:");
			if (Recordset != null)
				foreach (List<NameValue> record in Recordset)
				{
					builder.AppendLine("Record:");
					foreach (NameValue nameValue in record)
					{
						builder.AppendLine(string.Format("	[{0}] = [{1}]", nameValue.Name, nameValue.Value));
					}
					builder.AppendLine();
				}
			return builder.ToString();
		}
	}
}