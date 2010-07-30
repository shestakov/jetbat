// ReSharper disable ConvertToAutoProperty
// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable MoreSpecificForeachVariableTypeAvailable
// ReSharper disable UseObjectOrCollectionInitializer
using System.Collections.Specialized;
using System.ComponentModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using JetBat.DatabaseSchema;
using JetBat.DatabaseSchema.SqlServer;

namespace JetBat.Metadata.Templates.MultiversionDocument
{
	public partial class MultiversionDocumentStoredProcedureTemplate
	{
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

		#endregion

		private void initiateParameters()
		{
			if (string.IsNullOrEmpty(tableName)) throw new Exception("Атрибут TableName должен быть задан");
			if (string.IsNullOrEmpty(viewName)) throw new Exception("Атрибут ViewName должен быть задан");
			tableSchema = databaseSchema.Tables[tableName];
			viewSchema = databaseSchema.Views[viewName];
		}

		private string databaseName;
		private string tableName;
		private string viewName;
		private StringCollection ignoredColumnList;
		private StringCollection readOnlyColumnList;
		private StringCollection hiddenColumnList;
		private StringCollection headerNullableColumnList;
		private string entityName;
		private string entityNamespace;
		private string namespacePrefix;

		protected TableSchema tableSchema;
		protected ViewSchema viewSchema;

		#region Атрибуты

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
		public string NamespacePrefix
		{
			get { return namespacePrefix; }
			set { namespacePrefix = value; }
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

		[Category("0. Actual")]
		public StringCollection HeaderNullableColumnList
		{
			get { return headerNullableColumnList; }
			set { headerNullableColumnList = value; }
		}

		#endregion

		#region Атрибуты

		public string str_and = "";

		private bool afterCreate;
		private bool beforeUpdateVersion;
		//private bool afterUpdateVersion;
		private bool beforeConfirmEdit;
		private bool afterConfirmEdit;
		private bool beforeCommit;
		private bool beforeRollback;
		//private bool beforeDelete;
		//private bool afterDelete;
	
		private StringCollection nullableChildColumnsSave = new StringCollection();
		private StringCollection nullableChildColumnsCommit = new StringCollection();
		private StringCollection nullableDocumentColumnsSave = new StringCollection();
		private StringCollection nullableDocumentColumnsCommit = new StringCollection();
	
		#endregion
	
		#region Свойства
		
		#region Методы-делегаты

		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool AfterCreate
		{
			get { return afterCreate; }
			set { afterCreate = value; }
		}
		
		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool BeforeUpdateVersion
		{
			get { return beforeUpdateVersion; }
			set { beforeUpdateVersion = value; }
		}

		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool BeforeConfirmEdit
		{
			get { return beforeConfirmEdit; }
			set { beforeConfirmEdit = value; }
		}

		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool AfterConfirmEdit
		{
			get { return afterConfirmEdit; }
			set { afterConfirmEdit = value; }
		}

		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool BeforeCommit
		{
			get { return beforeCommit; }
			set { beforeCommit = value; }
		}

		[Category("Методы-делегаты")]
		[DefaultValue(false)]
		public bool BeforeRollback
		{
			get { return beforeRollback; }
			set { beforeRollback = value; }
		}
		
		#endregion
		
		#region 4. Колонки, допускающие null
		
		[Category("4. Колонки, допускающие NULL")]
		[Description("Список колонок, допускающих null в процедурах INSERT и UPDATE табличной части")]
		[DefaultValue("true")]
		public StringCollection NullableChildColumnsSave
		{
			get { return nullableChildColumnsSave; }
			set { nullableChildColumnsSave = value; }
		}
		
		[Category("4. Колонки, допускающие NULL")]
		[Description("Список колонок, допускающих null в табличной части при проведении документа")]
		[DefaultValue("true")]
		public StringCollection NullableChildColumnsCommit
		{
			get { return nullableChildColumnsCommit; }
			set { nullableChildColumnsCommit = value; }
		}
		
		[Category("4. Колонки, допускающие NULL")]
		[Description("Список колонок, допускающих null в заголовочной части при сохранении документа")]
		[DefaultValue("true")]
		public StringCollection NullableDocumentColumnsSave
		{
			get { return nullableDocumentColumnsSave; }
			set { nullableDocumentColumnsSave = value; }
		}
		
		[Category("4. Колонки, допускающие NULL")]
		[Description("Список колонок, допускающих null в заголовочной части при проведении документа")]
		[DefaultValue("true")]
		public StringCollection NullableDocumentColumnsCommit
		{
			get { return nullableDocumentColumnsCommit; }
			set { nullableDocumentColumnsCommit = value; }
		}
		
		#endregion

		#endregion
	
		#region Методы
	
		#region Объявление параметров
		
		public string GetParameterDeclaration(TableColumnSchema column, bool Output)
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

		public bool ColumnIsExternal(string ColumnName)
		{
			return !tableSchema.Columns.Contains(ColumnName);
		}
		
		public bool ColumnIsReadOnly(string ColumnName)
		{
			return
				(
					ColumnIsExternal(ColumnName) ||
					tableSchema.Columns[ColumnName].IsIdentity ||
					(readOnlyColumnList != null && readOnlyColumnList.Contains(ColumnName))
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

		public bool HeaderColumnIsNullable(string columnName)
		{
			if (headerNullableColumnList != null && headerNullableColumnList.Contains(columnName))
				return true;
			return false;
		}
		
		#endregion
		
		#region Свойства сущности, таблицы и процедур
		
		public string GetIdentityColumnName()
		{
			foreach (TableColumnSchema column in tableSchema.Columns)
				if (column.IsIdentity)
					return column.Name;
			return String.Empty;
		}
		
		public bool TableHasIdentity()
		{
			foreach (TableColumnSchema column in tableSchema.Columns)
				if (column.IsIdentity)
					return true;
			return false;
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
		
		#region Поиск подчиненных объектов
		
		#region Объявление класса
		
		public class ChildObject
		{
			public readonly string Namespace = "";
			public readonly string Name = "";
			public readonly string ForeignKeyToParentObjectName = "";
			public readonly string CopyByParentObjectProcedureName = "";
			public readonly string DeleteByParentObjectProcedureName = "";
			
			public ChildObject
				(
				string Namespace,
				string Name,
				string ForeignKeyToParentObjectName,
				string CopyByParentObjectProcedureName,
				string DeleteByParentObjectProcedureName
				)
			{
				this.Namespace = Namespace;
				this.Name = Name;
				this.ForeignKeyToParentObjectName = ForeignKeyToParentObjectName;
				this.CopyByParentObjectProcedureName = CopyByParentObjectProcedureName;
				this.DeleteByParentObjectProcedureName = DeleteByParentObjectProcedureName;
			}
		}
		
		#endregion
		
		#region Загрузка списка дочерних объектов
		
		public ChildObject[] GetChildObjectList()
		{
			ArrayList result = new ArrayList();
			
			SqlConnection connection = new SqlConnection();
			connection.ConnectionString = connectionString;
			connection.Open();
			SqlCommand command = new SqlCommand("MetadataEngine_LoadChildObjectList", connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@ObjectNamespace", EntityNamespace);
			command.Parameters.AddWithValue("@ObjectName", EntityName);
			SqlDataReader reader = command.ExecuteReader();
			try
			{
				while (reader.Read())
				{
					ChildObject child_object = new ChildObject
						(
						reader["ObjectNamespace"].ToString(),
						reader["ObjectName"].ToString(),
						reader["ForeignKeyToParentObjectName"].ToString(),
						reader["CopyByParentObjectStoredProcedureName"] == DBNull.Value ? String.Empty : reader["CopyByParentObjectStoredProcedureName"].ToString(),
						reader["DeleteByParentObjectStoredProcedureName"] == DBNull.Value ? String.Empty : reader["DeleteByParentObjectStoredProcedureName"].ToString()
						);
					result.Add(child_object);
				}
			}
			finally
			{
				reader.Close();
			}
			connection.Close();
			
			return (ChildObject[]) result.ToArray(typeof(ChildObject));
		}
		
		#endregion
		
		#endregion
	
		#endregion
	}
}
// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore MoreSpecificForeachVariableTypeAvailable
// ReSharper restore SuggestUseVarKeywordEvident
// ReSharper restore ConvertToAutoProperty