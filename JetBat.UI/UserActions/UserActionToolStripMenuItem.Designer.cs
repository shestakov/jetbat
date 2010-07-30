namespace JetBat.UI.UserActions
{
	partial class UserActionToolStripMenuItem
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// UserActionToolStripMenuItem
			// 
			this.EnabledChanged += new System.EventHandler(this.UserActionToolStripMenuItem_EnabledChanged);
			this.TextChanged += new System.EventHandler(this.UserActionToolStripMenuItem_TextChanged);
			this.VisibleChanged += new System.EventHandler(this.UserActionToolStripMenuItem_VisibleChanged);
			this.Click += new System.EventHandler(this.UserActionToolStripMenuItem_Click);

		}

		#endregion
	}
}