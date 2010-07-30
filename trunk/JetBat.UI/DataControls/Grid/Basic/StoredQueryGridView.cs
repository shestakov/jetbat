// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable UseObjectOrCollectionInitializer
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Definitions;
using JetBat.UI.UserActions;
using MessageBox = JetBat.UI.DataControls.Messages.MessageBox;

namespace JetBat.UI.DataControls.Grid.Basic
{
	[Serializable]
	public enum StoredQueryGridViewMode
	{
		ReadOnly
	} ;

	public partial class StoredQueryGridView : DataObjectListViewGrid
	{
		#region Initialization

		public StoredQueryGridView()
		{
			InitializeComponent();
			createUserActions();
		}

		#endregion

		#region User Actions

		#region Fields

		private readonly UserAction userActionAdjustGrid = new UserAction();
		private readonly UserAction userActionRefresh = new UserAction();

		#endregion

		#region Creating user actions

		private void createUserActions()
		{
			#region UserActionRefresh #1

			userActionRefresh.Active = true;
			userActionRefresh.BriefText = "Обновить";
			userActionRefresh.Enabled = true;
			userActionRefresh.Hint = "Обновить список объектов";
			userActionRefresh.Image = Resource.Refresh_16;
			userActionRefresh.ImageTransparentColor = Color.Magenta;
			userActionRefresh.Name = "userActionRefresh";
			userActionRefresh.Shortcut = Keys.F5;
			userActionRefresh.Text = "Обновить список";
			userActionRefresh.ActionExecute += delegate { LoadList(); };

			#endregion

			#region UserActionAdjustGrid #2

			userActionAdjustGrid.Active = true;
			userActionAdjustGrid.BriefText = "Представление";
			userActionAdjustGrid.Enabled = true;
			userActionAdjustGrid.Hint = "Настроить представление списка";
			userActionAdjustGrid.Image = Resource.Customize_16;
			userActionAdjustGrid.ImageTransparentColor = Color.Magenta;
			userActionAdjustGrid.Name = "userActionAdjustGrid";
			userActionAdjustGrid.Permitted = true;
			userActionAdjustGrid.Shortcut = Keys.None;
			userActionAdjustGrid.Text = "Настройка представления";
			userActionAdjustGrid.ActionExecute += delegate { showSettingsForm(); };

			#endregion

			userActions.Clear();
			userActions.AddPersistent(userActionRefresh);
			userActions.AddPersistent(userActionAdjustGrid);
		}

		#endregion

		#region Adjustment

		protected override void setActionEnabledState()
		{
		}

		protected override void setActionActiveState()
		{
			userActionRefresh.Active = allowRefresh;
			//userActionRefresh.Active = validateParameters() & allowRefresh;
			userActionAdjustGrid.Active = allowCustomize;
		}

		#endregion

		#region Action allowance

		#region Attributes

		private bool allowCustomize = true;
		private bool allowDelete = true;
		private bool allowInsert = true;
		private bool allowNavigateBack = true;
		private bool allowPick = true;
		private bool allowRefresh = true;
		private bool allowUpdate = true;
		private bool allowView = true;

		#endregion

		#region Properties

		public bool AllowInsert
		{
			get { return allowInsert; }
			set
			{
				if (allowInsert == value) return;
				allowInsert = value;
				setActionActiveState();
			}
		}

		public bool AllowUpdate
		{
			get { return allowUpdate; }
			set
			{
				if (allowUpdate == value) return;
				allowUpdate = value;
				setActionActiveState();
			}
		}

		public bool AllowDelete
		{
			get { return allowDelete; }
			set
			{
				if (allowDelete == value) return;
				allowDelete = value;
				setActionActiveState();
			}
		}

		public bool AllowView
		{
			get { return allowView; }
			set
			{
				if (allowView == value) return;
				allowView = value;
				setActionActiveState();
			}
		}

		public bool AllowPick
		{
			get { return allowPick; }
			set
			{
				if (allowPick == value) return;
				allowPick = value;
				setActionActiveState();
			}
		}

		public bool AllowRefresh
		{
			get { return allowRefresh; }
			set
			{
				if (allowRefresh == value) return;
				allowRefresh = value;
				setActionActiveState();
			}
		}

		public bool AllowNavigateBack
		{
			get { return allowNavigateBack; }
			set
			{
				if (allowNavigateBack == value) return;
				allowNavigateBack = value;
				setActionActiveState();
			}
		}

		public bool AllowCustomize
		{
			get { return allowCustomize; }
			set
			{
				if (allowCustomize == value) return;
				allowCustomize = value;
				setActionActiveState();
			}
		}

		#endregion

		#endregion

		#region User action events

		public override event EventHandler ListLoad;

		#endregion

		#endregion

		#region Загрузка списка

		public override void ConfigureList()
		{
			if (accessAdapter == null) return;

			objectListViewDefinition = accessAdapter.MetadataStore.Get<PlainObjectListViewDefinition>(listNamespace, listName);

			if (objectListViewDefinition == null)
			{
				ClearList();
				setActionEnabledState();
				return;
			}

			PlainObjectDefinition plainObjectDefinition =
				accessAdapter.MetadataStore.Get<PlainObjectDefinition>(objectListViewDefinition.BasicObjectNamespace,
																	   objectListViewDefinition.BasicObjectName);

			if (plainObjectDefinition == null)
				throw new NullReferenceException(string.Format("Описание объекта [{0}] {1} для представления списка не найдено.", objectListViewDefinition.BasicObjectNamespace, objectListViewDefinition.BasicObjectName));

			Text = objectListViewDefinition.UIListCaption;

			userActionRefresh.SetupFromObjectActionDefinition(objectListViewDefinition.Actions["LoadList"]);

			setActionEnabledState();

			gridViewListSettingsCollection.Fix(accessAdapter, listNamespace, listName);
			currentGridViewListSettings = gridViewListSettingsCollection[listNamespace, listName];

			dataView.Table = null;
			initiateDataGridViewColumnSettings();
			foreach (ObjectAttributeDefinition attribute in objectListViewDefinition.Attributes)
			{
				addDataGridViewColumn(attribute);
			}

			setColumns();
			setSort();
			applyGridSettings();
			configured = true;
		}

		public override void LoadList()
		{
			if (!configured) ConfigureList();
			setActionActiveState();
			disableDataGridView();
			try
			{
				if ((objectListViewDefinition == null) && (accessAdapter != null))
					objectListViewDefinition = accessAdapter.MetadataStore.Get<StoredQueryDefinition>(listNamespace, listName);
				if (objectListViewDefinition == null)
					return;

				if (!validateParameters())
					throw new Exception("Не заданы параметры для загрузки списка");

				clearDataGridViewDataSource();
				ObjectListViewDefinition resultingObjectListViewDefinition;
				if (accessAdapter == null) throw new NullReferenceException("AccessAdapter is not set");
				dataTable = accessAdapter.ObjectFactory.LoadObjectListView(listNamespace, listName, prepareParameterSet(), out resultingObjectListViewDefinition);
				objectListViewDefinition = resultingObjectListViewDefinition;

				dataView.Table = dataTable;
				setSort();
				fill();

				enableDataGridView();
				setActionActiveState();
			}
			catch (Exception ex)
			{
				MessageBox.Show(Text + ":" + Environment.NewLine + ex.Message, "Загрузка списка", MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}

			if (ListLoad != null)
				ListLoad(this, EventArgs.Empty);
		}

		#endregion

		#region Родительский список и параметры списка

		protected override bool validateParameters()
		{
			if (objectListViewDefinition == null)
				return false;
			foreach (
				ObjectMethodParameterDefinition procParameter in objectListViewDefinition.MethodParameterDefinitionsLoadList)
			{
				bool found = false;
				foreach (string attribute in parameters.Keys)
					if (procParameter.ActualName == attribute)
					{
						found = true;
						break;
					}
				if (!found)
					return false;
			}

			return true;
		}

		#endregion

		#region UI event handling

		private void DataObjectGridView_KeyDown(object sender, KeyEventArgs e)
		{
			foreach (UserAction action in userActions)
			{
				if (action.Shortcut == e.KeyData)
				{
					action.Execute();
					e.SuppressKeyPress = false;
					return;
				}
			}
		}

		protected override void dataGridView_KeyDown(object sender, KeyEventArgs e)
		{
			DataObjectGridView_KeyDown(sender, e);
		}

		protected override void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
		}

		#endregion

		#region Режим работы

		private StoredQueryGridViewMode currentMode = StoredQueryGridViewMode.ReadOnly;

		[DefaultValue(StoredQueryGridViewMode.ReadOnly)]
		public StoredQueryGridViewMode CurrentMode
		{
			get { return currentMode; }
			set
			{
				if (currentMode == value) return;
				currentMode = value;
				setActionEnabledState();
				setActionActiveState();
			}
		}

		#endregion
	}
}

// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore SuggestUseVarKeywordEvident