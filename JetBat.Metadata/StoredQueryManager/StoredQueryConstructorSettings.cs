using System.IO;
using System.Xml.Serialization;

namespace JetBat.Metadata.StoredQueryManager
{
	public class StoredQueryConstructorSettings : BusinessObjectConstructorSettings
	{
		public string UIListCaption { get; set; }
		public bool PredefinedAttributes { get; set; }
		public string StoredProcedureName { get; set; }

		public static StoredQueryConstructorSettings Load(string fileName)
		{
			StoredQueryConstructorSettings constructorSettings;
			var serializer = new XmlSerializer(typeof(StoredQueryConstructorSettings));
			using (TextReader reader = new StreamReader(fileName))
				constructorSettings = (StoredQueryConstructorSettings)serializer.Deserialize(reader);
			return constructorSettings;
		}

		public static void Save(StoredQueryConstructorSettings settings, string fileName)
		{
			var serializer = new XmlSerializer(typeof(StoredQueryConstructorSettings));
			using (TextWriter writer = new StreamWriter(fileName))
				serializer.Serialize(writer, settings);
		}
	}
}