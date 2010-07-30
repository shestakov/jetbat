using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using JetBat.DatabaseSchema;
using JetBat.Metadata.Templates.MultiversionDocumentListView;

namespace JetBat.Metadata.MultiversionDocumentListViewManager
{
	public static class MultiversionDocumentListViewTemplateRenderer
	{
		public static void RenderMethods(MultiversionDocumentListViewConstructorSettings settings, StringBuilder stringBuilder, string targetDatabaseName, string connectionStringToMetadataStore, MetadataContainer context)
		{
			executeCodeTemplate(settings, stringBuilder, context, targetDatabaseName, connectionStringToMetadataStore);
		}

		private static void executeCodeTemplate(MultiversionDocumentListViewConstructorSettings settings, StringBuilder stringBuilder, MetadataContainer context, string targetDatabaseName, string connectionStringToMetadataStore)
		{
			var template = new MultiversionDocumentListViewStoredProcedureTemplate();

			context.Connection.Close();
			context.Connection.Open();
			var plainObject =
				context.BusinessObject.OfType<MultiversionDocument>().Include("DatabaseTable").Where(
					string.Format("it.name = '{0}' and it.Namespace.Name = '{1}'", settings.TargetObjectName,
					              settings.TargetObjectNamespace)).FirstOrDefault();
			if (plainObject == null)
				throw new Exception(string.Format("Basic Plain Object [{0}].[{1}] for a Plain Object List View is not found.",
				                                  settings.TargetObjectNamespace, settings.TargetObjectName));

			template.ConnectionString = connectionStringToMetadataStore;
			template.DatabaseName = targetDatabaseName;
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
			template.ParameterDefinitions = new List<StoredProcedureParameterSchema>();

			template.PreRender();

			stringBuilder.AppendLine();
			stringBuilder.AppendLine(template.TransformText());
			stringBuilder.AppendLine();
		}
	}
}