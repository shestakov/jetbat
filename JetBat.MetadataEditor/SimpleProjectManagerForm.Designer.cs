namespace JetBat.MetadataEditor
{
	partial class SimpleProjectManagerForm
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
			this.buttonCreate = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonLoad = new System.Windows.Forms.Button();
			this.buttonPersist = new System.Windows.Forms.Button();
			this.buttonRender = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonCreate
			// 
			this.buttonCreate.Location = new System.Drawing.Point(12, 12);
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.Size = new System.Drawing.Size(184, 39);
			this.buttonCreate.TabIndex = 7;
			this.buttonCreate.Text = "Create project from directory";
			this.buttonCreate.UseVisualStyleBackColor = true;
			this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(12, 102);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(184, 39);
			this.buttonSave.TabIndex = 8;
			this.buttonSave.Text = "Save project";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.button1_Click);
			// 
			// buttonLoad
			// 
			this.buttonLoad.Location = new System.Drawing.Point(12, 57);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(184, 39);
			this.buttonLoad.TabIndex = 9;
			this.buttonLoad.Text = "Load project";
			this.buttonLoad.UseVisualStyleBackColor = true;
			this.buttonLoad.Click += new System.EventHandler(this.button3_Click);
			// 
			// buttonPersist
			// 
			this.buttonPersist.Location = new System.Drawing.Point(12, 147);
			this.buttonPersist.Name = "buttonPersist";
			this.buttonPersist.Size = new System.Drawing.Size(184, 39);
			this.buttonPersist.TabIndex = 10;
			this.buttonPersist.Text = "Perisist project";
			this.buttonPersist.UseVisualStyleBackColor = true;
			this.buttonPersist.Click += new System.EventHandler(this.buttonPersist_Click);
			// 
			// buttonRender
			// 
			this.buttonRender.Location = new System.Drawing.Point(12, 192);
			this.buttonRender.Name = "buttonRender";
			this.buttonRender.Size = new System.Drawing.Size(184, 39);
			this.buttonRender.TabIndex = 11;
			this.buttonRender.Text = "Render project";
			this.buttonRender.UseVisualStyleBackColor = true;
			this.buttonRender.Click += new System.EventHandler(this.buttonRender_Click);
			// 
			// SimpleProjectManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(677, 258);
			this.Controls.Add(this.buttonRender);
			this.Controls.Add(this.buttonPersist);
			this.Controls.Add(this.buttonLoad);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonCreate);
			this.Name = "SimpleProjectManagerForm";
			this.Text = "SimpleProjectManagerForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonCreate;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonLoad;
		private System.Windows.Forms.Button buttonPersist;
		private System.Windows.Forms.Button buttonRender;
	}
}