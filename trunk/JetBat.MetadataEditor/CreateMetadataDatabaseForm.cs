using System;
using System.IO;
using System.Windows.Forms;
using JetBat.Metadata;

namespace JetBat.MetadataEditor
{
	public partial class CreateMetadataDatabaseForm : Form
	{
		public CreateMetadataDatabaseForm()
		{
			InitializeComponent();
		}

		private void buttonGenerateMetadataStoreScript_Click(object sender, EventArgs e)
		{
			var scriptFileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Output\CreateMetadataStore.sql");
			new ScriptMaster("SqlScripts").CreateMetadataDatabase(scriptFileName, textBoxMetadataDatabaseName.Text, textBoxTargetDatabaseName.Text, checkBoxOpenScript.Checked);
		}

		private void textBoxTargetDatabaseName_TextChanged(object sender, EventArgs e)
		{
			textBoxMetadataDatabaseName.Text = textBoxTargetDatabaseName.Text + "_Metadata";
		}

		private void buttonGenerateMultiversionDocumentInfrastructrureScript_Click(object sender, EventArgs e)
		{
			var scriptFileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Output\CreateMultiversionDocumentInfrastructure.sql");
			new ScriptMaster("SqlScripts").GenerateMultiversionDocumentInfrastructrureScript(scriptFileName, textBoxTargetDatabaseName.Text, checkBoxOpenScript.Checked);
		}
	}
}