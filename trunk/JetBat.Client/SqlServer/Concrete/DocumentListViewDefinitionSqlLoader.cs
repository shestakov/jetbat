using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.SqlServer.Concrete
{
	public class DocumentListViewDefinitionSqlLoader : IObjectDefinitionSqlLoader
	{
		private readonly string _connectionString;
		private readonly bool _enableCaching;
		private const int DocumentListViewObjectTypeID = 5;

		public DocumentListViewDefinitionSqlLoader(string connectionString, bool enableCaching)
		{
			_connectionString = connectionString;
			_enableCaching = enableCaching;
		}

		#region IObjectDefinitionSqlLoader Members

		public Type TargetObjectType
		{
			get { return typeof(DocumentListViewDefinition); }
		}

		public int TargetObjectID
		{
			get { return DocumentListViewObjectTypeID; }
		}

		public ObjectDefinition LoadImmutable(string objectNamespace, string objectName)
		{
			return new DocumentListViewDefinition((DocumentListView)Load(objectNamespace, objectName));
		}

		public BusinessObject Load(string objectNamespace, string objectName)
		{
			string cacheDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticHelper.MetadataCacheDirectoryName + @"\DocumentListView");
			string cacheFileName = string.Format(@"{0}\{1}.{2}.xml", cacheDirectory, objectNamespace, objectName);

			#region Load from cache

			if (_enableCaching)
			{
				if (File.Exists(cacheFileName))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(DocumentListView));
					using (FileStream stream = new FileStream(cacheFileName, FileMode.Open, FileAccess.Read))
						return (DocumentListView)serializer.Deserialize(stream);
				}
			}

			#endregion

			string uiListCaption;
			string basicObjectNamespace;
			string basicObjectName;
			string objectActionNameLoadList;
			string objectMethodNameLoadList;
			string namespaceName;
			string name;
			int objectType;
			string friendlyName;
			string description;
			NamedObjectCollection<ObjectAttribute> attributes;
			NamedObjectCollection<ObjectComplexAttribute> complexAttributes;
			NamedObjectCollection<ObjectAction> actions;
			NamedObjectCollection<ObjectMethod> methods;

			using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
			{
				sqlConnection.Open();
				SqlCommand command = new SqlCommand("MetadataEngine_LoadMultiversionDocumentListViewDescriptor", sqlConnection)
					{CommandType = CommandType.StoredProcedure};
				command.Parameters.AddWithValue("@Namespace", objectNamespace);
				command.Parameters.AddWithValue("@Name", objectName);

				SqlDataReader reader = command.ExecuteReader();
				if (reader == null) throw new NullReferenceException("DataReader is null while must not be.");
				using(reader)
				{
					reader.Read();

					uiListCaption = reader["UIListCaption"].ToString();
					basicObjectNamespace = Convert.ToString(reader["MultiversionDocumentNamespaceName"]);
					basicObjectName = Convert.ToString(reader["MultiversionDocumentName"]);
					objectActionNameLoadList = reader["ObjectActionNameLoadList"].Equals(DBNull.Value)
					                           	? ""
					                           	: Convert.ToString(reader["ObjectActionNameLoadList"]);
					objectMethodNameLoadList = reader["ObjectMethodNameLoadList"].Equals(DBNull.Value)
					                           	? ""
					                           	: Convert.ToString(reader["ObjectMethodNameLoadList"]);

					objectType = Convert.ToInt32(reader["ObjectTypeID"]);
					namespaceName = Convert.ToString(reader["NamespaceName"]);
					name = Convert.ToString(reader["Name"]);
					friendlyName = Convert.ToString(reader["FriendlyName"]);
					description = Convert.ToString(reader["Description"]);

					reader.NextResult();
					attributes = StaticHelper.getAttributesFormReader(reader);
					reader.NextResult();
					complexAttributes = StaticHelper.getComplexAttributesFromReader(reader);
					reader.NextResult();
					StaticHelper.getComplexAttributeAttributePairsFromReader(reader, complexAttributes);
					reader.NextResult();
					actions = StaticHelper.getActionsFormReader(reader);
					reader.NextResult();
					methods = StaticHelper.getMethodsFromReader(reader);
					reader.NextResult();
					StaticHelper.getMethodParametersFromReader(reader, methods);
				}
			}

			BusinessObject definition = new DocumentListView
				{
					UIListCaption = uiListCaption,
					BasicObjectNamespace = basicObjectNamespace,
					BasicObjectName = basicObjectName,
					ObjectActionNameLoadList = objectActionNameLoadList,
					ObjectMethodNameLoadList = objectMethodNameLoadList,
					ObjectNamespace = namespaceName,
					ObjectName = name,
					ObjectType = objectType,
					FriendlyName = friendlyName,
					Description = description,
					Attributes = attributes,
					ComplexAttributes = complexAttributes,
					Actions = actions,
					Methods = methods
				};

			#region Save to cache

			if (_enableCaching)
			{
				if (!Directory.Exists(cacheDirectory))
					Directory.CreateDirectory(cacheDirectory);
				XmlSerializer serializer = new XmlSerializer(typeof(DocumentListView));
				using (FileStream stream = new FileStream(cacheFileName, FileMode.OpenOrCreate, FileAccess.Write))
					serializer.Serialize(stream, definition);
			}

			#endregion

			return definition;
		}

		#endregion
	}
}