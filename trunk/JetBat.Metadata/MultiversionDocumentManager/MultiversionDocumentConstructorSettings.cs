using System.IO;
using System.Xml.Serialization;

namespace JetBat.Metadata.MultiversionDocumentManager
{
	public class MultiversionDocumentConstructorSettings : BusinessObjectConstructorSettings
	{
		public string TableName{ get; set; }
		public string ViewName{ get; set; }
		public string[] InvisibleColumns{ get; set; }
		public string[] ReadOnlyColumns{ get; set; }
		public string[] IgnoredColumns{ get; set; }
		public string[] HeaderNullableColumns { get; set; }
		
		public bool MethodAfterCreate{ get; set; }
		public bool MethodBeforeUpdateVersion{ get; set; }
		public bool MethodBeforeConfirmEdit{ get; set; }
		public bool MethodAfterConfirmEdit{ get; set; }
		public bool MethodBeforeCommit { get; set; }
		public bool MethodBeforeRollback { get; set; }

		public static MultiversionDocumentConstructorSettings Load(string fileName)
		{
			MultiversionDocumentConstructorSettings constructorSettings;
			var serializer = new XmlSerializer(typeof(MultiversionDocumentConstructorSettings));
			using (TextReader reader = new StreamReader(fileName))
				constructorSettings = (MultiversionDocumentConstructorSettings)serializer.Deserialize(reader);
			return constructorSettings;
		}

		public static void Save(MultiversionDocumentConstructorSettings settings, string fileName)
		{
			var serializer = new XmlSerializer(typeof(MultiversionDocumentConstructorSettings));
			using (TextWriter writer = new StreamWriter(fileName))
				serializer.Serialize(writer, settings);
		}
	}
}