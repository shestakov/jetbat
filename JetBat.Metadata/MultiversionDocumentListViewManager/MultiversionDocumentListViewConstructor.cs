using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JetBat.DatabaseSchema;

namespace JetBat.Metadata.MultiversionDocumentListViewManager
{
	public class MultiversionDocumentListViewConstructor : BusinessObjectOnTableAndViewConstructor
	{
		private readonly MultiversionDocumentListViewConstructorSettings settings;
		private readonly string uiCaption;
		private readonly MultiversionDocument plainObject;
		private MultiversionDocumentListView instance;

		public MultiversionDocumentListViewConstructor(MultiversionDocumentListViewConstructorSettings settings, MetadataContainer context)
			: base(context)
		{
			this.settings = settings;
			objectNamespace = settings.EntityNamespace;
			objectName = settings.EntityName;
			friendlyName = settings.EntityName;
			uiCaption = settings.UICaption;

			plainObject = context.BusinessObject.OfType<MultiversionDocument>().Include("DatabaseTable").Where(string.Format("it.name = '{0}' and it.Namespace.Name = '{1}'", settings.TargetObjectName, settings.TargetObjectNamespace)).FirstOrDefault();
			if (plainObject == null) throw new Exception(string.Format("Basic Plain Object [{0}].[{1}] for a Plain Object List View is not found.", settings.TargetObjectNamespace, settings.TargetObjectName));
			databaseTable = loadDatabaseTable(plainObject.DatabaseTable.Name);
			databaseView = loadDatabaseView(settings.ViewName);
			HiddenColumns = new List<string>(settings.InvisibleColumns);
		}

		#region Business object construction

		public override void Load()
		{
			instance = null;
			bool exists =
				context.BusinessObject.OfType<MultiversionDocumentListView>().Any(businessObject => businessObject.Name == objectName && businessObject.Name == objectNamespace);

			if (exists)
			{
				instance = context.BusinessObject.OfType<MultiversionDocumentListView>()
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
			CreateMethodLoadList();
			transaction.Commit();
		}

		private void persist()
		{
			Load();

			if (instance != null)
				clear();
			else
			{
				instance = new MultiversionDocumentListView
							{
								ObjectType = loadObjectType("MultiversionDocumentListView"),
								Namespace = loadNamespace(objectNamespace),
								Name = objectName,
								FriendlyName = friendlyName,
								DatabaseView = databaseView,
								TargetMultiversionDocument = plainObject,
								UIListCaption = uiCaption
							};
				context.AddToBusinessObject(instance);
			}

			createAttributes();
			createComplexAttributes();
			createUserActions();

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
								Name = "LoadList",
								FriendlyName = "LoadList",
								UIFullText = "Загрузить список",
								UIBriefText = "Обновить",
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
										UIPreferredIndex = aliases.ContainsKey(foreignKey.Name) ? aliases[foreignKey.Name].UIPreferredIndex : 0,
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
										IsReadOnly = true,
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

		public void CreateMethodLoadList()
		{
			instance.MethodLoadList = createMethod("LoadList", "Загрузить список", createStoredProcedureParametersLoadList, false, instance);
			context.SaveChanges();
		}

		private void createStoredProcedureParametersLoadList(DatabaseStoredProcedure procedure)
		{
			#region StartDateTime

			var parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@StartDateTime",
									IsOutput = false,
									DataType = loadDataType("datetime"),
									MaxLength = 0,
									Precision = 0,
									Scale = 0
								};
			procedure.Parameters.Add(parameter);

			var binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = "StartDateTime"
							};
			parameter.Bindings.Add(binding);

			#endregion

			#region EndDateTime

			parameter = new DatabaseStoredProcedureParameter
							{
								Name = "@EndDateTime",
								IsOutput = false,
								DataType = loadDataType("datetime"),
								MaxLength = 0,
								Precision = 0,
								Scale = 0
							};
			procedure.Parameters.Add(parameter);

			binding = new Metadata_DBStoredProcedureParameterBinding
						{
							OwnerObject = instance,
							Metadata_ObjectAttribute = null,
							AlternativeName = "EndDateTime"
						};
			parameter.Bindings.Add(binding);

			#endregion

			foreach (StoredProcedureParameterSchema parameterDefinition in parameterDefinitions)
			{
				parameter = new DatabaseStoredProcedureParameter
								{
									Name = "@" + parameterDefinition.Name,
									IsOutput = parameterDefinition.IsOutput,
									DataType = loadDataType(parameterDefinition.DataTypeName),
									MaxLength = parameterDefinition.MaxLength,
									Precision = parameterDefinition.Precision,
									Scale = parameterDefinition.Scale
								};
				procedure.Parameters.Add(parameter);

				binding = new Metadata_DBStoredProcedureParameterBinding
							{
								OwnerObject = instance,
								Metadata_ObjectAttribute = null,
								AlternativeName = parameterDefinition.Name
							};
				parameter.Bindings.Add(binding);
			}
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

		#region LoadList procedure parameters

		private readonly List<StoredProcedureParameterSchema> parameterDefinitions = new List<StoredProcedureParameterSchema>();

		public void AddStoredProceudreParameter(StoredProcedureParameterSchema parameterDefinition)
		{
			parameterDefinitions.Add(parameterDefinition);
		}

		#endregion
	}
}