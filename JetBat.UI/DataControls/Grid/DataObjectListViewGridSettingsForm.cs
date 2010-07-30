using System;
using System.Windows.Forms;
using JetBat.Client.Metadata.Definitions;

namespace JetBat.UI.DataControls.Grid
{
	public sealed partial class DataObjectListViewGridSettingsForm : Form
	{
		public DataObjectListViewGridSettingsForm()
		{
			InitializeComponent();
		}

		#region Данные

		private GridViewListSettings internalObjectGridViewListSettings = new GridViewListSettings();
		private GridViewListSettings objectGridViewListSettings;

		#endregion

		#region Управление списками

		private void buttonSwitchSortOrder_Click(object sender, EventArgs e)
		{
			int selectedIndex = checkedListBoxSorting.SelectedIndex;
			if (selectedIndex > -1)
			{
				GridViewAttributeSettings item = checkedListBoxSorting.SelectedItem as GridViewAttributeSettings;
				if (item == null) throw new NullReferenceException("checkedListBoxSorting.SelectedItem is not a GridViewAttributeSettings");
				if (item.SortOrder == SortOrder.Ascending)
				{
					item.SortOrder = SortOrder.Descending;
					checkedListBoxSorting.Invalidate();
				}
				else if (item.SortOrder == SortOrder.Descending)
				{
					item.SortOrder = SortOrder.Ascending;
					checkedListBoxSorting.Invalidate();
				}
				else if (item.SortOrder == SortOrder.None)
				{
					item.SortOrder = SortOrder.Ascending;
					checkedListBoxSorting.Invalidate();
					checkedListBoxSorting.SetItemChecked(selectedIndex, true);
				}
			}
		}

		private void buttonSortUp_Click(object sender, EventArgs e)
		{
			int selectedIndex = checkedListBoxSorting.SelectedIndex;
			if (selectedIndex > 0)
			{
				bool isChecked = checkedListBoxSorting.CheckedIndices.Contains(selectedIndex);
				object item = checkedListBoxSorting.SelectedItem;
				checkedListBoxSorting.Items.RemoveAt(selectedIndex);
				checkedListBoxSorting.Items.Insert(selectedIndex - 1, item);
				checkedListBoxSorting.SetItemChecked(selectedIndex - 1, isChecked);
				checkedListBoxSorting.SelectedIndex = selectedIndex - 1;
				for (int i = 0;
					 i < checkedListBoxSorting.Items.Count;
					 ((GridViewAttributeSettings)checkedListBoxSorting.Items[i]).SortIndex = i, ++i)
				{
				}
			}
		}

		private void buttonSortDown_Click(object sender, EventArgs e)
		{
			int selectedIndex = checkedListBoxSorting.SelectedIndex;
			if (selectedIndex > -1 && selectedIndex < checkedListBoxSorting.Items.Count - 1)
			{
				bool isChecked = checkedListBoxSorting.CheckedIndices.Contains(selectedIndex);
				object item = checkedListBoxSorting.SelectedItem;
				checkedListBoxSorting.Items.RemoveAt(selectedIndex);
				checkedListBoxSorting.Items.Insert(selectedIndex + 1, item);
				checkedListBoxSorting.SetItemChecked(selectedIndex + 1, isChecked);
				checkedListBoxSorting.SelectedIndex = selectedIndex + 1;
				for (int i = 0;
					 i < checkedListBoxSorting.Items.Count;
					 ((GridViewAttributeSettings)checkedListBoxSorting.Items[i]).SortIndex = i, ++i)
				{
				}
			}
		}

		private void buttonSortUncheckAll_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < checkedListBoxSorting.Items.Count; checkedListBoxSorting.SetItemChecked(i, false), ++i)
			{
			}
		}

		private void buttonColumnsUp_Click(object sender, EventArgs e)
		{
			int selectedIndex = checkedListBoxColumns.SelectedIndex;
			if (selectedIndex > 0)
			{
				bool isChecked = checkedListBoxColumns.CheckedIndices.Contains(selectedIndex);
				object item = checkedListBoxColumns.SelectedItem;
				checkedListBoxColumns.Items.RemoveAt(selectedIndex);
				checkedListBoxColumns.Items.Insert(selectedIndex - 1, item);
				checkedListBoxColumns.SetItemChecked(selectedIndex - 1, isChecked);
				checkedListBoxColumns.SelectedIndex = selectedIndex - 1;
				for (int i = 0;
					 i < checkedListBoxColumns.Items.Count;
					 ((GridViewAttributeSettings)checkedListBoxColumns.Items[i]).ColumnIndex = i, ++i)
				{
				}
			}
		}

		private void buttonColumnsDown_Click(object sender, EventArgs e)
		{
			int selectedIndex = checkedListBoxColumns.SelectedIndex;
			if (selectedIndex > -1 && selectedIndex < checkedListBoxColumns.Items.Count - 1)
			{
				bool isChecked = checkedListBoxColumns.CheckedIndices.Contains(selectedIndex);
				object item = checkedListBoxColumns.SelectedItem;
				checkedListBoxColumns.Items.RemoveAt(selectedIndex);
				checkedListBoxColumns.Items.Insert(selectedIndex + 1, item);
				checkedListBoxColumns.SetItemChecked(selectedIndex + 1, isChecked);
				checkedListBoxColumns.SelectedIndex = selectedIndex + 1;
				for (int i = 0;
					 i < checkedListBoxColumns.Items.Count;
					 ((GridViewAttributeSettings)checkedListBoxColumns.Items[i]).ColumnIndex = i, ++i)
				{
				}
			}
		}

		private void buttonColumnsCheckAll_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < checkedListBoxColumns.Items.Count; checkedListBoxColumns.SetItemChecked(i, true), ++i)
			{
			}
		}

		private void buttonColumnsUncheckAll_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < checkedListBoxColumns.Items.Count; checkedListBoxColumns.SetItemChecked(i, false), ++i)
			{
			}
		}

		#endregion

		public GridViewListSettings ObjectGridViewListSettings
		{
			get { return objectGridViewListSettings; }
			set { objectGridViewListSettings = value; }
		}

		public void Initialize(ObjectListViewDefinition objectListViewDefinition)
		{
			internalObjectGridViewListSettings = new GridViewListSettings
			{
				ListNamespace = ObjectGridViewListSettings.ListNamespace,
				ListName = ObjectGridViewListSettings.ListName,
				AllowColumnReorder = ObjectGridViewListSettings.AllowColumnReorder,
				AllowColumnWidthChange = ObjectGridViewListSettings.AllowColumnWidthChange,
				AllowCellSelection = ObjectGridViewListSettings.AllowCellSelection,
				AllowHeaderHeightChange = ObjectGridViewListSettings.AllowHeaderHeightChange
			};

			checkBoxAllowColumnReorder.Checked = internalObjectGridViewListSettings.AllowColumnReorder;
			checkBoxAllowColumnWidthChange.Checked = internalObjectGridViewListSettings.AllowColumnWidthChange;
			checkBoxAllowCellSelection.Checked = internalObjectGridViewListSettings.AllowCellSelection;
			checkBoxAllowHeaderHeightChange.Checked = internalObjectGridViewListSettings.AllowHeaderHeightChange;

			internalObjectGridViewListSettings.AttributeSettings.Clear();
			foreach (GridViewAttributeSettings attribute in ObjectGridViewListSettings.AttributeSettings)
			{
				GridViewAttributeSettings attributeCopy = new GridViewAttributeSettings
				{
					ColumnName = attribute.ColumnName,
					ColumnLabel = attribute.ColumnLabel,
					ColumnIndex = attribute.ColumnIndex,
					ColumnVisible = attribute.ColumnVisible,
					SortOrder = attribute.SortOrder,
					SortIndex = attribute.SortIndex,
					ColumnWidth = attribute.ColumnWidth
				};
				internalObjectGridViewListSettings.AttributeSettings.Add(attributeCopy);
			}

			checkedListBoxColumns.Items.Clear();
			checkedListBoxSorting.Items.Clear();

			internalObjectGridViewListSettings.AttributeSettings.Sort(new SortIndexSorter());

			foreach (GridViewAttributeSettings attribute in internalObjectGridViewListSettings.AttributeSettings)
			{
				checkedListBoxSorting.Items.Add(attribute);
				checkedListBoxSorting.SetItemChecked(checkedListBoxSorting.Items.IndexOf(attribute),
													 attribute.SortOrder != SortOrder.None ? true : false);
			}

			internalObjectGridViewListSettings.AttributeSettings.Sort(new ColumnIndexSorter());

			foreach (GridViewAttributeSettings attribute in internalObjectGridViewListSettings.AttributeSettings)
			{
				checkedListBoxColumns.Items.Add(attribute);
				checkedListBoxColumns.SetItemChecked(checkedListBoxColumns.Items.IndexOf(attribute), attribute.ColumnVisible);
			}
		}

		private void checkedListBoxSorting_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			((GridViewAttributeSettings)checkedListBoxSorting.Items[e.Index]).SortOrder = e.NewValue == CheckState.Checked
																							? SortOrder.Ascending
																							: SortOrder.None;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			objectGridViewListSettings = internalObjectGridViewListSettings;
			Hide();
		}

		private void checkedListBoxColumns_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			((GridViewAttributeSettings)checkedListBoxColumns.Items[e.Index]).ColumnVisible = e.NewValue == CheckState.Checked
																								? true
																								: false;
		}

		#region Прочие настройки

		private void checkBoxAllowColumnWidthChange_CheckedChanged(object sender, EventArgs e)
		{
			internalObjectGridViewListSettings.AllowColumnWidthChange = checkBoxAllowColumnWidthChange.Checked;
		}

		private void checkBoxAllowColumnReorder_CheckedChanged(object sender, EventArgs e)
		{
			internalObjectGridViewListSettings.AllowColumnReorder = checkBoxAllowColumnReorder.Checked;
		}

		private void CheckBoxAllowCellSelection_CheckedChanged(object sender, EventArgs e)
		{
			internalObjectGridViewListSettings.AllowCellSelection = checkBoxAllowCellSelection.Checked;
		}

		private void checkBoxAllowHeaderHeightChange_CheckedChanged(object sender, EventArgs e)
		{
			internalObjectGridViewListSettings.AllowHeaderHeightChange = checkBoxAllowHeaderHeightChange.Checked;
		}

		#endregion
	}
}