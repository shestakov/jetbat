namespace JetBat.UI.DataControls.Forms
{
	partial class DocumentForm
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
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.buttonShowMessages = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.ButtonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// errorProvider
			// 
			this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errorProvider.ContainerControl = this;
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.Controls.Add(this.buttonShowMessages);
			this.ButtonPanel.Controls.Add(this.buttonCancel);
			this.ButtonPanel.Controls.Add(this.buttonOK);
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ButtonPanel.Location = new System.Drawing.Point(426, 0);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(46, 207);
			this.ButtonPanel.TabIndex = 0;
			// 
			// buttonShowMessages
			// 
			this.buttonShowMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
			                                                                       | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonShowMessages.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonShowMessages.Image = global::JetBat.UI.Resource.paste_32;
			this.buttonShowMessages.Location = new System.Drawing.Point(3, 164);
			this.buttonShowMessages.Name = "buttonShowMessages";
			this.buttonShowMessages.Size = new System.Drawing.Size(40, 40);
			this.buttonShowMessages.TabIndex = 2;
			this.buttonShowMessages.Visible = false;
			// 
			// buttonCancel
			// 
			this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Image = global::JetBat.UI.Resource.Cancel_32;
			this.buttonCancel.Location = new System.Drawing.Point(3, 49);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(40, 40);
			this.buttonCancel.TabIndex = 1;
			// 
			// buttonOK
			// 
			this.buttonOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                             | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Image = global::JetBat.UI.Resource.Save_32;
			this.buttonOK.Location = new System.Drawing.Point(3, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(40, 40);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Click += new System.EventHandler(this.OKBtn_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.BackColor = System.Drawing.Color.WhiteSmoke;
			this.statusStrip.Location = new System.Drawing.Point(0, 207);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(472, 22);
			this.statusStrip.TabIndex = 0;
			// 
			// DocumentForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(472, 229);
			this.Controls.Add(this.ButtonPanel);
			this.Controls.Add(this.statusStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "DocumentForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Форма документа";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MultiversionDocumentForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ButtonPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.Button buttonShowMessages;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		protected System.Windows.Forms.Panel ButtonPanel;
		protected System.Windows.Forms.StatusStrip statusStrip;
	}
}