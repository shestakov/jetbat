namespace JetBat.MetadataEditor.Editors
{
	partial class StoredQuerySettingsEditor
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
			this.checkBoxPredefinedAttributes = new System.Windows.Forms.CheckBox();
			this.textBoxUIListCaption = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxEntityName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxEntityNamespace = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxStoredProcedureName = new System.Windows.Forms.ComboBox();
			this.tabControl.SuspendLayout();
			this.tabPageBasic.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageBasic);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(462, 306);
			this.tabControl.TabIndex = 8;
			// 
			// tabPageBasic
			// 
			this.tabPageBasic.Controls.Add(this.checkBoxPredefinedAttributes);
			this.tabPageBasic.Controls.Add(this.textBoxUIListCaption);
			this.tabPageBasic.Controls.Add(this.label6);
			this.tabPageBasic.Controls.Add(this.textBoxEntityName);
			this.tabPageBasic.Controls.Add(this.label5);
			this.tabPageBasic.Controls.Add(this.label4);
			this.tabPageBasic.Controls.Add(this.comboBoxEntityNamespace);
			this.tabPageBasic.Controls.Add(this.label3);
			this.tabPageBasic.Controls.Add(this.comboBoxStoredProcedureName);
			this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
			this.tabPageBasic.Name = "tabPageBasic";
			this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageBasic.Size = new System.Drawing.Size(454, 280);
			this.tabPageBasic.TabIndex = 0;
			this.tabPageBasic.Text = "Basic";
			this.tabPageBasic.UseVisualStyleBackColor = true;
			// 
			// checkBoxPredefinedAttributes
			// 
			this.checkBoxPredefinedAttributes.AutoSize = true;
			this.checkBoxPredefinedAttributes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBoxPredefinedAttributes.Location = new System.Drawing.Point(31, 112);
			this.checkBoxPredefinedAttributes.Name = "checkBoxPredefinedAttributes";
			this.checkBoxPredefinedAttributes.Size = new System.Drawing.Size(123, 17);
			this.checkBoxPredefinedAttributes.TabIndex = 6;
			this.checkBoxPredefinedAttributes.Text = "Predefined attributes";
			this.checkBoxPredefinedAttributes.UseVisualStyleBackColor = true;
			// 
			// textBoxUIListCaption
			// 
			this.textBoxUIListCaption.Location = new System.Drawing.Point(140, 86);
			this.textBoxUIListCaption.Name = "textBoxUIListCaption";
			this.textBoxUIListCaption.Size = new System.Drawing.Size(291, 20);
			this.textBoxUIListCaption.TabIndex = 5;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(62, 89);
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
			this.label3.Location = new System.Drawing.Point(16, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Stored procedure name";
			// 
			// comboBoxStoredProcedureName
			// 
			this.comboBoxStoredProcedureName.FormattingEnabled = true;
			this.comboBoxStoredProcedureName.Location = new System.Drawing.Point(140, 59);
			this.comboBoxStoredProcedureName.Name = "comboBoxStoredProcedureName";
			this.comboBoxStoredProcedureName.Size = new System.Drawing.Size(291, 21);
			this.comboBoxStoredProcedureName.TabIndex = 4;
			// 
			// StoredQuerySettingsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.Name = "StoredQuerySettingsEditor";
			this.Size = new System.Drawing.Size(462, 306);
			this.tabControl.ResumeLayout(false);
			this.tabPageBasic.ResumeLayout(false);
			this.tabPageBasic.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageBasic;
		private System.Windows.Forms.CheckBox checkBoxPredefinedAttributes;
		private System.Windows.Forms.TextBox textBoxUIListCaption;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxEntityName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxEntityNamespace;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxStoredProcedureName;
	}
}