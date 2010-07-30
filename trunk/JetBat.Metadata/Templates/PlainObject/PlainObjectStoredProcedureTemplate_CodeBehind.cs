// ReSharper disable ConvertToAutoProperty
// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable MoreSpecificForeachVariableTypeAvailable
// ReSharper disable UseObjectOrCollectionInitializer
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using JetBat.DatabaseSchema;
using JetBat.DatabaseSchema.SqlServer;

namespace JetBat.Metadata.Templates.PlainObject
{
	public partial class PlainObjectStoredProcedureTemplate
	{
		public bool Restore { get; set; }
		public bool CopyByParentObject { get; set; }
		public bool DeleteByParentObject { get; set; }

		public void PreRender()
		{
			loadDatabaseSchema();
		}

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

		#region Database Schema

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

		#endregion

		#region Актуальное

		private void initiateParameters()
		{
			if (string.IsNullOrEmpty(tableName)) throw new Exception("Атрибут TableName должен быть задан");
			if (string.IsNullOrEmpty(viewName)) throw new Exception("Атрибут ViewName должен быть задан");
			tableSchema = databaseSchema.Tables[tableName];
			viewSchema = databaseSchema.Views[viewName];

			#region Столбец метки об удалении
			if (!string.IsNullOrEmpty(deleteFlagColumnName))
			{
				deleteFlagColumnSchema = tableSchema.Columns[deleteFlagColumnName];
				if (deleteFlagColumnSchema == null) throw new Exception(string.Format("Столбец флага логического удаления [{0}] не найден", deleteFlagColumnName));
			}
			else
				deleteFlagColumnSchema = null;
			#endregion

			#region Столбец метки даты-времени
			if (!string.IsNullOrEmpty(dateTimeMarkColumnName))
			{
				dateTimeMarkColumnSchema = tableSchema.Columns[dateTimeMarkColumnName];
				if (dateTimeMarkColumnSchema == null) throw new Exception(string.Format("Столбец метки даты-времени [{0}] не найден", dateTimeMarkColumnName));
			}
			else
				dateTimeMarkColumnSchema = null;
			#endregion

			#region Столбец порядкового номера
			if (!string.IsNullOrEmpty(sequenceNumberColumnName))
			{
				sequenceNumberColumnSchema = tableSchema.Columns[sequenceNumberColumnName];
				if (sequenceNumberColumnSchema == null) throw new Exception(string.Format("Столбец порядкового номера [{0}] не найден", sequenceNumberColumnName));
			}
			else
				sequenceNumberColumnSchema = null;
			#endregion

			#region Столбец состояния
			if (!string.IsNullOrEmpty(statusColumnName))
			{
				statusColumnSchema = tableSchema.Columns[statusColumnName];
				if (statusColumnSchema == null) throw new Exception(string.Format("Столбец состояния объекта [{0}] не найден", statusColumnName));
			}
			else
				statusColumnSchema = null;
			#endregion

			#region Внешний ключ на родительский объект
			if (!string.IsNullOrEmpty(foreignKeyToParentName))
			{
				foreignKeyToParentSchema = databaseSchema.ForeignKeys[foreignKeyToParentName];
				if (foreignKeyToParentSchema == null) throw new Exception(string.Format("Внешний ключ на родительский объект [{0}] не найден", foreignKeyToParentName));
				if (foreignKeyToParentSchema.ChildTableName != tableSchema.Name) throw new Exception(string.Format("Внешний ключ на родительский объект [{0}] не является ссылкой из базовой таблицы", foreignKeyToParentName));
			}
			else
				foreignKeyToParentSchema = null;
			#endregion
		}

		private string databaseName;
		private string tableName;
		private string viewName;
		private string deleteFlagColumnName;
		private string dateTimeMarkColumnName;
		private string sequenceNumberColumnName;
		private string statusColumnName;
		private string foreignKeyToParentName;
		private StringCollection ignoredColumnList;
		private StringCollection readOnlyColumnList;
		private StringCollection hiddenColumnList;
		private string entityName;
		private string entityNamespace;
		private string namespacePrefix;
		string uiEditorName;

		protected TableSchema tableSchema;
		protected ViewSchema viewSchema;
		protected TableColumnSchema deleteFlagColumnSchema;
		protected TableColumnSchema dateTimeMarkColumnSchema;
		protected TableColumnSchema sequenceNumberColumnSchema;
		protected TableColumnSchema statusColumnSchema;
		protected ForeignKeySchema foreignKeyToParentSchema;

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
		public string DeleteFlagColumnName
		{
			get { return deleteFlagColumnName; }
			set { deleteFlagColumnName = value; }
		}

		[Category("0. Actual")]
		public string DateTimeMarkColumnName
		{
			get { return dateTimeMarkColumnName; }
			set { dateTimeMarkColumnName = value; }
		}

		[Category("0. Actual")]
		public string SequenceNumberColumnName
		{
			get { return sequenceNumberColumnName; }
			set { sequenceNumberColumnName = value; }
		}

		[Category("0. Actual")]
		public string StatusColumnName
		{
			get { return statusColumnName; }
			set { statusColumnName = value; }
		}

		[Category("0. Actual")]
		public string ForeignKeyToParentName
		{
			get { return foreignKeyToParentName; }
			set { foreignKeyToParentName = value; }
		}

		[Category("0. Actual")]
		public StringCollection IgnoredColumnList
		{
			get { return ignoredColumnList; }
			set { ignoredColumnList = value; }
		}

		[Category("0. Actual")]
		public StringCollection ReadOnlyColumnList
		{
			get { return readOnlyColumnList; }
			set { readOnlyColumnList = value; }
		}

		[Category("0. Actual")]
		public StringCollection HiddenColumnList
		{
			get { return hiddenColumnList; }
			set { hiddenColumnList = value; }
		}

		#endregion

		#region Атрибуты



		public string str_and = "";

		#endregion

		#region Свойства

		#region 2. Сущность

		[Category("2. Сущность")]
		[Description("Префикс пространства имен")]
		public string NamespacePrefix
		{
			get { return namespacePrefix; }
			set { namespacePrefix = value; }
		}

		[Category("2. Сущность")]
		[Description("форма редактирование")]
		public string UIEditorName
		{
			get { return uiEditorName; }
			set { uiEditorName = value; }
		}

		#endregion

		#endregion

		#region Методы

		#region Объявление параметров

		public bool ParameterIsOutputInsert2(TableColumnSchema column)
		{
			return column.IsIdentity || (dateTimeMarkColumnSchema != null && column.Name == dateTimeMarkColumnSchema.Name);
		}

		public string GetParameterDeclarationInsert2(TableColumnSchema column)
		{
			if (ParameterIsOutputInsert2(column))
				return GetParameterDeclaration2(column, true);
			return GetParameterDeclaration2(column, false);
		}

		public bool ParameterIsOutputUpdate2(TableColumnSchema column)
		{
			return dateTimeMarkColumnSchema != null && column.Name == dateTimeMarkColumnSchema.Name;
		}

		public string GetParameterDeclarationUpdate2(TableColumnSchema column)
		{
			if (ParameterIsOutputUpdate2(column))
				return GetParameterDeclaration2(column, true);
			return GetParameterDeclaration2(column, false);
		}

		public string GetParameterDeclaration2(TableColumnSchema column, bool Output)
		{
			return GetParameterDeclaration2(column, Output, null);
		}

		public string GetParameterDeclaration2(TableColumnSchema column, bool Output, string Prefix)
		{
			string result = "@" + (Prefix ?? "") + column.Name + " " + column.DataTypeName;
			if (ColumnHasSize(column.DataTypeName))
				result += "(" + (column.MaxLength != -1 ? column.MaxLength.ToString() : "max") + ")";
			else if (ColumnHasPrecisionAndScale(column.DataTypeName))
				result += "(" + column.Precision + ", " + column.Scale + ")";

			if (Output)
				result += " OUTPUT";

			return result;
		}

		public string GetParameterDeclaration(ViewColumnSchema column, bool Output)
		{
			string result = "@" + column.Name + " " + column.DataTypeName;
			if (ColumnHasSize(column.DataTypeName))
				result += "(" + (column.MaxLength != -1 ? column.MaxLength.ToString() : "max") + ")";
			else if (ColumnHasPrecisionAndScale(column.DataTypeName))
				result += "(" + column.Precision + ", " + column.Scale + ")";

			if (Output)
				result += " OUTPUT";

			return result;
		}

		#endregion

		#region Свойства колонок

		public bool ColumnAllowsDBNull(string columnName)
		{
			if (sequenceNumberColumnSchema != null && columnName == sequenceNumberColumnSchema.Name)
				return true;
			if (tableSchema.Columns.Contains(columnName))
				return tableSchema.Columns[columnName].AllowNull;
			return true;
		}

		public bool ColumnIsIgnored(string columnName)
		{
			return ignoredColumnList != null && ignoredColumnList.Contains(columnName);
		}

		public bool ColumnIsReadOnly(string columnName)
		{
			return
				(
					!tableSchema.Columns.Contains(columnName) ||
					tableSchema.Columns[columnName].IsIdentity ||
					(dateTimeMarkColumnSchema != null && columnName == dateTimeMarkColumnSchema.Name) ||
					(readOnlyColumnList != null && readOnlyColumnList.Contains(columnName)) ||
					(deleteFlagColumnSchema != null && columnName == deleteFlagColumnSchema.Name) ||
					(statusColumnSchema != null && columnName == statusColumnSchema.Name)
				);
		}

		public bool ColumnHasPrecisionAndScale(string NativeType)
		{
			switch (NativeType)
			{
				case "decimal": return true;
				default: return false;
			}
		}

		public bool ColumnHasSize(string NativeType)
		{
			switch (NativeType)
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

		#endregion

		#region Свойства сущности, таблицы и процедур

		public bool TableHasIdentity()
		{
			return tableSchema.PrimaryKey[0].IsIdentity;
		}

		public string GetIdentityColumnName()
		{
			foreach (TableColumnSchema column in tableSchema.Columns)
				if (column.IsIdentity)
					return column.Name;
			return String.Empty;
		}

		public string GetEntityName()
		{
			return EntityName;
		}

		public string GetProcedureName(string OperationName)
		{
			return NamespacePrefix + "_" + EntityName + "_" + OperationName;
		}

		#endregion

		#region Работа с внешними ключами

		public bool ForeignKeyIsIncludedIntoView2(ForeignKeySchema foreignKey)
		{
			bool result = true;
			foreach (ForeignKeyColumnPairSchema fk_column in foreignKey.ColumnPairs)
			{
				bool found = false;
				foreach (ViewColumnSchema view_column in viewSchema.Columns)
					if (view_column.Name == fk_column.ForeignKeyColumnName)
					{
						found = true;
						break;
					}
				if (!found)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		#endregion

		#endregion

		#region Делегаты

		#region Insert

		private bool beforeInsert;
		private bool afterInsert;

		#region BeforeInsert
		[Description("Указывает, создается и вызывается ли метод")]
		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool BeforeInsert
		{
			get { return beforeInsert; }
			set { beforeInsert = value; }
		}
		#endregion

		#region AfterInsert
		[Description("Указывает, создается и вызывается ли метод")]
		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool AfterInsert
		{
			get { return afterInsert; }
			set { afterInsert = value; }
		}
		#endregion

		#endregion

		#region Update

		private bool beforeUpdate;
		private bool afterUpdate;

		#region BeforeUpdate
		[Description("Указывает, создается и вызывается ли метод")]
		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool BeforeUpdate
		{
			get { return beforeUpdate; }
			set { beforeUpdate = value; }
		}
		#endregion

		#region AfterUpdate
		[Description("Указывает, создается и вызывается ли метод")]
		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool AfterUpdate
		{
			get { return afterUpdate; }
			set { afterUpdate = value; }
		}
		#endregion

		#endregion

		#region Delete

		private bool beforeDelete;
		private bool afterDelete;

		#region BeforeDelete
		[Description("Указывает, создается и вызывается ли метод")]
		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool BeforeDelete
		{
			get { return beforeDelete; }
			set { beforeDelete = value; }
		}
		#endregion

		#region AfterDelete
		[Description("Указывает, создается и вызывается ли метод")]
		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool AfterDelete
		{
			get { return afterDelete; }
			set { afterDelete = value; }
		}
		#endregion

		#endregion

		#region Restore

		private bool beforeRestore;
		private bool afterRestore;

		#region BeforeRestore
		[Description("Указывает, создается и вызывается ли метод")]
		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool BeforeRestore
		{
			get { return beforeRestore; }
			set { beforeRestore = value; }
		}
		#endregion

		#region AfterRestore
		[Description("Указывает, создается и вызывается ли метод")]
		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool AfterRestore
		{
			get { return afterRestore; }
			set { afterRestore = value; }
		}
		#endregion

		#endregion

		#endregion

		#region Целочисленный статус

		private int initialStatusValue;

		[Description("Начальное значение статуса")]
		[Category("Статус объекта")]
		public int InitialStatusValue
		{
			get { return initialStatusValue; }
			set { initialStatusValue = value; }
		}

		#endregion
	}
}
// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore MoreSpecificForeachVariableTypeAvailable
// ReSharper restore SuggestUseVarKeywordEvident
// ReSharper restore ConvertToAutoProperty