using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace JetBat.DatabaseSchema.SqlServer
{
	public class SqlSchemaProvider
	{
		private const string SelectForeignKeyList = "SelectForeignKeyList";
		private const string SelectStoredProcedureList = "SelectStoredProcedureList";
		private const string SelectTableList = "SelectTableList";
		private const string SelectViewList = "SelectViewList";
		private readonly SqlConnection sqlConnection;

		public SqlSchemaProvider(SqlConnection sqlConnection)
		{
			this.sqlConnection = sqlConnection;
		}

		public DatabaseSchema LoadSchema(string databaseName)
		{
			Dictionary<string, TableDescriptor> tableDescriptors = selectTableList();
			Dictionary<string, ViewDescriptor> viewDescriptors = selectViewList();
			Dictionary<string, StoredProcedureDescriptor> storedProcedureDescriptors = selectStoredProcedureList();
			Dictionary<string, ForeignKeyDescriptor> foreignKeyDescriptors = selectForeignKeyList();

			#region Creating schema for foreign keys

			var foreignKeys = new Collection<ForeignKeySchema>();
			foreach (string name in foreignKeyDescriptors.Keys)
			{
				ForeignKeyDescriptor descriptor = foreignKeyDescriptors[name];
				var columnPairs = new List<ForeignKeyColumnPairSchema>(descriptor.ColumnPairs.Count);
				foreach (ForeignKeyColumnPairDescriptor pairDescriptor in descriptor.ColumnPairs)
				{
					var pairSchema =
						new ForeignKeyColumnPairSchema(pairDescriptor.PrimaryKeyColumnName, pairDescriptor.ForeignKeyColumnName);
					columnPairs.Add(pairSchema);
				}
				var foreignKeySchema =
					new ForeignKeySchema(descriptor.Name, descriptor.PrimaryTableName, descriptor.ForeignTableName,
										 descriptor.Descrtiption, columnPairs);
				foreignKeys.Add(foreignKeySchema);
			}

			#endregion

			#region Creating schema for tables

			var tables = new Collection<TableSchema>();
			foreach (string name in tableDescriptors.Keys)
			{
				TableDescriptor descriptor = tableDescriptors[name];
				var columns = new List<TableColumnSchema>(descriptor.Columns.Count);
				foreach (TableColumnDescriptor columnDescriptor in descriptor.Columns)
				{
					var columnSchema =
						new TableColumnSchema(columnDescriptor.Name, columnDescriptor.DataTypeName, columnDescriptor.AllowNull,
											  columnDescriptor.IsPrimaryKeyMember, columnDescriptor.IsForeignKeyMember,
											  columnDescriptor.IsIdentity, columnDescriptor.MaxLength, columnDescriptor.Precision,
											  columnDescriptor.Scale, columnDescriptor.Description);
					columns.Add(columnSchema);
				}

				var tableSchema = new TableSchema(descriptor.Name, descriptor.Descrtiption, columns, foreignKeys);
				tables.Add(tableSchema);
			}

			#endregion

			#region Creating schema for views

			var views = new Collection<ViewSchema>();
			foreach (string name in viewDescriptors.Keys)
			{
				ViewDescriptor descriptor = viewDescriptors[name];
				var viewColumns = new List<ViewColumnSchema>(descriptor.Columns.Count);
				foreach (ViewColumnDescriptor columnDescriptor in descriptor.Columns)
				{
					var viewColumnSchema =
						new ViewColumnSchema(columnDescriptor.Name, columnDescriptor.DataTypeName, columnDescriptor.MaxLength,
											 columnDescriptor.Precision, columnDescriptor.Scale, columnDescriptor.Description);
					viewColumns.Add(viewColumnSchema);
				}
				var viewSchema = new ViewSchema(descriptor.Name, descriptor.Descrtiption, viewColumns);
				views.Add(viewSchema);
			}

			#endregion

			#region Creating schema for stored procedures

			var storedProcedures = new Collection<StoredProcedureSchema>();
			foreach (string name in storedProcedureDescriptors.Keys)
			{
				StoredProcedureDescriptor descriptor = storedProcedureDescriptors[name];
				var procedureParameters =
					new List<StoredProcedureParameterSchema>(descriptor.Parameters.Count);
				foreach (StoredProcedureParameterDescriptor parameterDescriptor in descriptor.Parameters)
				{
					var storedProcedureParameterSchema =
						new StoredProcedureParameterSchema(parameterDescriptor.Name, parameterDescriptor.DataTypeName,
														   parameterDescriptor.IsOutput, parameterDescriptor.MaxLength,
														   parameterDescriptor.Precision, parameterDescriptor.Scale,
														   parameterDescriptor.Description);
					procedureParameters.Add(storedProcedureParameterSchema);
				}
				var storedProcedureSchema =
					new StoredProcedureSchema(descriptor.Name, descriptor.Descrtiption, procedureParameters);
				storedProcedures.Add(storedProcedureSchema);
			}

			#endregion

			var databaseSchema = new DatabaseSchema(databaseName, tables, views, storedProcedures, foreignKeys);

			return databaseSchema;
		}

		#region SelectForeignKeyList()

		private Dictionary<string, ForeignKeyDescriptor> selectForeignKeyList()
		{
			var foreignKeys = new Dictionary<string, ForeignKeyDescriptor>();
			var columnPairs = new ArrayList();
			var command = new SqlCommand(SelectForeignKeyList, sqlConnection) { CommandType = CommandType.StoredProcedure };
			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					var descriptor =
						new ForeignKeyDescriptor(Convert.ToString(reader["Name"]), Convert.ToString(reader["PrimaryTableName"]),
												 Convert.ToString(reader["ForeignTableName"]), Convert.ToString(reader["Description"]));
					foreignKeys.Add(descriptor.Name, descriptor);
				}
				reader.NextResult();
				while (reader.Read())
				{
					columnPairs.Add
						(new ForeignKeyColumnPairDescriptor
							(
							Convert.ToString(reader["ForeignKeyName"]),
							Convert.ToString(reader["PrimaryKeyColumnName"]),
							Convert.ToString(reader["ForeignKeyColumnName"])
							)
						);
				}
			}

			foreach (ForeignKeyColumnPairDescriptor columnPair in columnPairs)
			{
				if (foreignKeys.ContainsKey(columnPair.ForeignKeyName))
				{
					foreignKeys[columnPair.ForeignKeyName].ColumnPairs.Add(columnPair);
				}
			}

			return foreignKeys;
		}

		#endregion

		#region SelectStoredProcedureList()

		private Dictionary<string, StoredProcedureDescriptor> selectStoredProcedureList()
		{
			var storedProcedures = new Dictionary<string, StoredProcedureDescriptor>();
			var parameters = new ArrayList();
			var command = new SqlCommand(SelectStoredProcedureList, sqlConnection) { CommandType = CommandType.StoredProcedure };
			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					var descriptor =
						new StoredProcedureDescriptor(Convert.ToString(reader["Name"]), Convert.ToString(reader["Description"]));
					storedProcedures.Add(descriptor.Name, descriptor);
				}
				reader.NextResult();
				while (reader.Read())
				{
					parameters.Add
						(new StoredProcedureParameterDescriptor
							(
							Convert.ToString(reader["StoredProcedureName"]),
							Convert.ToString(reader["Name"]),
							Convert.ToString(reader["DataTypeName"]),
							Convert.ToBoolean(reader["IsOutput"]),
							Convert.ToInt32(reader["MaxLength"]),
							Convert.ToInt32(reader["Precision"]),
							Convert.ToInt32(reader["Scale"]),
							Convert.ToString(reader["Description"])
							)
						);
				}
			}

			foreach (StoredProcedureParameterDescriptor parameter in parameters)
			{
				if (storedProcedures.ContainsKey(parameter.StoredProcedureName))
				{
					storedProcedures[parameter.StoredProcedureName].Parameters.Add(parameter);
				}
			}

			return storedProcedures;
		}

		#endregion

		#region SelectTableList()

		private Dictionary<string, TableDescriptor> selectTableList()
		{
			var tables = new Dictionary<string, TableDescriptor>();
			var columns = new ArrayList();
			var command = new SqlCommand(SelectTableList, sqlConnection) { CommandType = CommandType.StoredProcedure };
			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					var descriptor =
						new TableDescriptor(Convert.ToString(reader["Name"]), Convert.ToString(reader["Description"]));
					tables.Add(descriptor.Name, descriptor);
				}
				reader.NextResult();
				while (reader.Read())
				{
					columns.Add
						(new TableColumnDescriptor
							(
							Convert.ToString(reader["TableName"]),
							Convert.ToString(reader["Name"]),
							Convert.ToString(reader["DataTypeName"]),
							Convert.ToBoolean(reader["AllowNull"]),
							Convert.ToBoolean(reader["IsPrimaryKeyMember"]),
							Convert.ToBoolean(reader["IsForeignKeyMember"]),
							Convert.ToBoolean(reader["IsIdentity"]),
							Convert.ToInt32(reader["MaxLength"]),
							Convert.ToInt32(reader["Precision"]),
							Convert.ToInt32(reader["Scale"]),
							Convert.ToString(reader["Description"])
							)
						);
				}
			}

			foreach (TableColumnDescriptor column in columns)
			{
				if (tables.ContainsKey(column.TableName))
				{
					tables[column.TableName].Columns.Add(column);
				}
			}

			return tables;
		}

		#endregion

		#region SelectViewList()

		private Dictionary<string, ViewDescriptor> selectViewList()
		{
			var tables = new Dictionary<string, ViewDescriptor>();
			var columns = new ArrayList();
			var command = new SqlCommand(SelectViewList, sqlConnection) { CommandType = CommandType.StoredProcedure };
			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					var descriptor =
						new ViewDescriptor(Convert.ToString(reader["Name"]), Convert.ToString(reader["Description"]));
					tables.Add(descriptor.Name, descriptor);
				}
				reader.NextResult();
				while (reader.Read())
				{
					columns.Add
						(new ViewColumnDescriptor
							(
							Convert.ToString(reader["ViewName"]),
							Convert.ToString(reader["Name"]),
							Convert.ToString(reader["DataTypeName"]),
							Convert.ToInt32(reader["MaxLength"]),
							Convert.ToInt32(reader["Precision"]),
							Convert.ToInt32(reader["Scale"]),
							Convert.ToString(reader["Description"])
							)
						);
				}
			}

			foreach (ViewColumnDescriptor column in columns)
			{
				if (tables.ContainsKey(column.ViewName))
				{
					tables[column.ViewName].Columns.Add(column);
				}
			}

			return tables;
		}

		#endregion
	}
}