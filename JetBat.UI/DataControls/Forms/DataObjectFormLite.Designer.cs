using System.Windows.Forms;

namespace JetBat.UI.DataControls.Forms
{
	partial class DataObjectFormLite
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataObjectFormLite));
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.imageListErrorSeverity = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.ButtonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.Controls.Add(this.buttonCancel);
			this.ButtonPanel.Controls.Add(this.buttonOK);
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ButtonPanel.Location = new System.Drawing.Point(381, 0);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(37, 139);
			this.ButtonPanel.TabIndex = 1000;
			// 
			// buttonCancel
			// 
			this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Image = global::JetBat.UI.Resource.Cancel_16;
			this.buttonCancel.Location = new System.Drawing.Point(3, 39);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(30, 30);
			this.buttonCancel.TabIndex = 1;
			// 
			// buttonOK
			// 
			this.buttonOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonOK.Image = global::JetBat.UI.Resource.Save_16;
			this.buttonOK.Location = new System.Drawing.Point(3, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(30, 30);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Click += new System.EventHandler(this.OKBtn_Click);
			// 
			// imageListErrorSeverity
			// 
			this.imageListErrorSeverity.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListErrorSeverity.ImageStream")));
			this.imageListErrorSeverity.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListErrorSeverity.Images.SetKeyName(0, "INFO.ICO");
			this.imageListErrorSeverity.Images.SetKeyName(1, "warning.ico");
			this.imageListErrorSeverity.Images.SetKeyName(2, "error.ico");
			this.imageListErrorSeverity.Images.SetKeyName(3, "mail_16.ico");
			// 
			// DataObjectFormLite
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(418, 139);
			this.Controls.Add(this.ButtonPanel);
			this.MinimumSize = new System.Drawing.Size(167, 100);
			this.Name = "DataObjectFormLite";
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ButtonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Button buttonCancel;
		private Panel ButtonPanel;
		private ImageList imageListErrorSeverity;
		private Button buttonOK;
	}
}