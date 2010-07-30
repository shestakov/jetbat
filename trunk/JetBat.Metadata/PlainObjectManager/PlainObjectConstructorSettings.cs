using System.IO;
using System.Xml.Serialization;

namespace JetBat.Metadata.PlainObjectManager
{
	public class PlainObjectConstructorSettings : BusinessObjectConstructorSettings
	{
		public string UIEditorName{ get; set; }
		public string TableName{ get; set; }
		public string ViewName{ get; set; }
		public string DateTimeMarkColumnName{ get; set; }
		public string DeleteFlagColumnName{ get; set; }
		public string StatusColumnName{ get; set; }
		public int StatusColumnInitialValue{ get; set; }
		public string ParentObjectNamespace{ get; set; }
		public string ParentObjectName{ get; set; }
		public string ForeignKeyToParentObjectName{ get; set; }
		public string SequenceNumberColumnName{ get; set; }
		public string[] InvisibleColumns{ get; set; }
		public string[] ReadOnlyColumns{ get; set; }
		public string[] IgnoredColumns{ get; set; }
		public bool MethodInsert{ get; set; }
		public bool MethodBeforeInsert{ get; set; }
		public bool MethodAfterInsert{ get; set; }
		public bool MethodUpdate{ get; set; }
		public bool MethodBeforeUpdate{ get; set; }
		public bool MethodAfterUpdate{ get; set; }
		public bool MethodLoad{ get; set; }
		public bool MethodDelete{ get; set; }
		public bool MethodBeforeDelete{ get; set; }
		public bool MethodAfterDelete{ get; set; }
		public bool MethodRestore{ get; set; }
		public bool MethodBeforeRestore{ get; set; }
		public bool MethodAfterRestore{ get; set; }
		public bool MethodCopyByParentObject{ get; set; }
		public bool MethodDeleteByParentObject{ get; set; }

		public static PlainObjectConstructorSettings Load(string fileName)
		{
			PlainObjectConstructorSettings constructorSettings;
			var serializer = new XmlSerializer(typeof(PlainObjectConstructorSettings));
			using (TextReader reader = new StreamReader(fileName))
				constructorSettings = (PlainObjectConstructorSettings) serializer.Deserialize(reader);
			return constructorSettings;
		}

		public static void Save(PlainObjectConstructorSettings settings, string fileName)
		{
			var serializer = new XmlSerializer(typeof(PlainObjectConstructorSettings));
			using (TextWriter writer = new StreamWriter(fileName))
				serializer.Serialize(writer, settings);
		}
	}
}