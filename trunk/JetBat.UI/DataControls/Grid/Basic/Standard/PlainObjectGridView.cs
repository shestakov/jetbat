using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.UI.UserActions;
using MessageBox = JetBat.UI.DataControls.Messages.MessageBox;

namespace JetBat.UI.DataControls.Grid.Basic.Standard
{
	public partial class PlainObjectGridView : Basic.PlainObjectGridView
	{
		public PlainObjectGridView()
		{
			InitializeComponent();
			initiateToolStrip();
			initiateContextMenuStrip();
			attachUserActionSetChangeEventHandlers();
		}

		protected override void setHeaderText(string value)
		{
			headerPanel.Text = value;
		}

		protected override void setHeaderVisibility()
		{
			headerPanel.Visible = showHeader;
		}

		protected override void setToolbarButtonTextVisibility()
		{
			foreach (ToolStripButton button in toolStrip.Items)
			{
				if (button != null)
				{
					button.DisplayStyle = showToolButtonText
											? ToolStripItemDisplayStyle.ImageAndText
											: ToolStripItemDisplayStyle.Image;
				}
			}
		}

		protected override void setToolbarVisibility()
		{
			toolStrip.Enabled = showToolbar;
			toolStrip.Visible = showToolbar;
		}

		protected override List<AttributeValueSet> getSelectedObjectsFromDataGridView()
		{
			List<AttributeValueSet> list = new List<AttributeValueSet>(dataGridView.SelectedRows.Count);
			foreach (DataGridViewRow row in dataGridView.SelectedRows)
				if (row.DataBoundItem is DataRowView)
				{
					AttributeValueSet keyValue = new AttributeValueSet();
					foreach (ObjectAttributeDefinition attribute in objectListViewDefinition.Attributes)
						if (attribute.IsPrimaryKeyMember)
							keyValue.Add(attribute.Name, ((DataRowView)row.DataBoundItem)[attribute.Name]);
					list.Add(keyValue);
				}
			return list;
		}

		protected override ArrayList getSelectedRowsFromDataGridView()
		{
			ArrayList list = new ArrayList(dataGridView.SelectedRows.Count);
			foreach (DataGridViewRow row in dataGridView.SelectedRows)
			{
				AttributeValueSet keyValue = new AttributeValueSet();
				foreach (ObjectAttributeDefinition attribute in objectListViewDefinition.Attributes)
					keyValue.Add(attribute.Name, ((DataRowView)row.DataBoundItem)[attribute.Name]);
				list.Add(keyValue);
			}
			return list;
		}

		protected override void applyDataGridViewRowColor(RowAddEventArgs args, DataGridViewCellFormattingEventArgs e)
		{
			if (args.BackColor != Color.Empty)
				dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = args.BackColor;
			if (args.ForeColor != Color.Empty)
				dataGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = args.ForeColor;
		}

		protected override void setSort()
		{
			StringBuilder sortOrder = new StringBuilder();
			bool comma = false;
			currentGridViewListSettings.AttributeSettings.Sort(new SortIndexSorter());
			foreach (GridViewAttributeSettings attribute in currentGridViewListSettings.AttributeSettings)
			{
				if (attribute.SortOrder != SortOrder.None)
				{
					if (comma) sortOrder.Append(", ");
					sortOrder.Append(attribute.ColumnName + (attribute.SortOrder == SortOrder.Ascending ? " ASC" : " DESC"));
					comma = true;
					if (dataGridView.Columns[attribute.ColumnName] != null)
						dataGridView.Columns[attribute.ColumnName].HeaderText = string.Format("{0} [{1}][{2}]", attribute.ColumnLabel, (attribute.SortOrder == SortOrder.Ascending
																																			? "А>Я"
																																			: "Я>А"), attribute.SortIndex);
				}
				else
				{
					if (dataGridView.Columns[attribute.ColumnName] != null)
						dataGridView.Columns[attribute.ColumnName].HeaderText = attribute.ColumnLabel;
				}
			}

			if (dataView.Table != null)
				try
				{
					dataView.Sort = sortOrder.ToString();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошбика при задании порядка сортировки", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
		}

		protected override void applyDataGridViewColumnOrder()
		{
			foreach (GridViewAttributeSettings attribute in currentGridViewListSettings.AttributeSettings)
				if (dataGridView.Columns.Contains(attribute.ColumnName))
					attribute.ColumnIndex = dataGridView.Columns[attribute.ColumnName].DisplayIndex;
		}

		protected override void applyGridSettings()
		{
			dataGridView.AllowUserToResizeColumns = currentGridViewListSettings.AllowColumnWidthChange;
			dataGridView.AllowUserToOrderColumns = currentGridViewListSettings.AllowColumnReorder;
			dataGridView.ColumnHeadersHeightSizeMode = currentGridViewListSettings.AllowHeaderHeightChange
														? DataGridViewColumnHeadersHeightSizeMode.EnableResizing
														: DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		}

		protected override void setColumns()
		{
			clearDataGridViewDataSource();
			currentGridViewListSettings.AttributeSettings.Sort(new ColumnIndexSorter());
			foreach (GridViewAttributeSettings attribute in currentGridViewListSettings.AttributeSettings)
			{
				GridViewColumn column = dataGridView.Columns[attribute.ColumnName] as GridViewColumn;
				if (column != null)
				{
					column.DisplayIndex = attribute.ColumnIndex;
					column.Visible = attribute.ColumnVisible;
					column.Width = attribute.ColumnWidth;
				}
			}
			assignDataGridViewDataSource();
		}

		protected override void colorHightlighted()
		{
			headerPanel.BackColor = Color.FromArgb(175, 210, 255);
			toolStrip.BackColor = Color.FromArgb(175, 210, 255);
			dataGridView.DefaultCellStyle.SelectionBackColor = Color.Plum;
			dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
			dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
			dataGridView.DefaultCellStyle.BackColor = Color.AliceBlue;
			dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
			dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
			dataGridView.BackgroundColor = Color.AliceBlue;
		}

		protected override void colorUnactive()
		{
			headerPanel.BackColor = Color.FromArgb(227, 239, 255);
			toolStrip.BackColor = Color.FromArgb(227, 239, 255);

			dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 220, 255);
			dataGridView.DefaultCellStyle.BackColor = Color.Lavender;
			dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;
			dataGridView.BackgroundColor = Color.Lavender;

			dataGridView.DefaultCellStyle.SelectionBackColor = hideSelection ? dataGridView.DefaultCellStyle.BackColor : Color.FromArgb(120, 120, 155);
			dataGridView.DefaultCellStyle.SelectionForeColor = hideSelection ? dataGridView.DefaultCellStyle.ForeColor : Color.GhostWhite;
		}

		protected override void setDataGridViewFocus()
		{
			dataGridView.Focus();
		}

		protected override void enableDataGridView()
		{
			dataGridView.Enabled = true;
		}

		protected override void disableDataGridView()
		{
			dataGridView.Enabled = false;
		}

		protected override int getDataGridViewSelectedRowCount()
		{
			return dataGridView.SelectedRows.Count;
		}

		protected override void addDataGridViewColumn(ObjectAttributeDefinition attribute)
		{
			dataGridView.Columns.Add(new GridViewColumn(attribute));
		}

		protected override void clearDataGridViewDataSource()
		{
			dataGridView.DataSource = null;
		}

		protected override void assignDataGridViewDataSource()
		{
			dataGridView.DataSource = dataView;
		}

		protected override void initiateDataGridViewColumnSettings()
		{
			dataGridView.AutoGenerateColumns = false;
			dataGridView.Columns.Clear();
		}

		protected override int getTheOnlySelectedRowIndex()
		{
			return dataGridView.SelectedRows[0].Index;
		}

		protected override void setDataGridViewMultiSelectProperty(bool value)
		{
			dataGridView.MultiSelect = value;
		}

		protected override void initiateToolStrip()
		{
			toolStrip.Items.Clear();
			ArrayList list = new ArrayList(userActions);
			list.Sort(new UserActionOrderIndexSorter());
			foreach (UserAction action in list)
			{
				UserActionToolStripButton item = new UserActionToolStripButton(components) { UserAction = action };
				toolStrip.Items.Add(item);
			}
		}

		protected override void initiateContextMenuStrip()
		{
			contextMenuStrip.Items.Clear();
			ArrayList list = new ArrayList(userActions);
			list.Sort(new UserActionOrderIndexSorter());
			foreach (UserAction action in list)
			{
				UserActionToolStripMenuItem item = new UserActionToolStripMenuItem(components) { UserAction = action };
				contextMenuStrip.Items.Add(item);
			}
		}

		private void headerPanel_Click(object sender, EventArgs e)
		{
			setDataGridViewFocus();
		}

		private void dataGridView_SelectionChanged(object sender, EventArgs e)
		{
			listView_SelectionChanged(sender, e);
		}

		private void dataGridView_KeyDown_1(object sender, KeyEventArgs e)
		{
			dataGridView_KeyDown(sender, e);
		}

		private void dataGridView_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
		{
			dataGridView_CellDoubleClick(sender, e);
		}

		private void dataGridView_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
		{
			dataGridView_CellFormatting(sender, e);
		}

		private void dataGridView_ColumnWidthChanged_1(object sender, DataGridViewColumnEventArgs e)
		{
			dataGridView_ColumnWidthChanged(sender, e);
		}
	}
}