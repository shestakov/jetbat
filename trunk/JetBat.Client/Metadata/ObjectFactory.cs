// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable UseObjectOrCollectionInitializer
using System;
using System.Collections;
using System.Data;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Metadata
{
	public class ObjectFactory : IObjectFactory
	{
		#region Инициализация

		public ObjectFactory(MetadataStore metadataStore, IAccessProvider accessProvider)
		{
			if (metadataStore == null)
				throw new ArgumentNullException("metadataStore");
			if (accessProvider == null)
				throw new ArgumentNullException("accessProvider");

			this.metadataStore = metadataStore;
			this.accessProvider = accessProvider;
		}

		#endregion

		private readonly IAccessProvider accessProvider;
		private readonly MetadataStore metadataStore;

		#region IObjectFactory Members

		public ObjectInstance New<T>(string entityNamespace, string entityName) where T : InstancedObjectDefinition
		{
			T descriptor = metadataStore.Get<T>(entityNamespace, entityName);

			if (descriptor == null)
				throw new Exception(string.Format("Объект [{0}]{1} не зарегистрирован!", entityNamespace, entityName));

			return descriptor.New(accessProvider);
		}

		#region Выполнение сохраненного запроса и формирование списка атрибутов объекта

		public DataTable ExecuteStoredQuery(string objectNamespace, string objectName, AttributeValueSet parameterValueSets, out ObjectListViewDefinition storedQueryDefinition)
		{
			ObjectListViewDefinition definition = metadataStore.Get<ObjectListViewDefinition>(objectNamespace, objectName);
			if (definition == null)
				throw new Exception(string.Format("Сохраненный запрос [{0}]{1} не зарегистрирован!", objectNamespace, objectName));

			AttributeValueSet procedureParameters = new AttributeValueSet();
			if (parameterValueSets != null)
			{
				NamedObjectReadOnlyCollection<ObjectMethodParameterDefinition> parameters =
					definition.MethodParameterDefinitionsLoadList;
				foreach (ObjectMethodParameterDefinition parameter in parameters)
				{
					if
						(
						(parameter.Direction == SqlParameterDirection.Input ||
						 parameter.Direction == SqlParameterDirection.InputOutput) &&
						(parameterValueSets.ContainsKey(parameter.AlternativeName))
						)
					{
						procedureParameters.Add(parameter.AlternativeName, parameterValueSets[parameter.AlternativeName]);
					}
				}
			}

			DataTable[] resultSet;
			accessProvider.ExecuteProcedure(objectNamespace, objectName, "LoadList", procedureParameters, out resultSet, null);

			DataTable resultTable = null;
			if (resultSet != null && resultSet[0] != null)
			{
				resultTable = resultSet[0];
				resultTable.TableName = "[" + definition.Namespace + "].[" + definition.Name + "]";
			}
			if (resultTable == null)
			{
				resultTable = ConstructTable(definition);
			}
			if (definition.AddUnexpectedAttributes)
			{
				foreach (DataColumn column in resultTable.Columns)
				{
					//TODO: set SqlDbType too
					//TODO: determine precision and scale
					ObjectAttribute objectAttribute = new ObjectAttribute
														{
															Name = column.ColumnName,
															FriendlyName = column.ColumnName,
															DataType = column.DataType.ToString(),
															SqlDbType = "SqlDbType.Int",
															DateTimeFormatID = 1,
															IsNullable = false,
															IsReadOnly = true,
															IsExternal = true,
															IsUserVisible = true,
															IsPrimaryKeyMember = false,
															MaxLength = 0,
															Precision = 0,
															Scale = 0,
															UILabel = column.ColumnName,
															UIPreferredWidth = 60
														};
					ObjectAttributeDefinition attribute = new ObjectAttributeDefinition(objectAttribute);
					definition.AddUnexpectedAttribute(attribute);
				}
			}

			storedQueryDefinition = definition;
			return resultTable;
		}

		#endregion

		#region Конструирование и загрузка таблицы представления списка объектов

		public DataTable LoadObjectListView(string objectNamespace, string objectName, AttributeValueSet parameterValueSets)
		{
			ObjectListViewDefinition output;
			return LoadObjectListView(objectNamespace, objectName, parameterValueSets, out output);
		}

		public DataTable LoadObjectListView(string objectNamespace, string objectName, AttributeValueSet parameterValueSets, out ObjectListViewDefinition storedQueryDefinition)
		{
			ObjectListViewDefinition definition = metadataStore.Get<ObjectListViewDefinition>(objectNamespace, objectName);
			if (definition == null)
				throw new Exception(
					string.Format("Представление списка [{0}]{1} не зарегистрировано!", objectNamespace, objectName));

			AttributeValueSet procedureParameters = new AttributeValueSet();
			if (parameterValueSets != null)
			{
				NamedObjectReadOnlyCollection<ObjectMethodParameterDefinition> parameters =
					definition.MethodParameterDefinitionsLoadList;
				foreach (ObjectMethodParameterDefinition parameter in parameters)
				{
					if
						(
						(parameter.Direction == SqlParameterDirection.Input ||
						 parameter.Direction == SqlParameterDirection.InputOutput) &&
						(parameterValueSets.ContainsKey(parameter.AlternativeName))
						)
					{
						procedureParameters.Add(parameter.AlternativeName, parameterValueSets[parameter.AlternativeName]);
					}
				}
			}

			DataTable[] resultSet;
			accessProvider.ExecuteProcedure(objectNamespace, objectName, "LoadList", procedureParameters, out resultSet, null);

			DataTable resultTable = null;
			if (resultSet != null && resultSet[0] != null)
			{
				resultTable = resultSet[0];
				resultTable.TableName = "[" + definition.Namespace + "].[" + definition.Name + "]";
			}

			if (resultTable == null)
			{
				resultTable = ConstructTable(definition);
			}
			if (definition.AddUnexpectedAttributes)
			{
				foreach (DataColumn column in resultTable.Columns)
				{
					//TODO: set SqlDbType too
					//TODO: determine precision and scale
					if (!definition.Attributes.Contains(column.ColumnName))
					{
						ObjectAttribute objectAttribute = new ObjectAttribute
						{
							Name = column.ColumnName,
							FriendlyName = column.ColumnName,
							DataType = column.DataType.ToString(),
							SqlDbType = "SqlDbType.Int",
							DateTimeFormatID = 1,
							IsNullable = false,
							IsReadOnly = true,
							IsExternal = true,
							IsUserVisible = true,
							IsPrimaryKeyMember = false,
							MaxLength = 0,
							Precision = 0,
							Scale = 0,
							UILabel = column.ColumnName,
							UIPreferredWidth = 60
						};
						ObjectAttributeDefinition attribute = new ObjectAttributeDefinition(objectAttribute);
						definition.AddUnexpectedAttribute(attribute);
					}
				}
			}
			storedQueryDefinition = definition;
			return resultTable;
		}

		public static DataTable ConstructTable(ObjectListViewDefinition definition)
		{
			DataTable resultTable = new DataTable("[" + definition.Namespace + "].[" + definition.Name + "]");
			ArrayList primaryKey = new ArrayList();
			foreach (ObjectAttributeDefinition attribute in definition.Attributes)
			{
				DataColumn column = new DataColumn(attribute.Name, attribute.DataType);
				column.Caption = attribute.UILabel;
				column.DefaultValue = attribute.DefaultValue;
				if (attribute.DataType == typeof(string))
					column.MaxLength = attribute.MaxLength;
				column.ReadOnly = attribute.IsReadOnly;
				resultTable.Columns.Add(column);
				if (attribute.IsPrimaryKeyMember)
					primaryKey.Add(column);
			}
			resultTable.PrimaryKey = (DataColumn[])primaryKey.ToArray(typeof(DataColumn));
			return resultTable;
		}

		#endregion

		#endregion
	}
}
// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore SuggestUseVarKeywordEvident