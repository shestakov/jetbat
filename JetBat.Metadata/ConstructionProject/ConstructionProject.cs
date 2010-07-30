using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;
using JetBat.Metadata.MultiversionDocumentListViewManager;
using JetBat.Metadata.MultiversionDocumentManager;
using JetBat.Metadata.PlainObjectListViewManager;
using JetBat.Metadata.PlainObjectManager;
using JetBat.Metadata.StoredQueryManager;

namespace JetBat.Metadata.ConstructionProject
{
	public class ConstructionProject
	{
		public string TargetDatabaseName { set; get; }
		public string ConnectionStringToMetadataStore { set; get; }
		public List<ConstructionProjectItem> Items = new List<ConstructionProjectItem>();

		public string Persist(MetadataContainer metadataContext)
		{
			metadataContext.UpdateMetadata();
			var result = true;
			var queue = buildConstructionQueue();
			var builder = new StringBuilder();
			builder.AppendLine();
			builder.AppendLine(string.Format("Starting metadata construction: {0}", DateTime.Now.ToString("HH:mm:ss")));
			foreach (var projectItem in queue)
			{
				if (!persistItem(projectItem, metadataContext, builder))
					result = false;
			}
			builder.AppendLine((result ? "Construction complete: " : "Construction failed: ") + DateTime.Now.ToString("HH:mm:ss"));
			return builder.ToString();
		}

		public string RenderMethods(MetadataContainer metadataContext, StringBuilder output)
		{
			metadataContext.UpdateMetadata();
			var result = true;
			var queue = buildConstructionQueue();
			var log = new StringBuilder(queue.Count + 2);
			log.AppendLine("Starting rendering stored procedures:");
			foreach (var projectItem in queue)
			{
				if (!renderMethods(projectItem, metadataContext, log, output))
					result = false;
			}
			log.AppendLine(result ? "Rendering complete." : "Rendering failed.");
			return log.ToString();
		}

		#region Building item queue

		internal Queue<ConstructionProjectItem> buildConstructionQueue()
		{
			var result = new Queue<ConstructionProjectItem>(Items.Count);
			foreach (var projectItem in Items)
			{
				if (string.IsNullOrEmpty(projectItem.ParentObjectNamespace) && string.IsNullOrEmpty(projectItem.ParentObjectName))
					enqueueProjectItem(projectItem, result);
			}

			return result;
		}

		private void enqueueProjectItem(ConstructionProjectItem currentItem, Queue<ConstructionProjectItem> queue)
		{
			queue.Enqueue(currentItem);
			foreach (var projectItem in Items)
			{
				if (projectItem.ParentObjectNamespace == currentItem.ObjectNamespace && projectItem.ParentObjectName == currentItem.ObjectName)
					enqueueProjectItem(projectItem, queue);
			}
		}

		#endregion

		#region Rendering methods for project items

		private bool renderMethods(ConstructionProjectItem item, MetadataContainer metadataContext, StringBuilder messages, StringBuilder output)
		{
			var result = false;
			try
			{
				if (item.ItemType == ConstructionProjectItemType.PlainObject)
					renderMethodsPlainObject(item, output);
				else if (item.ItemType == ConstructionProjectItemType.PlainObjectListView)
					renderMethodsPlainObjectListView(item, metadataContext, output);
				else if (item.ItemType == ConstructionProjectItemType.MultiversionDocument)
					renderMethodsMultiversionDocument(item, output);
				else if (item.ItemType == ConstructionProjectItemType.MultiversionDocumentListView)
					renderMethodsMultiversionDocumentListView(item, metadataContext, output);
				else if (item.ItemType == ConstructionProjectItemType.StoredQuery)
					renderMethodsStoredQuery(item, metadataContext, output);
				else
				{
					messages.AppendLine("Unknown type of a busienss object");
					return false;
				}
				messages.AppendLine(string.Format("[{0}].[{1}] - Succeded", item.ObjectNamespace, item.ObjectName));
				result = true;
			}
			catch (Exception ex)
			{
				var errorMessage = ex.ToString();//ex.InnerException != null ? ex.InnerException.Message : ex.Message;
				messages.AppendLine(string.Format("[{0}].[{1}] - {2}", item.ObjectNamespace, item.ObjectName, errorMessage));
			}
			return result;
		}

		private void renderMethodsStoredQuery(ConstructionProjectItem item, MetadataContainer metadataContext, StringBuilder output)
		{
			var constructorSettings = StoredQueryConstructorSettings.Load(item.FileName);
		}

		private void renderMethodsMultiversionDocumentListView(ConstructionProjectItem item, MetadataContainer metadataContext, StringBuilder output)
		{
			var constructorSettings = MultiversionDocumentListViewConstructorSettings.Load(item.FileName);
			MultiversionDocumentListViewTemplateRenderer.RenderMethods(constructorSettings, output, TargetDatabaseName, ConnectionStringToMetadataStore, metadataContext);
		}

		private void renderMethodsMultiversionDocument(ConstructionProjectItem item, StringBuilder output)
		{
			var constructorSettings = MultiversionDocumentConstructorSettings.Load(item.FileName);
			MultiversionDocumentTemplateRenderer.RenderMethods(constructorSettings, output, TargetDatabaseName, ConnectionStringToMetadataStore);
		}

		private void renderMethodsPlainObjectListView(ConstructionProjectItem item, MetadataContainer metadataContext, StringBuilder output)
		{
			var constructorSettings = PlainObjectListViewConstructorSettings.Load(item.FileName);
			PlainObjectListViewTemplateRenderer.RenderMethods(constructorSettings, output, TargetDatabaseName, ConnectionStringToMetadataStore, metadataContext);
		}

		private void renderMethodsPlainObject(ConstructionProjectItem item, StringBuilder output)
		{
			var constructorSettings = PlainObjectConstructorSettings.Load(item.FileName);
			PlainObjectTemplateRenderer.RenderMethods(constructorSettings, output, TargetDatabaseName, ConnectionStringToMetadataStore);
		}

		#endregion

		#region Persisting a project item

		private static bool persistItem(ConstructionProjectItem item, MetadataContainer metadataContext, StringBuilder messages)
		{
			var result = false;
			try
			{
				if (item.ItemType == ConstructionProjectItemType.PlainObject)
					persistPlainObject(item, metadataContext);
				else if (item.ItemType == ConstructionProjectItemType.PlainObjectListView)
					persistPlainObjectListView(item, metadataContext);
				else if (item.ItemType == ConstructionProjectItemType.MultiversionDocument)
					persistMultiversionDocument(item, metadataContext);
				else if (item.ItemType == ConstructionProjectItemType.MultiversionDocumentListView)
					persistMultiversionDocumentListView(item, metadataContext);
				else if (item.ItemType == ConstructionProjectItemType.StoredQuery)
					persistStoredQuery(item, metadataContext);
				else
				{
					messages.AppendLine("Unknown type of a busienss object");
					return false;
				}
				result = true;
				messages.AppendLine(string.Format("[{0}].[{1}] - Succeded", item.ObjectNamespace, item.ObjectName));
			}
			catch (Exception ex)
			{
				var errorMessage = ex.InnerException != null ? ex.InnerException.ToString() : ex.ToString();
				messages.AppendLine(string.Format("[{0}].[{1}] - {2}", item.ObjectNamespace, item.ObjectName, errorMessage));
			}
			return result;
		}

		private static void persistStoredQuery(ConstructionProjectItem item, MetadataContainer metadataContext)
		{
			var constructorSettings = StoredQueryConstructorSettings.Load(item.FileName);
			new StoredQueryConstructor(constructorSettings, metadataContext).Save();
			StoredQueryConstructorSettings.Save(constructorSettings, item.FileName);
		}

		private static void persistMultiversionDocumentListView(ConstructionProjectItem item, MetadataContainer metadataContext)
		{
			var constructorSettings = MultiversionDocumentListViewConstructorSettings.Load(item.FileName);
			new MultiversionDocumentListViewConstructor(constructorSettings, metadataContext).Save();
			MultiversionDocumentListViewConstructorSettings.Save(constructorSettings, item.FileName);
		}

		private static void persistMultiversionDocument(ConstructionProjectItem item, MetadataContainer metadataContext)
		{
			var constructorSettings = MultiversionDocumentConstructorSettings.Load(item.FileName);
			new MultiversionDocumentConstructor(constructorSettings, metadataContext).Save();
			MultiversionDocumentConstructorSettings.Save(constructorSettings, item.FileName);

		}

		private static void persistPlainObjectListView(ConstructionProjectItem item, MetadataContainer metadataContext)
		{
			var constructorSettings = PlainObjectListViewConstructorSettings.Load(item.FileName);
			new PlainObjectListViewConstructor(constructorSettings, metadataContext).Save();
			PlainObjectListViewConstructorSettings.Save(constructorSettings, item.FileName);
		}

		private static void persistPlainObject(ConstructionProjectItem item, MetadataContainer metadataContext)
		{
			var constructorSettings = PlainObjectConstructorSettings.Load(item.FileName);
			new PlainObjectConstructor(constructorSettings, metadataContext).Save();
			PlainObjectConstructorSettings.Save(constructorSettings, item.FileName);
		}

		#endregion

		#region Excluding a project item

		public void ExcludeItem(string objectNamespace, string objectName)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Adding a project item

		public void AddItem(string fileName)
		{
			if (!File.Exists(fileName))
				throw new ArgumentException("The specified file does not exist", "fileName");
			string extension = Path.GetExtension(fileName);
			switch (extension)
			{
				case ".PlainObject":
					addPlainObject(fileName);
					break;
				case ".PlainObjectListView":
					addPlainObjectListView(fileName);
					break;
				case ".MultiversionDocument":
					addMultiversionDocument(fileName);
					break;
				case ".MultiversionDocumentListView":
					addMultiversionDocumentListView(fileName);
					break;
				case ".StoredQuery":
					addStoredQuery(fileName);
					break;
			}
		}

		private void addStoredQuery(string fileName)
		{
			var constructorSettings = StoredQueryConstructorSettings.Load(fileName);
			var item = new ConstructionProjectItem
						{
							ItemType = ConstructionProjectItemType.StoredQuery,
							FileName = fileName,
							ObjectName = constructorSettings.EntityName,
							ObjectNamespace = constructorSettings.EntityNamespace,
							ParentObjectNamespace = null,
							ParentObjectName = null
						};
			Items.Add(item);
		}

		private void addMultiversionDocumentListView(string fileName)
		{
			var constructorSettings = MultiversionDocumentListViewConstructorSettings.Load(fileName);
			var item = new ConstructionProjectItem
						{
							ItemType = ConstructionProjectItemType.MultiversionDocumentListView,
							FileName = fileName,
							ObjectName = constructorSettings.EntityName,
							ObjectNamespace = constructorSettings.EntityNamespace,
							ParentObjectNamespace = constructorSettings.TargetObjectNamespace,
							ParentObjectName = constructorSettings.TargetObjectName
						};
			Items.Add(item);
		}

		private void addMultiversionDocument(string fileName)
		{
			var constructorSettings = MultiversionDocumentConstructorSettings.Load(fileName);
			var item = new ConstructionProjectItem
						{
							ItemType = ConstructionProjectItemType.MultiversionDocument,
							FileName = fileName,
							ObjectName = constructorSettings.EntityName,
							ObjectNamespace = constructorSettings.EntityNamespace,
							ParentObjectNamespace = null,
							ParentObjectName = null
						};
			Items.Add(item);
		}

		private void addPlainObjectListView(string fileName)
		{
			var constructorSettings = PlainObjectListViewConstructorSettings.Load(fileName);
			var item = new ConstructionProjectItem
						{
							ItemType = ConstructionProjectItemType.PlainObjectListView,
							FileName = fileName,
							ObjectName = constructorSettings.EntityName,
							ObjectNamespace = constructorSettings.EntityNamespace,
							ParentObjectNamespace = constructorSettings.TargetObjectNamespace,
							ParentObjectName = constructorSettings.TargetObjectName
						};
			Items.Add(item);
		}

		private void addPlainObject(string fileName)
		{
			var constructorSettings = PlainObjectConstructorSettings.Load(fileName);
			var item = new ConstructionProjectItem
						{
							ItemType = ConstructionProjectItemType.PlainObject,
							FileName = fileName,
							ObjectName = constructorSettings.EntityName,
							ObjectNamespace = constructorSettings.EntityNamespace,
							ParentObjectNamespace = constructorSettings.ParentObjectNamespace,
							ParentObjectName = constructorSettings.ParentObjectName
						};
			Items.Add(item);
		}

		#endregion

		#region Serialization

		public static void Save(ConstructionProject project, string fileName)
		{
			var serializer = new XmlSerializer(typeof(ConstructionProject));
			using (TextWriter writer = new StreamWriter(fileName))
				serializer.Serialize(writer, project);
		}

		public static ConstructionProject Load(string fileName)
		{
			ConstructionProject project;
			var serializer = new XmlSerializer(typeof(ConstructionProject));
			using (TextReader reader = new StreamReader(fileName))
				project = (ConstructionProject)serializer.Deserialize(reader);
			return project;
		}

		#endregion
	}
}