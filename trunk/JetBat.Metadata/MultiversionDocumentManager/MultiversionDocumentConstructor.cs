using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JetBat.Metadata.MultiversionDocumentManager
{
	public class MultiversionDocumentConstructor : BusinessObjectOnTableAndViewConstructor
	{
		private readonly MultiversionDocumentConstructorSettings settings;
		private MultiversionDocument instance;

		public MultiversionDocumentConstructor(MultiversionDocumentConstructorSettings settings, MetadataContainer context)
			: base(context)
		{
			this.settings = settings;
			objectNamespace = settings.EntityNamespace;
			objectName = settings.EntityName;
			friendlyName = settings.EntityName;
			databaseTable = loadDatabaseTable(settings.TableName);
			databaseView = loadDatabaseView(settings.ViewName);
			ReadOnlyColumns = new List<string>(settings.ReadOnlyColumns);
			HeaderNullableColumns = settings.HeaderNullableColumns != null ? new List<string>(settings.HeaderNullableColumns) : new List<string>();
			HiddenColumns = new List<string>(settings.InvisibleColumns);
		}

		#region Business object construction

		public override void Load()
		{
			instance = null;
			bool exists =
				context.BusinessObject.OfType<MultiversionDocument>().Any(businessObject => businessObject.Name == objectName && businessObject.Name == objectNamespace);

			if (exists)
			{
				instance = context.BusinessObject.OfType<MultiversionDocument>()
					.Include("Namespace")
					.Include("Attributes")
					.Include("Attributes.StoredProcedureParameterBindings")
					.Include("ComplexAttributes")
					.Include("Actions")
					.Include("Methods")
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
			persist();
			CreateMethodCreate();
			CreateMethodStartEdit();
			CreateMethodUpdateVersion();
			CreateMethodConfirmEdit();
			CreateMethodCancelEdit();
			CreateMethodLoad();
			CreateMethodDelete();
			CreateMethodCommit();
			CreateMethodRollback();
			transaction.Commit();
		}

		private void persist()
		{
			Load();

			if (instance != null)
				clear();
			else
			{
				instance = new MultiversionDocument
							{
								ObjectType = loadObjectType("MultiversionDocument"),
								Namespace = loadNamespace(objectNamespace),
								Name = objectName,
								FriendlyName = friendlyName,
								DatabaseTable = databaseTable,
								DatabaseView = databaseView,
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

			foreach (object objectToDelete in objectsToDelete)
			{
				context.DeleteObject(objectToDelete);
			}
		}

		protected override void createUserActions()
		{
			var action = new ObjectAction
							{
								Name = "Create",
								FriendlyName = "Create",
								UIFullText = "Создать",
								UIBriefText = "Создать",
								Enabled = true
							};
			instance.Actions.Add(action);
			instance.ActionCreate = action;

			action = new ObjectAction
						{
							Name = "StartEdit",
							FriendlyName = "StartEdit",
							UIFullText = "Редактировать",
							UIBriefText = "Редактировать",
							Enabled = true
						};
			instance.Actions.Add(action);
			instance.ActionStartEdit = action;

			action = new ObjectAction
						{
							Name = "ConfirmEdit",
							FriendlyName = "ConfirmEdit",
							UIFullText = "Сохранить",
							UIBriefText = "Сохранить",
							Enabled = true
						};
			instance.Actions.Add(action);
			instance.ActionConfirmEdit = action;

			action = new ObjectAction
						{
							Name = "CancelEdit",
							FriendlyName = "CancelEdit",
							UIFullText = "Просмотреть",
							UIBriefText = "Просмотреть",
							Enabled = true
						};
			instance.Actions.Add(action);
			instance.ActionCancelEdit = action;

			action = new ObjectAction
						{
							Name = "Delete",
							FriendlyName = "Delete",
							UIFullText = "Удалить",
							UIBriefText = "Удалить",
							Enabled = true
						};
			instance.Actions.Add(action);
			instance.ActionDelete = action;

			action = new ObjectAction
						{
							Name = "Commit",
							FriendlyName = "Commit",
							UIFullText = "Провести",
							UIBriefText = "Провести",
							Enabled = true
						};
			instance.Actions.Add(action);
			instance.ActionCommit = action;

			action = new ObjectAction
						{
							Name = "Rollback",
							FriendlyName = "Rollback",
							UIFullText = "Откатить",
							UIBriefText = "Откатить",
							Enabled = true
						};
			instance.Actions.Add(action);
			instance.ActionRollback = action;

			action = new ObjectAction
						{
							Name = "Pick",
							FriendlyName = "Pick",
							UIFullText = "Выбрать",
							UIBriefText = "Выбрать",
							Enabled = true
						};
			instance.Actions.Add(action);
			instance.ActionPick = action;
		}

		protected override void createComplexAttributes()
		{
			var aliases = new Dictionary<string, AttributeAlias>(settings.ComplexAttributeAliases.Length);
			foreach (var attributeAlias in settings.ComplexAttributeAliases)
				aliases.Add(attributeAlias.Name, attributeAlias);

			foreach (DatabaseForeignKey foreignKey in databaseTable.ForeignKeysAsPrimaryKeyTable)
			{
				#region required
				bool required = false;
				foreach (DatabaseForeignKeyColumnPair columnPair in foreignKey.ColumnPairs)
				{
					if (!columnPair.ForeignKeyColumn.AllowNull)
					{
						required = true;
						break;
					}
				}
				#endregion

				#region included into view
				bool includedIntoView = true;
				foreach (DatabaseForeignKeyColumnPair columnPair in foreignKey.ColumnPairs)
				{
					if (getViewColumn(columnPair.ForeignKeyColumn.Name) == null)
					{
						includedIntoView = false;
						break;
					}
				}
				#endregion

				#region includes primary key
				bool includesPrimaryKey = false;
				foreach (DatabaseForeignKeyColumnPair columnPair in foreignKey.ColumnPairs)
				{
					if (columnPair.ForeignKeyColumn.IsPrimaryKeyMember)
					{
						includesPrimaryKey = true;
						break;
					}
				}
				#endregion

				if (includedIntoView && !includesPrimaryKey)
				{
					var attribute = new ObjectComplexAttribute
										{
											ForeignKey = foreignKey,
											Name = foreignKey.Name,
											FriendlyName = aliases.ContainsKey(foreignKey.Name) ? aliases[foreignKey.Name].FriendlyName : foreignKey.Name,
											Description = null,
											UILabel = aliases.ContainsKey(foreignKey.Name) ? aliases[foreignKey.Name].UILabel : foreignKey.Name,
											UIPreferredIndex = aliases.ContainsKey(foreignKey.Name) ? aliases[foreignKey.Name].UIPreferredIndex : 0,
											Required = required
										};
					instance.ComplexAttributes.Add(attribute);
				}
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
			DatabaseTableColumn targetColumn = getTableColumn(columnName);
			if (targetColumn != null)
				return targetColumn.AllowNull;
			return true;
		}

		#endregion

		#region Method construction

		public void CreateMethodCreate()
		{
			instance.MethodCreate = createMethod("Create", "Создать", createStoredProcedureParametersCreate, false, instance);
			context.SaveChanges();
		}

		public void CreateMethodStartEdit()
		{
			instance.MethodStartEdit = createMethod("StartEdit", "Редактировать", createStoredProcedureParametersStartEdit, false, instance);
			context.SaveChanges();
		}

		public void CreateMethodUpdateVersion()
		{
			instance.MethodUpdateVersion = createMethod("UpdateVersion", "ИзменитьВерсию", createStoredProcedureParametersUpdateVersion, true, instance);
			context.SaveChanges();
		}

		public void CreateMethodConfirmEdit()
		{
			instance.MethodConfirmEdit = createMethod("ConfirmEdit", "Сохранить", createStoredProcedureParametersConfirmEdit, true, instance);
			context.SaveChanges();
		}

		public void CreateMethodCancelEdit()
		{
			instance.MethodCancelEdit = createMethod("CancelEdit", "Отменить", createStoredProcedureParametersCancelEdit, false, instance);
			context.SaveChanges();
		}

		public void CreateMethodDelete()
		{
			instance.MethodDelete = createMethod("Delete", "Удалить", createStoredProcedureParametersDelete, false, instance);
			context.SaveChanges();
		}

		public void CreateMethodLoad()
		{
			instance.MethodLoad = createMethod("Load", "Загрузить", createStoredProcedureParametersLoad, false, instance);
			context.SaveChanges();
		}

		public void CreateMethodCommit()
		{
			instance.MethodCommit = createMethod("Commit", "Провести", createStoredProcedureParametersCommit, true, instance);
			context.SaveChanges();
		}

		public void CreateMethodRollback()
		{
			instance.MethodRollback = createMethod("Rollback", "Откатить", createStoredProcedureParametersRollback, true, instance);
			context.SaveChanges();
		}

		private void createStoredProcedureParametersCreate(DatabaseStoredProcedure procedure)
		{
			#region DocumentID

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@DocumentID",
									IsOutput = true,
									DataType = loadDataType("int"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "DocumentID"
							};
			parameter.Bindings.Add(binding);

			#endregion

			#region VersionID

			parameter = new DatabaseStoredProcedureParameter
							{
								Name = "@VersionID",
								IsOutput = true,
								DataType = loadDataType("int"),
								MaxLength = 0,
								Precision = 0,
								Scale = 0
							};
			procedure.Parameters.Add(parameter);

			binding = new Metadata_DBStoredProcedureParameterBinding
						{
							OwnerObject = instance,
							Metadata_ObjectAttribute = null,
							AlternativeName = "VersionID"
						};
			parameter.Bindings.Add(binding);

			#endregion
		}

		private void createStoredProcedureParametersStartEdit(DatabaseStoredProcedure procedure)
		{
			#region DocumentID

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@DocumentID",
									IsOutput = false,
									DataType = loadDataType("int"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "DocumentID"
							};
			parameter.Bindings.Add(binding);

			#endregion

			#region VersionID

			parameter = new DatabaseStoredProcedureParameter
							{
								Name = "@VersionID",
								IsOutput = true,
								DataType = loadDataType("int"),
								MaxLength = 0,
								Precision = 0,
								Scale = 0
							};
			procedure.Parameters.Add(parameter);

			binding = new Metadata_DBStoredProcedureParameterBinding
						{
							OwnerObject = instance,
							Metadata_ObjectAttribute = null,
							AlternativeName = "VersionID"
						};
			parameter.Bindings.Add(binding);

			#endregion
		}

		private void createStoredProcedureParametersConfirmEdit(DatabaseStoredProcedure procedure)
		{
			#region VersionID

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@VersionID",
									IsOutput = false,
									DataType = loadDataType("int"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "VersionID"
							};
			parameter.Bindings.Add(binding);

			#endregion
		}

		private void createStoredProcedureParametersCancelEdit(DatabaseStoredProcedure procedure)
		{
			#region VersionID

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@VersionID",
									IsOutput = false,
									DataType = loadDataType("int"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "VersionID"
							};
			parameter.Bindings.Add(binding);

			#endregion
		}

		private void createStoredProcedureParametersLoad(DatabaseStoredProcedure procedure)
		{
			#region DocumentID

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@DocumentID",
									IsOutput = false,
									DataType = loadDataType("int"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "DocumentID"
							};
			parameter.Bindings.Add(binding);

			#endregion

			#region CurrentVersionID

			parameter = new DatabaseStoredProcedureParameter
							{
								Name = "@CurrentVersionID",
								IsOutput = true,
								DataType = loadDataType("int"),
								MaxLength = 0,
								Precision = 0,
								Scale = 0
							};
			procedure.Parameters.Add(parameter);

			binding = new Metadata_DBStoredProcedureParameterBinding
						{
							OwnerObject = instance,
							Metadata_ObjectAttribute = null,
							AlternativeName = "CurrentVersionID"
						};
			parameter.Bindings.Add(binding);

			#endregion

			#region View columns

			foreach (DatabaseViewColumn viewColumn in databaseView.Columns)
			{
				DatabaseTableColumn tableColumn = getTableColumn(viewColumn.Name);
				if
					(
					(tableColumn == null || tableColumn.IsPrimaryKeyMember == false) &&
					(viewColumn.Name != "DocumentVersionID")
					)
				{
					parameter = new DatabaseStoredProcedureParameter
									{
										Name = "@" + viewColumn.Name,
										IsOutput = true,
										DataType = loadDataType(viewColumn.DataType.Name),
										MaxLength = viewColumn.MaxLength,
										Precision = viewColumn.Precision,
										Scale = viewColumn.Scale
									};
					procedure.Parameters.Add(parameter);

					binding = new Metadata_DBStoredProcedureParameterBinding
								{
									OwnerObject = instance,
									Metadata_ObjectAttribute = getObjectAttribute(viewColumn.Name, instance),
									AlternativeName = viewColumn.Name
								};
					parameter.Bindings.Add(binding);
				}
			}

			#endregion
		}

		private void createStoredProcedureParametersDelete(DatabaseStoredProcedure procedure)
		{
			#region DocumentID

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@DocumentID",
									IsOutput = false,
									DataType = loadDataType("int"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "DocumentID"
							};
			parameter.Bindings.Add(binding);

			#endregion
		}

		private void createStoredProcedureParametersCommit(DatabaseStoredProcedure procedure)
		{
			#region DocumentID

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@DocumentID",
									IsOutput = false,
									DataType = loadDataType("int"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "DocumentID"
							};
			parameter.Bindings.Add(binding);

			#endregion
		}

		private void createStoredProcedureParametersRollback(DatabaseStoredProcedure procedure)
		{
			#region DocumentID

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@DocumentID",
									IsOutput = false,
									DataType = loadDataType("int"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "DocumentID"
							};
			parameter.Bindings.Add(binding);

			#endregion
		}

		private void createStoredProcedureParametersUpdateVersion(DatabaseStoredProcedure procedure)
		{
			#region VersionID

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@VersionID",
									IsOutput = false,
									DataType = loadDataType("int"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "VersionID"
							};
			parameter.Bindings.Add(binding);

			#endregion

			#region Table columns

			foreach (DatabaseTableColumn tableColumn in databaseTable.Columns)
				if
					(
					!tableColumn.IsPrimaryKeyMember &&
					!(tableColumn.Name == "DocumentVersionID") &&
					!columnIsReadOnly(tableColumn.Name)
					)
				{
					parameter = new DatabaseStoredProcedureParameter
									{
										Name = "@" + tableColumn.Name,
										IsOutput = false,
										DataType = loadDataType(tableColumn.DataType.Name),
										MaxLength = tableColumn.MaxLength,
										Precision = tableColumn.Precision,
										Scale = tableColumn.Scale
									};
					procedure.Parameters.Add(parameter);

					binding = new Metadata_DBStoredProcedureParameterBinding
								{
									OwnerObject = instance,
									Metadata_ObjectAttribute = getObjectAttribute(tableColumn.Name, instance),
									AlternativeName = tableColumn.Name
								};
					parameter.Bindings.Add(binding);
				}

			#endregion
		}

		#endregion

		#region Read only attibutes

		public List<string> ReadOnlyColumns { get; set; }

		public bool columnIsReadOnly(string columnName)
		{
			if (ReadOnlyColumns != null && ReadOnlyColumns.Contains(columnName))
				return true;
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

		#region Header nullable attributes

		public List<string> HeaderNullableColumns { get; set; }

		public bool headerColumnIsNullable(string columnName)
		{
			if (HeaderNullableColumns != null && HeaderNullableColumns.Contains(columnName))
				return true;
			return false;
		}

		#endregion
	}
}