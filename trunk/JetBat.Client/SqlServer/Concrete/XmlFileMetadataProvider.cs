using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.SqlServer.Concrete
{
	public class XmlFileMetadataProvider : IMetadataProvider
	{
		private enum BusinessObjectType { PlainObject = 1, PlainObjectListView = 2, Document = 3, StoredQuery = 4, DocumentListView = 5 }

		private readonly string metadataDirectory;

		public XmlFileMetadataProvider(string metadataDirectory)
		{
			this.metadataDirectory = metadataDirectory;
		}

		public BusinessObject LoadBusinessObject(QualifiedName qualifiedName, int objectTypeID)
		{
			string fileName;
			Type serializedType;
			if (objectTypeID == (int)BusinessObjectType.PlainObject)
			{
				fileName = Path.Combine(metadataDirectory, string.Format(@"PlainObject\{0}.{1}.xml", qualifiedName.Namespace, qualifiedName.Name));
				serializedType = typeof(PlainObject);
			}
			else if (objectTypeID == (int)BusinessObjectType.PlainObjectListView)
			{
				fileName = Path.Combine(metadataDirectory, string.Format(@"PlainObjectListView\{0}.{1}.xml", qualifiedName.Namespace, qualifiedName.Name));
				serializedType = typeof(PlainObjectListView);
			}
			else if (objectTypeID == (int)BusinessObjectType.Document)
			{
				fileName = Path.Combine(metadataDirectory, string.Format(@"Document\{0}.{1}.xml", qualifiedName.Namespace, qualifiedName.Name));
				serializedType = typeof(Document);
			}
			else if (objectTypeID == (int)BusinessObjectType.DocumentListView)
			{
				fileName = Path.Combine(metadataDirectory, string.Format(@"DocumentListView\{0}.{1}.xml", qualifiedName.Namespace, qualifiedName.Name));
				serializedType = typeof(DocumentListView);
			}
			else if (objectTypeID == (int)BusinessObjectType.StoredQuery)
			{
				fileName = Path.Combine(metadataDirectory, string.Format(@"StoredQuery\{0}.{1}.xml", qualifiedName.Namespace, qualifiedName.Name));
				serializedType = typeof(StoredQuery);
			}
			else
			{
				throw new Exception(string.Format("Object type {0} not supported", objectTypeID));
			}
			if (!File.Exists(fileName))
				throw new Exception(string.Format("Definition on [{0}] of type {1} not found", qualifiedName, objectTypeID));

			XmlSerializer serializer = new XmlSerializer(serializedType);
			using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
				return (BusinessObject)serializer.Deserialize(stream);
		}

		public Dictionary<QualifiedName, ObjectDefinition> LoadObjectList(Dictionary<QualifiedName, int> nameAndTypeList)
		{
			Dictionary<QualifiedName, ObjectDefinition> result = new Dictionary<QualifiedName, ObjectDefinition>(nameAndTypeList.Count);
			foreach (KeyValuePair<QualifiedName, int> keyValuePair in nameAndTypeList)
			{
				int objectTypeID = keyValuePair.Value;
				if (objectTypeID == (int)BusinessObjectType.PlainObject)
				{
					result.Add(keyValuePair.Key, new PlainObjectDefinition((PlainObject) LoadBusinessObject(keyValuePair.Key, keyValuePair.Value)));
				}
				else if (objectTypeID == (int)BusinessObjectType.PlainObjectListView)
				{
					result.Add(keyValuePair.Key, new PlainObjectListViewDefinition((PlainObjectListView)LoadBusinessObject(keyValuePair.Key, keyValuePair.Value)));
				}
				else if (objectTypeID == (int)BusinessObjectType.Document)
				{
					result.Add(keyValuePair.Key, new DocumentDefinition((Document)LoadBusinessObject(keyValuePair.Key, keyValuePair.Value)));
				}
				else if (objectTypeID == (int)BusinessObjectType.DocumentListView)
				{
					result.Add(keyValuePair.Key, new DocumentListViewDefinition((DocumentListView)LoadBusinessObject(keyValuePair.Key, keyValuePair.Value)));
				}
				else if (objectTypeID == (int)BusinessObjectType.StoredQuery)
				{
					result.Add(keyValuePair.Key, new StoredQueryDefinition((StoredQuery)LoadBusinessObject(keyValuePair.Key, keyValuePair.Value)));
				}
				else
				{
					throw new Exception(string.Format("Object type {0} not supported", objectTypeID));
				}
			}
			return result;
		}

		public Dictionary<QualifiedName, int> LoadNameList()
		{
			Dictionary<QualifiedName, int> result = new Dictionary<QualifiedName, int>();

			#region PlainObject

			string[] fileList = Directory.GetFiles(Path.Combine(metadataDirectory, "PlainObject"));
			foreach (string fileName in fileList)
			{
				if (Path.GetExtension(fileName) != ".xml") continue;
				QualifiedName qualifiedName = GetQualifiedName(fileName);
				result.Add(qualifiedName, (int)BusinessObjectType.PlainObject);
			}

			#endregion

			#region PlainObjectListView

			fileList = Directory.GetFiles(Path.Combine(metadataDirectory, "PlainObjectListView"));
			foreach (string fileName in fileList)
			{
				if (Path.GetExtension(fileName) != ".xml") continue;
				QualifiedName qualifiedName = GetQualifiedName(fileName);
				result.Add(qualifiedName, (int)BusinessObjectType.PlainObjectListView);
			}

			#endregion

			#region Document

			fileList = Directory.GetFiles(Path.Combine(metadataDirectory, "Document"));
			foreach (string fileName in fileList)
			{
				if (Path.GetExtension(fileName) != ".xml") continue;
				QualifiedName qualifiedName = GetQualifiedName(fileName);
				result.Add(qualifiedName, (int)BusinessObjectType.Document);
			}

			#endregion

			#region DocumentListView

			fileList = Directory.GetFiles(Path.Combine(metadataDirectory, "DocumentListView"));
			foreach (string fileName in fileList)
			{
				if (Path.GetExtension(fileName) != ".xml") continue;
				QualifiedName qualifiedName = GetQualifiedName(fileName);
				result.Add(qualifiedName, (int)BusinessObjectType.DocumentListView);
			}

			#endregion

			#region DocumentListView

			fileList = Directory.GetFiles(Path.Combine(metadataDirectory, "StoredQuery"));
			foreach (string fileName in fileList)
			{
				if (Path.GetExtension(fileName) != ".xml") continue;
				QualifiedName qualifiedName = GetQualifiedName(fileName);
				result.Add(qualifiedName, (int)BusinessObjectType.StoredQuery);
			}

			#endregion

			return result;
		}

		public int GetObjectType(string objectNamespace, string objectName)
		{
			throw new NotImplementedException();
		}

		#region Utility

		private static QualifiedName GetQualifiedName(string fileName)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
			string[] fileNameParts = fileNameWithoutExtension.Split('.');
			string objectName = fileNameParts[fileNameParts.Length - 1];
			string objectNamespace = fileNameWithoutExtension.Substring(0, fileNameWithoutExtension.Length - objectName.Length - 1);
			return new QualifiedName(objectNamespace, objectName);
		}

		#endregion
	}
}