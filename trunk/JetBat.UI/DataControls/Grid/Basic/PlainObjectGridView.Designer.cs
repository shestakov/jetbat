using System.Configuration;
using System.Windows.Forms;
using UserActionSet=JetBat.UI.UserActions.UserActionSet;

namespace JetBat.UI.DataControls.Grid.Basic
{
	partial class PlainObjectGridView
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
			try
			{
				if ((this as IPersistComponentSettings).SaveSettings)
					SaveComponentSettings();
			}
			catch
			{ }
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
			this.components = new System.ComponentModel.Container();
			this.timerDetailLoad = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
			this.SuspendLayout();
			// 
			// timerDetailLoad
			// 
			this.timerDetailLoad.Tick += new System.EventHandler(this.timerDetailLoad_Tick);
			// 
			// PlainObjectGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "PlainObjectGridView";
			this.Size = new System.Drawing.Size(627, 291);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataObjectGridView_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
			((System.Configuration.IPersistComponentSettings)(this)).LoadComponentSettings();
			this.ResumeLayout(false);

		}

		#endregion

		private Timer timerDetailLoad;

	}
}