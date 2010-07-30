using System.Data.Objects;
using System.Windows.Forms;
using JetBat.Metadata;
using JetBat.Metadata.MultiversionDocumentListViewManager;

namespace JetBat.MetadataEditor.Editors
{
	public partial class MultiversionDocumentListViewSettingsEditor : UserControl
	{
		private MultiversionDocumentListViewConstructorSettings constructorSettings;

		public MultiversionDocumentListViewConstructorSettings ConstructorSettings
		{
			get
			{
				setConstructorSettings();
				return constructorSettings;
			}
			set
			{
				constructorSettings = value;
				getConstructorSettings();
			}
		}

		private void setConstructorSettings()
		{
			if (constructorSettings == null)
				constructorSettings = new MultiversionDocumentListViewConstructorSettings();
			constructorSettings.EntityNamespace = comboBoxEntityNamespace.Text;
			constructorSettings.EntityName = textBoxEntityName.Text;
			constructorSettings.UICaption = textBoxUICaption.Text;
			constructorSettings.TargetObjectName = comboBoxTargetObjectName.Text;
			constructorSettings.TargetObjectNamespace = comboBoxTargetObjectNamespace.Text;
			constructorSettings.ViewName = comboBoxViewName.Text;
			constructorSettings.ShowDeletedObjects = checkBoxShowDeletedObjects.Checked;
			constructorSettings.InvisibleColumns = textBoxInvisibleColumns.Lines;
			constructorSettings.IgnoredColumns = textBoxIgnoredColumns.Lines;
		}

		private void getConstructorSettings()
		{
			comboBoxEntityNamespace.Text = constructorSettings.EntityNamespace;
			textBoxEntityName.Text = constructorSettings.EntityName;
			textBoxUICaption.Text = constructorSettings.UICaption;
			comboBoxTargetObjectName.Text = constructorSettings.TargetObjectName;
			comboBoxViewName.Text = constructorSettings.ViewName;
			comboBoxTargetObjectName.Text = constructorSettings.TargetObjectName;
			comboBoxTargetObjectNamespace.Text = constructorSettings.TargetObjectNamespace;
			checkBoxShowDeletedObjects.Checked = constructorSettings.ShowDeletedObjects;

			textBoxInvisibleColumns.Lines = constructorSettings.InvisibleColumns;
			textBoxIgnoredColumns.Lines = constructorSettings.IgnoredColumns;
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
			text = comboBoxTargetObjectName.Text;
			comboBoxTargetObjectName.DataSource = query;
			comboBoxTargetObjectName.DisplayMember = "Name";
			comboBoxTargetObjectName.Text = text;

			query = metadataContext.DatabaseView;
			query.Execute(MergeOption.OverwriteChanges);
			text = comboBoxViewName.Text;
			comboBoxViewName.DataSource = query;
			comboBoxViewName.DisplayMember = "Name";
			comboBoxViewName.Text = text;
			metadataContext.Connection.Close();
		}

		public MultiversionDocumentListViewSettingsEditor()
		{
			InitializeComponent();
		}
	}
}