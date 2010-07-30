using System;
using System.Collections.Generic;
using System.Windows;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;
using JetBat.Silverlight.UI.AttributeEditiors;
using NameValue = JetBat.Client.Metadata.Misc.NameValue;

namespace JetBat.Silverlight.UI
{
	public partial class PlainObjectEditPanel
	{
		public enum Mode { Insert, Update }
		private readonly MetadataProvider metadataProvider = new MetadataProvider();
		private NamedObjectCollection<NameValue> predefinedAttributeValues;

		public PlainObjectEditPanel()
		{
			InitializeComponent();
		}

		public PlainObject PlainObject { get; set; }

		public IAfterUpdateAttributeProcessor AfterUpdateAttributeProcessor { get; set; }

		private PlainObjectInstance instance;
		private Mode currentMode;

		public void Load(NamedObjectCollection<NameValue> primaryKey)
		{
			if (PlainObject == null) throw new NullReferenceException("Plain Object Metadata not set");
			instance.ExecuteMethodComplete += InstanceOnExecuteMethodComplete;
			instance.LoadAsync(primaryKey);
		}

		private void InstanceOnExecuteMethodComplete(object sender, ExecuteMethodCompleteEventArgs e)
		{
			instance.ExecuteMethodComplete -= InstanceOnExecuteMethodComplete;
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			if (e.MethodName == "Load")
				GetInstanceAttributeValues();
		}

		private void GetInstanceAttributeValues()
		{
			foreach (UIElement uiElement in LayoutRoot.Children)
			{
				if (uiElement is IObjectAttributeEditor)
					((IObjectAttributeEditor)uiElement).Value = instance.Attributes[((IObjectAttributeEditor)uiElement).AttributeName].Value;
				if (uiElement is IComplexAttributeEditor)
				{
					IComplexAttributeEditor complexAttributeComboBox = ((IComplexAttributeEditor)uiElement);
					NamedObjectCollection<NameValue> actualParameters = new NamedObjectCollection<NameValue>();
					if (complexAttributeComboBox.MigratedAttributes.Count > 0)
					{
						foreach (KeyValuePair<string, string> migratedAttribute in complexAttributeComboBox.MigratedAttributes)
						{
							if (instance.Attributes.Contains(migratedAttribute.Key))
								actualParameters.Add(new NameValue { Name = migratedAttribute.Value, Value = instance.Attributes[migratedAttribute.Key].Value });
						}
						complexAttributeComboBox.Load(actualParameters);
					}
					complexAttributeComboBox.SelectItem(instance.GetComplexAttributeValue(complexAttributeComboBox.ComplexAttributeName));
				}
			}
		}

		private void SetInstanceAttributeValues()
		{
			foreach (UIElement uiElement in LayoutRoot.Children)
			{
				if (uiElement is IObjectAttributeEditor)
					if (instance.Attributes.Contains(((IObjectAttributeEditor)uiElement).AttributeName))
						instance.Attributes[((IObjectAttributeEditor)uiElement).AttributeName].Value = ((IObjectAttributeEditor)uiElement).Value;
				if (uiElement is IComplexAttributeEditor)
				{
					IComplexAttributeEditor element = (IComplexAttributeEditor)uiElement;
					if (PlainObject.ComplexAttributes.Contains(element.ComplexAttributeName))
						instance.SetComplexAttribute(element.ComplexAttributeName,
													 element.SelectedObject);
				}
			}
		}

		public void Insert()
		{
		}

		public void SetUp(string objectNamespace, string objectName, Mode mode, NamedObjectCollection<NameValue> predefinedAttributeValues)
		{
			this.predefinedAttributeValues = predefinedAttributeValues;
			LayoutRoot.DataContext = null;
			LayoutRoot.Children.Clear();
			currentMode = mode;

			if (metadataProvider == null) return;
			try
			{
				metadataProvider.LoadPlainObjectDefinitionCompleted += LoadPlainObjectDefinitionCompleted;
				metadataProvider.LoadPlainObjectDefinitionAsync(objectNamespace, objectName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void LoadPlainObjectDefinitionCompleted(object sender, MetadataProvider.LoadPlainObjectEventArgs e)
		{
			metadataProvider.LoadPlainObjectDefinitionCompleted -= LoadPlainObjectDefinitionCompleted;
			if (e.Exception != null)
			{
				MessageBox.Show(e.Exception.InnerException.ToString());
				return;
			}

			Dictionary<FrameworkElement, int> editors = new Dictionary<FrameworkElement, int>();

			PlainObject = e.PlainObject;
			instance = new PlainObjectInstance(PlainObject);
			if (predefinedAttributeValues != null)
			{
				foreach (NameValue attributeValue in predefinedAttributeValues)
				{
					if (instance.Attributes.Contains(attributeValue.Name))
						instance.Attributes[attributeValue.Name].Value = attributeValue.Value;
				}
			}
			foreach (ObjectAttribute objectAttribute in PlainObject.Attributes)
				if (objectAttribute.IsUserVisible)
				{
					FrameworkElement attributeEditor = ObjectAttributeEditorFactory.CreateAttributeEditor(objectAttribute);
					editors.Add(attributeEditor, objectAttribute.UIPreferredIndex);
				}
			foreach (ObjectComplexAttribute complexAttribute in PlainObject.ComplexAttributes)
				if (!complexAttribute.FriendlyName.StartsWith("_"))
				{
					IComplexAttributeEditor complexAttributeEditor = null;
					if (CreateComplexAttributeEditor != null)
					{
						bool hideFromUser;
						complexAttributeEditor = CreateComplexAttributeEditor(PlainObject.ObjectNamespace, PlainObject.ObjectName, complexAttribute, out hideFromUser);
						if (hideFromUser) continue;
					}
					if (complexAttributeEditor == null)
						complexAttributeEditor = (IComplexAttributeEditor)ObjectAttributeEditorFactory.CreateComplexAttributeEditor(PlainObject.ObjectNamespace, PlainObject.ObjectName, complexAttribute, GetPlainObjectListQualifiedNamespace);
					editors.Add((FrameworkElement) complexAttributeEditor, complexAttribute.UIPreferredIndex);
					if (complexAttributeEditor.MigratedAttributes.Count == 0)
					{
						complexAttributeEditor.Load(predefinedAttributeValues);
					}
				}

			List<FrameworkElement> editorList = new List<FrameworkElement>(editors.Keys);
			editorList.Sort((x, y) => editors[x] - editors[y]);
			foreach (FrameworkElement editor in editorList)
			{
				LayoutRoot.Children.Add(editor);
			}

			GetInstanceAttributeValues();
			if (SetUpComplete != null) SetUpComplete(sender, EventArgs.Empty);
		}

		public GetPlainObjectListQualifiedNamespaceDelegate GetPlainObjectListQualifiedNamespace;
		public CreateComplexAttributeEditorDelegate CreateComplexAttributeEditor;
		public event EventHandler SetUpComplete;

		public event EditCompleteDelegate EditComplete;
		public delegate void EditCompleteDelegate(bool success);

		public void CommitChanges()
		{
			SetInstanceAttributeValues();
			if (AfterUpdateAttributeProcessor != null)
				AfterUpdateAttributeProcessor.Process(instance, currentMode == Mode.Insert ? "Insert" : "Update");
			instance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteSave;
			if (currentMode == Mode.Insert)
			{
				instance.InsertAsync();
			}
			else if (currentMode == Mode.Update)
			{
				instance.UpdateAsync();
			}
		}

		private void InstanceOnExecuteMethodCompleteSave(object sender, ExecuteMethodCompleteEventArgs e)
		{
			instance.ExecuteMethodComplete -= InstanceOnExecuteMethodCompleteSave;
			bool success = true;
			if (e.Exception != null)
			{
				success = false;
				MessageBox.Show(e.Exception);
			}
			if (e.ErrorMessages.Count > 0)
			{
				success = false;
				MessageBox.Show(e.ErrorMessages[0].Text);
			}
			if (EditComplete != null)
				EditComplete(success);
		}
	}
}