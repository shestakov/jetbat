using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;
using JetBat.Client.SqlServer.Common;
using log4net;
using log4net.Config;

namespace JetBat.BusinessLogic
{
	public class BusinessLogicProvider
	{
		public BusinessLogicProvider(IMetadataProvider metadataProvider, MetadataStore metadataStore, string connectionString)
			: this(metadataProvider, metadataStore, null, connectionString)
		{
		}

		public BusinessLogicProvider(IMetadataProvider metadataProvider, MetadataStore metadataStore,
									 IPermissionProvider permissionProvider, string connectionString)
		{
			XmlConfigurator.Configure();
			Log.Info("Web service started...");
			try
			{
				connectionStringDatabase = connectionString;
				this.metadataProvider = metadataProvider;
				this.metadataStore = metadataStore;
				this.permissionProvider = permissionProvider;
			}
			catch (Exception ex)
			{
				Log.Fatal(ex.ToString());
				throw;
			}
		}

		private string authenticationRequiredResponse;
		public string AuthenticationRequiredResponse
		{
			get
			{
				if(authenticationRequiredResponse == null)
				{
					ObjectMethodResult objectMethodResult = new ObjectMethodResult { AuthenticationRequired = true };
					var xmlSerializer = new XmlSerializer(typeof(ObjectMethodResult));
					var stringBuilder = new StringBuilder();
					using(XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
						xmlSerializer.Serialize(xmlWriter, objectMethodResult);
					authenticationRequiredResponse = stringBuilder.ToString();
				}
				return authenticationRequiredResponse;
			}
		}

		#region Getting metadata

		public string GetObjectDefinition(string objectType, string objectNamespace, string objectName)
		{
			Log.Info(string.Format("Object definition request. Type: {0}, Name: {1}.{2}", objectType, objectNamespace, objectName));
			if (objectType.ToLower() == "PlainObject".ToLower())
				return getPlainObjectDefinition(objectNamespace, objectName);
			if (objectType.ToLower() == "PlainObjectListView".ToLower())
				return getPlainObjectListViewDefinition(objectNamespace, objectName);
			if (objectType.ToLower() == "Document".ToLower())
				return getDocumentDefinition(objectNamespace, objectName);
			if (objectType.ToLower() == "DocumentListView".ToLower())
				return getDocumentListViewDefinition(objectNamespace, objectName);
			
			Log.Error(string.Format("Unknown business object type requested: {0}", objectType));
			throw new Exception("Unknown business object type requested");
		}

		private string getPlainObjectDefinition(string objectNamespace, string objectName)
		{
			try
			{
				BusinessObject businessObject;
				QualifiedName qualifiedName = new QualifiedName(objectNamespace, objectName);
				if (BusinessObjectCache.ContainsKey(qualifiedName))
					businessObject = BusinessObjectCache[qualifiedName];
				else
				{
					businessObject = metadataProvider.LoadBusinessObject(qualifiedName, 1);
					lock (BusinessObjectCache)
					{
						BusinessObjectCache[qualifiedName] = businessObject;
					}
				}
				XmlSerializer serializer = new XmlSerializer(typeof(PlainObject));

				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
					serializer.Serialize(xmlWriter, businessObject);
				return stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}
		}

		private string getDocumentDefinition(string objectNamespace, string objectName)
		{
			try
			{
				BusinessObject businessObject;
				QualifiedName qualifiedName = new QualifiedName(objectNamespace, objectName);
				if (BusinessObjectCache.ContainsKey(qualifiedName))
					businessObject = BusinessObjectCache[qualifiedName];
				else
				{
					businessObject = metadataProvider.LoadBusinessObject(qualifiedName, 3);
					lock (BusinessObjectCache)
					{
						BusinessObjectCache[qualifiedName] = businessObject;
					}
				}
				XmlSerializer serializer = new XmlSerializer(typeof(Document));

				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
					serializer.Serialize(xmlWriter, businessObject);
				return stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}
		}

		private string getPlainObjectListViewDefinition(string objectNamespace, string objectName)
		{
			try
			{
				BusinessObject businessObject;
				QualifiedName qualifiedName = new QualifiedName(objectNamespace, objectName);
				if (BusinessObjectCache.ContainsKey(qualifiedName))
					businessObject = BusinessObjectCache[qualifiedName];
				else
				{
					businessObject = metadataProvider.LoadBusinessObject(qualifiedName, 2);
					lock (BusinessObjectCache)
					{
						BusinessObjectCache[qualifiedName] = businessObject;
					}
				}
				XmlSerializer serializer = new XmlSerializer(typeof(PlainObjectListView));

				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
					serializer.Serialize(xmlWriter, businessObject);
				return stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}
		}

		private string getDocumentListViewDefinition(string objectNamespace, string objectName)
		{
			try
			{
				BusinessObject businessObject;
				QualifiedName qualifiedName = new QualifiedName(objectNamespace, objectName);
				if (BusinessObjectCache.ContainsKey(qualifiedName))
					businessObject = BusinessObjectCache[qualifiedName];
				else
				{
					businessObject = metadataProvider.LoadBusinessObject(qualifiedName, 5);
					lock (BusinessObjectCache)
					{
						BusinessObjectCache[qualifiedName] = businessObject;
					}
				}
				XmlSerializer serializer = new XmlSerializer(typeof(DocumentListView));

				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
					serializer.Serialize(xmlWriter, businessObject);
				return stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}
		}

		#endregion

		public string ExecuteMethod(string objectNamespace, string objectName, string methodName, string parametersSerialized)
		{
			return ExecuteMethod(null, objectNamespace, objectName, methodName, parametersSerialized);
		}

		public string ExecuteMethod(string userName, string objectNamespace, string objectName, string methodName,
									string parametersSerialized)
		{
			Log.Info(string.Format("Object methos call. Object name: {0}.{1}; Method name: {2}", objectNamespace, objectName, methodName));
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<NameValue>));
			List<NameValue> parameterValues;
			using (StringReader reader = new StringReader(parametersSerialized))
			{
				parameterValues = (List<NameValue>)xmlSerializer.Deserialize(reader);
			}

			DataTable[] tableRecordSet;

			AttributeValueSet parameterValueSet = new AttributeValueSet();
			foreach (NameValue attributeValue in parameterValues)
				parameterValueSet.Add(attributeValue.Name, attributeValue.Value ?? DBNull.Value);

			parameterValueSet[SystemUserNameParameterName] = userName;

			ErrorMessageCollection errorMessageCollection;
			StringBuilder stringBuilder;
			ObjectMethodResult objectMethodResult;

			try
			{
				if (permissionProvider != null)
					permissionProvider.CheckPremission(userName, objectNamespace, objectName, methodName);

				SqlAccessProvider sqlAccessProvider;
				using (SqlConnection sqlConnection = new SqlConnection(connectionStringDatabase))
				{
					sqlConnection.Open();
					sqlAccessProvider = new SqlAccessProvider(metadataStore, sqlConnection);
					errorMessageCollection = sqlAccessProvider.ExecuteProcedure(objectNamespace, objectName, methodName,
																				parameterValueSet, out tableRecordSet, null);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				objectMethodResult = new ObjectMethodResult
					{
						Exception = ex.ToString()
					};
				xmlSerializer = new XmlSerializer(typeof(ObjectMethodResult));
				stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
					xmlSerializer.Serialize(xmlWriter, objectMethodResult);
				return stringBuilder.ToString();
			}

			NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
			foreach (DictionaryEntry parameterValue in parameterValueSet)
				parameters.Add(new NameValue
					{
						Name = (string)parameterValue.Key,
						Value = parameterValue.Value != DBNull.Value ? parameterValue.Value : null
					});

			List<List<NameValue>> recordSet = null;
			if (tableRecordSet != null && tableRecordSet.Length > 0)
			{
				DataTable dataTable = tableRecordSet[0];
				recordSet = new List<List<NameValue>>(tableRecordSet.Length);
				foreach (DataRow row in dataTable.Rows)
				{
					List<NameValue> columnValues = new List<NameValue>(dataTable.Columns.Count);
					foreach (DataColumn column in dataTable.Columns)
					{
						columnValues.Add(new NameValue { Name = column.ColumnName, Value = row[column.ColumnName] });
					}
					recordSet.Add(columnValues);
				}
			}

			objectMethodResult = new ObjectMethodResult
				{
					ErrorMessages = errorMessageCollection,
					Parameters = parameters,
					Recordset = recordSet
				};

			try
			{
				xmlSerializer = new XmlSerializer(typeof(ObjectMethodResult));
				stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
					xmlSerializer.Serialize(xmlWriter, objectMethodResult);
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}
			return stringBuilder.ToString();
		}

		public string BulkExecute(string methodCallArgumentsListSerialized)
		{
			return BulkExecute(null, methodCallArgumentsListSerialized);
		}

		public string BulkExecute(string userName, string methodCallArgumentsListSerialized)
		{
			try
			{
				BulkMethodCallResult result = new BulkMethodCallResult();
				result.MethodResults = new List<ObjectMethodResult>();
				result.Success = true;
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<BulkMethodCallArguments>));
				List<BulkMethodCallArguments> methodCallArgumentsList;
				using (StringReader reader = new StringReader(methodCallArgumentsListSerialized))
				{
					methodCallArgumentsList = (List<BulkMethodCallArguments>)xmlSerializer.Deserialize(reader);
				}

				using (SqlConnection sqlConnection = new SqlConnection(connectionStringDatabase))
				{
					sqlConnection.Open();
					SqlTransaction transaction = sqlConnection.BeginTransaction();
					SqlAccessProvider sqlAccessProvider = new SqlAccessProvider(metadataStore, sqlConnection);
					foreach (BulkMethodCallArguments callArguments in methodCallArgumentsList)
					{
						AttributeValueSet parameterValueSet = new AttributeValueSet();
						foreach (NameValue attributeValue in callArguments.Parameters)
							parameterValueSet.Add(attributeValue.Name, attributeValue.Value ?? DBNull.Value);

						if (parameterValueSet.ContainsKey(SystemUserNameParameterName))
							parameterValueSet[SystemUserNameParameterName] = userName;

						ErrorMessageCollection errorMessageCollection;

						DataTable[] tableRecordSet;
						try
						{
							if (permissionProvider != null)
								permissionProvider.CheckPremission(userName, callArguments.ObjectNamespace, callArguments.ObjectName,
																	callArguments.MethodName);
							errorMessageCollection = sqlAccessProvider.ExecuteProcedure(callArguments.ObjectNamespace,
																						callArguments.ObjectName, callArguments.MethodName,
																						parameterValueSet, out tableRecordSet, transaction);
						}
						catch (Exception ex)
						{
							result.Exception = ex.ToString();
							result.FailedMethodIndex = methodCallArgumentsList.IndexOf(callArguments);
							result.Success = false;
							break;
						}

						ObjectMethodResult methodResult = new ObjectMethodResult();

						NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
						foreach (DictionaryEntry parameterValue in parameterValueSet)
							parameters.Add(new NameValue
								{
									Name = (string)parameterValue.Key,
									Value = parameterValue.Value != DBNull.Value ? parameterValue.Value : null
								});
						methodResult.Parameters = parameters;

						List<List<NameValue>> recordSet = null;
						if (tableRecordSet != null && tableRecordSet.Length > 0)
						{
							DataTable dataTable = tableRecordSet[0];
							recordSet = new List<List<NameValue>>(tableRecordSet.Length);
							foreach (DataRow row in dataTable.Rows)
							{
								List<NameValue> columnValues = new List<NameValue>(dataTable.Columns.Count);
								foreach (DataColumn column in dataTable.Columns)
								{
									columnValues.Add(new NameValue { Name = column.ColumnName, Value = row[column.ColumnName] });
								}
								recordSet.Add(columnValues);
							}
						}
						methodResult.Recordset = recordSet;

						methodResult.ErrorMessages = errorMessageCollection;

						result.MethodResults.Add(methodResult);

						bool errorsExist = false;
						foreach (ErrorMessage errorMessage in errorMessageCollection)
						{
							if (errorMessage.Severity > 1)
							{
								errorsExist = true;
								break;
							}
						}
						if (errorsExist)
						{
							result.Exception = null;
							result.FailedMethodIndex = methodCallArgumentsList.IndexOf(callArguments);
							result.Success = false;
							result.FailedMethodCallErrors = errorMessageCollection;
							break;
						}
					}
					if (result.Success)
						transaction.Commit();
					else
						transaction.Rollback();
				}

				StringBuilder stringBuilder;
				try
				{
					xmlSerializer = new XmlSerializer(typeof(BulkMethodCallResult));
					stringBuilder = new StringBuilder();
					using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
						xmlSerializer.Serialize(xmlWriter, result);
				}
				catch (Exception ex)
				{
					Log.Error(ex);
					throw;
				}
				return stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				Log.Error(ex.ToString());
				throw;
			}
		}

		private static readonly ILog Log = LogManager.GetLogger(typeof(BusinessLogicProvider));
		private readonly string connectionStringDatabase;
		private readonly IMetadataProvider metadataProvider;
		private readonly MetadataStore metadataStore;
		private readonly IPermissionProvider permissionProvider;

		private static readonly Dictionary<QualifiedName, BusinessObject> BusinessObjectCache =
			new Dictionary<QualifiedName, BusinessObject>();

		private const string SystemUserNameParameterName = "_UserName";
	}

	public class BulkMethodCallArguments
	{
		public string ObjectNamespace { get; set; }
		public string ObjectName { get; set; }
		public string MethodName { get; set; }
		public List<NameValue> Parameters { get; set; }
	}

	public class BulkMethodCallResult
	{
		public bool Success { get; set; }
		public int FailedMethodIndex { get; set; }
		public string Exception { get; set; }
		public List<ErrorMessage> FailedMethodCallErrors { get; set; }
		public List<ObjectMethodResult> MethodResults { get; set; }
	}
}