using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;
using JetBat.Silverlight.UI.AttributeEditiors;
using JetBat.Silverlight.UI.UserActions;

namespace JetBat.Silverlight.UI
{
	public partial class DocumentListViewGrid
	{
		private string objectName;
		private string objectNamespace;
		DocumentListView documentListView;
		private Document document;
		private DocumentInstance deletedInstance;
		private DocumentInstance commitedInstance;
		private DocumentInstance rolledBackInstance;

		readonly MetadataProvider metadataProvider = new MetadataProvider();
		public PlainObjectEditPanel PlainObjectEditPanel { get; set; }
		public GetPlainObjectListQualifiedNamespaceDelegate GetPlainObjectListQualifiedNamespace;
		public CreateComplexAttributeEditorDelegate CreateComplexAttributeEditor;
		private NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
		public event EventHandler SelectionChanged;

		public DocumentListViewGrid()
		{
			UserActions = new List<UserAction>();
			listViewLoader = new ListViewLoader();
			listViewLoader.OnLoadComplete += listViewLoaderOnOnLoadComplete;

			InitializeComponent();
			createToolBarButtons();
		}

		#region ShowBorder

		private bool showBorder = true;

		[DefaultValue(true)]
		public bool ShowBorder
		{
			get { return showBorder; }
			set
			{
				if (showBorder == value) return;
				showBorder = value;
				borderOuter.BorderThickness = showBorder ? new Thickness(1) : new Thickness(0);
			}
		}

		#endregion

		#region ShowHeader

		private bool showHeader = true;

		[DefaultValue(true)]
		public bool ShowHeader
		{
			get { return showHeader; }
			set
			{
				if (showHeader == value) return;
				showHeader = value;
				borderHeader.Visibility = showHeader ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		#endregion

		#region User Actions

		private bool showButtonText = true;
		private bool isLoading;
		private readonly ListViewLoader listViewLoader;

		[DefaultValue(true)]
		public bool ShowButtonText
		{
			get { return showButtonText; }
			set
			{
				if (showButtonText == value) return;
				showButtonText = value;
				foreach (UserActionButton userActionButton in stackPanelToolBar.Children)
				{
					userActionButton.ShowText = showButtonText;
				}
			}
		}

		public List<UserAction> UserActions { get; private set; }

		private void createToolBarButtons()
		{
			UserActions = new List<UserAction>();

			UserAction userAction = new UserAction
										{
											Text = "Добавить",
											Hint = "Добавить новый объект",
											OnExecute = OnExecuteInsert,
											BitmapImage = new BitmapImage(new Uri("Images/add_16.png", UriKind.Relative)),
											ShowText = true
										};
			UserActions.Add(userAction);

			userAction = new UserAction
							{
								Text = "Изменить",
								Hint = "Изменить объект",
								OnExecute = OnExecuteUpdate,
								BitmapImage = new BitmapImage(new Uri("Images/edit_16.png", UriKind.Relative)),
								ShowText = true
							};
			UserActions.Add(userAction);

			userAction = new UserAction
			{
				Text = "Применить",
				Hint = "Применить документ",
				OnExecute = OnExecuteCommit,
				BitmapImage = new BitmapImage(new Uri("Images/commit_16.png", UriKind.Relative)),
				ShowText = true
			};
			UserActions.Add(userAction);

			userAction = new UserAction
			{
				Text = "Отменить",
				Hint = "Отменить документ",
				OnExecute = OnExecuteRollback,
				BitmapImage = new BitmapImage(new Uri("Images/rollback_16.png", UriKind.Relative)),
				ShowText = true
			};
			UserActions.Add(userAction);

			userAction = new UserAction
							{
								Text = "Удалить",
								Hint = "Удалить объект",
								OnExecute = OnExecuteDelete,
								BitmapImage = new BitmapImage(new Uri("Images/delete_16.png", UriKind.Relative)),
								ShowText = true
							};
			UserActions.Add(userAction);

			userAction = new UserAction
							{
								Text = "Обновить",
								Hint = "Обновить список",
								OnExecute = OnExecuteRefresh,
								BitmapImage = new BitmapImage(new Uri("Images/refresh_16.png", UriKind.Relative)),
								ShowText = true
							};
			UserActions.Add(userAction);

			foreach (UserAction action in UserActions)
			{
				UserActionButton button = new UserActionButton { UserAction = action };
				stackPanelToolBar.Children.Add(button);
			}
		}

		private void OnExecuteRollback(object sender)
		{
			if (dataGrid.SelectedItems.Count != 1) return;
			if (MessageBox.Show("Вы уверены?", "Документ будет отменен...", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
				return;
			if (document == null) return;
			rolledBackInstance = new DocumentInstance(document);

			rolledBackInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteLoadForRollback;
			//NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
			//parameters.Add(new NameValue { Name = "DocumentID", Value = GetSelectedObjectPrimaryKey()["ID"].Value });

			rolledBackInstance.LoadAsync();
		}

		private void InstanceOnExecuteMethodCompleteLoadForRollback(object sender, ExecuteMethodCompleteEventArgs e)
		{
			rolledBackInstance.ExecuteMethodComplete -= InstanceOnExecuteMethodCompleteLoadForRollback;
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			if (e.MethodName == "Load")
			{
				rolledBackInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteRollback;
				rolledBackInstance.Rollback();
			}
		}

		private void InstanceOnExecuteMethodCompleteRollback(object sender, ExecuteMethodCompleteEventArgs e)
		{
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			rolledBackInstance = null;
		}

		private void OnExecuteCommit(object sender)
		{
			if (dataGrid.SelectedItems.Count != 1) return;
			if (MessageBox.Show("Вы уверены?", "Документ будет применен...", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
				return;
			if (document == null) return;
			commitedInstance = new DocumentInstance(document);

			commitedInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteLoadForCommit;
			//NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
			//parameters.Add(new NameValue { Name = "DocumentID", Value = GetSelectedObjectPrimaryKey()["ID"].Value });

			commitedInstance.LoadAsync(parameters);
		}

		private void InstanceOnExecuteMethodCompleteLoadForCommit(object sender, ExecuteMethodCompleteEventArgs e)
		{
			commitedInstance.ExecuteMethodComplete -= InstanceOnExecuteMethodCompleteLoadForCommit;
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			if (e.MethodName == "Load")
			{
				commitedInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteCommit;
				commitedInstance.Commit();
			}
		}

		private void InstanceOnExecuteMethodCompleteCommit(object sender, ExecuteMethodCompleteEventArgs e)
		{
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			commitedInstance = null;
		}

		private void OnExecuteRefresh(object sender)
		{
			refresh();
		}

		private void OnExecuteDelete(object sender)
		{
			if (dataGrid.SelectedItems.Count != 1) return;
			if (MessageBox.Show("Вы уверены?", "Объект будет удален...", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
				return;
			if (document == null) return;
			deletedInstance = new DocumentInstance(document);

			deletedInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteLoadForDelete;
			//NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
			//parameters.Add(new NameValue { Name = "DocumentID", Value = GetSelectedObjectPrimaryKey()["ID"].Value });

			deletedInstance.LoadAsync();
		}

		private void InstanceOnExecuteMethodCompleteLoadForDelete(object sender, ExecuteMethodCompleteEventArgs e)
		{
			deletedInstance.ExecuteMethodComplete -= InstanceOnExecuteMethodCompleteLoadForDelete;
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			if (e.MethodName == "Load")
			{
				deletedInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteDelete;
				deletedInstance.Delete(new NamedObjectCollection<NameValue> { new NameValue { Name = "DocumentID", Value = deletedInstance.DocumentID.Value } });
			}
		}

		private void InstanceOnExecuteMethodCompleteDelete(object sender, ExecuteMethodCompleteEventArgs e)
		{
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			deletedInstance = null;
		}

		private void OnExecuteUpdate(object sender)
		{
			if (dataGrid.SelectedItems.Count != 1) return;
			if (UpdateDocument != null)
				UpdateDocument(documentListView.BasicObjectNamespace, documentListView.BasicObjectName, GetSelectedObjectPrimaryKey());
		}

		public delegate void UpdateDocumentDelegate(string documentNamespace, string documentName, NamedObjectCollection<NameValue> primaryKey);
		public event UpdateDocumentDelegate UpdateDocument;

		private void OnExecuteInsert(object sender)
		{
			if (CreateDocument != null)
				CreateDocument(documentListView.BasicObjectNamespace, documentListView.BasicObjectName);
		}

		public delegate void CreateDocumentDelegate(string documentNamespace, string documentName);
		public event CreateDocumentDelegate CreateDocument;

		#endregion

		public NamedObjectCollection<NameValue> GetSelectedObjectPrimaryKey()
		{
			NamedObjectCollection<NameValue> primaryKey = new NamedObjectCollection<NameValue>();
			if (dataGrid.SelectedItem == null) return null;
			DataGridRow dataGridRow = (DataGridRow)dataGrid.SelectedItem;
			foreach (ObjectAttribute attribute in documentListView.Attributes)
			{
				if (attribute.IsPrimaryKeyMember)
				{
					NameValue primaryKeyAttribute = new NameValue
					{
						Name = attribute.Name,
						Value = dataGridRow[attribute.Name]
					};
					primaryKey.Add(primaryKeyAttribute);
				}
			}
			return primaryKey;
		}

		public NamedObjectCollection<NameValue> SelectedObjectAttributes
		{
			get
			{
				if (dataGrid.SelectedItem == null) return null;
				NamedObjectCollection<NameValue> primaryKey = new NamedObjectCollection<NameValue>();
				DataGridRow dataGridRow = (DataGridRow)dataGrid.SelectedItem;
				foreach (ObjectAttribute attribute in documentListView.Attributes)
				{
					NameValue primaryKeyAttribute = new NameValue
														{
															Name = attribute.Name,
															Value = dataGridRow[attribute.Name]
														};
					primaryKey.Add(primaryKeyAttribute);
				}
				return primaryKey;
			}
		}

		public void LoadList(string listViewNamespace, string listViewName)
		{
			LoadList(listViewNamespace, listViewName, new NamedObjectCollection<NameValue>());
		}

		public void LoadList(string listViewNamespace, string listViewName, NamedObjectCollection<NameValue> actualParameters)
		{
			if (isLoading) return;
			isLoading = true;
			IsEnabled = false;
			parameters = actualParameters;
			objectNamespace = listViewNamespace;
			objectName = listViewName;
			try
			{
				metadataProvider.LoadDocumentListViewDefinitionCompleted += proxyGetDocumentListViewDefinitionCompleted;
				metadataProvider.LoadDocumentListViewDefinitionAsync(listViewNamespace, listViewName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		public void PreLoadList(string listViewNamespace, string listViewName)
		{
			PreLoadList(listViewNamespace, listViewName, new NamedObjectCollection<NameValue>());
		}

		public void PreLoadList(string listViewNamespace, string listViewName, NamedObjectCollection<NameValue> actualParameters)
		{
			if (isLoading) return;
			IsEnabled = false;
			parameters = actualParameters;
			objectNamespace = listViewNamespace;
			objectName = listViewName;
			try
			{
				metadataProvider.LoadDocumentListViewDefinitionCompleted += proxyGetDocumentListViewDefinitionOnlyCompleted;
				metadataProvider.LoadDocumentListViewDefinitionAsync(listViewNamespace, listViewName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		public event EventHandler LoadComplete;

		private void refresh()
		{
			IsEnabled = false;
			try
			{
				listViewLoader.Load(objectNamespace, objectName, parameters);
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
			dataGrid.ItemsSource = rows;
			if (rows.Count > 0)
			{
				dataGrid.SelectedIndex = 0;
			}
			IsEnabled = true;
			isLoading = false;
			if (LoadComplete != null)
				LoadComplete(this, EventArgs.Empty);
		}

		void proxyGetDocumentListViewDefinitionOnlyCompleted(object sender, MetadataProvider.LoadDocumentListViewEventArgs e)
		{
			dataGrid.ItemsSource = null;
			dataGrid.AutoGenerateColumns = false;
			dataGrid.Columns.Clear();
			dataGrid.CanUserSortColumns = false;
			documentListView = e.DocumentListView;
			textBlockCaption.Text = documentListView.UIListCaption;
			dataGrid.HorizontalContentAlignment = HorizontalAlignment.Stretch;
			foreach (ObjectAttribute attribute in documentListView.Attributes)
			{
				DataGridTextColumn column = new DataGridTextColumn
				{
					Width = new DataGridLength(200),
					Header = attribute.UILabel,
					CanUserSort = false,
					SortMemberPath = attribute.Name,
					Visibility = attribute.IsUserVisible ? Visibility.Visible : Visibility.Collapsed,
					Binding = new Binding
					{
						Converter = new DataGridRowIndexConverter(),
						ConverterParameter = attribute.Name
					}
				};

				dataGrid.Columns.Add(column);
			}

			resizeColumns(dataGrid.ActualWidth);
		}

		private void dataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			resizeColumns(e.NewSize.Width);
		}

		void proxyGetDocumentListViewDefinitionCompleted(object sender, MetadataProvider.LoadDocumentListViewEventArgs e)
		{
			if (e.Exception != null)
			{
				MessageBox.Show(e.Exception.InnerException.ToString());
				return;
			}
			documentListView = e.DocumentListView;
			proxyGetDocumentListViewDefinitionOnlyCompleted(sender, e);

			try
			{
				listViewLoader.Load(objectNamespace, objectName, parameters);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

			try
			{
				metadataProvider.LoadDocumentDefinitionCompleted += proxyGetDocumentDefinitionCompleted;
				metadataProvider.LoadDocumentDefinitionAsync(documentListView.BasicObjectNamespace, documentListView.BasicObjectName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void proxyGetDocumentDefinitionCompleted(object sender, MetadataProvider.LoadDocumentEventArgs e)
		{
			if (e.Exception != null)
			{
				MessageBox.Show(e.Exception.InnerException.ToString());
				return;
			}
			document = e.Document;
		}

		private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (SelectionChanged != null)
				SelectionChanged(sender, e);
		}

		public void Clear()
		{
			dataGrid.ItemsSource = null;
			dataGrid.Columns.Clear();
		}

		private void resizeColumns(double newWidth)
		{
			double visibleColumnCount = 0;

			foreach (DataGridTextColumn column in dataGrid.Columns)
			{
				if (column.Visibility == Visibility.Visible)
					visibleColumnCount = visibleColumnCount + 1;
			}

			if (newWidth == 0)
			{
				newWidth = 500;
			}

			if (visibleColumnCount != 0)
			{
				foreach (DataGridTextColumn column in dataGrid.Columns)
				{
					column.Width = new DataGridLength((newWidth / visibleColumnCount) - 2);
				}
			}
		}
	}
}