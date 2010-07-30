// ReSharper disable ConvertToAutoProperty
// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable MoreSpecificForeachVariableTypeAvailable
// ReSharper disable UseObjectOrCollectionInitializer
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System;
using System.Data;
using JetBat.DatabaseSchema;
using JetBat.DatabaseSchema.SqlServer;

namespace JetBat.Metadata.Templates.PlainObjectListView
{
	public partial class PlainObjectListViewStoredProcedureTemplate
	{
		public void PreRender()
		{
			loadDatabaseSchema();
		}

		#region Актуальное

		private List<StoredProcedureParameterSchema> parameterDefinitions = new List<StoredProcedureParameterSchema>();
		private string databaseName;
		private string tableName;
		private string viewName;
		private StringCollection ignoredColumnList;
		private StringCollection hiddenColumnList;
		private string entityName;
		private string entityNamespace;
		private string namespacePrefix;
		private string uiCaption;
		private string deleteFlagColumnName;
		private bool showDeleted;
		private string selectionCondition;

		protected TableSchema tableSchema;
		protected ViewSchema viewSchema;
		protected TableColumnSchema deleteFlagColumnSchema;

		public List<StoredProcedureParameterSchema> ParameterDefinitions
		{
			get { return parameterDefinitions; }
			set { parameterDefinitions = value; }
		}

		[Category("0. Actual")]
		public string EntityNamespace
		{
			get { return entityNamespace; }
			set { entityNamespace = value; }
		}

		[Category("0. Actual")]
		public string EntityName
		{
			get { return entityName; }
			set { entityName = value; }
		}

		[Category("0. Actual")]
		public string DatabaseName
		{
			get { return databaseName; }
			set { databaseName = value; }
		}

		[Category("0. Actual")]
		public string TableName
		{
			get { return tableName; }
			set { tableName = value; }
		}

		[Category("0. Actual")]
		public string ViewName
		{
			get { return viewName; }
			set { viewName = value; }
		}

		[Category("0. Actual")]
		public StringCollection IgnoredColumnList
		{
			get { return ignoredColumnList; }
			set { ignoredColumnList = value; }
		}

		[Category("0. Actual")]
		public StringCollection HiddenColumnList
		{
			get { return hiddenColumnList; }
			set { hiddenColumnList = value; }
		}

		[Category("0. Actual")]
		public string NamespacePrefix
		{
			get { return namespacePrefix; }
			set { namespacePrefix = value; }
		}

		[Category("0. Actual")]
		public string UICaption
		{
			get { return uiCaption; }
			set { uiCaption = value; }
		}

		[Category("0. Actual")]
		public string DeleteFlagColumnName
		{
			get { return deleteFlagColumnName; }
			set { deleteFlagColumnName = value; }
		}

		[Category("0. Actual")]
		[DefaultValue(false)]
		public bool ShowDeleted
		{
			get { return showDeleted; }
			set { showDeleted = value; }
		}

		[Category("0. Actual")]
		public string SelectionCondition
		{
			get { return selectionCondition; }
			set { selectionCondition = value; }
		}


		#endregion

		#region Database Schema

		[Browsable(false)]
		public DatabaseSchema.DatabaseSchema DatabaseSchema
		{
			get { return databaseSchema; }
		}

		private DatabaseSchema.DatabaseSchema databaseSchema;
		private string connectionString;

		[Category("0. Actual")]
		public string ConnectionString
		{
			get { return connectionString; }
			set { connectionString = value; }
		}

		private void loadDatabaseSchema()
		{
			if (connectionString == null) return;
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();
			try
			{
				SqlSchemaProvider SqlSchemaProvider =
					new SqlSchemaProvider(sqlConnection);
				databaseSchema = SqlSchemaProvider.LoadSchema("MealCalc2");

				loadAttributeAliases(sqlConnection);
				loadComplexAttributeAliases(sqlConnection);
			}
			finally
			{
				sqlConnection.Close();
			}
			initiateParameters();
		}

		private void initiateParameters()
		{
			if (string.IsNullOrEmpty(tableName)) throw new Exception("Attribute TableName must be set");
			if (string.IsNullOrEmpty(viewName)) throw new Exception("Attribute ViewName must be set");
			tableSchema = databaseSchema.Tables[tableName];
			viewSchema = databaseSchema.Views[viewName];
			if (tableSchema == null) throw new Exception(string.Format("Table [{0}] not found", tableName));
			if (viewSchema == null) throw new Exception(string.Format("View [{0}] not found", viewName));

			#region Столбец метки об удалении
			if (!string.IsNullOrEmpty(deleteFlagColumnName))
			{
				deleteFlagColumnSchema = tableSchema.Columns[deleteFlagColumnName];
				if (deleteFlagColumnSchema == null) throw new Exception(string.Format("Столбец флага логического удаления [{0}] не найден", deleteFlagColumnName));
			}
			else
				deleteFlagColumnSchema = null;
			#endregion
		}

		#endregion

		#region Атрибуты

		protected string str_and = "";
		private string orderBy; //Выражение для сортировки

		#endregion

		#region Свойства

		[DefaultValue("")]
		[Category("Список объектов")]
		[Description("Выражение для сортировки")]
		public string OrderBy
		{
			get { return orderBy; }
			set { orderBy = value; }
		}

		#endregion

		#region Параметры процедуры

		public string GetParameterDeclaration(StoredProcedureParameterSchema column)
		{
			string result = "@" + column.Name + " " + column.DataTypeName;
			if (ColumnHasMaxLength(column.DataTypeName))
				result += "(" + (column.MaxLength != -1 ? column.MaxLength.ToString() : "max") + ")";
			else if (ColumnHasPrecision(column.DataTypeName))
				result += "(" + column.Precision + ", " + column.Scale + ")";

			if (column.IsOutput)
				result += " OUTPUT";

			return result;
		}

		public bool ParameterHasMaxLength(SqlDbType ParameterType)
		{
			switch (ParameterType)
			{
				case SqlDbType.Char: return true;
				case SqlDbType.NChar: return true;
				case SqlDbType.NVarChar: return true;
				case SqlDbType.VarChar: return true;
				default: return false;
			}
		}

		public bool ParameterHasPrecisionAndScale(SqlDbType ParameterType)
		{
			switch (ParameterType)
			{
				case SqlDbType.Decimal: return true;
				default: return false;
			}
		}

		#endregion

		#region Флаги столбцов

		public bool ColumnIsVisible(string ColumnName)
		{
			return (hiddenColumnList == null || !hiddenColumnList.Contains(ColumnName)) &&
			       !((tableSchema != null) && (tableSchema.Columns[ColumnName] != null) && tableSchema.Columns[ColumnName].IsIdentity) &&
			       !((tableSchema != null) && (tableSchema.Columns[ColumnName] != null) && tableSchema.Columns[ColumnName].IsForeignKeyMember) &&
			       !(deleteFlagColumnSchema != null && !ShowDeleted && (ColumnName == deleteFlagColumnSchema.Name));
		}

		public bool ColumnIsIgnored(string ColumnName)
		{
			return (ignoredColumnList != null && ignoredColumnList.Contains(ColumnName));
		}

		public bool ColumnIsExternal(string ColumnName)
		{
			return !tableSchema.Columns.Contains(ColumnName);
		}

		#endregion

		#region Методы

		public bool ColumnHasMaxLength(string DataTypeName)
		{
			switch (DataTypeName)
			{
				case "char": return true;
				case "nchar": return true;
				case "ntext": return true;
				case "numeric": return true;
				case "nvarchar": return true;
				case "sysname": return true;
				case "text": return true;
				case "varchar": return true;
				default: return false;
			}
		}

		public bool ColumnHasPrecision(string DataTypeName)
		{
			switch (DataTypeName)
			{
				case "decimal": return true;
				default: return false;
			}
		}

		#endregion

		#region Request for attribute alias

		private void loadAttributeAliases(SqlConnection connection)
		{
			SqlCommand command = new SqlCommand("SelectAttributeAliases", connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@ObjectNamespace", EntityNamespace);
			command.Parameters.AddWithValue("@ObjectName", EntityName);
			SqlDataReader reader = command.ExecuteReader();
			try
			{
				while (reader.Read())
				{
					attributeAliases.Add((string)reader["Name"], (string)reader["UILabel"]);
				}
			}
			finally
			{
				reader.Close();
			}
		}

		protected string GetAttributeAlias(string attributeName)
		{
			string result = attributeAliases[attributeName];
			return result ?? attributeName;
		}

		private readonly NameValueCollection attributeAliases = new NameValueCollection();

		#endregion

		#region Request for complex attribute alias

		private void loadComplexAttributeAliases(SqlConnection connection)
		{
			SqlCommand command = new SqlCommand("SelectComplexAttributeAliases", connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@ObjectNamespace", EntityNamespace);
			command.Parameters.AddWithValue("@ObjectName", EntityName);
			SqlDataReader reader = command.ExecuteReader();
			try
			{
				while (reader.Read())
				{
					complexAttributeAliases.Add((string)reader["Name"], (string)reader["UILabel"]);
				}
			}
			finally
			{
				reader.Close();
			}
		}

		protected string GetComplexAttributeAlias(string attributeName)
		{
			string result = complexAttributeAliases[attributeName];
			return result ?? attributeName;
		}

		private readonly NameValueCollection complexAttributeAliases = new NameValueCollection();

		#endregion
	}
}
// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore MoreSpecificForeachVariableTypeAvailable
// ReSharper restore SuggestUseVarKeywordEvident
// ReSharper restore ConvertToAutoProperty