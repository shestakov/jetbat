using System.Collections.Specialized;
using System.Text;
using JetBat.Metadata.Templates.PlainObject;

namespace JetBat.Metadata.PlainObjectManager
{
	public static class PlainObjectTemplateRenderer
	{
		public static void RenderMethods(PlainObjectConstructorSettings constructorSettings, StringBuilder stringBuilder,
		                                 string targetDatabaseName, string connectionStringToMetadataStore)
		{
			executeCodeTemplate(constructorSettings, stringBuilder, targetDatabaseName, connectionStringToMetadataStore);
		}

		private static void executeCodeTemplate(PlainObjectConstructorSettings constructorSettings, StringBuilder stringBuilder, string targetDatabaseName, string connectionStringToMetadataStore)
		{
			PlainObjectStoredProcedureTemplate template = new PlainObjectStoredProcedureTemplate();
			
			template.ConnectionString = connectionStringToMetadataStore;
			template.DatabaseName = targetDatabaseName;
			template.TableName = constructorSettings.TableName;
			template.ViewName = constructorSettings.ViewName;
			template.DeleteFlagColumnName = constructorSettings.DeleteFlagColumnName;
			template.DateTimeMarkColumnName = constructorSettings.DateTimeMarkColumnName;
			template.SequenceNumberColumnName = constructorSettings.SequenceNumberColumnName;
			template.StatusColumnName = constructorSettings.StatusColumnName;
			template.InitialStatusValue = constructorSettings.StatusColumnInitialValue;
			template.ForeignKeyToParentName = constructorSettings.ForeignKeyToParentObjectName;
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
			template.EntityName = constructorSettings.EntityName;
			template.EntityNamespace = constructorSettings.EntityNamespace;
			template.NamespacePrefix = constructorSettings.EntityNamespace;
			template.UIEditorName = constructorSettings.UIEditorName;
			template.BeforeInsert = constructorSettings.MethodBeforeInsert;
			template.AfterInsert = constructorSettings.MethodAfterInsert;
			template.BeforeUpdate = constructorSettings.MethodBeforeUpdate;
			template.AfterUpdate = constructorSettings.MethodAfterUpdate;
			template.BeforeDelete = constructorSettings.MethodBeforeDelete;
			template.AfterDelete = constructorSettings.MethodAfterDelete;
			template.BeforeRestore = constructorSettings.MethodBeforeRestore;
			template.AfterRestore = constructorSettings.MethodAfterRestore;

			template.Restore = constructorSettings.MethodRestore;
			template.CopyByParentObject = constructorSettings.MethodCopyByParentObject;
			template.DeleteByParentObject = constructorSettings.MethodDeleteByParentObject;

			template.PreRender();

			stringBuilder.AppendLine();
			try
			{
				stringBuilder.AppendLine(template.TransformText());
			}
			catch (System.Exception ex)
			{
				stringBuilder.AppendLine(ex.ToString());
				throw;
			}
			stringBuilder.AppendLine();
		}
	}
}