using System;
using System.Data.Objects;
using System.Windows.Forms;
using JetBat.Metadata;
using JetBat.Metadata.MultiversionDocumentManager;

namespace JetBat.MetadataEditor.Editors
{
	public partial class MultiversionDocumentSettingsEditor : UserControl
	{
		private MultiversionDocumentConstructorSettings constructorSettings;

		public MultiversionDocumentSettingsEditor()
		{
			InitializeComponent();
		}

		public MultiversionDocumentConstructorSettings ConstructorSettings
		{
			get
			{
				setBusinessObjectSettings();
				return constructorSettings;
			}
			set
			{
				constructorSettings = value;
				getObjectSettings();
			}
		}

		private void getObjectSettings()
		{
			comboBoxEntityNamespace.Text = constructorSettings.EntityNamespace;
			textBoxEntityName.Text = constructorSettings.EntityName;
			comboBoxTableName.Text = constructorSettings.TableName;
			comboBoxViewName.Text = constructorSettings.ViewName;

			textBoxInvisibleColumns.Lines = constructorSettings.InvisibleColumns;
			textBoxReadOnlyColumns.Lines = constructorSettings.ReadOnlyColumns;
			textBoxIgnoredColumns.Lines = constructorSettings.IgnoredColumns;
			textBoxHeaderNullableColumns.Lines = constructorSettings.HeaderNullableColumns;

			TreeNode nodeMethodCreate = treeViewMethods.Nodes["NodeMethodCreate"];
			TreeNode nodeMethodUpdateVersion = treeViewMethods.Nodes["NodeMethodUpdateVersion"];
			TreeNode nodeMethodConfirmEdit = treeViewMethods.Nodes["NodeMethodConfirmEdit"];
			TreeNode nodeMethodCommit = treeViewMethods.Nodes["NodeMethodCommit"];
			TreeNode nodeMethodRollback = treeViewMethods.Nodes["NodeMethodRollback"];

			nodeMethodCreate.Checked = true;
			nodeMethodCreate.Nodes["NodeMethodAfterCreate"].Checked = constructorSettings.MethodAfterCreate;
			nodeMethodUpdateVersion.Checked = true;
			nodeMethodUpdateVersion.Nodes["NodeMethodBeforeUpdateVersion"].Checked = constructorSettings.MethodBeforeUpdateVersion;
			nodeMethodConfirmEdit.Checked = true;
			nodeMethodConfirmEdit.Nodes["NodeMethodBeforeConfirmEdit"].Checked = constructorSettings.MethodBeforeConfirmEdit;
			nodeMethodConfirmEdit.Nodes["NodeMethodAfterConfirmEdit"].Checked = constructorSettings.MethodAfterConfirmEdit;
			nodeMethodCommit.Checked = true;
			nodeMethodCommit.Nodes["NodeMethodBeforeCommit"].Checked = constructorSettings.MethodBeforeCommit;
			nodeMethodRollback.Checked = true;
			nodeMethodRollback.Nodes["NodeMethodBeforeRollback"].Checked = constructorSettings.MethodBeforeRollback;
		}

		private void setBusinessObjectSettings()
		{
			if (constructorSettings == null)
				constructorSettings = new MultiversionDocumentConstructorSettings();
			constructorSettings.EntityNamespace = comboBoxEntityNamespace.Text;
			constructorSettings.EntityName = textBoxEntityName.Text;
			constructorSettings.TableName = comboBoxTableName.Text;
			constructorSettings.ViewName = comboBoxViewName.Text;
			constructorSettings.InvisibleColumns = textBoxInvisibleColumns.Lines;
			constructorSettings.ReadOnlyColumns = textBoxReadOnlyColumns.Lines;
			constructorSettings.IgnoredColumns = textBoxIgnoredColumns.Lines;
			constructorSettings.HeaderNullableColumns = textBoxHeaderNullableColumns.Lines;

			TreeNode nodeMethodCreate = treeViewMethods.Nodes["NodeMethodCreate"];
			TreeNode nodeMethodUpdateVersion = treeViewMethods.Nodes["NodeMethodUpdateVersion"];
			TreeNode nodeMethodConfirmEdit = treeViewMethods.Nodes["NodeMethodConfirmEdit"];
			TreeNode nodeMethodCommit = treeViewMethods.Nodes["NodeMethodCommit"];
			TreeNode nodeMethodRollback = treeViewMethods.Nodes["NodeMethodRollback"];

			constructorSettings.MethodAfterCreate = nodeMethodCreate.Nodes["NodeMethodAfterCreate"].Checked;
			constructorSettings.MethodBeforeUpdateVersion = nodeMethodUpdateVersion.Nodes["NodeMethodBeforeUpdateVersion"].Checked;
			constructorSettings.MethodBeforeConfirmEdit = nodeMethodConfirmEdit.Nodes["NodeMethodBeforeConfirmEdit"].Checked;
			constructorSettings.MethodAfterConfirmEdit = nodeMethodConfirmEdit.Nodes["NodeMethodAfterConfirmEdit"].Checked;
			constructorSettings.MethodBeforeCommit = nodeMethodCommit.Nodes["NodeMethodBeforeCommit"].Checked;
			constructorSettings.MethodBeforeRollback = nodeMethodRollback.Nodes["NodeMethodBeforeRollback"].Checked;
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
			text = comboBoxTableName.Text;
			comboBoxTableName.DataSource = query;
			comboBoxTableName.DisplayMember = "Name";
			comboBoxTableName.Text = text;

			query = metadataContext.DatabaseView;
			query.Execute(MergeOption.OverwriteChanges);
			text = comboBoxViewName.Text;
			comboBoxViewName.DataSource = query;
			comboBoxViewName.DisplayMember = "Name";
			comboBoxViewName.Text = text;
			metadataContext.Connection.Close();
		}

		private void MultiversionDocumentSettingsEditor_Load(object sender, EventArgs e)
		{
			foreach (TreeNode node in treeViewMethods.Nodes)
				node.ExpandAll();
		}
	}
}