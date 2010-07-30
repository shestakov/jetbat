using System;
using System.Windows.Forms;

namespace JetBat.UI.DataControls.Forms
{
	public partial class DataObjectFormStandard : DataObjectForm
	{
		private const int ErrorListPanelHeight = 100;
		private bool showMessagePanel;

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

		public DataObjectFormStandard()
		{
			InitializeComponent();
		}

		#endregion

		private void buttonShowMessages_Click(object sender, EventArgs e)
		{
			showMessagePanel = !showMessagePanel;
			SuspendLayout();
			panelErrorMessages.Height = showMessagePanel ? ErrorListPanelHeight : 0;
			Height += showMessagePanel ? ErrorListPanelHeight : -ErrorListPanelHeight;
			panelErrorMessages.Visible = showMessagePanel;
			ResumeLayout();
		}

		private void listViewMessages_SizeChanged(object sender, EventArgs e)
		{
			columnHeaderMessage.Width = listViewMessages.ClientSize.Width;
		}

		private void DataObjectForm_Load(object sender, EventArgs e)
		{
			showMessagePanel = false;
			panelErrorMessages.Height = 0;
			Height -= panelErrorMessages.Height;
		}
	}
}