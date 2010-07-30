using System.IO;
using System.Xml.Serialization;

namespace JetBat.Metadata.PlainObjectListViewManager
{
	public class PlainObjectListViewConstructorSettings : BusinessObjectConstructorSettings
	{
		public string ViewName{ get; set; }
		public bool ShowDeletedObjects { get; set; }
		public string[] InvisibleColumns{ get; set; }
		public string[] IgnoredColumns{ get; set; }
		public string UICaption { get; set; }
		public string TargetObjectNamespace { get; set; }
		public string TargetObjectName { get; set; }
		public string SelectionCondition { get; set; }
		public string OrderBy { get; set; }
		public StoredProcedureParameterDefinition[] ParameterDefinitions { get; set; }

		public static PlainObjectListViewConstructorSettings Load(string fileName)
		{
			PlainObjectListViewConstructorSettings constructorSettings;
			var serializer = new XmlSerializer(typeof(PlainObjectListViewConstructorSettings));
			using (TextReader reader = new StreamReader(fileName))
				constructorSettings = (PlainObjectListViewConstructorSettings)serializer.Deserialize(reader);
			return constructorSettings;
		}

		public static void Save(PlainObjectListViewConstructorSettings settings, string fileName)
		{
			var serializer = new XmlSerializer(typeof(PlainObjectListViewConstructorSettings));
			using (TextWriter writer = new StreamWriter(fileName))
				serializer.Serialize(writer, settings);
		}
	}
}