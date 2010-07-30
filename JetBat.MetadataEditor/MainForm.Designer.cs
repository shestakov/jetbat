namespace JetBat.MetadataEditor
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.plainObjectSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.plainObjectListViewSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.multiversionDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.multiversionDocumentListViewSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.storedQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRender = new System.Windows.Forms.ToolStripMenuItem();
			this.generateStoredProceduresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.updateMetadataStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tooldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateMetadatastoreDatabaseScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.simpleProjectManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPersist = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonGenerate = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonUpdateMetadataStore = new System.Windows.Forms.ToolStripButton();
			this.toolStripLabelTargetDatabase = new System.Windows.Forms.ToolStripLabel();
			this.toolStripComboBoxEnvironment = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
			this.panelEditor = new System.Windows.Forms.Panel();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.buttonTestEnvironment = new System.Windows.Forms.Button();
			this.comboBoxEnvironment = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageEditor = new System.Windows.Forms.TabPage();
			this.tabPageEnvironment = new System.Windows.Forms.TabPage();
			this.buttonReloadEnvironmentList = new System.Windows.Forms.Button();
			this.buttonDeleteEnvironment = new System.Windows.Forms.Button();
			this.buttonRestoreEnvironment = new System.Windows.Forms.Button();
			this.buttonSaveEnvironment = new System.Windows.Forms.Button();
			this.textBoxMetadataFileDirectory = new System.Windows.Forms.TextBox();
			this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
			this.textBoxConnectionStringToMetadataStore = new System.Windows.Forms.TextBox();
			this.textBoxConnectionStringToDatabase = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.statusStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPageEditor.SuspendLayout();
			this.tabPageEnvironment.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                                                         	this.toolStripStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 361);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(706, 22);
			this.statusStrip.TabIndex = 0;
			this.statusStrip.Text = "statusStrip";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(51, 17);
			this.toolStripStatusLabel.Text = "Success.";
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                                                       	this.fileToolStripMenuItem,
			                                                                       	this.editToolStripMenuItem,
			                                                                       	this.tooldToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(706, 24);
			this.menuStrip.TabIndex = 1;
			this.menuStrip.Text = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                                                                           	this.newToolStripMenuItem,
			                                                                                           	this.openToolStripMenuItem,
			                                                                                           	this.toolStripSeparator,
			                                                                                           	this.saveToolStripMenuItem,
			                                                                                           	this.saveAsToolStripMenuItem,
			                                                                                           	this.toolStripSeparator1,
			                                                                                           	this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                                                                          	this.plainObjectSettingsToolStripMenuItem,
			                                                                                          	this.plainObjectListViewSettingsToolStripMenuItem,
			                                                                                          	this.multiversionDocumentToolStripMenuItem,
			                                                                                          	this.multiversionDocumentListViewSettingsToolStripMenuItem,
			                                                                                          	this.storedQueryToolStripMenuItem});
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.newToolStripMenuItem.Text = "&New Settings";
			// 
			// plainObjectSettingsToolStripMenuItem
			// 
			this.plainObjectSettingsToolStripMenuItem.Name = "plainObjectSettingsToolStripMenuItem";
			this.plainObjectSettingsToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
			this.plainObjectSettingsToolStripMenuItem.Text = "&1. Plain Object";
			this.plainObjectSettingsToolStripMenuItem.Click += new System.EventHandler(this.plainObjectSettingsToolStripMenuItem_Click);
			// 
			// plainObjectListViewSettingsToolStripMenuItem
			// 
			this.plainObjectListViewSettingsToolStripMenuItem.Name = "plainObjectListViewSettingsToolStripMenuItem";
			this.plainObjectListViewSettingsToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
			this.plainObjectListViewSettingsToolStripMenuItem.Text = "&2. Plain Object List View";
			this.plainObjectListViewSettingsToolStripMenuItem.Click += new System.EventHandler(this.plainObjectListViewSettingsToolStripMenuItem_Click);
			// 
			// multiversionDocumentToolStripMenuItem
			// 
			this.multiversionDocumentToolStripMenuItem.Name = "multiversionDocumentToolStripMenuItem";
			this.multiversionDocumentToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
			this.multiversionDocumentToolStripMenuItem.Text = "&3. Multiversion Document";
			this.multiversionDocumentToolStripMenuItem.Click += new System.EventHandler(this.multiversionDocumentToolStripMenuItem_Click);
			// 
			// multiversionDocumentListViewSettingsToolStripMenuItem
			// 
			this.multiversionDocumentListViewSettingsToolStripMenuItem.Name = "multiversionDocumentListViewSettingsToolStripMenuItem";
			this.multiversionDocumentListViewSettingsToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
			this.multiversionDocumentListViewSettingsToolStripMenuItem.Text = "&4. Multiversion Document List View";
			this.multiversionDocumentListViewSettingsToolStripMenuItem.Click += new System.EventHandler(this.multiversionDocumentListViewSettingsToolStripMenuItem_Click);
			// 
			// storedQueryToolStripMenuItem
			// 
			this.storedQueryToolStripMenuItem.Name = "storedQueryToolStripMenuItem";
			this.storedQueryToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
			this.storedQueryToolStripMenuItem.Text = "&5. Stored Query";
			this.storedQueryToolStripMenuItem.Click += new System.EventHandler(this.storedQueryToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                                                                           	this.toolStripMenuItemRender,
			                                                                                           	this.generateStoredProceduresToolStripMenuItem,
			                                                                                           	this.toolStripSeparator2,
			                                                                                           	this.updateMetadataStoreToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.editToolStripMenuItem.Text = "&Action";
			// 
			// toolStripMenuItemRender
			// 
			this.toolStripMenuItemRender.Name = "toolStripMenuItemRender";
			this.toolStripMenuItemRender.Size = new System.Drawing.Size(219, 22);
			this.toolStripMenuItemRender.Text = "&Persist object";
			this.toolStripMenuItemRender.Click += new System.EventHandler(this.persistObjectToolStripMenuItem_Click);
			// 
			// generateStoredProceduresToolStripMenuItem
			// 
			this.generateStoredProceduresToolStripMenuItem.Name = "generateStoredProceduresToolStripMenuItem";
			this.generateStoredProceduresToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.generateStoredProceduresToolStripMenuItem.Text = "&Generate stored procedures";
			this.generateStoredProceduresToolStripMenuItem.Click += new System.EventHandler(this.generateStoredProceduresToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(216, 6);
			// 
			// updateMetadataStoreToolStripMenuItem
			// 
			this.updateMetadataStoreToolStripMenuItem.Name = "updateMetadataStoreToolStripMenuItem";
			this.updateMetadataStoreToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.updateMetadataStoreToolStripMenuItem.Text = "&Update metadata store";
			this.updateMetadataStoreToolStripMenuItem.Click += new System.EventHandler(this.updateMetadataStoreToolStripMenuItem_Click);
			// 
			// tooldToolStripMenuItem
			// 
			this.tooldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                                                                            	this.generateMetadatastoreDatabaseScriptToolStripMenuItem,
			                                                                                            	this.simpleProjectManagerToolStripMenuItem});
			this.tooldToolStripMenuItem.Name = "tooldToolStripMenuItem";
			this.tooldToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.tooldToolStripMenuItem.Text = "&Tools";
			// 
			// generateMetadatastoreDatabaseScriptToolStripMenuItem
			// 
			this.generateMetadatastoreDatabaseScriptToolStripMenuItem.Name = "generateMetadatastoreDatabaseScriptToolStripMenuItem";
			this.generateMetadatastoreDatabaseScriptToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
			this.generateMetadatastoreDatabaseScriptToolStripMenuItem.Text = "Generate metadata &store database script";
			this.generateMetadatastoreDatabaseScriptToolStripMenuItem.Click += new System.EventHandler(this.generateMetadatastoreDatabaseScriptToolStripMenuItem_Click);
			// 
			// simpleProjectManagerToolStripMenuItem
			// 
			this.simpleProjectManagerToolStripMenuItem.Name = "simpleProjectManagerToolStripMenuItem";
			this.simpleProjectManagerToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
			this.simpleProjectManagerToolStripMenuItem.Text = "Simple Project Manager";
			this.simpleProjectManagerToolStripMenuItem.Click += new System.EventHandler(this.simpleProjectManagerToolStripMenuItem_Click);
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                                                       	this.toolStripButtonOpen,
			                                                                       	this.toolStripButtonSave,
			                                                                       	this.toolStripButtonPersist,
			                                                                       	this.toolStripButtonGenerate,
			                                                                       	this.toolStripButtonUpdateMetadataStore,
			                                                                       	this.toolStripLabelTargetDatabase,
			                                                                       	this.toolStripComboBoxEnvironment,
			                                                                       	this.toolStripButtonReload});
			this.toolStrip.Location = new System.Drawing.Point(3, 3);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(692, 25);
			this.toolStrip.TabIndex = 2;
			this.toolStrip.Text = "toolStrip";
			// 
			// toolStripButtonOpen
			// 
			this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
			this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonOpen.Name = "toolStripButtonOpen";
			this.toolStripButtonOpen.Size = new System.Drawing.Size(40, 22);
			this.toolStripButtonOpen.Text = "Open";
			this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
			// 
			// toolStripButtonSave
			// 
			this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
			this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSave.Name = "toolStripButtonSave";
			this.toolStripButtonSave.Size = new System.Drawing.Size(35, 22);
			this.toolStripButtonSave.Text = "Save";
			this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
			// 
			// toolStripButtonPersist
			// 
			this.toolStripButtonPersist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonPersist.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPersist.Image")));
			this.toolStripButtonPersist.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPersist.Name = "toolStripButtonPersist";
			this.toolStripButtonPersist.Size = new System.Drawing.Size(45, 22);
			this.toolStripButtonPersist.Text = "Persist";
			this.toolStripButtonPersist.Click += new System.EventHandler(this.toolStripButtonPersist_Click);
			// 
			// toolStripButtonGenerate
			// 
			this.toolStripButtonGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonGenerate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGenerate.Image")));
			this.toolStripButtonGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonGenerate.Name = "toolStripButtonGenerate";
			this.toolStripButtonGenerate.Size = new System.Drawing.Size(58, 22);
			this.toolStripButtonGenerate.Text = "Generate";
			this.toolStripButtonGenerate.Click += new System.EventHandler(this.toolStripButtonRender_Click);
			// 
			// toolStripButtonUpdateMetadataStore
			// 
			this.toolStripButtonUpdateMetadataStore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonUpdateMetadataStore.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUpdateMetadataStore.Image")));
			this.toolStripButtonUpdateMetadataStore.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonUpdateMetadataStore.Name = "toolStripButtonUpdateMetadataStore";
			this.toolStripButtonUpdateMetadataStore.Size = new System.Drawing.Size(102, 22);
			this.toolStripButtonUpdateMetadataStore.Text = "Update Metadata";
			this.toolStripButtonUpdateMetadataStore.Click += new System.EventHandler(this.toolStripButtonUpdateMetadataStore_Click);
			// 
			// toolStripLabelTargetDatabase
			// 
			this.toolStripLabelTargetDatabase.Name = "toolStripLabelTargetDatabase";
			this.toolStripLabelTargetDatabase.Size = new System.Drawing.Size(95, 22);
			this.toolStripLabelTargetDatabase.Text = "Target Database:";
			// 
			// toolStripComboBoxEnvironment
			// 
			this.toolStripComboBoxEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBoxEnvironment.Name = "toolStripComboBoxEnvironment";
			this.toolStripComboBoxEnvironment.Size = new System.Drawing.Size(151, 25);
			this.toolStripComboBoxEnvironment.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxEnvironment_SelectedIndexChanged);
			// 
			// toolStripButtonReload
			// 
			this.toolStripButtonReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonReload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReload.Image")));
			this.toolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonReload.Name = "toolStripButtonReload";
			this.toolStripButtonReload.Size = new System.Drawing.Size(47, 22);
			this.toolStripButtonReload.Text = "Reload";
			this.toolStripButtonReload.Click += new System.EventHandler(this.toolStripButtonReload_Click);
			// 
			// panelEditor
			// 
			this.panelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEditor.Location = new System.Drawing.Point(3, 28);
			this.panelEditor.Name = "panelEditor";
			this.panelEditor.Size = new System.Drawing.Size(692, 280);
			this.panelEditor.TabIndex = 3;
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "xml";
			this.openFileDialog.Filter = resources.GetString("openFileDialog.Filter");
			this.openFileDialog.Title = "Load Plain Object Constructor project";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "xml";
			this.saveFileDialog.Filter = resources.GetString("saveFileDialog.Filter");
			this.saveFileDialog.Title = "Save Plain Object Constructor Project";
			// 
			// buttonTestEnvironment
			// 
			this.buttonTestEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonTestEnvironment.Location = new System.Drawing.Point(579, 85);
			this.buttonTestEnvironment.Name = "buttonTestEnvironment";
			this.buttonTestEnvironment.Size = new System.Drawing.Size(111, 23);
			this.buttonTestEnvironment.TabIndex = 3;
			this.buttonTestEnvironment.Text = "Test";
			this.buttonTestEnvironment.UseVisualStyleBackColor = true;
			this.buttonTestEnvironment.Click += new System.EventHandler(this.buttonTestEnvironment_Click);
			// 
			// comboBoxEnvironment
			// 
			this.comboBoxEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                        | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEnvironment.FormattingEnabled = true;
			this.comboBoxEnvironment.Location = new System.Drawing.Point(11, 19);
			this.comboBoxEnvironment.Name = "comboBoxEnvironment";
			this.comboBoxEnvironment.Size = new System.Drawing.Size(618, 21);
			this.comboBoxEnvironment.TabIndex = 1;
			this.comboBoxEnvironment.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnvironment_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select environment";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageEditor);
			this.tabControl.Controls.Add(this.tabPageEnvironment);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 24);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(706, 337);
			this.tabControl.TabIndex = 6;
			// 
			// tabPageEditor
			// 
			this.tabPageEditor.Controls.Add(this.panelEditor);
			this.tabPageEditor.Controls.Add(this.toolStrip);
			this.tabPageEditor.Location = new System.Drawing.Point(4, 22);
			this.tabPageEditor.Name = "tabPageEditor";
			this.tabPageEditor.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageEditor.Size = new System.Drawing.Size(698, 311);
			this.tabPageEditor.TabIndex = 0;
			this.tabPageEditor.Text = "Editor";
			this.tabPageEditor.UseVisualStyleBackColor = true;
			// 
			// tabPageEnvironment
			// 
			this.tabPageEnvironment.Controls.Add(this.buttonReloadEnvironmentList);
			this.tabPageEnvironment.Controls.Add(this.buttonDeleteEnvironment);
			this.tabPageEnvironment.Controls.Add(this.buttonRestoreEnvironment);
			this.tabPageEnvironment.Controls.Add(this.buttonSaveEnvironment);
			this.tabPageEnvironment.Controls.Add(this.textBoxMetadataFileDirectory);
			this.tabPageEnvironment.Controls.Add(this.textBoxOutputDirectory);
			this.tabPageEnvironment.Controls.Add(this.textBoxConnectionStringToMetadataStore);
			this.tabPageEnvironment.Controls.Add(this.textBoxConnectionStringToDatabase);
			this.tabPageEnvironment.Controls.Add(this.label5);
			this.tabPageEnvironment.Controls.Add(this.label4);
			this.tabPageEnvironment.Controls.Add(this.label3);
			this.tabPageEnvironment.Controls.Add(this.label2);
			this.tabPageEnvironment.Controls.Add(this.textBoxName);
			this.tabPageEnvironment.Controls.Add(this.label6);
			this.tabPageEnvironment.Controls.Add(this.buttonTestEnvironment);
			this.tabPageEnvironment.Controls.Add(this.comboBoxEnvironment);
			this.tabPageEnvironment.Controls.Add(this.label1);
			this.tabPageEnvironment.Location = new System.Drawing.Point(4, 22);
			this.tabPageEnvironment.Name = "tabPageEnvironment";
			this.tabPageEnvironment.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageEnvironment.Size = new System.Drawing.Size(698, 311);
			this.tabPageEnvironment.TabIndex = 1;
			this.tabPageEnvironment.Text = "Environment";
			this.tabPageEnvironment.UseVisualStyleBackColor = true;
			// 
			// buttonReloadEnvironmentList
			// 
			this.buttonReloadEnvironmentList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReloadEnvironmentList.Location = new System.Drawing.Point(635, 17);
			this.buttonReloadEnvironmentList.Name = "buttonReloadEnvironmentList";
			this.buttonReloadEnvironmentList.Size = new System.Drawing.Size(55, 23);
			this.buttonReloadEnvironmentList.TabIndex = 23;
			this.buttonReloadEnvironmentList.Text = "Reload";
			this.buttonReloadEnvironmentList.UseVisualStyleBackColor = true;
			this.buttonReloadEnvironmentList.Click += new System.EventHandler(this.buttonReloadEnvironmentList_Click);
			// 
			// buttonDeleteEnvironment
			// 
			this.buttonDeleteEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDeleteEnvironment.Location = new System.Drawing.Point(579, 172);
			this.buttonDeleteEnvironment.Name = "buttonDeleteEnvironment";
			this.buttonDeleteEnvironment.Size = new System.Drawing.Size(111, 23);
			this.buttonDeleteEnvironment.TabIndex = 22;
			this.buttonDeleteEnvironment.Text = "Delete";
			this.buttonDeleteEnvironment.UseVisualStyleBackColor = true;
			this.buttonDeleteEnvironment.Click += new System.EventHandler(this.buttonDeleteEnvironment_Click);
			// 
			// buttonRestoreEnvironment
			// 
			this.buttonRestoreEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRestoreEnvironment.Location = new System.Drawing.Point(579, 143);
			this.buttonRestoreEnvironment.Name = "buttonRestoreEnvironment";
			this.buttonRestoreEnvironment.Size = new System.Drawing.Size(111, 23);
			this.buttonRestoreEnvironment.TabIndex = 21;
			this.buttonRestoreEnvironment.Text = "Restore";
			this.buttonRestoreEnvironment.UseVisualStyleBackColor = true;
			this.buttonRestoreEnvironment.Click += new System.EventHandler(this.buttonRestoreEnvironment_Click);
			// 
			// buttonSaveEnvironment
			// 
			this.buttonSaveEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSaveEnvironment.Location = new System.Drawing.Point(579, 114);
			this.buttonSaveEnvironment.Name = "buttonSaveEnvironment";
			this.buttonSaveEnvironment.Size = new System.Drawing.Size(111, 23);
			this.buttonSaveEnvironment.TabIndex = 20;
			this.buttonSaveEnvironment.Text = "Save";
			this.buttonSaveEnvironment.UseVisualStyleBackColor = true;
			this.buttonSaveEnvironment.Click += new System.EventHandler(this.buttonSaveEnvironment_Click);
			// 
			// textBoxMetadataFileDirectory
			// 
			this.textBoxMetadataFileDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMetadataFileDirectory.Location = new System.Drawing.Point(11, 277);
			this.textBoxMetadataFileDirectory.Name = "textBoxMetadataFileDirectory";
			this.textBoxMetadataFileDirectory.Size = new System.Drawing.Size(562, 20);
			this.textBoxMetadataFileDirectory.TabIndex = 19;
			// 
			// textBoxOutputDirectory
			// 
			this.textBoxOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                           | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxOutputDirectory.Location = new System.Drawing.Point(11, 238);
			this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
			this.textBoxOutputDirectory.Size = new System.Drawing.Size(562, 20);
			this.textBoxOutputDirectory.TabIndex = 18;
			// 
			// textBoxConnectionStringToMetadataStore
			// 
			this.textBoxConnectionStringToMetadataStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                                           | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxConnectionStringToMetadataStore.Location = new System.Drawing.Point(11, 168);
			this.textBoxConnectionStringToMetadataStore.Multiline = true;
			this.textBoxConnectionStringToMetadataStore.Name = "textBoxConnectionStringToMetadataStore";
			this.textBoxConnectionStringToMetadataStore.Size = new System.Drawing.Size(562, 51);
			this.textBoxConnectionStringToMetadataStore.TabIndex = 17;
			// 
			// textBoxConnectionStringToDatabase
			// 
			this.textBoxConnectionStringToDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                                      | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxConnectionStringToDatabase.Location = new System.Drawing.Point(11, 98);
			this.textBoxConnectionStringToDatabase.Multiline = true;
			this.textBoxConnectionStringToDatabase.Name = "textBoxConnectionStringToDatabase";
			this.textBoxConnectionStringToDatabase.Size = new System.Drawing.Size(562, 51);
			this.textBoxConnectionStringToDatabase.TabIndex = 16;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 261);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(111, 13);
			this.label5.TabIndex = 15;
			this.label5.Text = "Metadata file directory";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 222);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(82, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Output directory";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(174, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Connection string to metadata store";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(178, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Connection string to target database";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(11, 59);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(618, 20);
			this.textBoxName.TabIndex = 11;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 43);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Name";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(706, 383);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "Business Object Editor";
			this.Load += new System.EventHandler(this.EditorForm_Load);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPageEditor.ResumeLayout(false);
			this.tabPageEditor.PerformLayout();
			this.tabPageEnvironment.ResumeLayout(false);
			this.tabPageEnvironment.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.Panel panelEditor;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRender;
		private System.Windows.Forms.ToolStripMenuItem updateMetadataStoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generateStoredProceduresToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem plainObjectSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem plainObjectListViewSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem multiversionDocumentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem multiversionDocumentListViewSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonSave;
		private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
		private System.Windows.Forms.ToolStripButton toolStripButtonPersist;
		private System.Windows.Forms.ToolStripButton toolStripButtonGenerate;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpdateMetadataStore;
		private System.Windows.Forms.ToolStripLabel toolStripLabelTargetDatabase;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxEnvironment;
		private System.Windows.Forms.ToolStripMenuItem tooldToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generateMetadatastoreDatabaseScriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem simpleProjectManagerToolStripMenuItem;
		private System.Windows.Forms.Button buttonTestEnvironment;
		private System.Windows.Forms.ComboBox comboBoxEnvironment;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageEditor;
		private System.Windows.Forms.TabPage tabPageEnvironment;
		private System.Windows.Forms.TextBox textBoxMetadataFileDirectory;
		private System.Windows.Forms.TextBox textBoxOutputDirectory;
		private System.Windows.Forms.TextBox textBoxConnectionStringToMetadataStore;
		private System.Windows.Forms.TextBox textBoxConnectionStringToDatabase;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonSaveEnvironment;
		private System.Windows.Forms.Button buttonDeleteEnvironment;
		private System.Windows.Forms.Button buttonRestoreEnvironment;
		private System.Windows.Forms.Button buttonReloadEnvironmentList;
		private System.Windows.Forms.ToolStripButton toolStripButtonReload;
		private System.Windows.Forms.ToolStripMenuItem storedQueryToolStripMenuItem;
	}
}