namespace JetBat.MetadataEditor.Editors
{
	partial class PlainObjectSettingsEditor
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
			System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("BeforeInsert");
			System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("AfterInsert");
			System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("Insert", new System.Windows.Forms.TreeNode[] {
			                                                                                                                           	treeNode46,
			                                                                                                                           	treeNode47});
			System.Windows.Forms.TreeNode treeNode49 = new System.Windows.Forms.TreeNode("BeforeUpdate");
			System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("AfterUpdate");
			System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("Update", new System.Windows.Forms.TreeNode[] {
			                                                                                                                           	treeNode49,
			                                                                                                                           	treeNode50});
			System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("Load");
			System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("BeforeDelete");
			System.Windows.Forms.TreeNode treeNode54 = new System.Windows.Forms.TreeNode("AfterDelete");
			System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("Delete", new System.Windows.Forms.TreeNode[] {
			                                                                                                                           	treeNode53,
			                                                                                                                           	treeNode54});
			System.Windows.Forms.TreeNode treeNode56 = new System.Windows.Forms.TreeNode("BeforeRestore");
			System.Windows.Forms.TreeNode treeNode57 = new System.Windows.Forms.TreeNode("AfterRestore");
			System.Windows.Forms.TreeNode treeNode58 = new System.Windows.Forms.TreeNode("Restore", new System.Windows.Forms.TreeNode[] {
			                                                                                                                            	treeNode56,
			                                                                                                                            	treeNode57});
			System.Windows.Forms.TreeNode treeNode59 = new System.Windows.Forms.TreeNode("CopyByParentObject");
			System.Windows.Forms.TreeNode treeNode60 = new System.Windows.Forms.TreeNode("DeleteByParentObject");
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageBasic = new System.Windows.Forms.TabPage();
			this.textBoxUIEditorName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxEntityName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxEntityNamespace = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxViewName = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxTableName = new System.Windows.Forms.ComboBox();
			this.tabPageExtended = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.numericUpDownStatusColumnInitialValue = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.comboBoxStatusColumnName = new System.Windows.Forms.ComboBox();
			this.checkBoxUseStatusColumn = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.comboBoxSequenceNumberColumnName = new System.Windows.Forms.ComboBox();
			this.checkBoxUseSequenceNumberColumn = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.comboBoxForeignKeyToParentObject = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.comboBoxParentObjectNamespace = new System.Windows.Forms.ComboBox();
			this.comboBoxParentObjectName = new System.Windows.Forms.ComboBox();
			this.checkBoxObjectHasParent = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comboBoxDeleteFlagColumn = new System.Windows.Forms.ComboBox();
			this.checkBoxLogicalDeletion = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxDateTimeMarkColumnName = new System.Windows.Forms.ComboBox();
			this.checkBoxDateTimeMark = new System.Windows.Forms.CheckBox();
			this.tabPageMethods = new System.Windows.Forms.TabPage();
			this.treeViewMethods = new System.Windows.Forms.TreeView();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textBoxIgnoredColumns = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxReadOnlyColumns = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxInvisibleColumns = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tabPageAdditionalMethods = new System.Windows.Forms.TabPage();
			this.checkBoxAdditionalMethodReturnsXmlErrorList = new System.Windows.Forms.CheckBox();
			this.textBoxAdditionalMethodFriendlyName = new System.Windows.Forms.TextBox();
			this.buttonAdditionalMethodDelete = new System.Windows.Forms.Button();
			this.buttonAdditionalMethodUpdate = new System.Windows.Forms.Button();
			this.buttonAdditionalMethodAdd = new System.Windows.Forms.Button();
			this.comboBoxAdditionalMethodStordeProcedure = new System.Windows.Forms.ComboBox();
			this.textBoxAdditionalMethodName = new System.Windows.Forms.TextBox();
			this.listBoxAdditionalMethods = new System.Windows.Forms.ListBox();
			this.tabControl.SuspendLayout();
			this.tabPageBasic.SuspendLayout();
			this.tabPageExtended.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownStatusColumnInitialValue)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPageMethods.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPageAdditionalMethods.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageBasic);
			this.tabControl.Controls.Add(this.tabPageExtended);
			this.tabControl.Controls.Add(this.tabPageMethods);
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPageAdditionalMethods);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(574, 300);
			this.tabControl.TabIndex = 8;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tabPageBasic
			// 
			this.tabPageBasic.Controls.Add(this.textBoxUIEditorName);
			this.tabPageBasic.Controls.Add(this.label6);
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
			this.tabPageBasic.Size = new System.Drawing.Size(566, 274);
			this.tabPageBasic.TabIndex = 0;
			this.tabPageBasic.Text = "Basic";
			this.tabPageBasic.UseVisualStyleBackColor = true;
			// 
			// textBoxUIEditorName
			// 
			this.textBoxUIEditorName.Location = new System.Drawing.Point(89, 59);
			this.textBoxUIEditorName.Name = "textBoxUIEditorName";
			this.textBoxUIEditorName.Size = new System.Drawing.Size(278, 20);
			this.textBoxUIEditorName.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 62);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(76, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "UI editor name";
			// 
			// textBoxEntityName
			// 
			this.textBoxEntityName.Location = new System.Drawing.Point(89, 33);
			this.textBoxEntityName.Name = "textBoxEntityName";
			this.textBoxEntityName.Size = new System.Drawing.Size(278, 20);
			this.textBoxEntityName.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 36);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Object name";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(18, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Namespace";
			// 
			// comboBoxEntityNamespace
			// 
			this.comboBoxEntityNamespace.FormattingEnabled = true;
			this.comboBoxEntityNamespace.Location = new System.Drawing.Point(89, 6);
			this.comboBoxEntityNamespace.Name = "comboBoxEntityNamespace";
			this.comboBoxEntityNamespace.Size = new System.Drawing.Size(278, 21);
			this.comboBoxEntityNamespace.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 115);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "View name";
			// 
			// comboBoxViewName
			// 
			this.comboBoxViewName.FormattingEnabled = true;
			this.comboBoxViewName.Location = new System.Drawing.Point(89, 112);
			this.comboBoxViewName.Name = "comboBoxViewName";
			this.comboBoxViewName.Size = new System.Drawing.Size(278, 21);
			this.comboBoxViewName.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Table name";
			// 
			// comboBoxTableName
			// 
			this.comboBoxTableName.FormattingEnabled = true;
			this.comboBoxTableName.Location = new System.Drawing.Point(89, 85);
			this.comboBoxTableName.Name = "comboBoxTableName";
			this.comboBoxTableName.Size = new System.Drawing.Size(278, 21);
			this.comboBoxTableName.TabIndex = 3;
			// 
			// tabPageExtended
			// 
			this.tabPageExtended.Controls.Add(this.groupBox5);
			this.tabPageExtended.Controls.Add(this.groupBox3);
			this.tabPageExtended.Controls.Add(this.groupBox2);
			this.tabPageExtended.Controls.Add(this.groupBox1);
			this.tabPageExtended.Location = new System.Drawing.Point(4, 22);
			this.tabPageExtended.Name = "tabPageExtended";
			this.tabPageExtended.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageExtended.Size = new System.Drawing.Size(566, 274);
			this.tabPageExtended.TabIndex = 1;
			this.tabPageExtended.Text = "Extended";
			this.tabPageExtended.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.numericUpDownStatusColumnInitialValue);
			this.groupBox5.Controls.Add(this.label13);
			this.groupBox5.Controls.Add(this.comboBoxStatusColumnName);
			this.groupBox5.Controls.Add(this.checkBoxUseStatusColumn);
			this.groupBox5.Location = new System.Drawing.Point(8, 160);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(221, 109);
			this.groupBox5.TabIndex = 7;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Status Column";
			// 
			// numericUpDownStatusColumnInitialValue
			// 
			this.numericUpDownStatusColumnInitialValue.Location = new System.Drawing.Point(6, 82);
			this.numericUpDownStatusColumnInitialValue.Name = "numericUpDownStatusColumnInitialValue";
			this.numericUpDownStatusColumnInitialValue.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownStatusColumnInitialValue.TabIndex = 11;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(6, 66);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(91, 13);
			this.label13.TabIndex = 10;
			this.label13.Text = "Initial status value";
			// 
			// comboBoxStatusColumnName
			// 
			this.comboBoxStatusColumnName.FormattingEnabled = true;
			this.comboBoxStatusColumnName.Location = new System.Drawing.Point(6, 42);
			this.comboBoxStatusColumnName.Name = "comboBoxStatusColumnName";
			this.comboBoxStatusColumnName.Size = new System.Drawing.Size(209, 21);
			this.comboBoxStatusColumnName.TabIndex = 6;
			// 
			// checkBoxUseStatusColumn
			// 
			this.checkBoxUseStatusColumn.AutoSize = true;
			this.checkBoxUseStatusColumn.Location = new System.Drawing.Point(6, 19);
			this.checkBoxUseStatusColumn.Name = "checkBoxUseStatusColumn";
			this.checkBoxUseStatusColumn.Size = new System.Drawing.Size(113, 17);
			this.checkBoxUseStatusColumn.TabIndex = 5;
			this.checkBoxUseStatusColumn.Text = "Use status column";
			this.checkBoxUseStatusColumn.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.groupBox4);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.comboBoxForeignKeyToParentObject);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.comboBoxParentObjectNamespace);
			this.groupBox3.Controls.Add(this.comboBoxParentObjectName);
			this.groupBox3.Controls.Add(this.checkBoxObjectHasParent);
			this.groupBox3.Location = new System.Drawing.Point(235, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(320, 263);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Parent Object";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.comboBoxSequenceNumberColumnName);
			this.groupBox4.Controls.Add(this.checkBoxUseSequenceNumberColumn);
			this.groupBox4.Location = new System.Drawing.Point(6, 162);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(200, 71);
			this.groupBox4.TabIndex = 12;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Sequence number";
			// 
			// comboBoxSequenceNumberColumnName
			// 
			this.comboBoxSequenceNumberColumnName.FormattingEnabled = true;
			this.comboBoxSequenceNumberColumnName.Location = new System.Drawing.Point(6, 42);
			this.comboBoxSequenceNumberColumnName.Name = "comboBoxSequenceNumberColumnName";
			this.comboBoxSequenceNumberColumnName.Size = new System.Drawing.Size(188, 21);
			this.comboBoxSequenceNumberColumnName.TabIndex = 6;
			// 
			// checkBoxUseSequenceNumberColumn
			// 
			this.checkBoxUseSequenceNumberColumn.AutoSize = true;
			this.checkBoxUseSequenceNumberColumn.Location = new System.Drawing.Point(6, 19);
			this.checkBoxUseSequenceNumberColumn.Name = "checkBoxUseSequenceNumberColumn";
			this.checkBoxUseSequenceNumberColumn.Size = new System.Drawing.Size(170, 17);
			this.checkBoxUseSequenceNumberColumn.TabIndex = 5;
			this.checkBoxUseSequenceNumberColumn.Text = "Use sequence number column";
			this.checkBoxUseSequenceNumberColumn.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 119);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(147, 13);
			this.label12.TabIndex = 11;
			this.label12.Text = "Foreign Key To Parent Object";
			// 
			// comboBoxForeignKeyToParentObject
			// 
			this.comboBoxForeignKeyToParentObject.FormattingEnabled = true;
			this.comboBoxForeignKeyToParentObject.Location = new System.Drawing.Point(6, 135);
			this.comboBoxForeignKeyToParentObject.Name = "comboBoxForeignKeyToParentObject";
			this.comboBoxForeignKeyToParentObject.Size = new System.Drawing.Size(308, 21);
			this.comboBoxForeignKeyToParentObject.TabIndex = 10;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 79);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(35, 13);
			this.label11.TabIndex = 9;
			this.label11.Text = "Name";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 39);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(64, 13);
			this.label10.TabIndex = 8;
			this.label10.Text = "Namespace";
			// 
			// comboBoxParentObjectNamespace
			// 
			this.comboBoxParentObjectNamespace.FormattingEnabled = true;
			this.comboBoxParentObjectNamespace.Location = new System.Drawing.Point(6, 55);
			this.comboBoxParentObjectNamespace.Name = "comboBoxParentObjectNamespace";
			this.comboBoxParentObjectNamespace.Size = new System.Drawing.Size(308, 21);
			this.comboBoxParentObjectNamespace.TabIndex = 7;
			// 
			// comboBoxParentObjectName
			// 
			this.comboBoxParentObjectName.FormattingEnabled = true;
			this.comboBoxParentObjectName.Location = new System.Drawing.Point(6, 95);
			this.comboBoxParentObjectName.Name = "comboBoxParentObjectName";
			this.comboBoxParentObjectName.Size = new System.Drawing.Size(308, 21);
			this.comboBoxParentObjectName.TabIndex = 6;
			// 
			// checkBoxObjectHasParent
			// 
			this.checkBoxObjectHasParent.AutoSize = true;
			this.checkBoxObjectHasParent.Location = new System.Drawing.Point(6, 19);
			this.checkBoxObjectHasParent.Name = "checkBoxObjectHasParent";
			this.checkBoxObjectHasParent.Size = new System.Drawing.Size(119, 17);
			this.checkBoxObjectHasParent.TabIndex = 5;
			this.checkBoxObjectHasParent.Text = "Object has a parent";
			this.checkBoxObjectHasParent.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.comboBoxDeleteFlagColumn);
			this.groupBox2.Controls.Add(this.checkBoxLogicalDeletion);
			this.groupBox2.Location = new System.Drawing.Point(8, 83);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(221, 71);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Logical Deletion";
			// 
			// comboBoxDeleteFlagColumn
			// 
			this.comboBoxDeleteFlagColumn.FormattingEnabled = true;
			this.comboBoxDeleteFlagColumn.Location = new System.Drawing.Point(6, 42);
			this.comboBoxDeleteFlagColumn.Name = "comboBoxDeleteFlagColumn";
			this.comboBoxDeleteFlagColumn.Size = new System.Drawing.Size(209, 21);
			this.comboBoxDeleteFlagColumn.TabIndex = 6;
			// 
			// checkBoxLogicalDeletion
			// 
			this.checkBoxLogicalDeletion.AutoSize = true;
			this.checkBoxLogicalDeletion.Location = new System.Drawing.Point(6, 19);
			this.checkBoxLogicalDeletion.Name = "checkBoxLogicalDeletion";
			this.checkBoxLogicalDeletion.Size = new System.Drawing.Size(140, 17);
			this.checkBoxLogicalDeletion.TabIndex = 5;
			this.checkBoxLogicalDeletion.Text = "Use delete mark column";
			this.checkBoxLogicalDeletion.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBoxDateTimeMarkColumnName);
			this.groupBox1.Controls.Add(this.checkBoxDateTimeMark);
			this.groupBox1.Location = new System.Drawing.Point(8, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(221, 71);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Date-Time Mark";
			// 
			// comboBoxDateTimeMarkColumnName
			// 
			this.comboBoxDateTimeMarkColumnName.FormattingEnabled = true;
			this.comboBoxDateTimeMarkColumnName.Location = new System.Drawing.Point(6, 42);
			this.comboBoxDateTimeMarkColumnName.Name = "comboBoxDateTimeMarkColumnName";
			this.comboBoxDateTimeMarkColumnName.Size = new System.Drawing.Size(209, 21);
			this.comboBoxDateTimeMarkColumnName.TabIndex = 4;
			// 
			// checkBoxDateTimeMark
			// 
			this.checkBoxDateTimeMark.AutoSize = true;
			this.checkBoxDateTimeMark.Location = new System.Drawing.Point(6, 19);
			this.checkBoxDateTimeMark.Name = "checkBoxDateTimeMark";
			this.checkBoxDateTimeMark.Size = new System.Drawing.Size(117, 17);
			this.checkBoxDateTimeMark.TabIndex = 4;
			this.checkBoxDateTimeMark.Text = "Use date-time mark";
			this.checkBoxDateTimeMark.UseVisualStyleBackColor = true;
			// 
			// tabPageMethods
			// 
			this.tabPageMethods.Controls.Add(this.treeViewMethods);
			this.tabPageMethods.Location = new System.Drawing.Point(4, 22);
			this.tabPageMethods.Name = "tabPageMethods";
			this.tabPageMethods.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMethods.Size = new System.Drawing.Size(566, 274);
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
			treeNode46.Name = "NodeMethodBeforeInsert";
			treeNode46.Text = "BeforeInsert";
			treeNode47.Name = "NodeMethodAfterInsert";
			treeNode47.Text = "AfterInsert";
			treeNode48.Name = "NodeMethodInsert";
			treeNode48.Text = "Insert";
			treeNode49.Name = "NodeMethodBeforeUpdate";
			treeNode49.Text = "BeforeUpdate";
			treeNode50.Name = "NodeMethodAfterUpdate";
			treeNode50.Text = "AfterUpdate";
			treeNode51.Name = "NodeMethodUpdate";
			treeNode51.Text = "Update";
			treeNode52.Name = "NodeMethodLoad";
			treeNode52.Text = "Load";
			treeNode53.Name = "NodeMethodBeforeDelete";
			treeNode53.Text = "BeforeDelete";
			treeNode54.Name = "NodeMethodAfterDelete";
			treeNode54.Text = "AfterDelete";
			treeNode55.Name = "NodeMethodDelete";
			treeNode55.Text = "Delete";
			treeNode56.Name = "NodeMethodBeforeRestore";
			treeNode56.Text = "BeforeRestore";
			treeNode57.Name = "NodeMethodAfterRestore";
			treeNode57.Text = "AfterRestore";
			treeNode58.Name = "NodeMethodRestore";
			treeNode58.Text = "Restore";
			treeNode59.Name = "nodeMethodCopyByParentObject";
			treeNode59.Text = "CopyByParentObject";
			treeNode60.Name = "NodeMethodDeleteByParentObject";
			treeNode60.Text = "DeleteByParentObject";
			this.treeViewMethods.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
			                                                                        	treeNode48,
			                                                                        	treeNode51,
			                                                                        	treeNode52,
			                                                                        	treeNode55,
			                                                                        	treeNode58,
			                                                                        	treeNode59,
			                                                                        	treeNode60});
			this.treeViewMethods.ShowPlusMinus = false;
			this.treeViewMethods.Size = new System.Drawing.Size(560, 268);
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
			this.tabPage1.Size = new System.Drawing.Size(566, 274);
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
			this.textBoxIgnoredColumns.Size = new System.Drawing.Size(180, 230);
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
			this.textBoxReadOnlyColumns.Size = new System.Drawing.Size(180, 230);
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
			this.textBoxInvisibleColumns.Size = new System.Drawing.Size(180, 230);
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
			// tabPageAdditionalMethods
			// 
			this.tabPageAdditionalMethods.Controls.Add(this.checkBoxAdditionalMethodReturnsXmlErrorList);
			this.tabPageAdditionalMethods.Controls.Add(this.textBoxAdditionalMethodFriendlyName);
			this.tabPageAdditionalMethods.Controls.Add(this.buttonAdditionalMethodDelete);
			this.tabPageAdditionalMethods.Controls.Add(this.buttonAdditionalMethodUpdate);
			this.tabPageAdditionalMethods.Controls.Add(this.buttonAdditionalMethodAdd);
			this.tabPageAdditionalMethods.Controls.Add(this.comboBoxAdditionalMethodStordeProcedure);
			this.tabPageAdditionalMethods.Controls.Add(this.textBoxAdditionalMethodName);
			this.tabPageAdditionalMethods.Controls.Add(this.listBoxAdditionalMethods);
			this.tabPageAdditionalMethods.Location = new System.Drawing.Point(4, 22);
			this.tabPageAdditionalMethods.Name = "tabPageAdditionalMethods";
			this.tabPageAdditionalMethods.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAdditionalMethods.Size = new System.Drawing.Size(566, 274);
			this.tabPageAdditionalMethods.TabIndex = 4;
			this.tabPageAdditionalMethods.Text = "Additional methods";
			this.tabPageAdditionalMethods.UseVisualStyleBackColor = true;
			// 
			// checkBoxAdditionalMethodReturnsXmlErrorList
			// 
			this.checkBoxAdditionalMethodReturnsXmlErrorList.AutoSize = true;
			this.checkBoxAdditionalMethodReturnsXmlErrorList.Location = new System.Drawing.Point(9, 161);
			this.checkBoxAdditionalMethodReturnsXmlErrorList.Name = "checkBoxAdditionalMethodReturnsXmlErrorList";
			this.checkBoxAdditionalMethodReturnsXmlErrorList.Size = new System.Drawing.Size(161, 17);
			this.checkBoxAdditionalMethodReturnsXmlErrorList.TabIndex = 28;
			this.checkBoxAdditionalMethodReturnsXmlErrorList.Text = "Method returns XML error list";
			this.checkBoxAdditionalMethodReturnsXmlErrorList.UseVisualStyleBackColor = true;
			// 
			// textBoxAdditionalMethodFriendlyName
			// 
			this.textBoxAdditionalMethodFriendlyName.Location = new System.Drawing.Point(232, 107);
			this.textBoxAdditionalMethodFriendlyName.Name = "textBoxAdditionalMethodFriendlyName";
			this.textBoxAdditionalMethodFriendlyName.Size = new System.Drawing.Size(221, 20);
			this.textBoxAdditionalMethodFriendlyName.TabIndex = 27;
			// 
			// buttonAdditionalMethodDelete
			// 
			this.buttonAdditionalMethodDelete.Location = new System.Drawing.Point(459, 64);
			this.buttonAdditionalMethodDelete.Name = "buttonAdditionalMethodDelete";
			this.buttonAdditionalMethodDelete.Size = new System.Drawing.Size(75, 23);
			this.buttonAdditionalMethodDelete.TabIndex = 26;
			this.buttonAdditionalMethodDelete.Text = "Delete";
			this.buttonAdditionalMethodDelete.UseVisualStyleBackColor = true;
			this.buttonAdditionalMethodDelete.Click += new System.EventHandler(this.buttonAdditionalMethodDelete_Click);
			// 
			// buttonAdditionalMethodUpdate
			// 
			this.buttonAdditionalMethodUpdate.Location = new System.Drawing.Point(459, 35);
			this.buttonAdditionalMethodUpdate.Name = "buttonAdditionalMethodUpdate";
			this.buttonAdditionalMethodUpdate.Size = new System.Drawing.Size(75, 23);
			this.buttonAdditionalMethodUpdate.TabIndex = 25;
			this.buttonAdditionalMethodUpdate.Text = "Update";
			this.buttonAdditionalMethodUpdate.UseVisualStyleBackColor = true;
			// 
			// buttonAdditionalMethodAdd
			// 
			this.buttonAdditionalMethodAdd.Location = new System.Drawing.Point(459, 6);
			this.buttonAdditionalMethodAdd.Name = "buttonAdditionalMethodAdd";
			this.buttonAdditionalMethodAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdditionalMethodAdd.TabIndex = 24;
			this.buttonAdditionalMethodAdd.Text = "Add";
			this.buttonAdditionalMethodAdd.UseVisualStyleBackColor = true;
			this.buttonAdditionalMethodAdd.Click += new System.EventHandler(this.buttonAdditionalMethodAdd_Click);
			// 
			// comboBoxAdditionalMethodStordeProcedure
			// 
			this.comboBoxAdditionalMethodStordeProcedure.FormattingEnabled = true;
			this.comboBoxAdditionalMethodStordeProcedure.Location = new System.Drawing.Point(8, 133);
			this.comboBoxAdditionalMethodStordeProcedure.Name = "comboBoxAdditionalMethodStordeProcedure";
			this.comboBoxAdditionalMethodStordeProcedure.Size = new System.Drawing.Size(445, 21);
			this.comboBoxAdditionalMethodStordeProcedure.TabIndex = 20;
			// 
			// textBoxAdditionalMethodName
			// 
			this.textBoxAdditionalMethodName.Location = new System.Drawing.Point(8, 107);
			this.textBoxAdditionalMethodName.Name = "textBoxAdditionalMethodName";
			this.textBoxAdditionalMethodName.Size = new System.Drawing.Size(218, 20);
			this.textBoxAdditionalMethodName.TabIndex = 19;
			// 
			// listBoxAdditionalMethods
			// 
			this.listBoxAdditionalMethods.FormattingEnabled = true;
			this.listBoxAdditionalMethods.Location = new System.Drawing.Point(8, 6);
			this.listBoxAdditionalMethods.Name = "listBoxAdditionalMethods";
			this.listBoxAdditionalMethods.Size = new System.Drawing.Size(445, 95);
			this.listBoxAdditionalMethods.TabIndex = 18;
			// 
			// PlainObjectSettingsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.MinimumSize = new System.Drawing.Size(574, 300);
			this.Name = "PlainObjectSettingsEditor";
			this.Size = new System.Drawing.Size(574, 300);
			this.Load += new System.EventHandler(this.PlainObjectSettingsEditor_Load);
			this.tabControl.ResumeLayout(false);
			this.tabPageBasic.ResumeLayout(false);
			this.tabPageBasic.PerformLayout();
			this.tabPageExtended.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownStatusColumnInitialValue)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPageMethods.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPageAdditionalMethods.ResumeLayout(false);
			this.tabPageAdditionalMethods.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageBasic;
		private System.Windows.Forms.TextBox textBoxUIEditorName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxEntityName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxEntityNamespace;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxViewName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxTableName;
		private System.Windows.Forms.TabPage tabPageExtended;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.NumericUpDown numericUpDownStatusColumnInitialValue;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ComboBox comboBoxStatusColumnName;
		private System.Windows.Forms.CheckBox checkBoxUseStatusColumn;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ComboBox comboBoxSequenceNumberColumnName;
		private System.Windows.Forms.CheckBox checkBoxUseSequenceNumberColumn;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboBoxForeignKeyToParentObject;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox comboBoxParentObjectNamespace;
		private System.Windows.Forms.ComboBox comboBoxParentObjectName;
		private System.Windows.Forms.CheckBox checkBoxObjectHasParent;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox comboBoxDeleteFlagColumn;
		private System.Windows.Forms.CheckBox checkBoxLogicalDeletion;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboBoxDateTimeMarkColumnName;
		private System.Windows.Forms.CheckBox checkBoxDateTimeMark;
		private System.Windows.Forms.TabPage tabPageMethods;
		private System.Windows.Forms.TreeView treeViewMethods;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox textBoxIgnoredColumns;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxReadOnlyColumns;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxInvisibleColumns;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TabPage tabPageAdditionalMethods;
		private System.Windows.Forms.CheckBox checkBoxAdditionalMethodReturnsXmlErrorList;
		private System.Windows.Forms.TextBox textBoxAdditionalMethodFriendlyName;
		private System.Windows.Forms.Button buttonAdditionalMethodDelete;
		private System.Windows.Forms.Button buttonAdditionalMethodUpdate;
		private System.Windows.Forms.Button buttonAdditionalMethodAdd;
		private System.Windows.Forms.ComboBox comboBoxAdditionalMethodStordeProcedure;
		private System.Windows.Forms.TextBox textBoxAdditionalMethodName;
		private System.Windows.Forms.ListBox listBoxAdditionalMethods;
	}
}