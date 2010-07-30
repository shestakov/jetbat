using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using JetBat.DatabaseSchema;
using JetBat.Metadata.Templates.PlainObjectListView;

namespace JetBat.Metadata.PlainObjectListViewManager
{
	public static class PlainObjectListViewTemplateRenderer
	{
		public static void RenderMethods(PlainObjectListViewConstructorSettings constructorSettings, StringBuilder stringBuilder, string targetDatabaseName, string connectionStringToMetadataStore, MetadataContainer metadataContext)
		{
			executeCodeTemplate(constructorSettings, stringBuilder, metadataContext, targetDatabaseName, connectionStringToMetadataStore);
		}

		private static void executeCodeTemplate(PlainObjectListViewConstructorSettings settings, StringBuilder stringBuilder, MetadataContainer context, string targetDatabaseName, string connectionStringToMetadataStore)
		{
			PlainObjectListViewStoredProcedureTemplate template = new PlainObjectListViewStoredProcedureTemplate();

			context.Connection.Close();
			context.Connection.Open();
			var plainObject =
				context.BusinessObject.OfType<PlainObject>().Include("DatabaseTable").Include("LogicalDeletionTableColumn").Where(
					string.Format("it.name = '{0}' and it.Namespace.Name = '{1}'", settings.TargetObjectName,
					              settings.TargetObjectNamespace)).FirstOrDefault();
			if (plainObject == null)
				throw new Exception(string.Format("Basic Plain Object [{0}].[{1}] for a Plain Object List View is not found.",
				                                  settings.TargetObjectNamespace, settings.TargetObjectName));

			List<StoredProcedureParameterSchema> parameters;
			if (settings.ParameterDefinitions != null)
			{
				parameters = new List<StoredProcedureParameterSchema>(settings.ParameterDefinitions.Count());
				foreach (var definition in settings.ParameterDefinitions)
				{
					parameters.Add(new StoredProcedureParameterSchema(definition.Name, definition.DataTypeName, definition.IsOutput,
					                                                  definition.MaxLength, definition.Precision, definition.Scale,
					                                                  definition.Description));
				}
			}
			else
			{
				parameters = new List<StoredProcedureParameterSchema>();
			}

			template.ConnectionString = connectionStringToMetadataStore;
			template.DatabaseName = targetDatabaseName;
			template.DeleteFlagColumnName = plainObject.LogicalDeletionTableColumn != null ? plainObject.LogicalDeletionTableColumn.Name : null;
			template.EntityName = settings.EntityName;
			template.EntityNamespace = settings.EntityNamespace;

			template.HiddenColumnList = new StringCollection();
			foreach (var invisibleColumn in settings.InvisibleColumns)
			{
				template.HiddenColumnList.Add(invisibleColumn);
			}

			template.IgnoredColumnList = new StringCollection();
			foreach (var ignoredColumn in settings.IgnoredColumns)
			{
				template.IgnoredColumnList.Add(ignoredColumn);
			}

			template.NamespacePrefix = settings.EntityNamespace;
			template.TableName = plainObject.DatabaseTable.Name;
			template.ShowDeleted = settings.ShowDeletedObjects;
			template.UICaption = settings.UICaption;
			template.ViewName = settings.ViewName;
			template.SelectionCondition = settings.SelectionCondition;
			template.OrderBy = settings.OrderBy;
			template.ParameterDefinitions = parameters;

			template.PreRender();

			stringBuilder.AppendLine();
			stringBuilder.AppendLine(template.TransformText());
			stringBuilder.AppendLine();
		}
	}
}