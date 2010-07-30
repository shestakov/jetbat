using System.Windows.Forms;

namespace JetBat.UI.DataControls.Forms
{
	partial class DataObjectFormStandard
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataObjectFormStandard));
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.buttonShowMessages = new System.Windows.Forms.Button();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.OKBtn = new System.Windows.Forms.Button();
			this.panelErrorMessages = new System.Windows.Forms.Panel();
			this.listViewMessages = new System.Windows.Forms.ListView();
			this.columnHeaderMessage = new System.Windows.Forms.ColumnHeader(3);
			this.imageListErrorSeverity = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.ButtonPanel.SuspendLayout();
			this.panelErrorMessages.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.Controls.Add(this.buttonShowMessages);
			this.ButtonPanel.Controls.Add(this.CancelBtn);
			this.ButtonPanel.Controls.Add(this.OKBtn);
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ButtonPanel.Location = new System.Drawing.Point(372, 0);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(46, 139);
			this.ButtonPanel.TabIndex = 1000;
			// 
			// buttonShowMessages
			// 
			this.buttonShowMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
			                                                                       | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonShowMessages.Image = global::JetBat.UI.Resource.paste_32;
			this.buttonShowMessages.Location = new System.Drawing.Point(3, 96);
			this.buttonShowMessages.Name = "buttonShowMessages";
			this.buttonShowMessages.Size = new System.Drawing.Size(40, 40);
			this.buttonShowMessages.TabIndex = 2;
			this.buttonShowMessages.Visible = false;
			this.buttonShowMessages.Click += new System.EventHandler(this.buttonShowMessages_Click);
			// 
			// CancelBtn
			// 
			this.CancelBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                              | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Image = global::JetBat.UI.Resource.Cancel_32;
			this.CancelBtn.Location = new System.Drawing.Point(3, 49);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(40, 40);
			this.CancelBtn.TabIndex = 1;
			// 
			// OKBtn
			// 
			this.OKBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                          | System.Windows.Forms.AnchorStyles.Right)));
			this.OKBtn.Image = global::JetBat.UI.Resource.Save_32;
			this.OKBtn.Location = new System.Drawing.Point(3, 3);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(40, 40);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
			// 
			// panelErrorMessages
			// 
			this.panelErrorMessages.Controls.Add(this.listViewMessages);
			this.panelErrorMessages.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelErrorMessages.Location = new System.Drawing.Point(0, 139);
			this.panelErrorMessages.Name = "panelErrorMessages";
			this.panelErrorMessages.Size = new System.Drawing.Size(372, 0);
			this.panelErrorMessages.TabIndex = 1001;
			this.panelErrorMessages.Visible = false;
			// 
			// listViewMessages
			// 
			this.listViewMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			                                                                               	this.columnHeaderMessage});
			this.listViewMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewMessages.Location = new System.Drawing.Point(0, 0);
			this.listViewMessages.Name = "listViewMessages";
			this.listViewMessages.Size = new System.Drawing.Size(372, 0);
			this.listViewMessages.SmallImageList = this.imageListErrorSeverity;
			this.listViewMessages.TabIndex = 0;
			this.listViewMessages.UseCompatibleStateImageBehavior = false;
			this.listViewMessages.View = System.Windows.Forms.View.Details;
			this.listViewMessages.SizeChanged += new System.EventHandler(this.listViewMessages_SizeChanged);
			// 
			// columnHeaderMessage
			// 
			this.columnHeaderMessage.Text = "Сообщение";
			this.columnHeaderMessage.Width = 400;
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
			// DataObjectFormStandard
			// 
			this.AcceptButton = this.OKBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.CancelButton = this.CancelBtn;
			this.ClientSize = new System.Drawing.Size(418, 139);
			this.Controls.Add(this.panelErrorMessages);
			this.Controls.Add(this.ButtonPanel);
			this.Name = "DataObjectFormStandard";
			this.Load += new System.EventHandler(this.DataObjectForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ButtonPanel.ResumeLayout(false);
			this.panelErrorMessages.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Button CancelBtn;
		private Button OKBtn;
		private Panel ButtonPanel;
		private Button buttonShowMessages;
		private Panel panelErrorMessages;
		private ListView listViewMessages;
		private ColumnHeader columnHeaderMessage;
		private ImageList imageListErrorSeverity;
	}
}