using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.SqlServer.Common
{
	public class SqlMetadataProvider : IMetadataProvider
	{
		public bool EnableCaching { get; private set; }
		protected readonly List<IObjectDefinitionSqlLoader> loaderList = new List<IObjectDefinitionSqlLoader>();
		private readonly string _connectionString;

		public SqlMetadataProvider(string connectionString)
			: this(connectionString, false)
		{
		}

		public SqlMetadataProvider(string connectionString, bool enableCaching)
		{
			EnableCaching = enableCaching;
			if (connectionString == null)
				throw new ArgumentNullException("connectionString");
			_connectionString = connectionString;
		}

		public BusinessObject LoadBusinessObject(QualifiedName qualifiedName, int objectTypeID)
		{
			foreach (IObjectDefinitionSqlLoader loader in loaderList)
			{
				if (loader.TargetObjectID == objectTypeID)
					return loader.Load(qualifiedName.Namespace, qualifiedName.Name);
			}
			throw new Exception(
				string.Format("[{0}].[{1}] - загрузка для объекта данного типа не поддерживается",
							  qualifiedName.Namespace,
							  qualifiedName.Name));
		}

		#region IMetadataProvider Members

		public Dictionary<QualifiedName, ObjectDefinition> LoadObjectList(Dictionary<QualifiedName, int> nameAndTypeList)
		{
			Dictionary<QualifiedName, ObjectDefinition> result = new Dictionary<QualifiedName, ObjectDefinition>(nameAndTypeList.Count);

			foreach (QualifiedName qualifiedName in nameAndTypeList.Keys)
			{
				ObjectDefinition objectDefinition = LoadObjectDefinition(qualifiedName, nameAndTypeList[qualifiedName]);
				if (result.ContainsKey(qualifiedName))
					throw new Exception(
						string.Format("Сущность [{0}].[{1}] дублируется", qualifiedName.Namespace, qualifiedName.Name));
				if (objectDefinition != null)
					result.Add(new QualifiedName(objectDefinition.Namespace, objectDefinition.Name), objectDefinition);
			}

			return result;
		}

		public Dictionary<QualifiedName, int> LoadNameList()
		{
			SqlCommand command;
			Dictionary<QualifiedName, int> objectList;
			using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
			{
				sqlConnection.Open();
				command = new SqlCommand("MetadataEngine_LoadObjectList", sqlConnection) { CommandType = CommandType.StoredProcedure };
				objectList = new Dictionary<QualifiedName, int>();
				SqlDataReader reader = command.ExecuteReader();
				if (reader == null) throw new NullReferenceException("DataReader is null while must not be.");
				using (reader)
				{
					while (reader.Read())
					{
						objectList.Add(new QualifiedName(reader["NamespaceName"].ToString(), reader["Name"].ToString()),
								   Convert.ToInt32(reader["ObjectTypeID"]));
					}
				}
			}

			return objectList;
		}

		public int GetObjectType(string objectNamespace, string objectName)
		{
			int objectTypeID;
			using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
			{
				sqlConnection.Open();
				SqlCommand command = new SqlCommand("MetadataEngine_GetObjectTypeID", sqlConnection) { CommandType = CommandType.StoredProcedure };
				command.Parameters.AddWithValue("@Namespace", objectNamespace);
				command.Parameters.AddWithValue("@Name", objectName);
				command.Parameters.Add("@ObjectTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
				command.ExecuteNonQuery();
				objectTypeID = Convert.ToInt32(command.Parameters["@ObjectTypeID"].Value);
			}
			return objectTypeID;
		}

		#endregion

		private ObjectDefinition LoadObjectDefinition(QualifiedName qualifiedName, int objectTypeID)
		{
			foreach (IObjectDefinitionSqlLoader loader in loaderList)
			{
				if (loader.TargetObjectID == objectTypeID)
					return loader.LoadImmutable(qualifiedName.Namespace, qualifiedName.Name);
			}
			throw new Exception(
				string.Format("[{0}].[{1}] - загрузка для объекта данного типа не поддерживается",
							  qualifiedName.Namespace,
							  qualifiedName.Name));
		}
	}
}