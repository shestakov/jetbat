using System;
using System.Windows.Forms;

namespace JetBat.UI.DataControls.Forms
{
	public partial class DataObjectFormLite : DataObjectForm
	{
		#region События

		private void OKBtn_Click(object sender, EventArgs e)
		{
			ValidateChildren();
			if (!ValidateInputControls(this))
				return;
			DialogResult = DialogResult.OK;
		}

		#endregion

		#region Конструктор

		public DataObjectFormLite()
		{
			InitializeComponent();
		}

		#endregion
	}
}