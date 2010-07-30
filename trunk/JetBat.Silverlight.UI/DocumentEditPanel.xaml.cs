using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;
using JetBat.Silverlight.UI.AttributeEditiors;
using NameValue = JetBat.Client.Metadata.Misc.NameValue;

namespace JetBat.Silverlight.UI
{
	public partial class DocumentEditPanel
	{
		private readonly MetadataProvider metadataProvider = new MetadataProvider();

		private NamedObjectCollection<NameValue> predefinedAttributeValues;

		public GetPlainObjectListQualifiedNamespaceDelegate GetPlainObjectListQualifiedNamespace;
		public CreateComplexAttributeEditorDelegate CreateComplexAttributeEditor;

		public event EventHandler SetUpComplete;
		public event DocumentVersionRetrieved StartEditMethodComplete;
		public event DocumentVersionRetrieved LoadMethodComplete;
		
		public delegate void DocumentVersionRetrieved(NameValue versionDocumentId);

		public Document Document { get; set; }
		private DocumentInstance instance;

		private DocumentFormMode mode;

		public int DocumentVersionID
		{
			get
			{
				if (instance == null)
					return -1;
				if (instance.VersionID.Value != null)
					return (int) instance.VersionID.Value;
				if (instance.CurrentVersionID.Value != null)
					return (int)instance.CurrentVersionID.Value;
				throw new Exception("Documtent version is unknown");
			}
		}

		public DocumentEditPanel()
		{
			InitializeComponent();
		}

		public void Load(NamedObjectCollection<NameValue> primaryKey)
		{
			if (Document == null) throw new NullReferenceException("Document Metadata not set");
			instance.ExecuteMethodComplete += InstanceOnExecuteMethodComplete;
			if (mode == DocumentFormMode.View)
			{
				instance.DocumentID = primaryKey["DocumentID"];
				instance.LoadAsync();
			}
			else
				instance.StartEdit(primaryKey);
		}

		private void InstanceOnExecuteMethodComplete(object sender, ExecuteMethodCompleteEventArgs e)
		{
			instance.ExecuteMethodComplete -= InstanceOnExecuteMethodComplete;
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			if (e.MethodName == "Load")
			{
				GetInstanceAttributeValues();
				if (LoadMethodComplete != null)
					LoadMethodComplete(instance.CurrentVersionID);
			}
			if (e.MethodName == "StartEdit")
			{
				instance.ExecuteMethodComplete += InstanceOnExecuteMethodComplete;
				instance.LoadAsync();
				if (StartEditMethodComplete != null)
					StartEditMethodComplete(instance.VersionID);
			}
			if (e.MethodName == "UpdateVersion")
			{
				instance.ExecuteMethodComplete += InstanceOnExecuteMethodComplete;
				instance.ConfirmEdit();
			}
			if (e.MethodName == "ConfirmEdit")
			{
				if(e.Exception != null || e.ErrorMessages.Count > 0)
					return;
				if (ConfirmEditComplete != null)
					ConfirmEditComplete(sender, EventArgs.Empty);
			}
			if (e.MethodName == "Delete" || e.MethodName == "CancelEdit")
			{
				HtmlPage.Window.Navigate(new Uri("DocumentList.aspx", UriKind.Relative));
			}

		}

		private void GetInstanceAttributeValues()
		{
			foreach (UIElement uiElement in LayoutRoot.Children)
			{
				if (uiElement is IObjectAttributeEditor)
				{
					((IObjectAttributeEditor)uiElement).IsReadOnly = mode == DocumentFormMode.View;
					((IObjectAttributeEditor)uiElement).Value = instance.Attributes[((IObjectAttributeEditor)uiElement).AttributeName].Value;
				}
				if (uiElement is IComplexAttributeEditor)
				{
					var complexAttributeEditor = ((IComplexAttributeEditor)uiElement);
					complexAttributeEditor.IsReadOnly = mode == DocumentFormMode.View;
					var actualParameters = new NamedObjectCollection<NameValue>();
					if (complexAttributeEditor.MigratedAttributes.Count > 0)
					{
						foreach (var migratedAttribute in complexAttributeEditor.MigratedAttributes)
						{
							if (instance.Attributes.Contains(migratedAttribute.Key))
								actualParameters.Add(new NameValue { Name = migratedAttribute.Value, Value = instance.Attributes[migratedAttribute.Key].Value });
						}
						complexAttributeEditor.Load(actualParameters);
					}
					complexAttributeEditor.SelectItem(instance.GetComplexAttributeValue(complexAttributeEditor.ComplexAttributeName));
				}
			}
		}

		private void SetInstanceAttributeValues()
		{
			foreach (var uiElement in LayoutRoot.Children)
			{
				if (uiElement is IObjectAttributeEditor)
					if (instance.Attributes.Contains(((IObjectAttributeEditor)uiElement).AttributeName))
						instance.Attributes[((IObjectAttributeEditor)uiElement).AttributeName].Value = ((IObjectAttributeEditor)uiElement).Value;
				if (!(uiElement is IComplexAttributeEditor)) continue;
				var element = (IComplexAttributeEditor)uiElement;
				if (Document.ComplexAttributes.Contains(element.ComplexAttributeName))
					instance.SetComplexAttribute(element.ComplexAttributeName,
												 element.SelectedObject);
			}
		}

		public void Insert()
		{
		}

		public void SetUp(string objectNamespace, string objectName, DocumentFormMode documentFormMode, NamedObjectCollection<NameValue> predefinedAttributeValues)
		{
			mode = documentFormMode;
			this.predefinedAttributeValues = predefinedAttributeValues;
			LayoutRoot.DataContext = null;
			LayoutRoot.Children.Clear();

			if (metadataProvider == null) return;
			try
			{
				metadataProvider.LoadDocumentDefinitionCompleted += LoadDocumentDefinitionCompleted;
				metadataProvider.LoadDocumentDefinitionAsync(objectNamespace, objectName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void LoadDocumentDefinitionCompleted(object sender, MetadataProvider.LoadDocumentEventArgs e)
		{
			metadataProvider.LoadDocumentDefinitionCompleted -= LoadDocumentDefinitionCompleted;
			if (e.Exception != null)
			{
				MessageBox.Show(e.Exception.InnerException.ToString());
				return;
			}

			var editors = new Dictionary<FrameworkElement, int>();

			Document = e.Document;
			instance = new DocumentInstance(Document);
			if (predefinedAttributeValues != null)
			{
				foreach (var attributeValue in predefinedAttributeValues)
				{
					if (instance.Attributes.Contains(attributeValue.Name))
						instance.Attributes[attributeValue.Name].Value = attributeValue.Value;
				}
			}
			foreach (ObjectAttribute objectAttribute in Document.Attributes)
				if (objectAttribute.IsUserVisible)
				{
					FrameworkElement attributeEditor = ObjectAttributeEditorFactory.CreateAttributeEditor(objectAttribute);
					editors.Add(attributeEditor, objectAttribute.UIPreferredIndex);
				}
			foreach (ObjectComplexAttribute complexAttribute in Document.ComplexAttributes)
				if (!complexAttribute.FriendlyName.StartsWith("_"))
				{
					IComplexAttributeEditor complexAttributeEditor = null;
					if (CreateComplexAttributeEditor != null)
					{
						bool hideFromUser;
						complexAttributeEditor = CreateComplexAttributeEditor(Document.ObjectNamespace, Document.ObjectName, complexAttribute, out hideFromUser);
						if (hideFromUser) continue;
					}
					if (complexAttributeEditor == null)
						complexAttributeEditor = (IComplexAttributeEditor)ObjectAttributeEditorFactory.CreateComplexAttributeEditor(Document.ObjectNamespace, Document.ObjectName, complexAttribute, GetPlainObjectListQualifiedNamespace);
					editors.Add((FrameworkElement)complexAttributeEditor, complexAttribute.UIPreferredIndex);
					if (complexAttributeEditor.MigratedAttributes.Count == 0)
					{
						NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
						if (predefinedAttributeValues != null)
							foreach (NameValue nameValue in predefinedAttributeValues)
								parameters.Add(nameValue);
						complexAttributeEditor.Load(parameters);
					}
				}

			var editorList = new List<FrameworkElement>(editors.Keys);
			editorList.Sort((x, y) => editors[x] - editors[y]);
			foreach (FrameworkElement editor in editorList)
			{
				LayoutRoot.Children.Add(editor);
			}

			GetInstanceAttributeValues();
			if (SetUpComplete != null) SetUpComplete(sender, EventArgs.Empty);
		}

		public void OkButtonClick(object sender, RoutedEventArgs e)
		{
			instance.ExecuteMethodComplete += InstanceOnExecuteMethodComplete;
			SetInstanceAttributeValues();
			instance.UpdateVersion();
		}

		public void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			instance.ExecuteMethodComplete += InstanceOnExecuteMethodComplete;
			if (mode == DocumentFormMode.Create)
			{
				instance.Delete();
			}
			else
			{
				instance.CancelEdit();
			}
		}

		public event EventHandler UpdateVersionComplete;
		public event EventHandler ConfirmEditComplete;

		public void UpdateVersion()
		{
			instance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteUpdateVersion;
			SetInstanceAttributeValues();
			instance.UpdateVersion();
		}

		private void InstanceOnExecuteMethodCompleteUpdateVersion(object sender, ExecuteMethodCompleteEventArgs e)
		{
			instance.ExecuteMethodComplete -= InstanceOnExecuteMethodCompleteUpdateVersion;
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			if (UpdateVersionComplete != null)
				UpdateVersionComplete(sender, EventArgs.Empty);
		}
	}
}