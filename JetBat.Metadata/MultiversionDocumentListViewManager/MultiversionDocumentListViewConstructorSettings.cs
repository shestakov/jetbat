using System.IO;
using System.Xml.Serialization;

namespace JetBat.Metadata.MultiversionDocumentListViewManager
{
	public class MultiversionDocumentListViewConstructorSettings : BusinessObjectConstructorSettings
	{
		public string ViewName{ get; set; }
		public bool ShowDeletedObjects { get; set; }
		public string[] InvisibleColumns{ get; set; }
		public string[] IgnoredColumns{ get; set; }
		public string UICaption { get; set; }
		public string TargetObjectNamespace { get; set; }
		public string TargetObjectName { get; set; }

		public static MultiversionDocumentListViewConstructorSettings Load(string fileName)
		{
			MultiversionDocumentListViewConstructorSettings constructorSettings;
			var serializer = new XmlSerializer(typeof(MultiversionDocumentListViewConstructorSettings));
			using (TextReader reader = new StreamReader(fileName))
				constructorSettings = (MultiversionDocumentListViewConstructorSettings)serializer.Deserialize(reader);
			return constructorSettings;
		}

		public static void Save(MultiversionDocumentListViewConstructorSettings settings, string fileName)
		{
			var serializer = new XmlSerializer(typeof(MultiversionDocumentListViewConstructorSettings));
			using (TextWriter writer = new StreamWriter(fileName))
				serializer.Serialize(writer, settings);
		}
	}
}