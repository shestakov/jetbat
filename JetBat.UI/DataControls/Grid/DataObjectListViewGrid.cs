// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable UseObjectOrCollectionInitializer
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.UI.UserActions;
using MessageBox = JetBat.UI.DataControls.Messages.MessageBox;

namespace JetBat.UI.DataControls.Grid
{
	public partial class DataObjectListViewGrid : UserControl, IPersistComponentSettings
	{
		protected DataObjectListViewGrid()
		{
			InitializeComponent();
			//toolStrip.Renderer = new ToolbarRenderer();
			userActions = new UserActionCollection();
		}

		protected void attachUserActionSetChangeEventHandlers()
		{
			userActions.ActionSetChanged += UserActions_OnActionSetChanged;
		}

		private void UserActions_OnActionSetChanged(object sender, UserActionCollection.UserActionEventArgs args)
		{
			initiateToolStrip();
			initiateContextMenuStrip();
		}

		#region Загрузка списка

		protected DataTable dataTable;
		protected DataView dataView = new DataView();

		#endregion

		#region Настройка представления

		private readonly GridViewSettings gridViewSettings = new GridViewSettings();
		protected GridViewListSettings currentGridViewListSettings = new GridViewListSettings();
		protected DataObjectListViewGridSettingsForm dataObjectListViewGridSettingsForm;
		protected GridViewListSettingsCollection gridViewListSettingsCollection = new GridViewListSettingsCollection();

		#endregion

		#region Внешний вид

		protected bool showHeader = true;
		protected bool showToolbar = true;
		protected bool showToolButtonText = true;

		[DefaultValue(true)]
		public bool ShowHeader
		{
			get { return showHeader; }
			set
			{
				showHeader = value;
				setHeaderVisibility();
			}
		}

		[DefaultValue(true)]
		public bool ShowToolbar
		{
			get { return showToolbar; }
			set
			{
				showToolbar = value;
				setToolbarVisibility();
			}
		}

		[DefaultValue(true)]
		public bool ShowToolButtonText
		{
			get { return showToolButtonText; }
			set
			{
				showToolButtonText = value;
				setToolbarButtonTextVisibility();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(BorderStyle.None)]
		public new BorderStyle BorderStyle
		{
			get { return base.BorderStyle; }
			set { base.BorderStyle = value; }
		}

		[DefaultValue("Представление списка")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				setHeaderText(value);
			}
		}

		#endregion

		public bool SaveSettings
		{
			get { return true; }
			set { }
		}

		public string SettingsKey
		{
			get { return Name; }
			set { }
		}

		#region Провайдер доступа

		[Browsable(true)]
		public virtual IAccessAdapter AccessAdapter
		{
			get { return accessAdapter; }
			set { accessAdapter = value; }
		}

		protected IAccessAdapter accessAdapter;

		#endregion

		public void LoadComponentSettings()
		{
			gridViewSettings.SettingsKey = SettingsKey;
			gridViewSettings.Reload();
			if (gridViewSettings.GridViewListSettingsCollection != null)
				gridViewListSettingsCollection = gridViewSettings.GridViewListSettingsCollection;
		}

		public void ResetComponentSettings()
		{
			gridViewSettings.Reset();
		}

		public void SaveComponentSettings()
		{
			gridViewSettings.GridViewListSettingsCollection = gridViewListSettingsCollection;
			gridViewSettings.SettingsKey = SettingsKey;
			gridViewSettings.Save();
		}

		#region Набор действий пользователя

		protected readonly UserActionCollection userActions;

		public UserActionCollection UserActions
		{
			get { return userActions; }
		}

		protected virtual void setActionActiveState() { }

		protected virtual void setActionEnabledState()
		{
		}

		#endregion

		#region Обработка событий

		public event RowAddEvent RowAdd;

		protected virtual void dataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			foreach (GridViewAttributeSettings attribute in currentGridViewListSettings.AttributeSettings)
				if (attribute.ColumnName == e.Column.Name)
				{
					attribute.ColumnWidth = e.Column.Width;
				}
		}

		protected virtual void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (RowAdd != null)
			{
				RowAddEventArgs args = new RowAddEventArgs();
				args.RowView = dataView[e.RowIndex];
				RowAdd(sender, args);
				applyDataGridViewRowColor(args, e);
			}
		}

		protected virtual void listView_SelectionChanged(object sender, EventArgs e)
		{
			setActionActiveState();
			if (SelectedIndexChanged != null) SelectedIndexChanged(sender, e);
		}

		protected virtual void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		protected virtual void dataGridView_KeyDown(object sender, KeyEventArgs e)
		{
		}

		#endregion

		protected void showSettingsForm()
		{
			if (dataObjectListViewGridSettingsForm == null)
				dataObjectListViewGridSettingsForm = new DataObjectListViewGridSettingsForm();
			applyDataGridViewColumnOrder();
			dataObjectListViewGridSettingsForm.ObjectGridViewListSettings = currentGridViewListSettings;
			dataObjectListViewGridSettingsForm.Initialize(objectListViewDefinition);
			dataObjectListViewGridSettingsForm.ShowDialog();
			if (dataObjectListViewGridSettingsForm.DialogResult == DialogResult.OK)
			{
				gridViewListSettingsCollection[listNamespace, listName] =
					dataObjectListViewGridSettingsForm.ObjectGridViewListSettings;
				currentGridViewListSettings = dataObjectListViewGridSettingsForm.ObjectGridViewListSettings;
				setColumns();
				setSort();
				applyGridSettings();
			}
		}

		#region Множественное выделение

		protected bool multiSelect = true;

		public bool MultiSelect
		{
			get { return multiSelect; }
			set
			{
				multiSelect = value;
				setDataGridViewMultiSelectProperty(value);
				setActionActiveState();
			}
		}

		#endregion

		#region Цвета четных и нечетных строк

		private Color evenItemColor = Color.GhostWhite;
		private Color oddItemColor = Color.AliceBlue;

		public Color OddItemColor
		{
			get { return oddItemColor; }
			set
			{
				oddItemColor = value;
				//dataGridView.BackColor = oddItemColor;
			}
		}

		public Color EvenItemColor
		{
			get { return evenItemColor; }
			set
			{
				evenItemColor = value;
				//dataGridView.AlternatingRowsDefaultCellStyle.BackColor = evenItemColor;
			}
		}

		#endregion

		#region Раскраска строк в момент заполнения списка

		#region Delegates

		public delegate void RowAddEvent(object sender, RowAddEventArgs e);

		#endregion

		public class RowAddEventArgs : EventArgs
		{
			public Color BackColor = Color.Empty;
			public Color ForeColor = Color.Empty;
			public DataRowView RowView;
		}

		#endregion

		#region Скрытие выделения

		protected bool hideSelection;

		[Browsable(true)]
		[DefaultValue(false)]
		public bool HideSelection
		{
			get { return hideSelection; }
			set
			{
				hideSelection = value;
				if (Focused)
					colorHightlighted();
				else
					colorUnactive();
			}
		}

		#endregion

		#region Загружаемый список

		protected string listName;
		protected string listNamespace;
		protected ObjectListViewDefinition objectListViewDefinition;

		public string ListNamespace
		{
			get { return listNamespace; }
			set
			{
				listNamespace = value;
				configured = false;
				ClearList();
			}
		}

		public string ListName
		{
			get { return listName; }
			set
			{
				listName = value;
				configured = false;
				ClearList();
			}
		}

		[Browsable(false)]
		public string BasicObjectNamespace
		{
			get { return objectListViewDefinition != null ? objectListViewDefinition.BasicObjectNamespace : String.Empty; }
		}

		[Browsable(false)]
		public string BasicObjectName
		{
			get { return objectListViewDefinition != null ? objectListViewDefinition.BasicObjectName : String.Empty; }
		}

		#endregion

		#region Выделенные объекты

		[Browsable(false)]
		public AttributeValueSet[] SelectedObjects
		{
			get
			{
				return getSelectedObjectsFromDataGridView().ToArray();
			}
		}

		[Browsable(false)]
		public AttributeValueSet[] SelectedRows
		{
			get
			{
				ArrayList list = getSelectedRowsFromDataGridView();
				return (AttributeValueSet[])list.ToArray(typeof(AttributeValueSet));
			}
		}

		[Browsable(false)]
		public int SelectedItemsCount
		{
			get { return getDataGridViewSelectedRowCount(); }
		}

		#endregion

		#region Парметры загрузки

		protected AttributeValueSet parameters = new AttributeValueSet();

		public virtual AttributeValueSet Parameters
		{
			get { return parameters; }
		}

		protected virtual AttributeValueSet prepareParameterSet()
		{
			AttributeValueSet result = new AttributeValueSet();

			if (parameters != null)
				foreach (string attribute in parameters.Keys)
				{
					result.Add(attribute, parameters[attribute]);
				}

			return result;
		}

		protected virtual bool validateParameters()
		{
			if (objectListViewDefinition == null)
				return false;
			foreach (
				ObjectMethodParameterDefinition procParameter in
					objectListViewDefinition.MethodParameterDefinitionsLoadList)
			{
				bool found = false;
				if (parameters != null)
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

		#region Конфигурирование и загрузка

		protected bool configured;
		public event EventHandler SelectedIndexChanged;
		public virtual event EventHandler ListLoad;

		public virtual void ConfigureList()
		{
		}

		public virtual void LoadList()
		{
		}

		public virtual void ClearList()
		{
			clearDataGridViewDataSource();
			dataView.Table = null;
			setActionActiveState();
			if (SelectedIndexChanged != null)
				SelectedIndexChanged(this, EventArgs.Empty);
		}

		protected virtual void fill()
		{
			try
			{
				clearDataGridViewDataSource();
				if (dataTable != null)
					assignDataGridViewDataSource();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Заполнение таблицы...", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			if (SelectedIndexChanged != null)
				SelectedIndexChanged(this, EventArgs.Empty);
		}

		#endregion

		#region UI specific

		protected virtual void initiateToolStrip()
		{

		}

		protected virtual void initiateContextMenuStrip()
		{

		}

		protected virtual void colorHightlighted()
		{

		}

		protected virtual void colorUnactive()
		{

		}

		protected virtual void setDataGridViewFocus()
		{

		}

		protected virtual void enableDataGridView()
		{

		}

		protected virtual void disableDataGridView()
		{

		}

		protected virtual int getDataGridViewSelectedRowCount()
		{
			return -1;
		}

		protected virtual void addDataGridViewColumn(ObjectAttributeDefinition attribute)
		{

		}

		protected virtual void clearDataGridViewDataSource()
		{

		}

		protected virtual void assignDataGridViewDataSource()
		{

		}

		protected virtual void initiateDataGridViewColumnSettings()
		{

		}

		protected virtual int getTheOnlySelectedRowIndex()
		{
			return -1;
		}

		protected virtual void setDataGridViewMultiSelectProperty(bool value)
		{

		}

		protected virtual void applyDataGridViewColumnOrder()
		{

		}

		protected virtual void applyGridSettings()
		{

		}

		protected virtual void setColumns()
		{

		}

		protected virtual void setSort()
		{

		}

		protected virtual void applyDataGridViewRowColor(RowAddEventArgs args, DataGridViewCellFormattingEventArgs e)
		{

		}

		protected virtual ArrayList getSelectedRowsFromDataGridView()
		{
			return new ArrayList();
		}

		protected virtual List<AttributeValueSet> getSelectedObjectsFromDataGridView()
		{
			return new List<AttributeValueSet>();
		}

		protected virtual void setToolbarVisibility()
		{

		}

		protected virtual void setToolbarButtonTextVisibility()
		{

		}

		protected virtual void setHeaderVisibility()
		{

		}

		protected virtual void setHeaderText(string value)
		{

		}

		#endregion

		private void DataObjectListViewGrid_Enter(object sender, EventArgs e)
		{
			colorHightlighted();
			setDataGridViewFocus();
		}

		private void DataObjectListViewGrid_Leave(object sender, EventArgs e)
		{
			colorUnactive();
		}
	}
}

// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore SuggestUseVarKeywordEvident