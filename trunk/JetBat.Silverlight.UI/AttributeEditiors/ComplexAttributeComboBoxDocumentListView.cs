using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public class ComplexAttributeComboBoxDocumentListView : ContentControl, IComplexAttributeEditor
	{
		public ObjectComplexAttribute ComplexAttribute
		{
			get { return complexAttribute; }
			set
			{
				if (complexAttribute == value) return;
				complexAttribute = value;
				textBlockHeader.Text = ComplexAttribute.UILabel;
			}
		}

		public string ListViewNamespace { get; set; }
		public string ListViewName { get; set; }
		public string DisplayMember { get; set; }

		public string UILabel
		{
			get { return uiLabel; }
			set
			{
				uiLabel = value;
				if (complexAttribute == null)
					textBlockHeader.Text = uiLabel;
			}
		}

		public string ComplexAttributeName
		{
			get { return ComplexAttribute.Name; }
		}


		public event EventHandler LoadCompleted;
		public event EventHandler SelectionChanged;

		private ObjectComplexAttribute complexAttribute;
		private readonly TextBlock textBlockHeader = new TextBlock();
		private Border innerBorder;
		private readonly ComboBox comboBox = new ComboBox();
		private readonly TextBox textBoxReadOnly = new TextBox();
		private string uiLabel;

		private readonly MetadataProvider metadataProvider = new MetadataProvider();
		private NamedObjectCollection<NameValue> selectedItemKey;
		private NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();

		public Dictionary<string, string> MigratedAttributes { get; private set; }

		public ComplexAttributeComboBoxDocumentListView()
		{
			listViewLoader = new ListViewLoader();
			listViewLoader.OnLoadComplete += listViewLoaderOnOnLoadComplete;
			construct();
		}

		public ComplexAttributeComboBoxDocumentListView(ObjectComplexAttribute complexAttribute, string listViewNamespace, string listViewName,
														string displayMember)
			: this(complexAttribute, listViewNamespace, listViewName, displayMember, new Dictionary<string, string>(0))
		{
		}

		public ComplexAttributeComboBoxDocumentListView(ObjectComplexAttribute complexAttribute, string listViewNamespace, string listViewName, string displayMember, Dictionary<string, string> migratedAttributes)
		{
			listViewLoader = new ListViewLoader();
			listViewLoader.OnLoadComplete += listViewLoaderOnOnLoadComplete;
			ComplexAttribute = complexAttribute;
			DisplayMember = displayMember;
			ListViewNamespace = listViewNamespace;
			ListViewName = listViewName;
			MigratedAttributes = migratedAttributes;

			construct();
		}

		private void construct()
		{
			Border border = new Border();
			border.Padding = new Thickness(1);
			border.Margin = new Thickness(2);
			border.BorderThickness = new Thickness(1);
			border.CornerRadius = new CornerRadius(3);
			border.BorderBrush = new SolidColorBrush(Colors.Black);
			StackPanel stackPanel = new StackPanel();
			border.Child = stackPanel;

			textBlockHeader.Text = ComplexAttribute != null ? ComplexAttribute.UILabel : UILabel;
			textBlockHeader.Margin = new Thickness(1);
			textBlockHeader.Foreground = new SolidColorBrush(Colors.Blue);
			stackPanel.Children.Add(textBlockHeader);

			comboBox.BorderThickness = new Thickness(1);
			comboBox.Margin = new Thickness(1);
			comboBox.SelectionChanged += comboBoxSelectionChanged;

			textBoxReadOnly.BorderThickness = new Thickness(1);
			textBoxReadOnly.Margin = new Thickness(1);
			textBoxReadOnly.IsReadOnly = true;
			textBoxReadOnly.Text = "[не задано]";

			innerBorder = new Border();
			innerBorder.BorderThickness = new Thickness(0);
			innerBorder.Background = new SolidColorBrush(Colors.White);
			innerBorder.Child = comboBox;

			stackPanel.Children.Add(innerBorder);

			comboBox.IsEnabled = false;

			Content = border;
		}

		void comboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (SelectionChanged != null)
				SelectionChanged(sender, EventArgs.Empty);
		}

		public NamedObjectCollection<NameValue> SelectedObject
		{
			get
			{
				if (comboBox.SelectedItem == null) return null;
				return ((ComboBoxDataItem)comboBox.SelectedItem).DataGridRow.Attributes;
			}
		}

		public void Load(NamedObjectCollection<NameValue> actualParameters)
		{
			parameters = actualParameters;
			//TODO: Убрать прототипичный хак
			parameters.Add(new NameValue { Name = "StartDateTime", Value = DateTime.Now.AddDays(-7) });
			parameters.Add(new NameValue { Name = "EndDateTime", Value = DateTime.Now });
			try
			{
				metadataProvider.LoadDocumentListViewDefinitionCompleted += proxyGetDocumentDefinitionCompleted;
				metadataProvider.LoadDocumentListViewDefinitionAsync(ListViewNamespace, ListViewName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void proxyGetDocumentDefinitionCompleted(object sender, MetadataProvider.LoadDocumentListViewEventArgs e)
		{
			if (e.Exception != null)
			{
				MessageBox.Show(e.Exception.InnerException.ToString());
				return;
			}

			comboBox.Items.Clear();

			try
			{
				listViewLoader.Load(ListViewNamespace, ListViewName, parameters);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void listViewLoaderOnOnLoadComplete(Exception exception, List<DataGridRow> rows)
		{
			if (exception != null)
			{
				MessageBox.Show(exception.InnerException.ToString());
				return;
			}

			comboBox.Items.Clear();
			foreach (DataGridRow dataGridRow in rows)
			{
				comboBox.Items.Add(new ComboBoxDataItem(dataGridRow, DisplayMember));
			}

			if (selectedItemKey != null)
				foreach (ComboBoxDataItem dataItem in comboBox.Items)
				{
					if (dataItem.DataGridRow.CheckColumnValueMatch(selectedItemKey))
					{
						comboBox.SelectedItem = dataItem;
					}
				}

			comboBox.IsEnabled = true;
			if (LoadCompleted != null)
				LoadCompleted(this, EventArgs.Empty);
		}

		public void SelectItem(NamedObjectCollection<NameValue> complexAttributeValue)
		{
			selectedItemKey = complexAttributeValue;
			if (!comboBox.IsEnabled) return;

			foreach (ComboBoxDataItem dataItem in comboBox.Items)
			{
				if (dataItem.DataGridRow.CheckColumnValueMatch(selectedItemKey))
				{
					comboBox.SelectedItem = dataItem;
				}
			}
		}

		#region IsReadOnly

		private bool isReadOnly;
		private readonly ListViewLoader listViewLoader;

		public bool IsReadOnly
		{
			get { return isReadOnly; }
			set
			{
				isReadOnly = value;
				innerBorder.Child = isReadOnly ? (UIElement)textBoxReadOnly : comboBox;
			}
		}

		#endregion
	}
}