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
	public class PlainObjectDefinitionSqlLoader : IObjectDefinitionSqlLoader
	{
		private const int PlainObjectTypeID = 1;
		private readonly string _connectionString;
		private readonly bool _enableCaching;

		public PlainObjectDefinitionSqlLoader(string connectionString, bool enableCaching)
		{
			_connectionString = connectionString;
			_enableCaching = enableCaching;
		}

		#region IObjectDefinitionSqlLoader Members

		public Type TargetObjectType
		{
			get { return typeof(PlainObjectDefinition); }
		}

		public int TargetObjectID
		{
			get { return PlainObjectTypeID; }
		}

		public ObjectDefinition LoadImmutable(string objectNamespace, string objectName)
		{
			return new PlainObjectDefinition((PlainObject)Load(objectNamespace, objectName));
		}

		public BusinessObject Load(string objectNamespace, string objectName)
		{
			#region Load from cache

			string cacheDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticHelper.MetadataCacheDirectoryName + @"\PlainObject");
			string cacheFileName = string.Format(@"{0}\{1}.{2}.xml", cacheDirectory, objectNamespace, objectName);

			if (_enableCaching)
			{
				if (File.Exists(cacheFileName))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(PlainObject));
					using (FileStream stream = new FileStream(cacheFileName, FileMode.Open, FileAccess.Read))
						return (PlainObject)serializer.Deserialize(stream);
				}
			}

			#endregion

			string namespaceName;
			string name;
			int objectType;
			string friendlyName;
			string description;
			string uiEditorName;
			NamedObjectCollection<ObjectAttribute> attributes;
			NamedObjectCollection<ObjectComplexAttribute> complexAttributes;
			NamedObjectCollection<ObjectAction> actions;
			NamedObjectCollection<ObjectMethod> methods;

			using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
			{
				sqlConnection.Open();
				SqlCommand command = new SqlCommand("MetadataEngine_LoadPlainObjectDescriptor", sqlConnection)
					{CommandType = CommandType.StoredProcedure};
				command.Parameters.AddWithValue("@Namespace", objectNamespace);
				command.Parameters.AddWithValue("@Name", objectName);

				SqlDataReader reader = command.ExecuteReader();
				if (reader == null) throw new NullReferenceException("DataReader is null while must not be.");

				using (reader)
				{
					reader.Read();

					objectType = Convert.ToInt32(reader["ObjectTypeID"]);
					namespaceName = Convert.ToString(reader["NamespaceName"]);
					name = Convert.ToString(reader["Name"]);
					friendlyName = Convert.ToString(reader["FriendlyName"]);
					description = Convert.ToString(reader["Description"]);
					uiEditorName = Convert.ToString(reader["UIEditorName"]);

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

			BusinessObject definition = new PlainObject
											{
												ObjectNamespace = namespaceName,
												ObjectName = name,
												ObjectType = objectType,
												FriendlyName = friendlyName,
												Description = description,
												UIEditorName = uiEditorName,
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
				XmlSerializer serializer = new XmlSerializer(typeof(PlainObject));
				using (FileStream stream = new FileStream(cacheFileName, FileMode.OpenOrCreate, FileAccess.Write))
					serializer.Serialize(stream, definition);
			}

			#endregion

			return definition;
		}

		#endregion
	}
}