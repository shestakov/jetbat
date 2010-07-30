using System;
using System.Data.Objects;
using System.Windows.Forms;
using JetBat.Metadata;
using JetBat.Metadata.PlainObjectManager;

namespace JetBat.MetadataEditor.Editors
{
	public partial class PlainObjectSettingsEditor : UserControl
	{
		private PlainObjectConstructorSettings constructorSettings;

		public PlainObjectConstructorSettings ConstructorSettings
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

			text = comboBoxParentObjectNamespace.Text;
			comboBoxParentObjectNamespace.DataSource = query;
			comboBoxParentObjectNamespace.DisplayMember = "Name";
			comboBoxParentObjectNamespace.Text = text;

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

		private void getBusinessObjectSettings()
		{
			comboBoxEntityNamespace.Text = constructorSettings.EntityNamespace;
			textBoxEntityName.Text = constructorSettings.EntityName;
			textBoxUIEditorName.Text = constructorSettings.UIEditorName;
			comboBoxTableName.Text = constructorSettings.TableName;
			comboBoxViewName.Text = constructorSettings.ViewName;

			checkBoxDateTimeMark.Checked = constructorSettings.DateTimeMarkColumnName != null;
			comboBoxDateTimeMarkColumnName.Text = constructorSettings.DateTimeMarkColumnName;

			checkBoxLogicalDeletion.Checked = constructorSettings.DeleteFlagColumnName != null;
			comboBoxDeleteFlagColumn.Text = constructorSettings.DeleteFlagColumnName;

			checkBoxUseStatusColumn.Checked = constructorSettings.StatusColumnName != null;
			comboBoxStatusColumnName.Text = constructorSettings.StatusColumnName;
			numericUpDownStatusColumnInitialValue.Value = constructorSettings.StatusColumnName != null ? constructorSettings.StatusColumnInitialValue : 0;

			checkBoxObjectHasParent.Checked = constructorSettings.ParentObjectNamespace != null && constructorSettings.ParentObjectName != null;
			comboBoxParentObjectNamespace.Text = constructorSettings.ParentObjectNamespace;
			comboBoxParentObjectName.Text = constructorSettings.ParentObjectName;
			comboBoxForeignKeyToParentObject.Text = constructorSettings.ForeignKeyToParentObjectName;

			checkBoxUseSequenceNumberColumn.Checked = constructorSettings.SequenceNumberColumnName != null;
			comboBoxSequenceNumberColumnName.Text = constructorSettings.SequenceNumberColumnName;

			textBoxInvisibleColumns.Lines = constructorSettings.InvisibleColumns;
			textBoxReadOnlyColumns.Lines = constructorSettings.ReadOnlyColumns;
			textBoxIgnoredColumns.Lines = constructorSettings.IgnoredColumns;

			listBoxAdditionalMethods.Items.Clear();
			if (constructorSettings.Methods != null)
				listBoxAdditionalMethods.Items.AddRange(constructorSettings.Methods);

			TreeNode nodeMethodInsert = treeViewMethods.Nodes["NodeMethodInsert"];
			TreeNode nodeMethodUpdate = treeViewMethods.Nodes["NodeMethodUpdate"];
			TreeNode nodeMethodDelete = treeViewMethods.Nodes["NodeMethodDelete"];
			TreeNode nodeMethodRestore = treeViewMethods.Nodes["NodeMethodRestore"];

			nodeMethodInsert.Checked = constructorSettings.MethodInsert;
			nodeMethodInsert.Nodes["NodeMethodBeforeInsert"].Checked = constructorSettings.MethodBeforeInsert;
			nodeMethodInsert.Nodes["NodeMethodAfterInsert"].Checked = constructorSettings.MethodAfterInsert;
			nodeMethodUpdate.Checked = constructorSettings.MethodUpdate;
			nodeMethodUpdate.Nodes["NodeMethodBeforeUpdate"].Checked = constructorSettings.MethodBeforeUpdate;
			nodeMethodUpdate.Nodes["NodeMethodAfterUpdate"].Checked = constructorSettings.MethodAfterUpdate;
			treeViewMethods.Nodes["NodeMethodLoad"].Checked = constructorSettings.MethodLoad;
			nodeMethodDelete.Checked = constructorSettings.MethodDelete;
			nodeMethodDelete.Nodes["NodeMethodBeforeDelete"].Checked = constructorSettings.MethodBeforeDelete;
			nodeMethodDelete.Nodes["NodeMethodAfterDelete"].Checked = constructorSettings.MethodAfterDelete;
			nodeMethodRestore.Checked = constructorSettings.MethodRestore;
			nodeMethodRestore.Nodes["NodeMethodBeforeRestore"].Checked = constructorSettings.MethodBeforeRestore;
			nodeMethodRestore.Nodes["NodeMethodAfterRestore"].Checked = constructorSettings.MethodAfterRestore;
			treeViewMethods.Nodes["NodeMethodCopyByParentObject"].Checked = constructorSettings.MethodCopyByParentObject;
			treeViewMethods.Nodes["NodeMethodDeleteByParentObject"].Checked = constructorSettings.MethodDeleteByParentObject;
		}

		private void setBusinessObjectSettings()
		{
			if (constructorSettings == null)
				constructorSettings = new PlainObjectConstructorSettings();
			constructorSettings.EntityNamespace = comboBoxEntityNamespace.Text;
			constructorSettings.EntityName = textBoxEntityName.Text;
			constructorSettings.UIEditorName = textBoxUIEditorName.Text;
			constructorSettings.TableName = comboBoxTableName.Text;
			constructorSettings.ViewName = comboBoxViewName.Text;
			constructorSettings.DateTimeMarkColumnName = checkBoxDateTimeMark.Checked ? comboBoxDateTimeMarkColumnName.Text : null;
			constructorSettings.DeleteFlagColumnName = checkBoxLogicalDeletion.Checked ? comboBoxDeleteFlagColumn.Text : null;
			constructorSettings.StatusColumnName = checkBoxUseStatusColumn.Checked ? comboBoxStatusColumnName.Text : null;
			constructorSettings.StatusColumnInitialValue = checkBoxUseStatusColumn.Checked ? Convert.ToInt32(numericUpDownStatusColumnInitialValue.Value) : 0;
			constructorSettings.ParentObjectNamespace = checkBoxObjectHasParent.Checked ? comboBoxParentObjectNamespace.Text : null;
			constructorSettings.ParentObjectName = checkBoxObjectHasParent.Checked ? comboBoxParentObjectName.Text : null;
			constructorSettings.ForeignKeyToParentObjectName = checkBoxObjectHasParent.Checked ? comboBoxForeignKeyToParentObject.Text : null;
			constructorSettings.SequenceNumberColumnName = checkBoxObjectHasParent.Checked && checkBoxUseSequenceNumberColumn.Checked ? comboBoxSequenceNumberColumnName.Text : null;
			constructorSettings.InvisibleColumns = textBoxInvisibleColumns.Lines;
			constructorSettings.ReadOnlyColumns = textBoxReadOnlyColumns.Lines;
			constructorSettings.IgnoredColumns = textBoxIgnoredColumns.Lines;

			constructorSettings.Methods =
				new BusinessObjectMethodDefinition[listBoxAdditionalMethods.Items.Count];
			for (int i = 0; i < listBoxAdditionalMethods.Items.Count; ++i)
			{
				constructorSettings.Methods[i] =
					(BusinessObjectMethodDefinition)listBoxAdditionalMethods.Items[i];
			}

			TreeNode nodeMethodInsert = treeViewMethods.Nodes["NodeMethodInsert"];
			TreeNode nodeMethodUpdate = treeViewMethods.Nodes["NodeMethodUpdate"];
			TreeNode nodeMethodDelete = treeViewMethods.Nodes["NodeMethodDelete"];
			TreeNode nodeMethodRestore = treeViewMethods.Nodes["NodeMethodRestore"];

			constructorSettings.MethodInsert = nodeMethodInsert.Checked;
			constructorSettings.MethodBeforeInsert = nodeMethodInsert.Nodes["NodeMethodBeforeInsert"].Checked;
			constructorSettings.MethodAfterInsert = nodeMethodInsert.Nodes["NodeMethodAfterInsert"].Checked;
			constructorSettings.MethodUpdate = nodeMethodUpdate.Checked;
			constructorSettings.MethodBeforeUpdate = nodeMethodUpdate.Nodes["NodeMethodBeforeUpdate"].Checked;
			constructorSettings.MethodAfterUpdate = nodeMethodUpdate.Nodes["NodeMethodAfterUpdate"].Checked;
			constructorSettings.MethodLoad = treeViewMethods.Nodes["NodeMethodLoad"].Checked;
			constructorSettings.MethodDelete = nodeMethodDelete.Checked;
			constructorSettings.MethodBeforeDelete = nodeMethodDelete.Nodes["NodeMethodBeforeDelete"].Checked;
			constructorSettings.MethodAfterDelete = nodeMethodDelete.Nodes["NodeMethodAfterDelete"].Checked;
			constructorSettings.MethodRestore = nodeMethodRestore.Checked;
			constructorSettings.MethodBeforeRestore = nodeMethodRestore.Nodes["NodeMethodBeforeRestore"].Checked;
			constructorSettings.MethodAfterRestore = nodeMethodRestore.Nodes["NodeMethodAfterRestore"].Checked;
			constructorSettings.MethodCopyByParentObject = treeViewMethods.Nodes["NodeMethodCopyByParentObject"].Checked;
			constructorSettings.MethodDeleteByParentObject = treeViewMethods.Nodes["NodeMethodDeleteByParentObject"].Checked;
		}

		public PlainObjectSettingsEditor()
		{
			InitializeComponent();
		}

		private void PlainObjectSettingsEditor_Load(object sender, EventArgs e)
		{
			foreach (TreeNode node in treeViewMethods.Nodes)
				node.ExpandAll();
		}

		private void buttonAdditionalMethodAdd_Click(object sender, EventArgs e)
		{
			var parameter = new BusinessObjectMethodDefinition(textBoxAdditionalMethodName.Text, textBoxAdditionalMethodFriendlyName.Text,
			                                                   checkBoxAdditionalMethodReturnsXmlErrorList.Checked, comboBoxAdditionalMethodStordeProcedure.Text);
			listBoxAdditionalMethods.Items.Add(parameter);
		}

		private void buttonAdditionalMethodDelete_Click(object sender, EventArgs e)
		{
			if (listBoxAdditionalMethods.SelectedItem == null)
				return;
			listBoxAdditionalMethods.Items.RemoveAt(listBoxAdditionalMethods.SelectedIndex);
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (tabControl.SelectedTab != tabPageBasic)
			//{
			//    metadataContext.Connection.Close();
			//    metadataContext.Connection.Open();
			//    var parameter = new ObjectParameter("TableName", comboBoxTableName.Text);
			//    selectedTable = metadataContext.DatabaseTable.Include("Columns").Where("it.Name = @TableName", parameter). FirstOrDefault();
			//    if (selectedTable != null)
			//    {
			//        var text = comboBoxDateTimeMarkColumnName.Text;
			//        comboBoxDateTimeMarkColumnName.DataSource = selectedTable.Columns;
			//        comboBoxDateTimeMarkColumnName.DisplayMember = "Name";
			//        comboBoxDateTimeMarkColumnName.Text = text;

			//        text = comboBoxDeleteFlagColumn.Text;
			//        comboBoxDeleteFlagColumn.DataSource = selectedTable.Columns;
			//        comboBoxDeleteFlagColumn.DisplayMember = "Name";
			//        comboBoxDeleteFlagColumn.Text = text;

			//        text = comboBoxStatusColumnName.Text;
			//        comboBoxStatusColumnName.DataSource = selectedTable.Columns;
			//        comboBoxStatusColumnName.DisplayMember = "Name";
			//        comboBoxStatusColumnName.Text = text;

			//        text = comboBoxSequenceNumberColumnName.Text;
			//        comboBoxSequenceNumberColumnName.DataSource = selectedTable.Columns;
			//        comboBoxSequenceNumberColumnName.DisplayMember = "Name";
			//        comboBoxSequenceNumberColumnName.Text = text;

			//        metadataContext.Connection.Close();
			//        setStatusMessage("Column list has been loaded for the choosen table");
			//    }
			//    else
			//    {
			//        comboBoxDateTimeMarkColumnName.DataSource = null;
			//        comboBoxDeleteFlagColumn.DataSource = null;
			//    }
			//}
		}
	}
}