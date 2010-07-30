namespace JetBat.MetadataEditor
{
	partial class CreateMetadataDatabaseForm
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
			this.textBoxTargetDatabaseName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonGenerateMetadataStoreScript = new System.Windows.Forms.Button();
			this.textBoxMetadataDatabaseName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBoxOpenScript = new System.Windows.Forms.CheckBox();
			this.buttonGenerateMultiversionDocumentInfrastructrureScript = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxTargetDatabaseName
			// 
			this.textBoxTargetDatabaseName.Location = new System.Drawing.Point(12, 25);
			this.textBoxTargetDatabaseName.Name = "textBoxTargetDatabaseName";
			this.textBoxTargetDatabaseName.Size = new System.Drawing.Size(322, 20);
			this.textBoxTargetDatabaseName.TabIndex = 0;
			this.textBoxTargetDatabaseName.Text = "OrderTracking";
			this.textBoxTargetDatabaseName.TextChanged += new System.EventHandler(this.textBoxTargetDatabaseName_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Target Database Name";
			// 
			// buttonGenerateMetadataStoreScript
			// 
			this.buttonGenerateMetadataStoreScript.Location = new System.Drawing.Point(12, 90);
			this.buttonGenerateMetadataStoreScript.Name = "buttonGenerateMetadataStoreScript";
			this.buttonGenerateMetadataStoreScript.Size = new System.Drawing.Size(322, 36);
			this.buttonGenerateMetadataStoreScript.TabIndex = 2;
			this.buttonGenerateMetadataStoreScript.Text = "Generate Metadata Store Script";
			this.buttonGenerateMetadataStoreScript.UseVisualStyleBackColor = true;
			this.buttonGenerateMetadataStoreScript.Click += new System.EventHandler(this.buttonGenerateMetadataStoreScript_Click);
			// 
			// textBoxMetadataDatabaseName
			// 
			this.textBoxMetadataDatabaseName.Location = new System.Drawing.Point(12, 64);
			this.textBoxMetadataDatabaseName.Name = "textBoxMetadataDatabaseName";
			this.textBoxMetadataDatabaseName.Size = new System.Drawing.Size(322, 20);
			this.textBoxMetadataDatabaseName.TabIndex = 1;
			this.textBoxMetadataDatabaseName.Text = "OrderTracking_Metadata";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(132, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Metadate Database Name";
			// 
			// checkBoxOpenScript
			// 
			this.checkBoxOpenScript.AutoSize = true;
			this.checkBoxOpenScript.Checked = true;
			this.checkBoxOpenScript.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxOpenScript.Location = new System.Drawing.Point(12, 174);
			this.checkBoxOpenScript.Name = "checkBoxOpenScript";
			this.checkBoxOpenScript.Size = new System.Drawing.Size(120, 17);
			this.checkBoxOpenScript.TabIndex = 3;
			this.checkBoxOpenScript.Text = "Open script in editor";
			this.checkBoxOpenScript.UseVisualStyleBackColor = true;
			// 
			// buttonGenerateMultiversionDocumentInfrastructrureScript
			// 
			this.buttonGenerateMultiversionDocumentInfrastructrureScript.Location = new System.Drawing.Point(12, 132);
			this.buttonGenerateMultiversionDocumentInfrastructrureScript.Name = "buttonGenerateMultiversionDocumentInfrastructrureScript";
			this.buttonGenerateMultiversionDocumentInfrastructrureScript.Size = new System.Drawing.Size(322, 36);
			this.buttonGenerateMultiversionDocumentInfrastructrureScript.TabIndex = 9;
			this.buttonGenerateMultiversionDocumentInfrastructrureScript.Text = "Generate Multiversion Document Infrastructrure Script";
			this.buttonGenerateMultiversionDocumentInfrastructrureScript.UseVisualStyleBackColor = true;
			this.buttonGenerateMultiversionDocumentInfrastructrureScript.Click += new System.EventHandler(this.buttonGenerateMultiversionDocumentInfrastructrureScript_Click);
			// 
			// CreateMetadataDatabaseForm
			// 
			this.AcceptButton = this.buttonGenerateMetadataStoreScript;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(348, 204);
			this.Controls.Add(this.buttonGenerateMultiversionDocumentInfrastructrureScript);
			this.Controls.Add(this.checkBoxOpenScript);
			this.Controls.Add(this.textBoxMetadataDatabaseName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonGenerateMetadataStoreScript);
			this.Controls.Add(this.textBoxTargetDatabaseName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "CreateMetadataDatabaseForm";
			this.Text = "Generate script for Metadata Store database";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxTargetDatabaseName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonGenerateMetadataStoreScript;
		private System.Windows.Forms.TextBox textBoxMetadataDatabaseName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBoxOpenScript;
		private System.Windows.Forms.Button buttonGenerateMultiversionDocumentInfrastructrureScript;
	}
}