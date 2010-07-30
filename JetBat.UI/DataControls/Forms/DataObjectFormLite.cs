using System;
using System.Windows.Forms;

namespace JetBat.UI.DataControls.Forms
{
	public partial class DataObjectFormLite : DataObjectForm
	{
		#region �������

		private void OKBtn_Click(object sender, EventArgs e)
		{
			ValidateChildren();
			if (!ValidateInputControls(this))
				return;
			DialogResult = DialogResult.OK;
		}

		#endregion

		#region �����������

		public DataObjectFormLite()
		{
			InitializeComponent();
		}

		#endregion
	}
}