// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable UseObjectOrCollectionInitializer
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.UI.DataControls.Forms;
using JetBat.UI.DataControls.Messages;
using JetBat.UI.UserActions;
using MessageBox = JetBat.UI.DataControls.Messages.MessageBox;

namespace JetBat.UI.DataControls.Grid.Basic
{
	[Serializable]
	public enum GridViewMode
	{
		Management,
		Pick,
		PickAndManagement,
		ReadOnly
	} ;

	public partial class PlainObjectGridView : DataObjectListViewGrid
	{
		#region Initialization

		public PlainObjectGridView()
		{
			InitializeComponent();
			createUserActions();
		}

		#endregion

		#region User Actions

		#region Fields

		private readonly UserAction userActionAdjustGrid = new UserAction();
		private readonly UserAction userActionDelete = new UserAction();
		private readonly UserAction userActionInsert = new UserAction();
		private readonly UserAction userActionNavigateBack = new UserAction();
		private readonly UserAction userActionPick = new UserAction();
		private readonly UserAction userActionRefresh = new UserAction();
		private readonly UserAction userActionUpdate = new UserAction();
		private readonly UserAction userActionView = new UserAction();

		#endregion

		#region Creating user actions

		private void createUserActions()
		{
			#region UserActionPick #1

			userActionPick.Active = true;
			userActionPick.BriefText = "Выбрать";
			userActionPick.Enabled = false;
			userActionPick.Hint = "Выбрать выделенный объект";
			userActionPick.Image = Resource.Pick_16;
			userActionPick.ImageTransparentColor = Color.Magenta;
			userActionPick.Name = "userActionPick";
			userActionPick.Shortcut = Keys.Return;
			userActionPick.Text = "Выбрать объект";
			userActionPick.ActionExecute += delegate { doPick(); };

			#endregion

			#region UserActionInsert #2

			userActionInsert.Active = true;
			userActionInsert.BriefText = "Создать";
			userActionInsert.Enabled = false;
			userActionInsert.Hint = "Создать новый объект, используя специальную форму";
			userActionInsert.Image = Resource.Add_16;
			userActionInsert.ImageTransparentColor = Color.Magenta;
			userActionInsert.Name = "userActionInsert";
			userActionInsert.Shortcut = Keys.Insert;
			userActionInsert.Text = "Создать объект";
			userActionInsert.ActionExecute += delegate { doInsert(); };

			#endregion

			#region UserActionUpdate #3

			userActionUpdate.Active = true;
			userActionUpdate.BriefText = "Изменить";
			userActionUpdate.Enabled = false;
			userActionUpdate.Hint = "Изменить атрибуты объекта с помощью специальной формы";
			userActionUpdate.Image = Resource.Edit_16;
			userActionUpdate.ImageTransparentColor = Color.Magenta;
			userActionUpdate.Name = "userActionUpdate";
			userActionUpdate.Shortcut = Keys.Control | Keys.Return;
			userActionUpdate.Text = "Изменить атрибуты объекта";
			userActionUpdate.ActionExecute += delegate { doUpdate(); };

			#endregion

			#region UserActionDelete #4

			userActionDelete.Active = true;
			userActionDelete.BriefText = "Удалить";
			userActionDelete.Enabled = false;
			userActionDelete.Hint = "Удалить выделенные объекты";
			userActionDelete.Image = Resource.Delete_16;
			userActionDelete.ImageTransparentColor = Color.Magenta;
			userActionDelete.Name = "userActionDelete";
			userActionDelete.Shortcut = Keys.Delete;
			userActionDelete.Text = "Удалить объект";
			userActionDelete.ActionExecute += delegate { doDelete(); };

			#endregion

			#region UserActionView #5

			userActionView.Active = true;
			userActionView.BriefText = "Просмотреть";
			userActionView.Enabled = false;
			userActionView.Hint = "Посмотреть атрибуты объекта с помощью специальной формы";
			userActionView.Image = Resource.View_16;
			userActionView.ImageTransparentColor = Color.Magenta;
			userActionView.Name = "userActionView";
			userActionView.Shortcut = Keys.Return;
			userActionView.Text = "Просмотреть атрибуты объекта";
			userActionView.ActionExecute += delegate { doView(); };

			#endregion

			#region UserActionRefresh #6

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

			#region UserActionNavigateBack #7

			userActionNavigateBack.Active = true;
			userActionNavigateBack.BriefText = "Назад";
			userActionNavigateBack.Enabled = false;
			userActionNavigateBack.Hint = "Вернуться к главному списку";
			userActionNavigateBack.Image = Resource.Back_16;
			userActionNavigateBack.ImageTransparentColor = Color.Magenta;
			userActionNavigateBack.Name = "userActionNavigateBack";
			userActionNavigateBack.Shortcut = Keys.Back;
			userActionNavigateBack.Text = "Назад";
			userActionNavigateBack.ActionExecute += delegate { doNavigateBack(); };

			#endregion

			#region UserActionAdjustGrid #8

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
			userActions.AddPersistent(userActionPick);
			userActions.AddPersistent(userActionInsert);
			userActions.AddPersistent(userActionUpdate);
			userActions.AddPersistent(userActionDelete);
			userActions.AddPersistent(userActionView);
			userActions.AddPersistent(userActionRefresh);
			userActions.AddPersistent(userActionNavigateBack);
			userActions.AddPersistent(userActionAdjustGrid);
		}

		#endregion

		#region Adjustment

		protected override void setActionEnabledState()
		{
			switch (currentMode)
			{
				case GridViewMode.Management:
					userActionPick.Enabled = false;
					userActionInsert.Enabled = true;
					userActionUpdate.Enabled = true;
					userActionView.Enabled = true;
					userActionDelete.Enabled = true;
					break;
				case GridViewMode.Pick:
					userActionPick.Enabled = true;
					userActionInsert.Enabled = false;
					userActionUpdate.Enabled = false;
					userActionView.Enabled = true;
					userActionDelete.Enabled = false;
					break;
				case GridViewMode.PickAndManagement:
					userActionPick.Enabled = true;
					userActionInsert.Enabled = true;
					userActionUpdate.Enabled = true;
					userActionView.Enabled = true;
					userActionDelete.Enabled = true;
					break;
				case GridViewMode.ReadOnly:
					userActionPick.Enabled = false;
					userActionInsert.Enabled = false;
					userActionUpdate.Enabled = false;
					userActionView.Enabled = true;
					userActionDelete.Enabled = false;
					break;
				default:
					break;
			}
			userActionNavigateBack.Enabled = MasterGridView != null;
		}

		protected override void setActionActiveState()
		{
			int selectedRowCount = getDataGridViewSelectedRowCount();
			if (selectedRowCount == 0)
			{
				userActionInsert.Active = validateParameters() && allowInsert;
				userActionUpdate.Active = false;
				userActionDelete.Active = false;
				userActionPick.Active = false;
				userActionView.Active = false;
				userActionRefresh.Active = validateParameters() && allowRefresh;
			}
			else if (selectedRowCount == 1)
			{
				userActionInsert.Active = validateParameters() & allowInsert;
				userActionUpdate.Active = allowUpdate;
				userActionDelete.Active = allowDelete;
				userActionPick.Active = allowPick;
				userActionView.Active = allowView;
				userActionRefresh.Active = validateParameters() & AllowRefresh;
			}
			else if (selectedRowCount > 1)
			{
				userActionInsert.Active = validateParameters() & allowInsert;
				userActionUpdate.Active = false;
				userActionDelete.Active = multiSelect & allowDelete;
				userActionPick.Active = multiSelect & allowPick;
				userActionView.Active = false;
				userActionRefresh.Active = validateParameters() & allowRefresh;
			}

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
		public event EventHandler ObjectInsert;
		public event EventHandler ObjectUpdate;
		public event EventHandler ObjectDelete;
		public event EventHandler ObjectView;
		public event EventHandler ObjectPick;
		public event EventHandler NavigateBack;

		#endregion

		#endregion

		#region Запрос на создание экземпляра формы

		public delegate void DataObjectFromCreatingDelegate(string formName, out DataObjectForm form);

		public event DataObjectFromCreatingDelegate DataObjectFromCreating;

		#endregion

		#region Action execution

		#region Insert

		protected virtual void doInsert()
		{
			if (!allowInsert)
				return;

			if (DataObjectFromCreating == null)
				throw new Exception("Не задан обработчик события создания формы объекта");

			try
			{
				DataObjectForm form;
				PlainObjectInstance dataObject =
					(PlainObjectInstance)accessAdapter.ObjectFactory.New<PlainObjectDefinition>(BasicObjectNamespace, BasicObjectName);
				DataObjectFromCreating(dataObject.PlainObjectDefinition.UIEditorName, out form);
				form.AccessAdapter = accessAdapter;
				if (form.Show(dataObject, DataObjectForm.Mode.Insert, prepareParameterSet()) == DialogResult.OK)
					LoadList();
				setDataGridViewFocus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Создание объекта", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


			if (ObjectInsert != null)
				ObjectInsert(this, EventArgs.Empty);
		}

		#endregion

		#region Update

		protected virtual void doUpdate()
		{
			if (!allowUpdate)
				return;

			if (DataObjectFromCreating == null)
				throw new Exception("Не задан обработчик события создания формы объекта");

			if ((currentMode != GridViewMode.Management) && (currentMode != GridViewMode.PickAndManagement))
				return;

			try
			{
				DataObjectForm form;
				PlainObjectInstance dataObject =
					(PlainObjectInstance)accessAdapter.ObjectFactory.New<PlainObjectDefinition>(BasicObjectNamespace, BasicObjectName);
				dataObject.Load(SelectedObjects[0]);
				DataObjectFromCreating(dataObject.PlainObjectDefinition.UIEditorName, out form);
				form.AccessAdapter = accessAdapter;
				if (form.Show(dataObject, DataObjectForm.Mode.Update, prepareParameterSet()) == DialogResult.OK)
					LoadList();
				setDataGridViewFocus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Изменение атрибутов объекта", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


			if (ObjectUpdate != null)
				ObjectUpdate(this, EventArgs.Empty);
		}

		#endregion

		#region Delete

		protected virtual void doDelete()
		{
			if (!allowDelete)
				return;

			if (SelectedItemsCount < 1)
				return;

			try
			{
				ErrorMessageCollection errorMessages;
				if (
					MessageBox.Show("Вы уверены?", "Выделенные объекты будут удалены!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
					                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					foreach (AttributeValueSet selectedObject in SelectedObjects)
					{
						PlainObjectInstance dataObject =
							(PlainObjectInstance)
							accessAdapter.ObjectFactory.New<PlainObjectDefinition>(BasicObjectNamespace, BasicObjectName);

						errorMessages = dataObject.Load(selectedObject);
						if (errorMessages.Count > 0)
						{
							MessageListWindowForm form = new MessageListWindowForm();
							form.ShowDialog(errorMessages, "Ошибки при обращении к БД!", MessageBoxButtons.OK,
							                MessageBoxDefaultButton.Button1);
						}

						errorMessages = dataObject.Delete();
						if (errorMessages.Count > 0)
						{
							MessageListWindowForm form = new MessageListWindowForm();
							form.ShowDialog(errorMessages, "Ошибки при обращении к БД!", MessageBoxButtons.OK,
							                MessageBoxDefaultButton.Button1);
						}
					}
					LoadList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Удаление объекта", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


			if (ObjectDelete != null)
				ObjectDelete(this, EventArgs.Empty);
		}

		#endregion

		#region View

		protected virtual void doView()
		{
			if (!allowView)
				return;

			if (DataObjectFromCreating == null)
				throw new Exception("Не задан обработчик события создания формы объекта");

			try
			{
				DataObjectForm form;
				PlainObjectInstance dataObject =
					(PlainObjectInstance)accessAdapter.ObjectFactory.New<PlainObjectDefinition>(BasicObjectNamespace, BasicObjectName);
				dataObject.Load(SelectedObjects[0]);
				DataObjectFromCreating(dataObject.PlainObjectDefinition.UIEditorName, out form);
				form.AccessAdapter = accessAdapter;
				if (form.Show(dataObject, DataObjectForm.Mode.View, prepareParameterSet()) == DialogResult.OK)
					LoadList();
				setDataGridViewFocus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Просмотр атрибутов объекта", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


			if (ObjectView != null)
				ObjectView(this, EventArgs.Empty);
		}

		#endregion

		#region Pick

		protected virtual void doPick()
		{
			if (ObjectPick != null)
				ObjectPick(this, EventArgs.Empty);
		}

		#endregion

		#region Navigate back

		protected virtual void doNavigateBack()
		{
			if (masterGrid != null)
				masterGrid.Focus();
			if (NavigateBack != null)
			{
				NavigateBack(this, EventArgs.Empty);
			}
		}

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

			userActionInsert.SetupFromObjectActionDefinition(plainObjectDefinition.Actions["Insert"]);
			userActionUpdate.SetupFromObjectActionDefinition(plainObjectDefinition.Actions["Update"]);
			userActionDelete.SetupFromObjectActionDefinition(plainObjectDefinition.Actions["Delete"]);
			userActionView.SetupFromObjectActionDefinition(plainObjectDefinition.Actions["View"]);
			userActionPick.SetupFromObjectActionDefinition(plainObjectDefinition.Actions["Pick"]);
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
					objectListViewDefinition = accessAdapter.MetadataStore.Get<PlainObjectListViewDefinition>(listNamespace, listName);
				if (objectListViewDefinition == null)
					return;

				if (!validateParameters())
					throw new Exception("Не заданы параметры для загрузки списка");

				clearDataGridViewDataSource();
				if (accessAdapter == null) throw new NullReferenceException("AccessAdapter is not set");
				dataTable = accessAdapter.ObjectFactory.LoadObjectListView(listNamespace, listName, prepareParameterSet());

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

		protected override AttributeValueSet prepareParameterSet()
		{
			AttributeValueSet result = base.prepareParameterSet();

			if (masterListSelectedObjectPK != null)
				foreach (string attribute in masterListSelectedObjectPK.Keys)
				{
					if (!result.Contains(attribute))
						result.Add(attribute, masterListSelectedObjectPK[attribute]);
				}
			if (masterGrid != null && adoptedMasterListAttributes != null)
			{
				AttributeValueSet[] rows = masterGrid.SelectedRows;
				if (rows.Length == 1)
				{
					foreach (string key in adoptedMasterListAttributes.AllKeys)
					{
						if (rows[0].Contains(key))
							result[adoptedMasterListAttributes[key]] = rows[0][key];
					}
				}
			}
			return result;
		}

		#endregion

		#region Родительский список и параметры списка

		private readonly NameValueCollection adoptedMasterListAttributes = new NameValueCollection();
		private bool immediateLoad;
		private PlainObjectGridView masterGrid;
		private string masterListForeignKeyName;
		private AttributeValueSet masterListSelectedObjectPK;

		public PlainObjectGridView MasterGridView
		{
			get { return masterGrid; }
			set
			{
				if (masterGrid != null)
				{
					masterGrid.SelectedIndexChanged -= MasterList_OnSelectedIndexChanged;
					masterGrid.ObjectPick -= MasterList_OnOnExecuteActionChoose;
				}
				masterGrid = value;
				if (masterGrid != null)
				{
					masterGrid.SelectedIndexChanged += MasterList_OnSelectedIndexChanged;
					masterGrid.ObjectPick += MasterList_OnOnExecuteActionChoose;
				}

				userActionNavigateBack.Enabled = (masterGrid != null);
			}
		}

		public string MasterListForeignKeyName
		{
			get { return masterListForeignKeyName; }
			set
			{
				masterListForeignKeyName = value;
				ClearList();
			}
		}

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
				if ((!found) && (masterListSelectedObjectPK != null))
					foreach (string attribute in masterListSelectedObjectPK.Keys)
						if (procParameter.ActualName == attribute)
						{
							found = true;
							break;
						}
				if (!found && adoptedMasterListAttributes != null)
				{
					foreach (string key in adoptedMasterListAttributes.AllKeys)
						if (procParameter.ActualName == adoptedMasterListAttributes[key])
						{
							found = true;
							break;
						}
				}
				if (!found)
					return false;
			}

			return true;
		}

		private void MasterList_OnOnExecuteActionChoose(object sender, EventArgs e)
		{
			if (!immediateLoad)
			{
				loadDetail();
			}
			setDataGridViewFocus();
		}

		private void MasterList_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (immediateLoad)
			{
				ClearList();
				timerDetailLoad.Stop();
				timerDetailLoad.Start();
				//loadDetail();
			}
			else
			{
				//parameters = null;
				ClearList();
			}
		}

		private void loadDetail()
		{
			if (!configured) ConfigureList();

			if (accessAdapter == null || masterGrid == null || objectListViewDefinition == null)
				return;

			PlainObjectDefinition plainObject =
				accessAdapter.MetadataStore.Get<PlainObjectDefinition>(objectListViewDefinition.BasicObjectNamespace,
				                                                       objectListViewDefinition.BasicObjectName);
			if (plainObject == null)
				return;

			ObjectComplexAttributeDefinition foreignKey = plainObject.ComplexAttributes[masterListForeignKeyName];
			if (foreignKey == null && (adoptedMasterListAttributes == null || adoptedMasterListAttributes.Count == 0))
				return;

			AttributeValueSet[] selectedObjects = masterGrid.SelectedObjects;
			if (selectedObjects.Length == 1)
			{
				if (foreignKey != null)
				{
					AttributeValueSet primaryKey = selectedObjects[0];
					masterListSelectedObjectPK = new AttributeValueSet();
					foreach (string pkAttribute in primaryKey.Keys)
					{
						foreach (string fkAttribute in foreignKey.MemberColumns.AllKeys)
						{
							if (fkAttribute == pkAttribute)
							{
								masterListSelectedObjectPK.Add(foreignKey.MemberColumns[fkAttribute], primaryKey[pkAttribute]);
							}
						}
					}
				}

				LoadList();
			}
			else
			{
				masterListSelectedObjectPK = null;
				ClearList();
			}
			//Focus();
			//masterGrid.Focus();
		}

		#region Параметры загрузки списка и главный список: Свойства

		[Browsable(true)]
		public NameValueCollection AdoptedMasterListAttributes
		{
			get { return adoptedMasterListAttributes; }
			//set { adoptedMasterListAttributes = value; }
		}

		public bool ImmediateLoad
		{
			get { return immediateLoad; }
			set { immediateLoad = value; }
		}

		#endregion

		#endregion

		#region UI event handling

		private void DataObjectGridView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				e.Handled = true;
				if (currentMode == GridViewMode.Pick || currentMode == GridViewMode.PickAndManagement)
				{
					userActionPick.Execute();
					return;
				}
				userActionUpdate.Execute();
				return;
			}
			if ((e.KeyData == (Keys.Enter | Keys.Shift)) &&
			    (currentMode == GridViewMode.Pick || currentMode == GridViewMode.PickAndManagement))
			{
				userActionView.Execute();
				return;
			}

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

			if (currentMode == GridViewMode.Pick || currentMode == GridViewMode.PickAndManagement)
			{
				if (userActionPick.Enabled && userActionPick.Permitted && userActionPick.Active)
					userActionPick.Execute();
			}
			else if (currentMode == GridViewMode.Management || currentMode == GridViewMode.ReadOnly)
			{
				if (userActionUpdate.Enabled && userActionUpdate.Permitted && userActionUpdate.Active)
					userActionUpdate.Execute();
			}
		}

		#endregion

		#region Режим работы

		private GridViewMode currentMode = GridViewMode.Management;

		[DefaultValue(GridViewMode.Management)]
		public GridViewMode CurrentMode
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

		private void timerDetailLoad_Tick(object sender, EventArgs e)
		{
			timerDetailLoad.Stop();
			loadDetail();
		}
	}
}

// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore SuggestUseVarKeywordEvident