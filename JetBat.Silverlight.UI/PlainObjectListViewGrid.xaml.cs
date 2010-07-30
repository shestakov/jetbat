using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;
using JetBat.Silverlight.UI.AttributeEditiors;
using JetBat.Silverlight.UI.UserActions;

namespace JetBat.Silverlight.UI
{
	public partial class PlainObjectListViewGrid
	{
		private string objectName;
		private string objectNamespace;
		PlainObjectListView plainObjectListView;
		private PlainObject plainObject;
		private EditorWindow editorWindow;
		private PlainObjectInstance deletedInstance;
		private NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
		private readonly MetadataProvider metadataProvider = new MetadataProvider();

		public PlainObjectEditPanel PlainObjectEditPanel { get; set; }
		public GetPlainObjectListQualifiedNamespaceDelegate GetPlainObjectListQualifiedNamespace;
		public CreateComplexAttributeEditorDelegate CreateComplexAttributeEditor;

		public event EventHandler SelectionChanged;
		public event EventHandler LoadComplete;

		public PlainObjectListViewGrid()
		{
			UserActions = new List<UserAction>();
			listViewLoader = new ListViewLoader();
			listViewLoader.OnLoadComplete += LoaderOnOnLoadComplete;

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

		#region ShowToolbar

		private bool showToolbar = true;

		[DefaultValue(true)]
		public bool ShowToolbar
		{
			get { return showToolbar; }
			set
			{
				if (showToolbar == value) return;
				showToolbar = value;
				stackPanelUserActionsToolBar.Visibility = showToolbar ? Visibility.Visible : Visibility.Collapsed;
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
		private DataGridRow storedSelectedItem;
		private UserAction userActionInsert;
		private UserAction userActionUpdate;
		private UserAction userActionDelete;
		private UserAction userActionRestore;
		private UserAction userActionRefresh;
		private CollectionViewSource viewSource;
		private string filterTextLowered;
		private readonly ListViewLoader listViewLoader;

		[DefaultValue(true)]
		public bool ShowButtonText
		{
			get { return showButtonText; }
			set
			{
				if (showButtonText == value) return;
				showButtonText = value;
				foreach (UserActionButton userActionButton in stackPanelUserActionsToolBar.Children)
				{
					userActionButton.ShowText = showButtonText;
				}
			}
		}

		public List<UserAction> UserActions { get; private set; }

		private void createToolBarButtons()
		{
			UserActions = new List<UserAction>();

			userActionInsert = new UserAction
										{
											Text = "Добавить",
											Hint = "Добавить новый объект",
											OnExecute = OnExecuteInsert,
											BitmapImage = new BitmapImage(new Uri("Images/add_16.png", UriKind.Relative)),
											ShowText = true
										};
			UserActions.Add(userActionInsert);

			userActionUpdate = new UserAction
							{
								Text = "Изменить",
								Hint = "Изменить объект",
								OnExecute = OnExecuteUpdate,
								BitmapImage = new BitmapImage(new Uri("Images/edit_16.png", UriKind.Relative)),
								ShowText = true
							};
			UserActions.Add(userActionUpdate);

			userActionDelete = new UserAction
							{
								Text = "Удалить",
								Hint = "Удалить объект",
								OnExecute = OnExecuteDelete,
								BitmapImage = new BitmapImage(new Uri("Images/delete_16.png", UriKind.Relative)),
								ShowText = true
							};
			UserActions.Add(userActionDelete);

			userActionRestore = new UserAction
							{
								Text = "Восстановить",
								Hint = "Восстановить объект",
								OnExecute = OnExecuteRestore,
								BitmapImage = new BitmapImage(new Uri("Images/rollback_16.png", UriKind.Relative)),
								ShowText = true
							};
			UserActions.Add(userActionRestore);

			userActionRefresh = new UserAction
							{
								Text = "Обновить",
								Hint = "Обновить список",
								OnExecute = OnExecuteRefresh,
								BitmapImage = new BitmapImage(new Uri("Images/refresh_16.png", UriKind.Relative)),
								ShowText = true
							};
			UserActions.Add(userActionRefresh);

			foreach (UserAction action in UserActions)
			{
				UserActionButton button = new UserActionButton { UserAction = action };
				button.IsTabStop = false;
				stackPanelUserActionsToolBar.Children.Add(button);
			}
		}

		private void OnExecuteRefresh(object sender)
		{
			loadListViewContent();
		}

		private void OnExecuteDelete(object sender)
		{
			if (dataGrid.SelectedItems.Count != 1) return;
			if (MessageBox.Show("Вы уверены?", "Объект будет удален...", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
				return;
			if (plainObject == null) return;
			deletedInstance = new PlainObjectInstance(plainObject);

			deletedInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteLoadForDelete;
			deletedInstance.LoadAsync(GetSelectedObjectPrimaryKey());
		}

		private void OnExecuteRestore(object sender)
		{
			if (dataGrid.SelectedItems.Count != 1) return;
			if (MessageBox.Show("Вы уверены?", "Восстановление удаленного объекта...", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
				return;
			if (plainObject == null) return;
			deletedInstance = new PlainObjectInstance(plainObject);

			deletedInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteLoadForRestore;
			deletedInstance.LoadAsync(GetSelectedObjectPrimaryKey());
		}

		private void InstanceOnExecuteMethodCompleteLoadForDelete(object sender, ExecuteMethodCompleteEventArgs e)
		{
			deletedInstance.ExecuteMethodComplete -= InstanceOnExecuteMethodCompleteLoadForDelete;
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			if (e.MethodName == "Load")
			{
				deletedInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteDelete;
				deletedInstance.DeleteAsync();
			}
		}

		private void InstanceOnExecuteMethodCompleteLoadForRestore(object sender, ExecuteMethodCompleteEventArgs e)
		{
			deletedInstance.ExecuteMethodComplete -= InstanceOnExecuteMethodCompleteLoadForDelete;
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			if (e.MethodName == "Load")
			{
				deletedInstance.ExecuteMethodComplete += InstanceOnExecuteMethodCompleteDelete;
				deletedInstance.RestoreAsync();
			}
		}

		private void InstanceOnExecuteMethodCompleteDelete(object sender, ExecuteMethodCompleteEventArgs e)
		{
			if (e.Exception != null)
				MessageBox.Show(e.Exception);
			deletedInstance = null;
			loadListViewContent();
		}

		private void OnExecuteUpdate(object sender)
		{
			editorWindow = new EditorWindow();
			PlainObjectEditPanel = editorWindow.PlainObjectEditPanel;
			PlainObjectEditPanel.GetPlainObjectListQualifiedNamespace = GetPlainObjectListQualifiedNamespace;
			PlainObjectEditPanel.CreateComplexAttributeEditor = CreateComplexAttributeEditor;

			if (dataGrid.SelectedItems.Count != 1) return;
			if (PlainObjectEditPanel == null) return;
			PlainObjectEditPanel.SetUpComplete += PlainObjectEditPanelOnSetUpCompleteUpdate;
			PlainObjectEditPanel.AfterUpdateAttributeProcessor = AfterUpdateAttributeProcessor;
			PlainObjectEditPanel.SetUp(plainObjectListView.BasicObjectNamespace, plainObjectListView.BasicObjectName, PlainObjectEditPanel.Mode.Update, parameters);
		}

		public IAfterUpdateAttributeProcessor AfterUpdateAttributeProcessor { get; set; }

		private void PlainObjectEditPanelOnSetUpCompleteUpdate(object sender, EventArgs args)
		{
			PlainObjectEditPanel.SetUpComplete -= PlainObjectEditPanelOnSetUpCompleteUpdate;
			NamedObjectCollection<NameValue> primaryKey = GetSelectedObjectPrimaryKey();
			editorWindow.Closed += EditorWindowClosed;
			editorWindow.Show();
			PlainObjectEditPanel.Load(primaryKey);
		}

		void EditorWindowClosed(object sender, EventArgs e)
		{
			editorWindow.Closed -= EditorWindowClosed;
			if (editorWindow.DialogResult == true)
				loadListViewContent();
		}

		public event InsteadOfInsertEvent InsteadOfInsert;

		private void OnExecuteInsert(object sender)
		{
			if (InsteadOfInsert != null)
			{
				InsteadOfInsert(sender, EventArgs.Empty);
				return;
			}
			if (plainObjectListView == null) return;
			editorWindow = new EditorWindow();
			PlainObjectEditPanel = editorWindow.PlainObjectEditPanel;
			editorWindow.PlainObjectEditPanel.GetPlainObjectListQualifiedNamespace = GetPlainObjectListQualifiedNamespace;
			editorWindow.PlainObjectEditPanel.CreateComplexAttributeEditor = CreateComplexAttributeEditor;

			if (PlainObjectEditPanel == null) return;
			PlainObjectEditPanel.AfterUpdateAttributeProcessor = AfterUpdateAttributeProcessor;
			PlainObjectEditPanel.SetUpComplete += PlainObjectEditPanelOnSetUpCompleteInsert;
			PlainObjectEditPanel.SetUp(plainObjectListView.BasicObjectNamespace, plainObjectListView.BasicObjectName, PlainObjectEditPanel.Mode.Insert, parameters);
		}

		private void PlainObjectEditPanelOnSetUpCompleteInsert(object sender, EventArgs e)
		{
			PlainObjectEditPanel.SetUpComplete -= PlainObjectEditPanelOnSetUpCompleteInsert;
			editorWindow.Closed += EditorWindowClosed;
			editorWindow.Show();
		}

		#endregion

		public List<DataGridRow> Rows
		{
			get
			{
				return (List<DataGridRow>)dataGrid.ItemsSource;
			}
		}

		public List<NamedObjectCollection<NameValue>> Items
		{
			get
			{
				List<NamedObjectCollection<NameValue>> items = new List<NamedObjectCollection<NameValue>>();
				foreach (DataGridRow dataGridRow in dataGrid.ItemsSource)
				{
					NamedObjectCollection<NameValue> item = new NamedObjectCollection<NameValue>();
					foreach (ObjectAttribute attribute in plainObjectListView.Attributes)
					{
						NameValue primaryKeyAttribute = new NameValue
							{
								Name = attribute.Name,
								Value = dataGridRow[attribute.Name]
							};
						item.Add(primaryKeyAttribute);
					}
					items.Add(item);
				}
				return items;
			}
		}

		public NamedObjectCollection<NameValue> GetSelectedObjectPrimaryKey()
		{
			NamedObjectCollection<NameValue> primaryKey = new NamedObjectCollection<NameValue>();
			if (dataGrid.SelectedItem == null) return null;
			DataGridRow dataGridRow = (DataGridRow)dataGrid.SelectedItem;
			foreach (ObjectAttribute attribute in plainObjectListView.Attributes)
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
				foreach (ObjectAttribute attribute in plainObjectListView.Attributes)
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

		public void Reload()
		{
			userActionRefresh.Execute();
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

			if (plainObjectListView != null && plainObjectListView.ObjectNamespace == objectNamespace && plainObjectListView.ObjectName == objectName)
			{
				loadListViewContent();
			}
			else
			{
				try
				{
					metadataProvider.LoadPlainObjectListViewDefinitionCompleted += proxyGetPlainObjectListViewDefinitionCompleted;
					metadataProvider.LoadPlainObjectListViewDefinitionAsync(listViewNamespace, listViewName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		public void LoadListViewDefinition(string listViewNamespace, string listViewName)
		{
			if (isLoading) return;
			if (plainObjectListView != null && plainObjectListView.ObjectNamespace == listViewNamespace && plainObjectListView.ObjectName == listViewName)
				return;
			IsEnabled = false;
			objectNamespace = listViewNamespace;
			objectName = listViewName;
			try
			{
				metadataProvider.LoadPlainObjectListViewDefinitionCompleted += proxyGetPlainObjectListViewDefinitionOnlyCompleted;
				metadataProvider.LoadPlainObjectListViewDefinitionAsync(listViewNamespace, listViewName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		void proxyGetPlainObjectListViewDefinitionOnlyCompleted(object sender, MetadataProvider.LoadPlainObjectListViewEventArgs e)
		{
			metadataProvider.LoadPlainObjectListViewDefinitionCompleted -= proxyGetPlainObjectListViewDefinitionOnlyCompleted;
			dataGrid.ItemsSource = null;
			dataGrid.AutoGenerateColumns = false;
			dataGrid.Columns.Clear();
			dataGrid.CanUserSortColumns = true;
			plainObjectListView = e.PlainObjectListView;
			textBlockCaption.Text = plainObjectListView.UIListCaption;
			dataGrid.HorizontalContentAlignment = HorizontalAlignment.Stretch;

			Dictionary<DataGridColumn, int> columns = new Dictionary<DataGridColumn, int>();

			foreach (ObjectAttribute attribute in plainObjectListView.Attributes)
			{
				DataGridTextColumn column = new DataGridTextColumn
				{
					Width = new DataGridLength(attribute.UIPreferredWidth > 0 ? attribute.UIPreferredWidth : 200),
					Header = attribute.UILabel,
					CanUserSort = true,
					SortMemberPath = string.Format("[{0}]", attribute.Name),
					Visibility = attribute.IsUserVisible ? Visibility.Visible : Visibility.Collapsed,
					Binding = new Binding
					{
						Converter = new DataGridRowIndexConverter(),
						ConverterParameter = attribute.Name
					}
				};
				columns.Add(column, attribute.UIPreferredIndex);
			}

			List<DataGridColumn> columnList = new List<DataGridColumn>(columns.Keys);
			columnList.Sort((x, y) => columns[x] - columns[y]);
			foreach (DataGridColumn column in columnList)
			{
				dataGrid.Columns.Add(column);
			}

			try
			{
				metadataProvider.LoadPlainObjectDefinitionCompleted += proxyGetPlainObjectDefinitionCompleted;
				metadataProvider.LoadPlainObjectDefinitionAsync(plainObjectListView.BasicObjectNamespace, plainObjectListView.BasicObjectName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void loadListViewContent()
		{
			IsEnabled = false;
			storedSelectedItem = (DataGridRow)dataGrid.SelectedItem;

			try
			{
				listViewLoader.Load(objectNamespace, objectName, parameters);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void LoaderOnOnLoadComplete(Exception exception, List<DataGridRow> rows)
		{
			if (exception != null)
			{
				MessageBox.Show(exception.InnerException.ToString());
				return;
			}

			viewSource = new CollectionViewSource();
			viewSource.Source = rows;
			viewSource.View.Filter = new Predicate<object>(Include);
			dataGrid.ItemsSource = viewSource.View;

			if (storedSelectedItem != null)
			{
				foreach (DataGridRow row in rows)
				{
					bool match = true;
					foreach (ObjectAttribute attribute in plainObject.Attributes)
					{
						if (attribute.IsPrimaryKeyMember && !row[attribute.Name].Equals(storedSelectedItem[attribute.Name]))
						{
							match = false;
							break;
						}
					}
					if (match)
					{
						dataGrid.SelectedItem = row;
						dataGrid.UpdateLayout();
						dataGrid.ScrollIntoView(dataGrid.SelectedItem, null);
						break;
					}
				}
				storedSelectedItem = null;
			}
			else if (rows.Count > 0)
			{
				dataGrid.SelectedIndex = 0;
			}
			IsEnabled = true;
			isLoading = false;
			if (LoadComplete != null)
				LoadComplete(this, EventArgs.Empty);
		}

		void proxyGetPlainObjectListViewDefinitionCompleted(object sender, MetadataProvider.LoadPlainObjectListViewEventArgs e)
		{
			metadataProvider.LoadPlainObjectListViewDefinitionCompleted -= proxyGetPlainObjectListViewDefinitionCompleted;
			if (e.Exception != null)
			{
				MessageBox.Show(e.Exception.InnerException.ToString());
				return;
			}
			plainObjectListView = e.PlainObjectListView;

			proxyGetPlainObjectListViewDefinitionOnlyCompleted(sender, e);

			loadListViewContent();

			try
			{
				metadataProvider.LoadPlainObjectDefinitionCompleted += proxyGetPlainObjectDefinitionCompleted;
				metadataProvider.LoadPlainObjectDefinitionAsync(plainObjectListView.BasicObjectNamespace, plainObjectListView.BasicObjectName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void setupUserActions()
		{
			userActionInsert.IsEnabled = plainObject.Methods.Contains("Insert");
			userActionUpdate.IsEnabled = plainObject.Methods.Contains("Update");
			userActionDelete.IsEnabled = plainObject.Methods.Contains("Delete");
			userActionRestore.IsEnabled = plainObject.Methods.Contains("Restore");
		}

		private void proxyGetPlainObjectDefinitionCompleted(object sender, MetadataProvider.LoadPlainObjectEventArgs e)
		{
			metadataProvider.LoadPlainObjectDefinitionCompleted -= proxyGetPlainObjectDefinitionCompleted;
			if (e.Exception != null)
			{
				MessageBox.Show(e.Exception.InnerException.ToString());
				return;
			}
			plainObject = e.PlainObject;
			setupUserActions();
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
			plainObjectListView = null;
		}

		public void ClearItemsOnly()
		{
			dataGrid.ItemsSource = null;
		}

		private void DataGrid_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Insert)
				userActionInsert.Execute();
			else if (e.Key == Key.Delete)
				userActionDelete.Execute();
			else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.R)
				userActionRefresh.Execute();
#if DEBUG
			if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.C)
			{
				StringBuilder builder = new StringBuilder();
				foreach (DataGridColumn column in dataGrid.Columns)
				{
					builder.AppendLine(string.Format("{0} : {1}", column.Header, column.ActualWidth));
				}
				MessageBox.Show(builder.ToString());
			}
#endif
		}

		public bool Include(object obj)
		{
			if (string.IsNullOrEmpty(filterTextLowered)) return true;
			var row = (DataGridRow)obj;
			foreach (NameValue nameValue in row.Attributes)
				if (nameValue.Value != null && nameValue.Value.ToString().ToLower().Contains(filterTextLowered)) return true;
			return false;
		}

		private void QuickFilterPrompt_OnTextChanged(object sender, EventArgs e1)
		{
			if (viewSource == null) return;
			filterTextLowered = string.IsNullOrEmpty(textBoxFilter.FilterPattern) ? null : textBoxFilter.FilterPattern.ToLower();
			viewSource.View.Refresh();
		}
	}

	public delegate void InsteadOfInsertEvent(object sender, EventArgs args);
}