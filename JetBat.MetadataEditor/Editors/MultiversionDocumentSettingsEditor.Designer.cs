namespace JetBat.MetadataEditor.Editors
{
	partial class MultiversionDocumentSettingsEditor
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("AfterCreate");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Create", new System.Windows.Forms.TreeNode[] {
            treeNode1});
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Before");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("UpdateVersion", new System.Windows.Forms.TreeNode[] {
            treeNode3});
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("BeforeConfirmEdit");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("AfterConfirmEdit");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("ConfirmEdit", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("BeforeCommit");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Commit", new System.Windows.Forms.TreeNode[] {
            treeNode8});
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("BeforeRollback");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Rollback", new System.Windows.Forms.TreeNode[] {
            treeNode10});
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageBasic = new System.Windows.Forms.TabPage();
			this.textBoxEntityName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxEntityNamespace = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxViewName = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxTableName = new System.Windows.Forms.ComboBox();
			this.tabPageMethods = new System.Windows.Forms.TabPage();
			this.treeViewMethods = new System.Windows.Forms.TreeView();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textBoxIgnoredColumns = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxReadOnlyColumns = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxInvisibleColumns = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tabPageNullableColumns = new System.Windows.Forms.TabPage();
			this.textBoxHeaderNullableColumns = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl.SuspendLayout();
			this.tabPageBasic.SuspendLayout();
			this.tabPageMethods.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPageNullableColumns.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageBasic);
			this.tabControl.Controls.Add(this.tabPageMethods);
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPageNullableColumns);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(574, 259);
			this.tabControl.TabIndex = 8;
			// 
			// tabPageBasic
			// 
			this.tabPageBasic.Controls.Add(this.textBoxEntityName);
			this.tabPageBasic.Controls.Add(this.label5);
			this.tabPageBasic.Controls.Add(this.label4);
			this.tabPageBasic.Controls.Add(this.comboBoxEntityNamespace);
			this.tabPageBasic.Controls.Add(this.label3);
			this.tabPageBasic.Controls.Add(this.comboBoxViewName);
			this.tabPageBasic.Controls.Add(this.label2);
			this.tabPageBasic.Controls.Add(this.comboBoxTableName);
			this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
			this.tabPageBasic.Name = "tabPageBasic";
			this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageBasic.Size = new System.Drawing.Size(566, 233);
			this.tabPageBasic.TabIndex = 0;
			this.tabPageBasic.Text = "Basic";
			this.tabPageBasic.UseVisualStyleBackColor = true;
			// 
			// textBoxEntityName
			// 
			this.textBoxEntityName.Location = new System.Drawing.Point(80, 33);
			this.textBoxEntityName.Name = "textBoxEntityName";
			this.textBoxEntityName.Size = new System.Drawing.Size(241, 20);
			this.textBoxEntityName.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 36);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Object name";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Namespace";
			// 
			// comboBoxEntityNamespace
			// 
			this.comboBoxEntityNamespace.FormattingEnabled = true;
			this.comboBoxEntityNamespace.Location = new System.Drawing.Point(80, 6);
			this.comboBoxEntityNamespace.Name = "comboBoxEntityNamespace";
			this.comboBoxEntityNamespace.Size = new System.Drawing.Size(241, 21);
			this.comboBoxEntityNamespace.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 89);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "View name";
			// 
			// comboBoxViewName
			// 
			this.comboBoxViewName.FormattingEnabled = true;
			this.comboBoxViewName.Location = new System.Drawing.Point(80, 86);
			this.comboBoxViewName.Name = "comboBoxViewName";
			this.comboBoxViewName.Size = new System.Drawing.Size(241, 21);
			this.comboBoxViewName.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Table name";
			// 
			// comboBoxTableName
			// 
			this.comboBoxTableName.FormattingEnabled = true;
			this.comboBoxTableName.Location = new System.Drawing.Point(80, 59);
			this.comboBoxTableName.Name = "comboBoxTableName";
			this.comboBoxTableName.Size = new System.Drawing.Size(241, 21);
			this.comboBoxTableName.TabIndex = 2;
			// 
			// tabPageMethods
			// 
			this.tabPageMethods.Controls.Add(this.treeViewMethods);
			this.tabPageMethods.Location = new System.Drawing.Point(4, 22);
			this.tabPageMethods.Name = "tabPageMethods";
			this.tabPageMethods.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMethods.Size = new System.Drawing.Size(566, 233);
			this.tabPageMethods.TabIndex = 2;
			this.tabPageMethods.Text = "Methods";
			this.tabPageMethods.UseVisualStyleBackColor = true;
			// 
			// treeViewMethods
			// 
			this.treeViewMethods.CheckBoxes = true;
			this.treeViewMethods.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewMethods.Location = new System.Drawing.Point(3, 3);
			this.treeViewMethods.Name = "treeViewMethods";
			treeNode1.Name = "NodeMethodAfterCreate";
			treeNode1.Text = "AfterCreate";
			treeNode2.Name = "NodeMethodCreate";
			treeNode2.Text = "Create";
			treeNode3.Name = "NodeMethodBeforeUpdateVersion";
			treeNode3.Text = "Before";
			treeNode4.Name = "NodeMethodUpdateVersion";
			treeNode4.Text = "UpdateVersion";
			treeNode5.Name = "NodeMethodBeforeConfirmEdit";
			treeNode5.Text = "BeforeConfirmEdit";
			treeNode6.Name = "NodeMethodAfterConfirmEdit";
			treeNode6.Text = "AfterConfirmEdit";
			treeNode7.Name = "NodeMethodConfirmEdit";
			treeNode7.Text = "ConfirmEdit";
			treeNode8.Name = "NodeMethodBeforeCommit";
			treeNode8.Text = "BeforeCommit";
			treeNode9.Name = "NodeMethodCommit";
			treeNode9.Text = "Commit";
			treeNode10.Name = "NodeMethodBeforeRollback";
			treeNode10.Text = "BeforeRollback";
			treeNode11.Name = "NodeMethodRollback";
			treeNode11.Text = "Rollback";
			this.treeViewMethods.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode4,
            treeNode7,
            treeNode9,
            treeNode11});
			this.treeViewMethods.ShowPlusMinus = false;
			this.treeViewMethods.Size = new System.Drawing.Size(560, 227);
			this.treeViewMethods.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.textBoxIgnoredColumns);
			this.tabPage1.Controls.Add(this.label9);
			this.tabPage1.Controls.Add(this.textBoxReadOnlyColumns);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.textBoxInvisibleColumns);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(566, 233);
			this.tabPage1.TabIndex = 3;
			this.tabPage1.Text = "Special Columns";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// textBoxIgnoredColumns
			// 
			this.textBoxIgnoredColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxIgnoredColumns.Location = new System.Drawing.Point(378, 35);
			this.textBoxIgnoredColumns.Multiline = true;
			this.textBoxIgnoredColumns.Name = "textBoxIgnoredColumns";
			this.textBoxIgnoredColumns.Size = new System.Drawing.Size(180, 192);
			this.textBoxIgnoredColumns.TabIndex = 5;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(375, 19);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(86, 13);
			this.label9.TabIndex = 4;
			this.label9.Text = "Ignored Columns";
			// 
			// textBoxReadOnlyColumns
			// 
			this.textBoxReadOnlyColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxReadOnlyColumns.Location = new System.Drawing.Point(192, 35);
			this.textBoxReadOnlyColumns.Multiline = true;
			this.textBoxReadOnlyColumns.Name = "textBoxReadOnlyColumns";
			this.textBoxReadOnlyColumns.Size = new System.Drawing.Size(180, 192);
			this.textBoxReadOnlyColumns.TabIndex = 3;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(189, 19);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 13);
			this.label8.TabIndex = 2;
			this.label8.Text = "Read Only Columns";
			// 
			// textBoxInvisibleColumns
			// 
			this.textBoxInvisibleColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxInvisibleColumns.Location = new System.Drawing.Point(6, 35);
			this.textBoxInvisibleColumns.Multiline = true;
			this.textBoxInvisibleColumns.Name = "textBoxInvisibleColumns";
			this.textBoxInvisibleColumns.Size = new System.Drawing.Size(180, 192);
			this.textBoxInvisibleColumns.TabIndex = 1;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 19);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Invisible Columns";
			// 
			// tabPageNullableColumns
			// 
			this.tabPageNullableColumns.Controls.Add(this.textBoxHeaderNullableColumns);
			this.tabPageNullableColumns.Controls.Add(this.label1);
			this.tabPageNullableColumns.Location = new System.Drawing.Point(4, 22);
			this.tabPageNullableColumns.Name = "tabPageNullableColumns";
			this.tabPageNullableColumns.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageNullableColumns.Size = new System.Drawing.Size(566, 233);
			this.tabPageNullableColumns.TabIndex = 4;
			this.tabPageNullableColumns.Text = "Column NULLability";
			this.tabPageNullableColumns.UseVisualStyleBackColor = true;
			// 
			// textBoxHeaderNullableColumns
			// 
			this.textBoxHeaderNullableColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxHeaderNullableColumns.Location = new System.Drawing.Point(6, 35);
			this.textBoxHeaderNullableColumns.Multiline = true;
			this.textBoxHeaderNullableColumns.Name = "textBoxHeaderNullableColumns";
			this.textBoxHeaderNullableColumns.Size = new System.Drawing.Size(180, 192);
			this.textBoxHeaderNullableColumns.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Header Nullable Columns";
			// 
			// MultiversionDocumentSettingsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.MinimumSize = new System.Drawing.Size(574, 259);
			this.Name = "MultiversionDocumentSettingsEditor";
			this.Size = new System.Drawing.Size(574, 259);
			this.Load += new System.EventHandler(this.MultiversionDocumentSettingsEditor_Load);
			this.tabControl.ResumeLayout(false);
			this.tabPageBasic.ResumeLayout(false);
			this.tabPageBasic.PerformLayout();
			this.tabPageMethods.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPageNullableColumns.ResumeLayout(false);
			this.tabPageNullableColumns.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageBasic;
		private System.Windows.Forms.TextBox textBoxEntityName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxEntityNamespace;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxViewName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxTableName;
		private System.Windows.Forms.TabPage tabPageMethods;
		private System.Windows.Forms.TreeView treeViewMethods;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox textBoxIgnoredColumns;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxReadOnlyColumns;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxInvisibleColumns;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TabPage tabPageNullableColumns;
		private System.Windows.Forms.TextBox textBoxHeaderNullableColumns;
		private System.Windows.Forms.Label label1;
	}
}