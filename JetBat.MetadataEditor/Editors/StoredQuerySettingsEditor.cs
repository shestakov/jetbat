using System.Data.Objects;
using System.Windows.Forms;
using JetBat.Metadata;
using JetBat.Metadata.StoredQueryManager;

namespace JetBat.MetadataEditor.Editors
{
	public partial class StoredQuerySettingsEditor : UserControl
	{
		private StoredQueryConstructorSettings constructorSettings;

		public StoredQuerySettingsEditor()
		{
			InitializeComponent();
		}

		public StoredQueryConstructorSettings ConstructorSettings
		{
			get
			{
				setBusinessObjectSettings();
				return constructorSettings;
			}
			set
			{
				constructorSettings = value;
				getBusinessObjectSettings();
			}
		}

		private void setBusinessObjectSettings()
		{
			if (constructorSettings == null)
				constructorSettings = new StoredQueryConstructorSettings();
			constructorSettings.EntityNamespace = comboBoxEntityNamespace.Text;
			constructorSettings.EntityName = textBoxEntityName.Text;
			constructorSettings.StoredProcedureName = comboBoxStoredProcedureName.Text;
			constructorSettings.UIListCaption = textBoxUIListCaption.Text;
			constructorSettings.PredefinedAttributes = checkBoxPredefinedAttributes.Checked;
		}

		private void getBusinessObjectSettings()
		{
			comboBoxEntityNamespace.Text = constructorSettings.EntityNamespace;
			textBoxEntityName.Text = constructorSettings.EntityName;
			textBoxUIListCaption.Text = constructorSettings.UIListCaption;
			comboBoxStoredProcedureName.Text = constructorSettings.StoredProcedureName;
			checkBoxPredefinedAttributes.Checked = constructorSettings.PredefinedAttributes;
		}

		public void LoadMetadata(MetadataContainer metadataContext)
		{
			metadataContext.Connection.Close();
			metadataContext.Connection.Open();
			ObjectQuery query = metadataContext.Namespace;
			query.Execute(MergeOption.OverwriteChanges);

			string text = comboBoxEntityNamespace.Text;
			comboBoxEntityNamespace.DataSource = query;
			comboBoxEntityNamespace.DisplayMember = "Name";
			comboBoxEntityNamespace.Text = text;

			query = metadataContext.DatabaseTable;
			query.Execute(MergeOption.OverwriteChanges);

			query = metadataContext.DatabaseView;
			query.Execute(MergeOption.OverwriteChanges);
			text = comboBoxStoredProcedureName.Text;
			comboBoxStoredProcedureName.DataSource = query;
			comboBoxStoredProcedureName.DisplayMember = "Name";
			comboBoxStoredProcedureName.Text = text;
			metadataContext.Connection.Close();
		}
	}
}