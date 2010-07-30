// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable UseObjectOrCollectionInitializer
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.SqlServer.Common
{
	public class SqlAccessProvider : IAccessProvider
	{
		private readonly MetadataStore metadataStore;
		private readonly SqlConnection sqlConnection;

		#region IAccessProvider Members

		public ErrorMessageCollection ExecuteProcedure(string objectNamespace, string objectName, string methodName,
													   AttributeValueSet parameterValues, out DataTable[] recordSets,
													   DbTransaction transaction)
		{
			ObjectDefinition objectDefinition = metadataStore.GetObjectDefinition(objectNamespace, objectName);
			if (objectDefinition == null)
			{
				throw new Exception(string.Format("Object [{0}].[{1}] does not exist", objectNamespace, objectName));
			}
			ObjectMethodDefinition methodDefinition = objectDefinition.Methods[methodName];
			if (methodDefinition == null)
			{
				throw new Exception(string.Format("Object [{0}].[{1}] does not have a method named [{2}]", objectNamespace, objectName, methodName));
			}

			SqlCommand command = new SqlCommand(methodDefinition.StoredProcedureName, sqlConnection);
			command.CommandType = CommandType.StoredProcedure;

			foreach (ObjectMethodParameterDefinition parameter in methodDefinition.ParameterDefinitions)
			{
				SqlParameter param = createSqlParameter(command, parameter);
				if (parameter.Direction == SqlParameterDirection.Input ||
					parameter.Direction == SqlParameterDirection.InputOutput)
				{
					if (parameterValues.Contains(parameter.AlternativeName))
						param.Value = parameterValues[parameter.AlternativeName];
				}
				if (methodDefinition.ReturnsXmlErrorList && parameter.Name == "@ErrorMessages")
				{
					param.Size = -1;
					param.Direction = ParameterDirection.InputOutput;
					param.Value = DBNull.Value;
				}
			}

			command.Transaction = (SqlTransaction)transaction;
			SqlDataReader reader = command.ExecuteReader();

			try
			{
				recordSets = reader.HasRows ? getResultSets(reader) : null;
			}
			finally
			{
				reader.Close();
			}

			foreach (ObjectMethodParameterDefinition parameter in methodDefinition.ParameterDefinitions)
			{
				if (parameter.Direction == SqlParameterDirection.Output ||
					parameter.Direction == SqlParameterDirection.InputOutput)
				{
					if (parameterValues.Contains(parameter.AlternativeName))
						parameterValues[parameter.AlternativeName] = command.Parameters[parameter.Name].Value;
				}
			}

			if (methodDefinition.ReturnsXmlErrorList)
				return buildErrorMessageCollection((SqlXml)command.Parameters["@ErrorMessages"].SqlValue);

			return new ErrorMessageCollection();
		}

		public ErrorMessageCollection ExecuteProcedure(string objectNamespace, string objectName, string methodName,
													   AttributeValueSet parameterValues, DbTransaction transaction)
		{
			DataTable[] recordSets;
			return
				ExecuteProcedure(objectNamespace, objectName, methodName, parameterValues, out recordSets, transaction);
		}

		public void Close()
		{
			sqlConnection.Close();
		}

		#region Initialization

		public SqlAccessProvider(MetadataStore metadataStore, SqlConnection sqlConnection)
		{
			if (metadataStore == null)
				throw new ArgumentNullException("metadataStore");
			if (sqlConnection == null)
				throw new ArgumentNullException("sqlConnection");
			this.metadataStore = metadataStore;
			this.sqlConnection = sqlConnection;
		}

		#endregion

		#region Private static methods

		private static DataTable[] getResultSets(IDataReader reader)
		{
			ArrayList result = new ArrayList();
			DataTable schemaTable = reader.GetSchemaTable();
			DataTable resutSetTable = createTableBySchema(schemaTable);
			while (reader.Read())
			{
				object[] values = new object[schemaTable.Rows.Count];
				reader.GetValues(values);
				resutSetTable.Rows.Add(values);
			}
			result.Add(resutSetTable);
			return (DataTable[])result.ToArray(typeof(DataTable));
		}

		private static DataTable createTableBySchema(DataTable schemaTable)
		{
			DataTable table = new DataTable("ResultTable");
			foreach (DataRow row in schemaTable.Rows)
			{
				DataColumn column = new DataColumn();
				column.ColumnName = (string)row["ColumnName"];
				column.DataType = (Type)row["DataType"];
				table.Columns.Add(column);
			}
			return table;
		}

		private static SqlParameter createSqlParameter(SqlCommand command, ObjectMethodParameterDefinition parameter)
		{
			SqlParameter param = command.Parameters.Add(parameter.Name, DataTypeEnumeration.GetSqlDbTypeByName(parameter.SqlDbType));
			param.Direction = DataTypeEnumeration.GetSystemParameterDirection(parameter.Direction);
			param.SourceColumn = parameter.AttributeName;
			if (DataTypeEnumeration.GetSqlDbTypeByName(parameter.SqlDbType) == SqlDbType.Decimal)
			{
				param.Precision = parameter.Precision;
				param.Scale = parameter.Scale;
			}
			if ((param.SqlDbType == SqlDbType.NChar) || (param.SqlDbType == SqlDbType.NVarChar) ||
				(param.SqlDbType == SqlDbType.Char) || (param.SqlDbType == SqlDbType.VarChar))
			{
				param.Size = parameter.MaxLength;
				param.Scale = parameter.Scale;
			}
			return param;
		}

		private static ErrorMessageCollection buildErrorMessageCollection(SqlXml xml)
		{
			ErrorMessageCollection result = new ErrorMessageCollection();
			if (!xml.IsNull)
			{
				XmlReader reader = xml.CreateReader();
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element && reader.Name == "row")
					{
						ErrorMessage message =
							new ErrorMessage
								{
									Text = reader.GetAttribute("Message"),
									Severity = Convert.ToInt32(reader.GetAttribute("Severity")),
									AttributeName = reader.GetAttribute("AttrbiuteName")
								};
						result.Add(message);
					}
				}
			}
			return result;
		}

		#endregion

		#endregion
	}
}

// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore SuggestUseVarKeywordEvident