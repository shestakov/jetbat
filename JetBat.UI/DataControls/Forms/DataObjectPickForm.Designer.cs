using System.Windows.Forms;

namespace JetBat.UI.DataControls.Forms
{
	partial class DataObjectPickForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataObjectPickForm));
			this.tabControlUserActions = new System.Windows.Forms.TabControl();
			this.tabPageActionsOverEntities = new System.Windows.Forms.TabPage();
			this.splitContainerUserActions = new System.Windows.Forms.SplitContainer();
			this.tabPageActionsWithNoEntity = new System.Windows.Forms.TabPage();
			this.panelExButtons = new Panel();
			this.buttonXOK = new Button();
			this.buttonXCancel = new Button();
			this.tabControlUserActions.SuspendLayout();
			this.tabPageActionsOverEntities.SuspendLayout();
			this.splitContainerUserActions.SuspendLayout();
			this.panelExButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlUserActions
			// 
			this.tabControlUserActions.Controls.Add(this.tabPageActionsOverEntities);
			this.tabControlUserActions.Controls.Add(this.tabPageActionsWithNoEntity);
			this.tabControlUserActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlUserActions.Location = new System.Drawing.Point(3, 3);
			this.tabControlUserActions.Name = "tabControlUserActions";
			this.tabControlUserActions.SelectedIndex = 0;
			this.tabControlUserActions.Size = new System.Drawing.Size(921, 341);
			this.tabControlUserActions.TabIndex = 0;
			// 
			// tabPageActionsOverEntities
			// 
			this.tabPageActionsOverEntities.Controls.Add(this.splitContainerUserActions);
			this.tabPageActionsOverEntities.Location = new System.Drawing.Point(4, 22);
			this.tabPageActionsOverEntities.Name = "tabPageActionsOverEntities";
			this.tabPageActionsOverEntities.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageActionsOverEntities.Size = new System.Drawing.Size(913, 315);
			this.tabPageActionsOverEntities.TabIndex = 0;
			this.tabPageActionsOverEntities.Text = "Действия над сущностями";
			this.tabPageActionsOverEntities.UseVisualStyleBackColor = true;
			// 
			// splitContainerUserActions
			// 
			this.splitContainerUserActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerUserActions.Location = new System.Drawing.Point(3, 3);
			this.splitContainerUserActions.Name = "splitContainerUserActions";
			this.splitContainerUserActions.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.splitContainerUserActions.Size = new System.Drawing.Size(907, 309);
			this.splitContainerUserActions.SplitterDistance = 161;
			this.splitContainerUserActions.TabIndex = 0;
			// 
			// tabPageActionsWithNoEntity
			// 
			this.tabPageActionsWithNoEntity.Location = new System.Drawing.Point(4, 22);
			this.tabPageActionsWithNoEntity.Name = "tabPageActionsWithNoEntity";
			this.tabPageActionsWithNoEntity.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageActionsWithNoEntity.Size = new System.Drawing.Size(913, 315);
			this.tabPageActionsWithNoEntity.TabIndex = 1;
			this.tabPageActionsWithNoEntity.Text = "Действия пользователя, не привязанные к сущности";
			this.tabPageActionsWithNoEntity.UseVisualStyleBackColor = true;
			// 
			// panelExButtons
			// 
			this.panelExButtons.Controls.Add(this.buttonXOK);
			this.panelExButtons.Controls.Add(this.buttonXCancel);
			this.panelExButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelExButtons.Location = new System.Drawing.Point(0, 218);
			this.panelExButtons.Name = "panelExButtons";
			this.panelExButtons.Size = new System.Drawing.Size(417, 35);
			this.panelExButtons.TabIndex = 0;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.Location = new System.Drawing.Point(258, 3);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(75, 29);
			this.buttonXOK.TabIndex = 1;
			this.buttonXOK.Text = "Выбрать";
			this.buttonXOK.Click += new System.EventHandler(this.OKBtn_Click);
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(339, 3);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(75, 29);
			this.buttonXCancel.TabIndex = 0;
			this.buttonXCancel.Text = "Отмена";
			// 
			// DataObjectPickForm
			// 
			this.AcceptButton = this.buttonXOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.GhostWhite;
			this.CancelButton = this.buttonXCancel;
			this.ClientSize = new System.Drawing.Size(417, 253);
			this.Controls.Add(this.panelExButtons);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DataObjectPickForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "DataObjectPickForm";
			this.tabControlUserActions.ResumeLayout(false);
			this.tabPageActionsOverEntities.ResumeLayout(false);
			this.splitContainerUserActions.ResumeLayout(false);
			this.panelExButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private TabControl tabControlUserActions;
		private TabPage tabPageActionsOverEntities;
		private SplitContainer splitContainerUserActions;
		private TabPage tabPageActionsWithNoEntity;
		private Panel panelExButtons;
		private Button buttonXCancel;
		private Button buttonXOK;
	}
}