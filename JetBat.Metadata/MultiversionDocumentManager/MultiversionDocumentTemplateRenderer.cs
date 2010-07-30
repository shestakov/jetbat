using System.Collections.Specialized;
using System.Text;
using JetBat.Metadata.Templates.MultiversionDocument;

namespace JetBat.Metadata.MultiversionDocumentManager
{
	public static class MultiversionDocumentTemplateRenderer
	{
		public static void RenderMethods(MultiversionDocumentConstructorSettings constructorSettings, StringBuilder stringBuilder, string targetDatabaseName, string connectionStringToMetadataStore)
		{
			ExecuteCodeTemplate(constructorSettings, stringBuilder, targetDatabaseName, connectionStringToMetadataStore);
		}

		public static void ExecuteCodeTemplate(MultiversionDocumentConstructorSettings constructorSettings, StringBuilder stringBuilder, string targetDatabaseName, string connectionStringToMetadataStore)
		{
			MultiversionDocumentStoredProcedureTemplate template = new MultiversionDocumentStoredProcedureTemplate();
			
			template.ConnectionString = connectionStringToMetadataStore;
			template.DatabaseName = targetDatabaseName;
			template.TableName = constructorSettings.TableName;
			template.ViewName = constructorSettings.ViewName;
			template.IgnoredColumnList = new StringCollection();
			foreach (var ignoredColumn in constructorSettings.IgnoredColumns)
			{
				template.IgnoredColumnList.Add(ignoredColumn);
			}
			template.ReadOnlyColumnList = new StringCollection();
			foreach (var readOnlyColumn in constructorSettings.ReadOnlyColumns)
			{
				template.ReadOnlyColumnList.Add(readOnlyColumn);
			}
			template.HiddenColumnList = new StringCollection();
			foreach (var invisibleColumn in constructorSettings.InvisibleColumns)
			{
				template.HiddenColumnList.Add(invisibleColumn);
			}
			template.HeaderNullableColumnList = new StringCollection();
			foreach (var headerNullableColumn in constructorSettings.HeaderNullableColumns)
			{
				template.HeaderNullableColumnList.Add(headerNullableColumn);
			}
			template.EntityName = constructorSettings.EntityName;
			template.EntityNamespace = constructorSettings.EntityNamespace;
			template.NamespacePrefix = constructorSettings.EntityNamespace;

			template.AfterCreate = constructorSettings.MethodAfterCreate;
			template.BeforeUpdateVersion = constructorSettings.MethodBeforeUpdateVersion;
			template.BeforeConfirmEdit = constructorSettings.MethodBeforeConfirmEdit;
			template.AfterConfirmEdit = constructorSettings.MethodAfterConfirmEdit;
			template.BeforeCommit = constructorSettings.MethodBeforeCommit;
			template.BeforeRollback = constructorSettings.MethodBeforeRollback;

			template.PreRender();

			stringBuilder.AppendLine();
			stringBuilder.AppendLine(template.TransformText());
			stringBuilder.AppendLine();
		}
	}
}