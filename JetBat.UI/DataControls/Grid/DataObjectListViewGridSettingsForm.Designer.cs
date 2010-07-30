namespace JetBat.UI.DataControls.Grid
{
	partial class DataObjectListViewGridSettingsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageSorting = new System.Windows.Forms.TabPage();
			this.buttonSwitchSortOrder = new System.Windows.Forms.Button();
			this.buttonSortUncheckAll = new System.Windows.Forms.Button();
			this.buttonSortDown = new System.Windows.Forms.Button();
			this.buttonSortUp = new System.Windows.Forms.Button();
			this.checkedListBoxSorting = new System.Windows.Forms.CheckedListBox();
			this.tabPageColumns = new System.Windows.Forms.TabPage();
			this.buttonColumnsUncheckAll = new System.Windows.Forms.Button();
			this.buttonColumnsCheckAll = new System.Windows.Forms.Button();
			this.buttonColumnsDown = new System.Windows.Forms.Button();
			this.buttonColumnsUp = new System.Windows.Forms.Button();
			this.checkedListBoxColumns = new System.Windows.Forms.CheckedListBox();
			this.tabPageOtherSettings = new System.Windows.Forms.TabPage();
			this.checkBoxAllowHeaderHeightChange = new System.Windows.Forms.CheckBox();
			this.checkBoxAllowCellSelection = new System.Windows.Forms.CheckBox();
			this.checkBoxAllowColumnWidthChange = new System.Windows.Forms.CheckBox();
			this.checkBoxAllowColumnReorder = new System.Windows.Forms.CheckBox();
			this.panelButtons = new System.Windows.Forms.Panel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabPageSorting.SuspendLayout();
			this.tabPageColumns.SuspendLayout();
			this.tabPageOtherSettings.SuspendLayout();
			this.panelButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageSorting);
			this.tabControl.Controls.Add(this.tabPageColumns);
			this.tabControl.Controls.Add(this.tabPageOtherSettings);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(294, 238);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageSorting
			// 
			this.tabPageSorting.Controls.Add(this.buttonSwitchSortOrder);
			this.tabPageSorting.Controls.Add(this.buttonSortUncheckAll);
			this.tabPageSorting.Controls.Add(this.buttonSortDown);
			this.tabPageSorting.Controls.Add(this.buttonSortUp);
			this.tabPageSorting.Controls.Add(this.checkedListBoxSorting);
			this.tabPageSorting.Location = new System.Drawing.Point(4, 22);
			this.tabPageSorting.Name = "tabPageSorting";
			this.tabPageSorting.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSorting.Size = new System.Drawing.Size(286, 212);
			this.tabPageSorting.TabIndex = 0;
			this.tabPageSorting.Text = "Сортировка";
			// 
			// buttonSwitchSortOrder
			// 
			this.buttonSwitchSortOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSwitchSortOrder.Location = new System.Drawing.Point(204, 6);
			this.buttonSwitchSortOrder.Name = "buttonSwitchSortOrder";
			this.buttonSwitchSortOrder.Size = new System.Drawing.Size(74, 58);
			this.buttonSwitchSortOrder.TabIndex = 7;
			this.buttonSwitchSortOrder.Text = "Изменить порядок сортировки";
			this.buttonSwitchSortOrder.UseVisualStyleBackColor = false;
			this.buttonSwitchSortOrder.Click += new System.EventHandler(this.buttonSwitchSortOrder_Click);
			// 
			// buttonSortUncheckAll
			// 
			this.buttonSortUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSortUncheckAll.Location = new System.Drawing.Point(204, 164);
			this.buttonSortUncheckAll.Name = "buttonSortUncheckAll";
			this.buttonSortUncheckAll.Size = new System.Drawing.Size(74, 41);
			this.buttonSortUncheckAll.TabIndex = 5;
			this.buttonSortUncheckAll.Text = "По умолчанию";
			this.buttonSortUncheckAll.UseVisualStyleBackColor = false;
			this.buttonSortUncheckAll.Click += new System.EventHandler(this.buttonSortUncheckAll_Click);
			// 
			// buttonSortDown
			// 
			this.buttonSortDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSortDown.Image = global::JetBat.UI.Resource.MoveDown_24;
			this.buttonSortDown.Location = new System.Drawing.Point(204, 117);
			this.buttonSortDown.Name = "buttonSortDown";
			this.buttonSortDown.Size = new System.Drawing.Size(74, 41);
			this.buttonSortDown.TabIndex = 4;
			this.buttonSortDown.UseVisualStyleBackColor = false;
			this.buttonSortDown.Click += new System.EventHandler(this.buttonSortDown_Click);
			// 
			// buttonSortUp
			// 
			this.buttonSortUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSortUp.Image = global::JetBat.UI.Resource.MoveUp_24;
			this.buttonSortUp.Location = new System.Drawing.Point(204, 70);
			this.buttonSortUp.Name = "buttonSortUp";
			this.buttonSortUp.Size = new System.Drawing.Size(74, 41);
			this.buttonSortUp.TabIndex = 3;
			this.buttonSortUp.UseVisualStyleBackColor = false;
			this.buttonSortUp.Click += new System.EventHandler(this.buttonSortUp_Click);
			// 
			// checkedListBoxSorting
			// 
			this.checkedListBoxSorting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                           | System.Windows.Forms.AnchorStyles.Left)
			                                                                          | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxSorting.FormattingEnabled = true;
			this.checkedListBoxSorting.HorizontalScrollbar = true;
			this.checkedListBoxSorting.Location = new System.Drawing.Point(8, 6);
			this.checkedListBoxSorting.Name = "checkedListBoxSorting";
			this.checkedListBoxSorting.Size = new System.Drawing.Size(190, 199);
			this.checkedListBoxSorting.TabIndex = 0;
			this.checkedListBoxSorting.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxSorting_ItemCheck);
			// 
			// tabPageColumns
			// 
			this.tabPageColumns.Controls.Add(this.buttonColumnsUncheckAll);
			this.tabPageColumns.Controls.Add(this.buttonColumnsCheckAll);
			this.tabPageColumns.Controls.Add(this.buttonColumnsDown);
			this.tabPageColumns.Controls.Add(this.buttonColumnsUp);
			this.tabPageColumns.Controls.Add(this.checkedListBoxColumns);
			this.tabPageColumns.Location = new System.Drawing.Point(4, 22);
			this.tabPageColumns.Name = "tabPageColumns";
			this.tabPageColumns.Size = new System.Drawing.Size(286, 212);
			this.tabPageColumns.TabIndex = 2;
			this.tabPageColumns.Text = "Отображаемые колонки";
			// 
			// buttonColumnsUncheckAll
			// 
			this.buttonColumnsUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonColumnsUncheckAll.Location = new System.Drawing.Point(210, 149);
			this.buttonColumnsUncheckAll.Name = "buttonColumnsUncheckAll";
			this.buttonColumnsUncheckAll.Size = new System.Drawing.Size(67, 41);
			this.buttonColumnsUncheckAll.TabIndex = 10;
			this.buttonColumnsUncheckAll.Text = "Сбросить все";
			this.buttonColumnsUncheckAll.UseVisualStyleBackColor = false;
			this.buttonColumnsUncheckAll.Click += new System.EventHandler(this.buttonColumnsUncheckAll_Click);
			// 
			// buttonColumnsCheckAll
			// 
			this.buttonColumnsCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonColumnsCheckAll.Location = new System.Drawing.Point(210, 102);
			this.buttonColumnsCheckAll.Name = "buttonColumnsCheckAll";
			this.buttonColumnsCheckAll.Size = new System.Drawing.Size(67, 41);
			this.buttonColumnsCheckAll.TabIndex = 9;
			this.buttonColumnsCheckAll.Text = "Выбрать все";
			this.buttonColumnsCheckAll.UseVisualStyleBackColor = false;
			this.buttonColumnsCheckAll.Click += new System.EventHandler(this.buttonColumnsCheckAll_Click);
			// 
			// buttonColumnsDown
			// 
			this.buttonColumnsDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonColumnsDown.Image = global::JetBat.UI.Resource.MoveDown_24;
			this.buttonColumnsDown.Location = new System.Drawing.Point(210, 55);
			this.buttonColumnsDown.Name = "buttonColumnsDown";
			this.buttonColumnsDown.Size = new System.Drawing.Size(67, 41);
			this.buttonColumnsDown.TabIndex = 8;
			this.buttonColumnsDown.UseVisualStyleBackColor = false;
			this.buttonColumnsDown.Click += new System.EventHandler(this.buttonColumnsDown_Click);
			// 
			// buttonColumnsUp
			// 
			this.buttonColumnsUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonColumnsUp.Image = global::JetBat.UI.Resource.MoveUp_24;
			this.buttonColumnsUp.Location = new System.Drawing.Point(210, 8);
			this.buttonColumnsUp.Name = "buttonColumnsUp";
			this.buttonColumnsUp.Size = new System.Drawing.Size(67, 41);
			this.buttonColumnsUp.TabIndex = 7;
			this.buttonColumnsUp.UseVisualStyleBackColor = false;
			this.buttonColumnsUp.Click += new System.EventHandler(this.buttonColumnsUp_Click);
			// 
			// checkedListBoxColumns
			// 
			this.checkedListBoxColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                           | System.Windows.Forms.AnchorStyles.Left)
			                                                                          | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxColumns.FormattingEnabled = true;
			this.checkedListBoxColumns.HorizontalScrollbar = true;
			this.checkedListBoxColumns.Location = new System.Drawing.Point(7, 8);
			this.checkedListBoxColumns.Name = "checkedListBoxColumns";
			this.checkedListBoxColumns.Size = new System.Drawing.Size(197, 199);
			this.checkedListBoxColumns.TabIndex = 6;
			this.checkedListBoxColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxColumns_ItemCheck);
			// 
			// tabPageOtherSettings
			// 
			this.tabPageOtherSettings.Controls.Add(this.checkBoxAllowHeaderHeightChange);
			this.tabPageOtherSettings.Controls.Add(this.checkBoxAllowCellSelection);
			this.tabPageOtherSettings.Controls.Add(this.checkBoxAllowColumnWidthChange);
			this.tabPageOtherSettings.Controls.Add(this.checkBoxAllowColumnReorder);
			this.tabPageOtherSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageOtherSettings.Name = "tabPageOtherSettings";
			this.tabPageOtherSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageOtherSettings.Size = new System.Drawing.Size(286, 212);
			this.tabPageOtherSettings.TabIndex = 3;
			this.tabPageOtherSettings.Text = "Прочее";
			// 
			// checkBoxAllowHeaderHeightChange
			// 
			this.checkBoxAllowHeaderHeightChange.AutoSize = true;
			this.checkBoxAllowHeaderHeightChange.Location = new System.Drawing.Point(8, 75);
			this.checkBoxAllowHeaderHeightChange.Name = "checkBoxAllowHeaderHeightChange";
			this.checkBoxAllowHeaderHeightChange.Size = new System.Drawing.Size(239, 17);
			this.checkBoxAllowHeaderHeightChange.TabIndex = 5;
			this.checkBoxAllowHeaderHeightChange.Text = "Разрешить изменение высоты заголовка";
			this.checkBoxAllowHeaderHeightChange.UseVisualStyleBackColor = true;
			this.checkBoxAllowHeaderHeightChange.CheckedChanged += new System.EventHandler(this.checkBoxAllowHeaderHeightChange_CheckedChanged);
			// 
			// checkBoxAllowCellSelection
			// 
			this.checkBoxAllowCellSelection.AutoSize = true;
			this.checkBoxAllowCellSelection.Location = new System.Drawing.Point(8, 52);
			this.checkBoxAllowCellSelection.Name = "checkBoxAllowCellSelection";
			this.checkBoxAllowCellSelection.Size = new System.Drawing.Size(191, 17);
			this.checkBoxAllowCellSelection.TabIndex = 4;
			this.checkBoxAllowCellSelection.Text = "Разрешить выделение столбцов";
			this.checkBoxAllowCellSelection.UseVisualStyleBackColor = true;
			this.checkBoxAllowCellSelection.CheckedChanged += new System.EventHandler(this.CheckBoxAllowCellSelection_CheckedChanged);
			// 
			// checkBoxAllowColumnWidthChange
			// 
			this.checkBoxAllowColumnWidthChange.AutoSize = true;
			this.checkBoxAllowColumnWidthChange.Checked = true;
			this.checkBoxAllowColumnWidthChange.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxAllowColumnWidthChange.Location = new System.Drawing.Point(8, 6);
			this.checkBoxAllowColumnWidthChange.Name = "checkBoxAllowColumnWidthChange";
			this.checkBoxAllowColumnWidthChange.Size = new System.Drawing.Size(229, 17);
			this.checkBoxAllowColumnWidthChange.TabIndex = 3;
			this.checkBoxAllowColumnWidthChange.Text = "Разрешить изменение ширины колонок";
			this.checkBoxAllowColumnWidthChange.UseVisualStyleBackColor = false;
			this.checkBoxAllowColumnWidthChange.CheckedChanged += new System.EventHandler(this.checkBoxAllowColumnWidthChange_CheckedChanged);
			// 
			// checkBoxAllowColumnReorder
			// 
			this.checkBoxAllowColumnReorder.AutoSize = true;
			this.checkBoxAllowColumnReorder.Checked = true;
			this.checkBoxAllowColumnReorder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxAllowColumnReorder.Location = new System.Drawing.Point(8, 29);
			this.checkBoxAllowColumnReorder.Name = "checkBoxAllowColumnReorder";
			this.checkBoxAllowColumnReorder.Size = new System.Drawing.Size(248, 17);
			this.checkBoxAllowColumnReorder.TabIndex = 2;
			this.checkBoxAllowColumnReorder.Text = "Разрешить перестановку колонок в списке";
			this.checkBoxAllowColumnReorder.UseVisualStyleBackColor = true;
			this.checkBoxAllowColumnReorder.CheckedChanged += new System.EventHandler(this.checkBoxAllowColumnReorder_CheckedChanged);
			// 
			// panelButtons
			// 
			this.panelButtons.Controls.Add(this.buttonCancel);
			this.panelButtons.Controls.Add(this.buttonOK);
			this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButtons.Location = new System.Drawing.Point(0, 238);
			this.panelButtons.Name = "panelButtons";
			this.panelButtons.Size = new System.Drawing.Size(294, 48);
			this.panelButtons.TabIndex = 2;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Image = global::JetBat.UI.Resource.Cancel_32;
			this.buttonCancel.Location = new System.Drawing.Point(209, 4);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(82, 41);
			this.buttonCancel.TabIndex = 9;
			this.buttonCancel.UseVisualStyleBackColor = false;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Image = global::JetBat.UI.Resource.Save_32;
			this.buttonOK.Location = new System.Drawing.Point(121, 4);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(82, 41);
			this.buttonOK.TabIndex = 8;
			this.buttonOK.UseVisualStyleBackColor = false;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// DataObjectListViewGridSettingsForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(294, 286);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.panelButtons);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DataObjectListViewGridSettingsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Настройка представления";
			this.tabControl.ResumeLayout(false);
			this.tabPageSorting.ResumeLayout(false);
			this.tabPageColumns.ResumeLayout(false);
			this.tabPageOtherSettings.ResumeLayout(false);
			this.tabPageOtherSettings.PerformLayout();
			this.panelButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageSorting;
		private System.Windows.Forms.TabPage tabPageColumns;
		private System.Windows.Forms.CheckedListBox checkedListBoxSorting;
		private System.Windows.Forms.Panel panelButtons;
		private System.Windows.Forms.Button buttonSortUncheckAll;
		private System.Windows.Forms.Button buttonSortDown;
		private System.Windows.Forms.Button buttonSortUp;
		private System.Windows.Forms.Button buttonColumnsUncheckAll;
		private System.Windows.Forms.Button buttonColumnsCheckAll;
		private System.Windows.Forms.Button buttonColumnsDown;
		private System.Windows.Forms.Button buttonColumnsUp;
		private System.Windows.Forms.CheckedListBox checkedListBoxColumns;
		private System.Windows.Forms.Button buttonSwitchSortOrder;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TabPage tabPageOtherSettings;
		private System.Windows.Forms.CheckBox checkBoxAllowColumnWidthChange;
		private System.Windows.Forms.CheckBox checkBoxAllowColumnReorder;
		private System.Windows.Forms.CheckBox checkBoxAllowCellSelection;
		private System.Windows.Forms.CheckBox checkBoxAllowHeaderHeightChange;
	}
}