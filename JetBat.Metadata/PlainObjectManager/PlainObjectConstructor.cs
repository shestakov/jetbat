using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;

namespace JetBat.Metadata.PlainObjectManager
{
	public class PlainObjectConstructor : BusinessObjectOnTableAndViewConstructor
	{
		private readonly PlainObjectConstructorSettings settings;
		private readonly string uiEditorName;
		private PlainObject instance;


		public PlainObjectConstructor(PlainObjectConstructorSettings settings, MetadataContainer context)
			: base(context)
		{
			this.settings = settings;
			objectNamespace = settings.EntityNamespace;
			objectName = settings.EntityName;
			friendlyName = settings.EntityName;
			uiEditorName = settings.UIEditorName;
			databaseTable = loadDatabaseTable(settings.TableName);
			databaseView = loadDatabaseView(settings.ViewName);
			ReadOnlyColumns = new List<string>(settings.ReadOnlyColumns);
			HiddenColumns = new List<string>(settings.InvisibleColumns);
		}

		#region Business object construction

		public override void Load()
		{
			instance = null;
			bool exists =
				context.BusinessObject.OfType<PlainObject>().Any(businessObject => businessObject.Name == objectName && businessObject.Name == objectNamespace);

			if (exists)
			{
				instance = context.BusinessObject.OfType<PlainObject>()
					.Include("Namespace")
					.Include("Attributes")
					.Include("Attributes.StoredProcedureParameterBindings")
					.Include("ComplexAttributes")
					.Include("Actions")
					.Include("Methods")
					.Include("DatabaseStoredProcedureParameterBindings")
					.Where(string.Format("it.name = '{0}' and it.Namespace.Name = '{1}'", objectName, objectNamespace)).FirstOrDefault();
			}
			addNewAttributeAliases(settings, instance);
			addNewComplexAttributeAliases(settings, instance);
		}

		public override void Save()
		{
			if (context.Connection.State != ConnectionState.Open)
				context.Connection.Open();
			var transaction = context.Connection.BeginTransaction();
			SetDateTimeMarkColumn(settings.DateTimeMarkColumnName);
			SetLogicalDeletionColumn(settings.DeleteFlagColumnName);
			SetStatusColumn(settings.StatusColumnName);
			SetForeignKeyToParentObject(settings.ForeignKeyToParentObjectName);
			SetParentObject(settings.ParentObjectNamespace, settings.ParentObjectName);
			SetSequenceNumberColumn(settings.SequenceNumberColumnName);
			persist();
			if (settings.MethodInsert) CreateMethodInsert();
			if (settings.MethodUpdate) CreateMethodUpdate();
			if (settings.MethodLoad) CreateMethodLoad();
			if (settings.MethodDelete) CreateMethodDelete();
			if (settings.MethodRestore) CreateMethodRestore();
			if (settings.MethodCopyByParentObject) CreateMethodCopyByParentObject();
			if (settings.MethodDeleteByParentObject) CreateMethodDeleteByParentObject();
			transaction.Commit();
		}

		private void persist()
		{
			Load();
			if (instance != null)
				clear();
			else
			{
				instance = new PlainObject
							{
								ObjectType = loadObjectType("Entity"),
								Namespace = loadNamespace(objectNamespace),
								Name = objectName,
								FriendlyName = friendlyName,
								DatabaseTable = databaseTable,
								DatabaseView = databaseView,
								ParentObject = parentObject,
								UIEditorName = uiEditorName,
								ForeignKeyToParentObject = foreignKeyToParentObject
							};
				context.AddToBusinessObject(instance);
			}

			createAttributes();
			createComplexAttributes();
			createUserActions();
			createAdditionalMethods(instance, settings);

			context.SaveChanges();
		}

		protected override void clear()
		{
			var objectsToDelete = new List<object>();
			objectsToDelete.AddRange(instance.Attributes.ToArray());
			objectsToDelete.AddRange(instance.ComplexAttributes.ToArray());
			objectsToDelete.AddRange(instance.Actions.ToArray());
			objectsToDelete.AddRange(instance.Methods.ToArray());
			objectsToDelete.AddRange(instance.DatabaseStoredProcedureParameterBindings.ToArray());

			foreach (var objectToDelete in objectsToDelete)
			{
				context.DeleteObject(objectToDelete);
			}
		}

		protected override void createUserActions()
		{
			var action = new ObjectAction
							{
								Name = "Insert",
								FriendlyName = "Insert",
								UIFullText = "Добавить",
								UIBriefText = "Добавить",
								Enabled = true
							};
			instance.Actions.Add(action);

			action = new ObjectAction
						{
							Name = "Update",
							FriendlyName = "Update",
							UIFullText = "Изменить",
							UIBriefText = "Изменить",
							Enabled = true
						};
			instance.Actions.Add(action);

			action = new ObjectAction
						{
							Name = "Delete",
							FriendlyName = "Delete",
							UIFullText = "Удалить",
							UIBriefText = "Удалить",
							Enabled = true
						};
			instance.Actions.Add(action);

			action = new ObjectAction
						{
							Name = "View",
							FriendlyName = "View",
							UIFullText = "Просмотреть",
							UIBriefText = "Просмотреть",
							Enabled = true
						};
			instance.Actions.Add(action);

			action = new ObjectAction
						{
							Name = "Pick",
							FriendlyName = "Pick",
							UIFullText = "Выбрать",
							UIBriefText = "Выбрать",
							Enabled = true
						};
			instance.Actions.Add(action);
		}

		protected override void createComplexAttributes()
		{
			var aliases = new Dictionary<string, AttributeAlias>(settings.ComplexAttributeAliases.Length);
			foreach (var attributeAlias in settings.ComplexAttributeAliases)
				aliases.Add(attributeAlias.Name, attributeAlias);

			foreach (DatabaseForeignKey foreignKey in databaseTable.ForeignKeysAsPrimaryKeyTable)
			{
				bool required = false;
				foreach (DatabaseForeignKeyColumnPair columnPair in foreignKey.ColumnPairs)
				{
					if (!columnPair.ForeignKeyColumn.AllowNull)
					{
						required = true;
						break;
					}
				}
				var attribute = new ObjectComplexAttribute
									{
										ForeignKey = foreignKey,
										Name = foreignKey.Name,
										FriendlyName = aliases.ContainsKey(foreignKey.Name) ? aliases[foreignKey.Name].FriendlyName : foreignKey.Name,
										Description = null,
										UILabel = aliases.ContainsKey(foreignKey.Name) ? aliases[foreignKey.Name].UILabel : foreignKey.Name,
										UIPreferredIndex= aliases.ContainsKey(foreignKey.Name) ? aliases[foreignKey.Name].UIPreferredIndex: 0,
										Required = required
									};
				instance.ComplexAttributes.Add(attribute);
			}
		}

		protected override void createAttributes()
		{
			var aliases = new Dictionary<string, AttributeAlias>(settings.AttributeAliases.Length);
			foreach (var attributeAlias in settings.AttributeAliases)
				aliases.Add(attributeAlias.Name, attributeAlias);

			foreach (DatabaseViewColumn viewColumn in databaseView.Columns)
			{
				DatabaseTableColumn tableColumn = getTableColumn(viewColumn.Name);
				var attribute = new ObjectAttribute
									{
										Name = viewColumn.Name,
										FriendlyName = aliases.ContainsKey(viewColumn.Name) ? aliases[viewColumn.Name].FriendlyName : viewColumn.Name,
										UILabel = aliases.ContainsKey(viewColumn.Name) ? aliases[viewColumn.Name].UILabel : viewColumn.Name,
										DataType = loadDataType(viewColumn.DataType.Name),
										IsNullable = columnAllowsNull(viewColumn.Name),
										IsReadOnly = columnIsReadOnly(viewColumn.Name),
										IsExternal = (tableColumn == null),
										IsUserVisible = aliases.ContainsKey(viewColumn.Name) ? aliases[viewColumn.Name].IsUserVisible : (!columnIsHidden(viewColumn.Name)),
										IsPrimaryKeyMember = (tableColumn != null && tableColumn.IsPrimaryKeyMember),
										TableColumn = tableColumn,
										MaxLength = (tableColumn != null ? tableColumn.MaxLength : viewColumn.MaxLength),
										Precision = (tableColumn != null ? tableColumn.Precision : viewColumn.Precision),
										Scale = (tableColumn != null ? tableColumn.Scale : viewColumn.Scale),
										UIPreferredWidth = aliases.ContainsKey(viewColumn.Name) ? aliases[viewColumn.Name].UIPreferredWidth : 100,
										UIAllowMultilineText = aliases.ContainsKey(viewColumn.Name) ? aliases[viewColumn.Name].UIAllowsMultilineText : false,
										UIPreferredIndex = aliases.ContainsKey(viewColumn.Name) ? aliases[viewColumn.Name].UIPreferredIndex : 0,
										DateTimeFormat = null
									};
				instance.Attributes.Add(attribute);
			}
		}

		protected override bool columnAllowsNull(string columnName)
		{
			if (parentObject != null && sequenceNumberColumn != null && columnName == sequenceNumberColumn.Name)
				return true;

			DatabaseTableColumn targetColumn = getTableColumn(columnName);
			if (targetColumn != null)
				return targetColumn.AllowNull;
			return true;
		}

		#endregion

		#region Method construction

		public void CreateMethodDeleteByParentObject()
		{
			if (parentObject == null)
				throw new Exception("Can not create method DeleteByParentObject as ParentObject is not set");
			if (foreignKeyToParentObject == null)
				throw new Exception("Can not create method DeleteByParentObject as ForeignKeyToParentObject is not set");
			instance.MethodDeleteByParentObject = createMethod("DeleteByParentObject", "DeleteByParentObject", createStoredProcedureParametersDeleteByParentObject, true, instance);
			context.SaveChanges();
		}

		public void CreateMethodCopyByParentObject()
		{
			if (parentObject == null)
				throw new Exception("Can not create method CopyByParentObject as ParentObject is not set");
			if (foreignKeyToParentObject == null)
				throw new Exception("Can not create method CopyByParentObject as ForeignKeyToParentObject is not set");
			instance.MethodCopyByParentObject = createMethod("CopyByParentObject", "CopyByParentObject", createStoredProcedureParametersCopyByParentObject, true, instance);
			context.SaveChanges();
		}

		public void CreateMethodRestore()
		{
			if (logicalDeletionColumn == null)
				throw new Exception("Can not create method Restore as LogicalDeletionColumn is not set");
			//TODO: Add referenc to the method Restore into the Metadata_PlainObject table
			createMethod("Restore", "Восстановить", createStoredProcedureParametersRestore, true, instance);
			context.SaveChanges();
		}

		public void CreateMethodLoad()
		{
			instance.MethodLoad = createMethod("Load", "Загрузить", createStoredProcedureParametersLoad, true, instance);
			context.SaveChanges();
		}

		public void CreateMethodDelete()
		{
			instance.MethodDelete = createMethod("Delete", "Удалить", createStoredProcedureParametersDelete, true, instance);
			context.SaveChanges();
		}

		public void CreateMethodUpdate()
		{
			instance.MethodUpdate = createMethod("Update", "Изменить", createStoredProcedureParametersUpdate, true, instance);
			context.SaveChanges();
		}

		public void CreateMethodInsert()
		{
			instance.MethodInsert = createMethod("Insert", "Добавить", createStoredProcedureParametersInsert, true, instance);
			context.SaveChanges();
		}

		private void createStoredProcedureParametersDeleteByParentObject(DatabaseStoredProcedure procedure)
		{
			foreach (var columnPair in foreignKeyToParentObject.ColumnPairs)
			{
				var parameter = new DatabaseStoredProcedureParameter
									{
										Name = "@" + columnPair.ForeignKeyColumn.Name,
										IsOutput = false,
										DataType = loadDataType(columnPair.ForeignKeyColumn.DataType.Name),
										MaxLength = columnPair.ForeignKeyColumn.MaxLength,
										Precision = columnPair.ForeignKeyColumn.Precision,
										Scale = columnPair.ForeignKeyColumn.Scale
									};
				procedure.Parameters.Add(parameter);

				var binding = new Metadata_DBStoredProcedureParameterBinding
								{
									OwnerObject = instance,
									Metadata_ObjectAttribute = null,
									AlternativeName = columnPair.ForeignKeyColumn.Name
								};
				parameter.Bindings.Add(binding);
			}
		}

		private void createStoredProcedureParametersCopyByParentObject(DatabaseStoredProcedure procedure)
		{
			foreach (var columnPair in foreignKeyToParentObject.ColumnPairs)
			{
				var parameter = new DatabaseStoredProcedureParameter
									{
										Name = "@Source" + columnPair.ForeignKeyColumn.Name,
										IsOutput = false,
										DataType = loadDataType(columnPair.ForeignKeyColumn.DataType.Name),
										MaxLength = columnPair.ForeignKeyColumn.MaxLength,
										Precision = columnPair.ForeignKeyColumn.Precision,
										Scale = columnPair.ForeignKeyColumn.Scale
									};
				procedure.Parameters.Add(parameter);

				var binding = new Metadata_DBStoredProcedureParameterBinding
								{
									OwnerObject = instance,
									Metadata_ObjectAttribute = null,
									AlternativeName = "Source" + columnPair.ForeignKeyColumn.Name
								};
				parameter.Bindings.Add(binding);
			}

			foreach (var columnPair in foreignKeyToParentObject.ColumnPairs)
			{
				var parameter = new DatabaseStoredProcedureParameter
									{
										Name = "@Destination" + columnPair.ForeignKeyColumn.Name,
										IsOutput = false,
										DataType = loadDataType(columnPair.ForeignKeyColumn.DataType.Name),
										MaxLength = columnPair.ForeignKeyColumn.MaxLength,
										Precision = columnPair.ForeignKeyColumn.Precision,
										Scale = columnPair.ForeignKeyColumn.Scale
									};
				procedure.Parameters.Add(parameter);

				var binding = new Metadata_DBStoredProcedureParameterBinding
								{
									OwnerObject = instance,
									Metadata_ObjectAttribute = null,
									AlternativeName = "Destination" + columnPair.ForeignKeyColumn.Name
								};
				parameter.Bindings.Add(binding);
			}
		}

		private void createStoredProcedureParametersRestore(DatabaseStoredProcedure procedure)
		{
			foreach (DatabaseTableColumn tableColumn in databaseTable.Columns)
				if (tableColumn.IsPrimaryKeyMember || (dateTimeMarkColumn != null && dateTimeMarkColumn.Name == tableColumn.Name))
				{
					var parameter = new DatabaseStoredProcedureParameter
										{
											Name = "@" + tableColumn.Name,
											IsOutput = false,
											DataType = loadDataType(tableColumn.DataType.Name),
											MaxLength = tableColumn.MaxLength,
											Precision = tableColumn.Precision,
											Scale = tableColumn.Scale
										};
					procedure.Parameters.Add(parameter);

					var binding = new Metadata_DBStoredProcedureParameterBinding
									{
										OwnerObject = instance,
										Metadata_ObjectAttribute = getObjectAttribute(tableColumn.Name, instance),
										AlternativeName = tableColumn.Name
									};
					parameter.Bindings.Add(binding);
				}
		}

		private void createStoredProcedureParametersLoad(DatabaseStoredProcedure procedure)
		{
			foreach (DatabaseViewColumn viewColumn in databaseView.Columns)
			{
				DatabaseTableColumn tableColumn = getTableColumn(viewColumn.Name);
				var parameter = new DatabaseStoredProcedureParameter
									{
										Name = "@" + viewColumn.Name,
										IsOutput = (tableColumn == null || !tableColumn.IsPrimaryKeyMember),
										DataType = loadDataType(viewColumn.DataType.Name),
										MaxLength = viewColumn.MaxLength,
										Precision = viewColumn.Precision,
										Scale = viewColumn.Scale
									};
				procedure.Parameters.Add(parameter);

				var binding = new Metadata_DBStoredProcedureParameterBinding
								{
									OwnerObject = instance,
									Metadata_ObjectAttribute = getObjectAttribute(viewColumn.Name, instance),
									AlternativeName = viewColumn.Name
								};
				parameter.Bindings.Add(binding);
			}
		}

		private void createStoredProcedureParametersDelete(DatabaseStoredProcedure procedure)
		{
			foreach (DatabaseTableColumn tableColumn in databaseTable.Columns)
				if (tableColumn.IsPrimaryKeyMember || (dateTimeMarkColumn != null && dateTimeMarkColumn.Name == tableColumn.Name))
				{
					var parameter = new DatabaseStoredProcedureParameter
										{
											Name = "@" + tableColumn.Name,
											IsOutput = false,
											DataType = loadDataType(tableColumn.DataType.Name),
											MaxLength = tableColumn.MaxLength,
											Precision = tableColumn.Precision,
											Scale = tableColumn.Scale
										};
					procedure.Parameters.Add(parameter);

					var binding = new Metadata_DBStoredProcedureParameterBinding
									{
										OwnerObject = instance,
										Metadata_ObjectAttribute = getObjectAttribute(tableColumn.Name, instance),
										AlternativeName = tableColumn.Name
									};
					parameter.Bindings.Add(binding);
				}
		}

		private void createStoredProcedureParametersUpdate(DatabaseStoredProcedure procedure)
		{
			foreach (DatabaseTableColumn tableColumn in databaseTable.Columns)
				if
					(
					tableColumn.IsPrimaryKeyMember ||
					(dateTimeMarkColumn != null && dateTimeMarkColumn.Name == tableColumn.Name) ||
					(
						getViewColumn(tableColumn.Name) != null &&
						!columnIsHidden(tableColumn.Name) &&
						!columnIsReadOnly(tableColumn.Name)
					)
					)
				{
					var parameter = new DatabaseStoredProcedureParameter
										{
											Name = "@" + tableColumn.Name,
											IsOutput = (dateTimeMarkColumn != null && dateTimeMarkColumn.Name == tableColumn.Name),
											DataType = loadDataType(tableColumn.DataType.Name),
											MaxLength = tableColumn.MaxLength,
											Precision = tableColumn.Precision,
											Scale = tableColumn.Scale
										};
					procedure.Parameters.Add(parameter);

					var binding = new Metadata_DBStoredProcedureParameterBinding
									{
										OwnerObject = instance,
										Metadata_ObjectAttribute = getObjectAttribute(tableColumn.Name, instance),
										AlternativeName = tableColumn.Name
									};
					parameter.Bindings.Add(binding);
				}
		}

		private void createStoredProcedureParametersInsert(DatabaseStoredProcedure procedure)
		{
			foreach (DatabaseTableColumn tableColumn in databaseTable.Columns)
				if
					(
					tableColumn.IsPrimaryKeyMember ||
					(dateTimeMarkColumn != null && dateTimeMarkColumn.Name == tableColumn.Name) ||
					(
						getViewColumn(tableColumn.Name) != null &&
						!columnIsHidden(tableColumn.Name) &&
						!columnIsReadOnly(tableColumn.Name)
					)
					)
				{
					var parameter = new DatabaseStoredProcedureParameter
										{
											Name = "@" + tableColumn.Name,
											IsOutput =
												tableColumn.IsIdentity ||
												(dateTimeMarkColumn != null && dateTimeMarkColumn.Name == tableColumn.Name),
											DataType = loadDataType(tableColumn.DataType.Name),
											MaxLength = tableColumn.MaxLength,
											Precision = tableColumn.Precision,
											Scale = tableColumn.Scale
										};
					procedure.Parameters.Add(parameter);

					var binding = new Metadata_DBStoredProcedureParameterBinding
									{
										OwnerObject = instance,
										Metadata_ObjectAttribute = getObjectAttribute(tableColumn.Name, instance),
										AlternativeName = tableColumn.Name
									};
					parameter.Bindings.Add(binding);
				}
		}

		#endregion

		#region DateTime mark column

		private DatabaseTableColumn dateTimeMarkColumn;

		public void SetDateTimeMarkColumn(string columnName)
		{
			if (columnName == null)
			{
				DropDateTimeMarkColumn();
				return;
			}

			var query = context.DatabaseTableColumn.Include("OwnerTable").Where(string.Format("it.name = '{0}' and it.OwnerTable.Name = '{1}'", columnName, databaseTable.Name));
			foreach (DatabaseTableColumn column in query.Execute(MergeOption.AppendOnly))
			{
				dateTimeMarkColumn = column;
				return;
			}
			throw new ObjectNotFoundException(
				string.Format("Failed to set DateTimeMarkColumn. There is no column [{0}] within table [{1}]", columnName,
							  databaseTable.Name));
		}

		public void DropDateTimeMarkColumn()
		{
			dateTimeMarkColumn = null;
		}

		#endregion

		#region Status column

		private DatabaseTableColumn statusColumn;

		public void SetStatusColumn(string columnName)
		{
			if (columnName == null)
			{
				DropStatusColumn();
				return;
			}

			var query = context.DatabaseTableColumn.Include("OwnerTable").Where(string.Format("it.name = '{0}' and it.OwnerTable.Name = '{1}'", columnName, databaseTable.Name));
			foreach (DatabaseTableColumn column in query.Execute(MergeOption.AppendOnly))
			{
				statusColumn = column;
				return;
			}
			throw new ObjectNotFoundException(
				string.Format("Failed to set StatusColumn. There is no column [{0}] within table [{1}]", columnName,
							  databaseTable.Name));
		}

		public void DropStatusColumn()
		{
			statusColumn = null;
		}

		#endregion

		#region Logical deletion column

		private DatabaseTableColumn logicalDeletionColumn;

		public void SetLogicalDeletionColumn(string columnName)
		{
			if (columnName == null)
			{
				DropLogicalDeletionColumn();
				return;
			}

			ObjectQuery<DatabaseTableColumn> query =
				context.DatabaseTableColumn.Include("OwnerTable").Where(
					string.Format("it.name = '{0}' and it.OwnerTable.Name = '{1}'", columnName, databaseTable.Name));
			foreach (DatabaseTableColumn column in query.Execute(MergeOption.AppendOnly))
			{
				logicalDeletionColumn = column;
				return;
			}
			throw new ObjectNotFoundException(
				string.Format("Failed to set LogicalDeletionColumn. There is no column [{0}] within table [{1}]", columnName,
							  databaseTable.Name));
		}

		public void DropLogicalDeletionColumn()
		{
			logicalDeletionColumn = null;
		}

		#endregion

		#region Read only attibutes

		public List<string> ReadOnlyColumns { get; set; }

		public bool columnIsReadOnly(string columnName)
		{
			if (ReadOnlyColumns != null && ReadOnlyColumns.Contains(columnName)) return true;
			if (statusColumn != null && statusColumn.Name == columnName) return true;
			if (logicalDeletionColumn != null && logicalDeletionColumn.Name == columnName) return true;
			DatabaseTableColumn tableColumn = getTableColumn(columnName);
			if (tableColumn == null) return true;
			if (tableColumn.IsPrimaryKeyMember) return true;
			return false;
		}

		#endregion

		#region Hidden attibutes

		public List<string> HiddenColumns { get; set; }

		public bool columnIsHidden(string columnName)
		{
			if (HiddenColumns != null && HiddenColumns.Contains(columnName)) return true;
			return false;
		}

		#endregion

		#region Parent object and sequence number

		private BusinessObject parentObject;
		private DatabaseTableColumn sequenceNumberColumn;

		public void SetParentObject(string parentObjectNamespace, string parentObjectName)
		{
			if (parentObjectNamespace == null || parentObjectName == null)
			{
				DropParentObject();
				return;
			}

			ObjectQuery<BusinessObject> query =
				context.BusinessObject.Where(string.Format("it.name = '{0}' and it.Namespace.Name = '{1}'", parentObjectName,
														   parentObjectNamespace));
			foreach (BusinessObject metadataObject in query.Execute(MergeOption.AppendOnly))
			{
				parentObject = metadataObject;
				return;
			}
			throw new ObjectNotFoundException(string.Format("There is no object [{0}] within namespace [{1}]", parentObjectName,
															parentObjectNamespace));
		}

		public void DropParentObject()
		{
			parentObject = null;
		}

		public void SetSequenceNumberColumn(string columnName)
		{
			//if (parentObject == null)
			//{
			//    throw new Exception("SequenceNumberColumn can only be set when ParentObject is set");
			//}
			if (columnName == null)
			{
				DropSequenceNumberColumn();
				return;
			}
			ObjectQuery<DatabaseTableColumn> query =
				context.DatabaseTableColumn.Include("OwnerTable").Where(
					string.Format("it.name = '{0}' and it.OwnerTable.Name = '{1}'", columnName, databaseTable.Name));
			foreach (DatabaseTableColumn column in query.Execute(MergeOption.AppendOnly))
			{
				sequenceNumberColumn = column;
				return;
			}
			throw new ObjectNotFoundException(
				string.Format("Failed to set SequencaNumberColumn. There is no column [{0}] within table [{1}]", columnName,
							  databaseTable.Name));
		}

		public void DropSequenceNumberColumn()
		{
			sequenceNumberColumn = null;
		}

		#endregion

		#region Foreign key to parent object table

		private DatabaseForeignKey foreignKeyToParentObject;

		public void SetForeignKeyToParentObject(string foreignKeyName)
		{
			if (foreignKeyName == null)
			{
				DropForeignKeyToParentObject();
				return;
			}

			ObjectQuery<DatabaseForeignKey> query =
				context.DatabaseForeignKey
				.Include("PrimaryKeyTable")
				.Include("ColumnPairs")
				.Where(
					string.Format("it.name = '{0}'", foreignKeyName));
			foreach (DatabaseForeignKey foreignKey in query.Execute(MergeOption.AppendOnly))
			{
				if (foreignKey.PrimaryKeyTable.Name != databaseTable.Name)
					throw new Exception(
						"Failed to set ForeignKeyToParentObject. The foreign key exists, but does not end in a created object's table");
				foreignKeyToParentObject = foreignKey;
				return;
			}
			throw new ObjectNotFoundException(
				string.Format("Failed to set ForeignKeyToParentObject. There is no foreign key named [{0}]", foreignKeyName));
		}

		public void DropForeignKeyToParentObject()
		{
			foreignKeyToParentObject = null;
		}

		#endregion
	}
}