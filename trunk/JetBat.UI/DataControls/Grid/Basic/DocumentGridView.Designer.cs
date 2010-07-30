using System.Configuration;
using UserActionSet=JetBat.UI.UserActions.UserActionSet;

namespace JetBat.UI.DataControls.Grid.Basic
{
	partial class DocumentGridView
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
				try
				{
					if ((this as IPersistComponentSettings).SaveSettings)
						SaveComponentSettings();
				}
				catch
				{ }
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
			((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
			this.SuspendLayout();
			// 
			// DocumentGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "DocumentGridView";
			this.Size = new System.Drawing.Size(784, 291);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MultiversionDocumentGridView_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		
	}
}