using System;
using System.Data.Objects;
using System.Windows.Forms;
using JetBat.Metadata;
using JetBat.Metadata.PlainObjectListViewManager;

namespace JetBat.MetadataEditor.Editors
{
	public partial class PlainObjectListViewSettingsEditor : UserControl
	{
		private PlainObjectListViewConstructorSettings constructorSettings;

		public PlainObjectListViewSettingsEditor()
		{
			InitializeComponent();
		}

		public PlainObjectListViewConstructorSettings ConstructorSettings
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
				constructorSettings = new PlainObjectListViewConstructorSettings();
			constructorSettings.EntityNamespace = comboBoxEntityNamespace.Text;
			constructorSettings.EntityName = textBoxEntityName.Text;
			constructorSettings.UICaption = textBoxUICaption.Text;
			constructorSettings.TargetObjectName = comboBoxTargetObjectName.Text;
			constructorSettings.TargetObjectNamespace = comboBoxTargetObjectNamespace.Text;
			constructorSettings.ViewName = comboBoxViewName.Text;
			constructorSettings.ShowDeletedObjects = checkBoxShowDeletedObjects.Checked;
			constructorSettings.InvisibleColumns = textBoxInvisibleColumns.Lines;
			constructorSettings.IgnoredColumns = textBoxIgnoredColumns.Lines;
			constructorSettings.SelectionCondition = textBoxSelectionCondition.Text;
			constructorSettings.ParameterDefinitions =
				new StoredProcedureParameterDefinition[listBoxSelectionParameters.Items.Count];
			for (int i = 0; i < listBoxSelectionParameters.Items.Count; ++i)
			{
				constructorSettings.ParameterDefinitions[i] =
					(StoredProcedureParameterDefinition)listBoxSelectionParameters.Items[i];
			}
		}

		private void getBusinessObjectSettings()
		{
			comboBoxEntityNamespace.Text = constructorSettings.EntityNamespace;
			textBoxEntityName.Text = constructorSettings.EntityName;
			textBoxUICaption.Text = constructorSettings.UICaption;
			comboBoxTargetObjectName.Text = constructorSettings.TargetObjectName;
			comboBoxViewName.Text = constructorSettings.ViewName;
			comboBoxTargetObjectName.Text = constructorSettings.TargetObjectName;
			comboBoxTargetObjectNamespace.Text = constructorSettings.TargetObjectNamespace;
			checkBoxShowDeletedObjects.Checked = constructorSettings.ShowDeletedObjects;
			textBoxSelectionCondition.Text = constructorSettings.SelectionCondition;
			listBoxSelectionParameters.Items.Clear();
			if (constructorSettings.ParameterDefinitions != null)
				listBoxSelectionParameters.Items.AddRange(constructorSettings.ParameterDefinitions);
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

		private void buttonParameterAdd_Click(object sender, EventArgs e)
		{
			var parameter = new StoredProcedureParameterDefinition(textBoxParameterName.Text, comboBoxParameterDataType.Text,
			                                                       false, Decimal.ToInt32(numericUpDownParameterMaxLength.Value),
			                                                       Decimal.ToInt32(numericUpDownParameterPrecision.Value),
			                                                       Decimal.ToInt32(numericUpDownParameterScale.Value), "");
			listBoxSelectionParameters.Items.Add(parameter);
		}

		private void buttonParameterDelete_Click(object sender, EventArgs e)
		{
			if (listBoxSelectionParameters.SelectedItem == null)
				return;
			listBoxSelectionParameters.Items.RemoveAt(listBoxSelectionParameters.SelectedIndex);
		}

		private void buttonParameterUpdate_Click(object sender, EventArgs e)
		{

		}
	}
}