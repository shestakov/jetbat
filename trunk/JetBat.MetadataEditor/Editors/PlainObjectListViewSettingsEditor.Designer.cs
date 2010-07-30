namespace JetBat.MetadataEditor.Editors
{
	partial class PlainObjectListViewSettingsEditor
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageBasic = new System.Windows.Forms.TabPage();
			this.checkBoxShowDeletedObjects = new System.Windows.Forms.CheckBox();
			this.label15 = new System.Windows.Forms.Label();
			this.comboBoxTargetObjectNamespace = new System.Windows.Forms.ComboBox();
			this.textBoxUICaption = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxEntityName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxEntityNamespace = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxViewName = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxTargetObjectName = new System.Windows.Forms.ComboBox();
			this.tabPageSelectionCondition = new System.Windows.Forms.TabPage();
			this.buttonParameterDelete = new System.Windows.Forms.Button();
			this.buttonParameterUpdate = new System.Windows.Forms.Button();
			this.buttonParameterAdd = new System.Windows.Forms.Button();
			this.numericUpDownParameterScale = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownParameterPrecision = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownParameterMaxLength = new System.Windows.Forms.NumericUpDown();
			this.comboBoxParameterDataType = new System.Windows.Forms.ComboBox();
			this.textBoxParameterName = new System.Windows.Forms.TextBox();
			this.listBoxSelectionParameters = new System.Windows.Forms.ListBox();
			this.textBoxSelectionCondition = new System.Windows.Forms.TextBox();
			this.tabPageSpectialColumns = new System.Windows.Forms.TabPage();
			this.textBoxIgnoredColumns = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxInvisibleColumns = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tabControl.SuspendLayout();
			this.tabPageBasic.SuspendLayout();
			this.tabPageSelectionCondition.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownParameterScale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownParameterPrecision)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownParameterMaxLength)).BeginInit();
			this.tabPageSpectialColumns.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageBasic);
			this.tabControl.Controls.Add(this.tabPageSelectionCondition);
			this.tabControl.Controls.Add(this.tabPageSpectialColumns);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(462, 306);
			this.tabControl.TabIndex = 8;
			// 
			// tabPageBasic
			// 
			this.tabPageBasic.Controls.Add(this.checkBoxShowDeletedObjects);
			this.tabPageBasic.Controls.Add(this.label15);
			this.tabPageBasic.Controls.Add(this.comboBoxTargetObjectNamespace);
			this.tabPageBasic.Controls.Add(this.textBoxUICaption);
			this.tabPageBasic.Controls.Add(this.label6);
			this.tabPageBasic.Controls.Add(this.textBoxEntityName);
			this.tabPageBasic.Controls.Add(this.label5);
			this.tabPageBasic.Controls.Add(this.label4);
			this.tabPageBasic.Controls.Add(this.comboBoxEntityNamespace);
			this.tabPageBasic.Controls.Add(this.label3);
			this.tabPageBasic.Controls.Add(this.comboBoxViewName);
			this.tabPageBasic.Controls.Add(this.label2);
			this.tabPageBasic.Controls.Add(this.comboBoxTargetObjectName);
			this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
			this.tabPageBasic.Name = "tabPageBasic";
			this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageBasic.Size = new System.Drawing.Size(454, 280);
			this.tabPageBasic.TabIndex = 0;
			this.tabPageBasic.Text = "Basic";
			this.tabPageBasic.UseVisualStyleBackColor = true;
			// 
			// checkBoxShowDeletedObjects
			// 
			this.checkBoxShowDeletedObjects.AutoSize = true;
			this.checkBoxShowDeletedObjects.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBoxShowDeletedObjects.Location = new System.Drawing.Point(25, 165);
			this.checkBoxShowDeletedObjects.Name = "checkBoxShowDeletedObjects";
			this.checkBoxShowDeletedObjects.Size = new System.Drawing.Size(128, 17);
			this.checkBoxShowDeletedObjects.TabIndex = 6;
			this.checkBoxShowDeletedObjects.Text = "Show deleted objects";
			this.checkBoxShowDeletedObjects.UseVisualStyleBackColor = true;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(6, 62);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(128, 13);
			this.label15.TabIndex = 18;
			this.label15.Text = "Target object namespace";
			// 
			// comboBoxTargetObjectNamespace
			// 
			this.comboBoxTargetObjectNamespace.FormattingEnabled = true;
			this.comboBoxTargetObjectNamespace.Location = new System.Drawing.Point(140, 59);
			this.comboBoxTargetObjectNamespace.Name = "comboBoxTargetObjectNamespace";
			this.comboBoxTargetObjectNamespace.Size = new System.Drawing.Size(291, 21);
			this.comboBoxTargetObjectNamespace.TabIndex = 2;
			// 
			// textBoxUICaption
			// 
			this.textBoxUICaption.Location = new System.Drawing.Point(140, 139);
			this.textBoxUICaption.Name = "textBoxUICaption";
			this.textBoxUICaption.Size = new System.Drawing.Size(291, 20);
			this.textBoxUICaption.TabIndex = 5;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(62, 142);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(71, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "UI list caption";
			// 
			// textBoxEntityName
			// 
			this.textBoxEntityName.Location = new System.Drawing.Point(140, 33);
			this.textBoxEntityName.Name = "textBoxEntityName";
			this.textBoxEntityName.Size = new System.Drawing.Size(291, 20);
			this.textBoxEntityName.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(66, 36);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Object name";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(69, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Namespace";
			// 
			// comboBoxEntityNamespace
			// 
			this.comboBoxEntityNamespace.FormattingEnabled = true;
			this.comboBoxEntityNamespace.Location = new System.Drawing.Point(140, 6);
			this.comboBoxEntityNamespace.Name = "comboBoxEntityNamespace";
			this.comboBoxEntityNamespace.Size = new System.Drawing.Size(291, 21);
			this.comboBoxEntityNamespace.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(74, 115);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "View name";
			// 
			// comboBoxViewName
			// 
			this.comboBoxViewName.FormattingEnabled = true;
			this.comboBoxViewName.Location = new System.Drawing.Point(140, 112);
			this.comboBoxViewName.Name = "comboBoxViewName";
			this.comboBoxViewName.Size = new System.Drawing.Size(291, 21);
			this.comboBoxViewName.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(34, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Target object name";
			// 
			// comboBoxTargetObjectName
			// 
			this.comboBoxTargetObjectName.FormattingEnabled = true;
			this.comboBoxTargetObjectName.Location = new System.Drawing.Point(140, 85);
			this.comboBoxTargetObjectName.Name = "comboBoxTargetObjectName";
			this.comboBoxTargetObjectName.Size = new System.Drawing.Size(291, 21);
			this.comboBoxTargetObjectName.TabIndex = 3;
			// 
			// tabPageSelectionCondition
			// 
			this.tabPageSelectionCondition.Controls.Add(this.buttonParameterDelete);
			this.tabPageSelectionCondition.Controls.Add(this.buttonParameterUpdate);
			this.tabPageSelectionCondition.Controls.Add(this.buttonParameterAdd);
			this.tabPageSelectionCondition.Controls.Add(this.numericUpDownParameterScale);
			this.tabPageSelectionCondition.Controls.Add(this.numericUpDownParameterPrecision);
			this.tabPageSelectionCondition.Controls.Add(this.numericUpDownParameterMaxLength);
			this.tabPageSelectionCondition.Controls.Add(this.comboBoxParameterDataType);
			this.tabPageSelectionCondition.Controls.Add(this.textBoxParameterName);
			this.tabPageSelectionCondition.Controls.Add(this.listBoxSelectionParameters);
			this.tabPageSelectionCondition.Controls.Add(this.textBoxSelectionCondition);
			this.tabPageSelectionCondition.Location = new System.Drawing.Point(4, 22);
			this.tabPageSelectionCondition.Name = "tabPageSelectionCondition";
			this.tabPageSelectionCondition.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSelectionCondition.Size = new System.Drawing.Size(454, 280);
			this.tabPageSelectionCondition.TabIndex = 4;
			this.tabPageSelectionCondition.Text = "Selection condition";
			this.tabPageSelectionCondition.UseVisualStyleBackColor = true;
			// 
			// buttonParameterDelete
			// 
			this.buttonParameterDelete.Location = new System.Drawing.Point(349, 64);
			this.buttonParameterDelete.Name = "buttonParameterDelete";
			this.buttonParameterDelete.Size = new System.Drawing.Size(75, 23);
			this.buttonParameterDelete.TabIndex = 17;
			this.buttonParameterDelete.Text = "Delete";
			this.buttonParameterDelete.UseVisualStyleBackColor = true;
			this.buttonParameterDelete.Click += new System.EventHandler(this.buttonParameterDelete_Click);
			// 
			// buttonParameterUpdate
			// 
			this.buttonParameterUpdate.Location = new System.Drawing.Point(349, 35);
			this.buttonParameterUpdate.Name = "buttonParameterUpdate";
			this.buttonParameterUpdate.Size = new System.Drawing.Size(75, 23);
			this.buttonParameterUpdate.TabIndex = 16;
			this.buttonParameterUpdate.Text = "Update";
			this.buttonParameterUpdate.UseVisualStyleBackColor = true;
			this.buttonParameterUpdate.Click += new System.EventHandler(this.buttonParameterUpdate_Click);
			// 
			// buttonParameterAdd
			// 
			this.buttonParameterAdd.Location = new System.Drawing.Point(349, 6);
			this.buttonParameterAdd.Name = "buttonParameterAdd";
			this.buttonParameterAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonParameterAdd.TabIndex = 15;
			this.buttonParameterAdd.Text = "Add";
			this.buttonParameterAdd.UseVisualStyleBackColor = true;
			this.buttonParameterAdd.Click += new System.EventHandler(this.buttonParameterAdd_Click);
			// 
			// numericUpDownParameterScale
			// 
			this.numericUpDownParameterScale.Location = new System.Drawing.Point(220, 133);
			this.numericUpDownParameterScale.Name = "numericUpDownParameterScale";
			this.numericUpDownParameterScale.Size = new System.Drawing.Size(46, 20);
			this.numericUpDownParameterScale.TabIndex = 14;
			// 
			// numericUpDownParameterPrecision
			// 
			this.numericUpDownParameterPrecision.Location = new System.Drawing.Point(168, 133);
			this.numericUpDownParameterPrecision.Name = "numericUpDownParameterPrecision";
			this.numericUpDownParameterPrecision.Size = new System.Drawing.Size(46, 20);
			this.numericUpDownParameterPrecision.TabIndex = 13;
			// 
			// numericUpDownParameterMaxLength
			// 
			this.numericUpDownParameterMaxLength.Location = new System.Drawing.Point(116, 133);
			this.numericUpDownParameterMaxLength.Name = "numericUpDownParameterMaxLength";
			this.numericUpDownParameterMaxLength.Size = new System.Drawing.Size(46, 20);
			this.numericUpDownParameterMaxLength.TabIndex = 12;
			// 
			// comboBoxParameterDataType
			// 
			this.comboBoxParameterDataType.FormattingEnabled = true;
			this.comboBoxParameterDataType.Location = new System.Drawing.Point(8, 133);
			this.comboBoxParameterDataType.Name = "comboBoxParameterDataType";
			this.comboBoxParameterDataType.Size = new System.Drawing.Size(102, 21);
			this.comboBoxParameterDataType.TabIndex = 11;
			// 
			// textBoxParameterName
			// 
			this.textBoxParameterName.Location = new System.Drawing.Point(8, 107);
			this.textBoxParameterName.Name = "textBoxParameterName";
			this.textBoxParameterName.Size = new System.Drawing.Size(335, 20);
			this.textBoxParameterName.TabIndex = 10;
			// 
			// listBoxSelectionParameters
			// 
			this.listBoxSelectionParameters.FormattingEnabled = true;
			this.listBoxSelectionParameters.Location = new System.Drawing.Point(8, 6);
			this.listBoxSelectionParameters.Name = "listBoxSelectionParameters";
			this.listBoxSelectionParameters.Size = new System.Drawing.Size(335, 95);
			this.listBoxSelectionParameters.TabIndex = 9;
			// 
			// textBoxSelectionCondition
			// 
			this.textBoxSelectionCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
			                                                                              | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSelectionCondition.Location = new System.Drawing.Point(8, 171);
			this.textBoxSelectionCondition.Multiline = true;
			this.textBoxSelectionCondition.Name = "textBoxSelectionCondition";
			this.textBoxSelectionCondition.Size = new System.Drawing.Size(438, 103);
			this.textBoxSelectionCondition.TabIndex = 8;
			// 
			// tabPageSpectialColumns
			// 
			this.tabPageSpectialColumns.Controls.Add(this.textBoxIgnoredColumns);
			this.tabPageSpectialColumns.Controls.Add(this.label9);
			this.tabPageSpectialColumns.Controls.Add(this.textBoxInvisibleColumns);
			this.tabPageSpectialColumns.Controls.Add(this.label7);
			this.tabPageSpectialColumns.Location = new System.Drawing.Point(4, 22);
			this.tabPageSpectialColumns.Name = "tabPageSpectialColumns";
			this.tabPageSpectialColumns.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSpectialColumns.Size = new System.Drawing.Size(454, 280);
			this.tabPageSpectialColumns.TabIndex = 3;
			this.tabPageSpectialColumns.Text = "Special Columns";
			this.tabPageSpectialColumns.UseVisualStyleBackColor = true;
			// 
			// textBoxIgnoredColumns
			// 
			this.textBoxIgnoredColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                          | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxIgnoredColumns.Location = new System.Drawing.Point(229, 19);
			this.textBoxIgnoredColumns.Multiline = true;
			this.textBoxIgnoredColumns.Name = "textBoxIgnoredColumns";
			this.textBoxIgnoredColumns.Size = new System.Drawing.Size(217, 255);
			this.textBoxIgnoredColumns.TabIndex = 5;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(226, 3);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(86, 13);
			this.label9.TabIndex = 4;
			this.label9.Text = "Ignored Columns";
			// 
			// textBoxInvisibleColumns
			// 
			this.textBoxInvisibleColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                            | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxInvisibleColumns.Location = new System.Drawing.Point(6, 19);
			this.textBoxInvisibleColumns.Multiline = true;
			this.textBoxInvisibleColumns.Name = "textBoxInvisibleColumns";
			this.textBoxInvisibleColumns.Size = new System.Drawing.Size(217, 255);
			this.textBoxInvisibleColumns.TabIndex = 1;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Invisible Columns";
			// 
			// PlainObjectListViewSettingsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.Name = "PlainObjectListViewSettingsEditor";
			this.Size = new System.Drawing.Size(462, 306);
			this.tabControl.ResumeLayout(false);
			this.tabPageBasic.ResumeLayout(false);
			this.tabPageBasic.PerformLayout();
			this.tabPageSelectionCondition.ResumeLayout(false);
			this.tabPageSelectionCondition.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownParameterScale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownParameterPrecision)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownParameterMaxLength)).EndInit();
			this.tabPageSpectialColumns.ResumeLayout(false);
			this.tabPageSpectialColumns.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageBasic;
		private System.Windows.Forms.CheckBox checkBoxShowDeletedObjects;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.ComboBox comboBoxTargetObjectNamespace;
		private System.Windows.Forms.TextBox textBoxUICaption;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxEntityName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxEntityNamespace;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxViewName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxTargetObjectName;
		private System.Windows.Forms.TabPage tabPageSelectionCondition;
		private System.Windows.Forms.Button buttonParameterDelete;
		private System.Windows.Forms.Button buttonParameterUpdate;
		private System.Windows.Forms.Button buttonParameterAdd;
		private System.Windows.Forms.NumericUpDown numericUpDownParameterScale;
		private System.Windows.Forms.NumericUpDown numericUpDownParameterPrecision;
		private System.Windows.Forms.NumericUpDown numericUpDownParameterMaxLength;
		private System.Windows.Forms.ComboBox comboBoxParameterDataType;
		private System.Windows.Forms.TextBox textBoxParameterName;
		private System.Windows.Forms.ListBox listBoxSelectionParameters;
		private System.Windows.Forms.TextBox textBoxSelectionCondition;
		private System.Windows.Forms.TabPage tabPageSpectialColumns;
		private System.Windows.Forms.TextBox textBoxIgnoredColumns;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxInvisibleColumns;
		private System.Windows.Forms.Label label7;
	}
}