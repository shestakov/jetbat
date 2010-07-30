using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using JetBat.Client.Metadata.Simple;
using JetBat.Silverlight.UI.WebService;

namespace JetBat.Silverlight.UI
{
	public class MetadataProvider
	{
		private readonly Dictionary<QualifiedName, PlainObject> plainObjectCache = new Dictionary<QualifiedName, PlainObject>();
		private readonly Dictionary<QualifiedName, PlainObjectListView> plainObjectListViewCache = new Dictionary<QualifiedName, PlainObjectListView>();
		private readonly Dictionary<QualifiedName, Document> documentCache = new Dictionary<QualifiedName, Document>();
		private readonly Dictionary<QualifiedName, DocumentListView> documentListViewCache = new Dictionary<QualifiedName, DocumentListView>();

		#region PlainObjectListView

		public void LoadPlainObjectListViewDefinitionAsync(string objectNamespace, string objectName)
		{
			PlainObjectListView plainObjectListView;
			bool found;
			lock (plainObjectListViewCache)
				found = plainObjectListViewCache.TryGetValue(new QualifiedName(objectNamespace, objectName), out plainObjectListView);

			if (found && LoadPlainObjectListViewDefinitionCompleted != null)
			{
				LoadPlainObjectListViewDefinitionCompleted(this, new LoadPlainObjectListViewEventArgs(null, plainObjectListView));
				return;
			}

			ServiceSoapClient proxy = WebServiceProxyHelper.CreateProxy();
			proxy.GetObjectDefinitionCompleted += ProxyOnGetPlainObjectListViewDefinitionCompleted;
			proxy.GetObjectDefinitionAsync("PlainObjectListView", objectNamespace, objectName);
		}

		private void ProxyOnGetPlainObjectListViewDefinitionCompleted(object sender, GetObjectDefinitionCompletedEventArgs e)
		{
			var exception = e.Error;
			PlainObjectListView plainObjectListView = null;
			if (exception == null)
			{
				XmlSerializer serializer = new XmlSerializer(typeof(PlainObjectListView));
				using (StringReader reader = new StringReader(e.Result))
					plainObjectListView = (PlainObjectListView)serializer.Deserialize(reader);
				lock(plainObjectListViewCache)
				{
					plainObjectListViewCache[new QualifiedName(plainObjectListView.ObjectNamespace, plainObjectListView.ObjectName)] =
						plainObjectListView;
				}
			}
			if (LoadPlainObjectListViewDefinitionCompleted != null)
				LoadPlainObjectListViewDefinitionCompleted(sender, new LoadPlainObjectListViewEventArgs(exception, plainObjectListView));
		}

		public EventHandler<LoadPlainObjectListViewEventArgs> LoadPlainObjectListViewDefinitionCompleted;

		public class LoadPlainObjectListViewEventArgs : EventArgs
		{
			public Exception Exception { get; private set; }
			public PlainObjectListView PlainObjectListView { get; private set; }

			public LoadPlainObjectListViewEventArgs(Exception exception, PlainObjectListView plainObjectListView)
			{
				Exception = exception;
				PlainObjectListView = plainObjectListView;
			}
		}

		#endregion

		#region PlainObject

		public void LoadPlainObjectDefinitionAsync(string objectNamespace, string objectName)
		{
			PlainObject plainObject;
			bool found;
			lock (plainObjectListViewCache)
				found = plainObjectCache.TryGetValue(new QualifiedName(objectNamespace, objectName), out plainObject);

			if (found && LoadPlainObjectListViewDefinitionCompleted != null)
			{
				LoadPlainObjectDefinitionCompleted(this, new LoadPlainObjectEventArgs(null, plainObject));
				return;
			}

			ServiceSoapClient proxy = WebServiceProxyHelper.CreateProxy();
			proxy.GetObjectDefinitionCompleted += ProxyOnGetPlainObjectDefinitionCompleted;
			proxy.GetObjectDefinitionAsync("PlainObject", objectNamespace, objectName);
		}

		private void ProxyOnGetPlainObjectDefinitionCompleted(object sender, GetObjectDefinitionCompletedEventArgs e)
		{
			var exception = e.Error;
			PlainObject plainObject = null;
			if (exception == null)
			{
				XmlSerializer serializer = new XmlSerializer(typeof(PlainObject));
				using (StringReader reader = new StringReader(e.Result))
					plainObject = (PlainObject)serializer.Deserialize(reader);
				lock (plainObjectListViewCache)
				{
					plainObjectCache[new QualifiedName(plainObject.ObjectNamespace, plainObject.ObjectName)] = plainObject;
				}
			}
			if (LoadPlainObjectDefinitionCompleted != null)
				LoadPlainObjectDefinitionCompleted(sender, new LoadPlainObjectEventArgs(exception, plainObject));
		}

		public EventHandler<LoadPlainObjectEventArgs> LoadPlainObjectDefinitionCompleted;

		public class LoadPlainObjectEventArgs : EventArgs
		{
			public Exception Exception { get; private set; }
			public PlainObject PlainObject { get; private set; }

			public LoadPlainObjectEventArgs(Exception exception, PlainObject plainObject)
			{
				Exception = exception;
				PlainObject = plainObject;
			}
		}

		#endregion

		#region DocumentListView

		public void LoadDocumentListViewDefinitionAsync(string objectNamespace, string objectName)
		{
			DocumentListView documenttListView;
			bool found;
			lock (documentListViewCache)
				found = documentListViewCache.TryGetValue(new QualifiedName(objectNamespace, objectName), out documenttListView);

			if (found && LoadDocumentListViewDefinitionCompleted != null)
			{
				LoadDocumentListViewDefinitionCompleted(this, new LoadDocumentListViewEventArgs(null, documenttListView));
				return;
			}

			ServiceSoapClient proxy = WebServiceProxyHelper.CreateProxy();
			proxy.GetObjectDefinitionCompleted += ProxyOnGetDocumentListViewDefinitionCompleted;
			proxy.GetObjectDefinitionAsync("DocumentListView", objectNamespace, objectName);
		}

		private void ProxyOnGetDocumentListViewDefinitionCompleted(object sender, GetObjectDefinitionCompletedEventArgs e)
		{
			var exception = e.Error;
			DocumentListView documentListView = null;
			if (exception == null)
			{
				XmlSerializer serializer = new XmlSerializer(typeof(DocumentListView));
				using (StringReader reader = new StringReader(e.Result))
					documentListView = (DocumentListView)serializer.Deserialize(reader);
				lock (documentListViewCache)
				{
					documentListViewCache[new QualifiedName(documentListView.ObjectNamespace, documentListView.ObjectName)] =
						documentListView;
				}
			}
			if (LoadDocumentListViewDefinitionCompleted != null)
				LoadDocumentListViewDefinitionCompleted(sender, new LoadDocumentListViewEventArgs(exception, documentListView));
		}

		public EventHandler<LoadDocumentListViewEventArgs> LoadDocumentListViewDefinitionCompleted;

		public class LoadDocumentListViewEventArgs : EventArgs
		{
			public Exception Exception { get; private set; }
			public DocumentListView DocumentListView { get; private set; }

			public LoadDocumentListViewEventArgs(Exception exception, DocumentListView documentListView)
			{
				Exception = exception;
				DocumentListView = documentListView;
			}
		}

		#endregion

		#region Document

		public void LoadDocumentDefinitionAsync(string objectNamespace, string objectName)
		{
			Document document;
			bool found;
			lock (documentCache)
				found = documentCache.TryGetValue(new QualifiedName(objectNamespace, objectName), out document);

			if (found && LoadDocumentListViewDefinitionCompleted != null)
			{
				LoadDocumentDefinitionCompleted(this, new LoadDocumentEventArgs(null, document));
				return;
			}

			ServiceSoapClient proxy = WebServiceProxyHelper.CreateProxy();
			proxy.GetObjectDefinitionCompleted += ProxyOnGetDocumentDefinitionCompleted;
			proxy.GetObjectDefinitionAsync("Document", objectNamespace, objectName);
		}

		private void ProxyOnGetDocumentDefinitionCompleted(object sender, GetObjectDefinitionCompletedEventArgs e)
		{
			var exception = e.Error;
			Document document = null;
			if (exception == null)
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Document));
				using (StringReader reader = new StringReader(e.Result))
					document = (Document)serializer.Deserialize(reader);
				lock (plainObjectListViewCache)
				{
					documentCache[new QualifiedName(document.ObjectNamespace, document.ObjectName)] = document;
				}
			}
			if (LoadDocumentDefinitionCompleted != null)
				LoadDocumentDefinitionCompleted(sender, new LoadDocumentEventArgs(exception, document));
		}

		public EventHandler<LoadDocumentEventArgs> LoadDocumentDefinitionCompleted;

		public class LoadDocumentEventArgs : EventArgs
		{
			public Exception Exception { get; private set; }
			public Document Document { get; private set; }

			public LoadDocumentEventArgs(Exception exception, Document document)
			{
				Exception = exception;
				Document = document;
			}
		}

		#endregion
	}
}