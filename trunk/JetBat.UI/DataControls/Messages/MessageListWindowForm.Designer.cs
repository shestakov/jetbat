namespace JetBat.UI.DataControls.Messages
{
	partial class MessageListWindowForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageListWindowForm));
			this.button1 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.listViewMessges = new System.Windows.Forms.ListView();
			this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
			this.columnHeaderErrorMessage = new System.Windows.Forms.ColumnHeader(4);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(165, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Кнопка 1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(3, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 3;
			this.button3.Text = "Кнопка 3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(84, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Кнопка 2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Location = new System.Drawing.Point(198, 117);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(244, 30);
			this.panel1.TabIndex = 0;
			// 
			// listViewMessges
			// 
			this.listViewMessges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                     | System.Windows.Forms.AnchorStyles.Left)
			                                                                    | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewMessges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(150)))));
			this.listViewMessges.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			                                                                              	this.columnHeaderErrorMessage});
			this.listViewMessges.Location = new System.Drawing.Point(12, 12);
			this.listViewMessges.Name = "listViewMessges";
			this.listViewMessges.Size = new System.Drawing.Size(430, 97);
			this.listViewMessges.SmallImageList = this.imageListIcons;
			this.listViewMessges.TabIndex = 2;
			this.listViewMessges.UseCompatibleStateImageBehavior = false;
			this.listViewMessges.View = System.Windows.Forms.View.Details;
			// 
			// imageListIcons
			// 
			this.imageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
			this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListIcons.Images.SetKeyName(0, "warning.ico");
			this.imageListIcons.Images.SetKeyName(1, "error.ico");
			this.imageListIcons.Images.SetKeyName(2, "INFO.ICO");
			this.imageListIcons.Images.SetKeyName(3, "question.ico");
			this.imageListIcons.Images.SetKeyName(4, "mail_16.ico");
			// 
			// columnHeaderErrorMessage
			// 
			this.columnHeaderErrorMessage.Text = "Сообщение";
			this.columnHeaderErrorMessage.Width = 400;
			// 
			// MessageListWindowForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.GhostWhite;
			this.ClientSize = new System.Drawing.Size(454, 152);
			this.Controls.Add(this.listViewMessges);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(462, 178);
			this.Name = "MessageListWindowForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Уведомление";
			this.Shown += new System.EventHandler(this.MessageWindowForm_Shown);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView listViewMessges;
		private System.Windows.Forms.ImageList imageListIcons;
		private System.Windows.Forms.ColumnHeader columnHeaderErrorMessage;
	}
}