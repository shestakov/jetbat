using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using JetBat.Metadata;
using JetBat.Metadata.MultiversionDocumentListViewManager;
using JetBat.Metadata.MultiversionDocumentManager;
using JetBat.Metadata.PlainObjectListViewManager;
using JetBat.Metadata.PlainObjectManager;
using JetBat.Metadata.StoredQueryManager;
using JetBat.MetadataEditor.Editors;

namespace JetBat.MetadataEditor
{
	public partial class MainForm : Form
	{
		private BusinessObjectConstructorSettings constructorSettings;
		private MetadataContainer metadataStoreContext;
		private string currentFileName;
		private Control currentEditor;

		private string currentFileExtension;

		private void setEditor(Control editor)
		{
			panelEditor.SuspendLayout();
			panelEditor.Controls.Clear();
			panelEditor.Controls.Add(editor);
			editor.Parent = panelEditor;
			editor.Dock = DockStyle.Fill;
			currentEditor = editor;
			panelEditor.ResumeLayout();
		}

		public MainForm()
		{
			InitializeComponent();
		}

		#region Event handlers

		private void EditorForm_Load(object sender, EventArgs e)
		{
			fillEnvironmentComboBox();
		}

		private void generateMetadatastoreDatabaseScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new CreateMetadataDatabaseForm().ShowDialog();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void updateMetadataStoreToolStripMenuItem_Click(object sender, EventArgs e)
		{
			updateMetadataStore();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			loadSettings();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveSettings();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveSettingsAs();
		}

		private void persistObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			persistBusinessObject();
		}

		private void plainObjectSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newPlainObjectSettings();
		}

		private void plainObjectListViewSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newPlainObjectListViewSettings();
		}

		private void multiversionDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newMultiversionDocumentSettings();
		}

		private void storedQueryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newStoredQuerySettings();
		}

		private void multiversionDocumentListViewSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newMultiversionDocumentListViewSettings();
		}

		private void generateStoredProceduresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			generateProcedures();
		}

		#endregion

		#region Generate stored procedures

		private void generateProcedures()
		{
			if (activeEnvironmentSettings == null) return;
			try
			{
				metadataStoreContext = MetadataContextFactory.Create(activeEnvironmentSettings.ConnectionStringToMetadataStore);
				string outputPath = activeEnvironmentSettings.OutputDirectory;
				if (!Directory.Exists(outputPath))
					Directory.CreateDirectory(outputPath);
				var stringBuilder = new StringBuilder();
				string targetDatabaseName =
					new SqlConnectionStringBuilder(activeEnvironmentSettings.ConnectionStringToDatabase).InitialCatalog;
				if (currentFileExtension == ".PlainObject")
					generateProceduresPlainObject(stringBuilder, targetDatabaseName);
				else if (currentFileExtension == ".PlainObjectListView")
					generateProceduresPlainObjectListView(stringBuilder, targetDatabaseName);
				else if (currentFileExtension == ".MultiversionDocument")
					generateProceduresMultiversionDocument(stringBuilder, targetDatabaseName);
				else if (currentFileExtension == ".MultiversionDocumentListView")
					generateProceduresMultiversionDocumentListView(stringBuilder, targetDatabaseName);
				else if (currentFileExtension == ".StoredQuery")
					generateProceduresStoredQuery(stringBuilder, targetDatabaseName);
				using (var writer = new StreamWriter(Path.Combine(outputPath, Path.GetFileName(currentFileName) + ".sql")))
					writer.Write(stringBuilder);
				setStatusMessage("Script to create object's methods has been just generated");
			}
			catch (Exception ex)
			{
				var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
				MessageBox.Show(message);
			}
		}

		private void generateProceduresPlainObject(StringBuilder stringBuilder, string targetDatabaseName)
		{
			metadataStoreContext.UpdateMetadata();
			constructorSettings = ((PlainObjectSettingsEditor)currentEditor).ConstructorSettings;
			PlainObjectTemplateRenderer.RenderMethods((PlainObjectConstructorSettings)constructorSettings, stringBuilder, targetDatabaseName, activeEnvironmentSettings.ConnectionStringToMetadataStore);
		}

		private void generateProceduresPlainObjectListView(StringBuilder stringBuilder, string targetDatabaseName)
		{
			metadataStoreContext.UpdateMetadata();
			constructorSettings = ((PlainObjectListViewSettingsEditor)currentEditor).ConstructorSettings;
			PlainObjectListViewTemplateRenderer.RenderMethods((PlainObjectListViewConstructorSettings)constructorSettings, stringBuilder, targetDatabaseName, activeEnvironmentSettings.ConnectionStringToMetadataStore, metadataStoreContext);
		}

		private void generateProceduresMultiversionDocument(StringBuilder stringBuilder, string targetDatabaseName)
		{
			metadataStoreContext.UpdateMetadata();
			constructorSettings = ((MultiversionDocumentSettingsEditor)currentEditor).ConstructorSettings;
			MultiversionDocumentTemplateRenderer.RenderMethods((MultiversionDocumentConstructorSettings)constructorSettings, stringBuilder, targetDatabaseName, activeEnvironmentSettings.ConnectionStringToMetadataStore);
		}

		private void generateProceduresMultiversionDocumentListView(StringBuilder stringBuilder, string targetDatabaseName)
		{
			metadataStoreContext.UpdateMetadata();
			constructorSettings = ((MultiversionDocumentListViewSettingsEditor)currentEditor).ConstructorSettings;
			MultiversionDocumentListViewTemplateRenderer.RenderMethods((MultiversionDocumentListViewConstructorSettings)constructorSettings, stringBuilder, targetDatabaseName, activeEnvironmentSettings.ConnectionStringToMetadataStore, metadataStoreContext);
		}

		private void generateProceduresStoredQuery(StringBuilder stringBuilder, string targetDatabaseName)
		{
		}

		#endregion

		#region Persist business object

		private void persistBusinessObject()
		{
			try
			{
				if (activeEnvironmentSettings == null) return;
				metadataStoreContext = MetadataContextFactory.Create(activeEnvironmentSettings.ConnectionStringToMetadataStore);
				if (currentFileExtension == ".PlainObject")
					persistPlainObject();
				else if (currentFileExtension == ".PlainObjectListView")
					persistPlainObjectListView();
				else if (currentFileExtension == ".MultiversionDocument")
					persistMultiversionDocument();
				else if (currentFileExtension == ".MultiversionDocumentListView")
					persistMultiversionDocumentListView();
				else if (currentFileExtension == ".StoredQuery")
					persistStoredQuery();
				setStatusMessage("Object metadata has been persisted");
			}
			catch (Exception ex)
			{
				var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
				MessageBox.Show(message);
			}
		}

		private void persistPlainObject()
		{
			metadataStoreContext.UpdateMetadata();
			constructorSettings = ((PlainObjectSettingsEditor)currentEditor).ConstructorSettings;
			new PlainObjectConstructor((PlainObjectConstructorSettings)constructorSettings, metadataStoreContext).Save();
		}

		private void persistPlainObjectListView()
		{
			metadataStoreContext.UpdateMetadata();
			constructorSettings = ((PlainObjectListViewSettingsEditor)currentEditor).ConstructorSettings;
			new PlainObjectListViewConstructor((PlainObjectListViewConstructorSettings)constructorSettings, metadataStoreContext).Save();
		}

		private void persistMultiversionDocument()
		{
			metadataStoreContext.UpdateMetadata();
			constructorSettings = ((MultiversionDocumentSettingsEditor)currentEditor).ConstructorSettings;
			new MultiversionDocumentConstructor((MultiversionDocumentConstructorSettings)constructorSettings, metadataStoreContext).Save();
		}

		private void persistMultiversionDocumentListView()
		{
			metadataStoreContext.UpdateMetadata();
			constructorSettings = ((MultiversionDocumentListViewSettingsEditor)currentEditor).ConstructorSettings;
			new MultiversionDocumentListViewConstructor((MultiversionDocumentListViewConstructorSettings)constructorSettings, metadataStoreContext).Save();
		}

		private void persistStoredQuery()
		{
			metadataStoreContext.UpdateMetadata();
			constructorSettings = ((StoredQuerySettingsEditor)currentEditor).ConstructorSettings;
			new StoredQueryConstructor((StoredQueryConstructorSettings)constructorSettings, metadataStoreContext).Save();
		}

		#endregion

		#region Update metadata store

		private void updateMetadataStore()
		{
			if (activeEnvironmentSettings == null) return;
			try
			{
				metadataStoreContext = MetadataContextFactory.Create(activeEnvironmentSettings.ConnectionStringToMetadataStore);
				metadataStoreContext.UpdateMetadata();
				setStatusMessage("Metadata store has been successfully updated");
			}
			catch (Exception ex)
			{
				setStatusMessage("Error occured while trying to update metadata in the store");
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region Save settings

		private void saveSettings()
		{
			if (string.IsNullOrEmpty(currentFileName))
				saveSettingsAs();
			else
				saveSettings(currentFileName);
		}

		private void saveSettingsAs()
		{
			saveFileDialog.FileName = Path.GetFileNameWithoutExtension(currentFileName);
			switch (currentFileExtension)
			{
				case ".PlainObject":
					saveFileDialog.FilterIndex = 1;
					break;
				case ".PlainObjectListView":
					saveFileDialog.FilterIndex = 2;
					break;
				case ".MultiversionDocument":
					saveFileDialog.FilterIndex = 3;
					break;
				case ".MultiversionDocumentListView":
					saveFileDialog.FilterIndex = 4;
					break;
				case ".StoredQuery":
					saveFileDialog.FilterIndex = 5;
					break;
				default:
					saveFileDialog.FilterIndex = 6;
					break;
			}
			if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
			saveSettings(saveFileDialog.FileName);
		}

		private void saveSettings(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) return;
			try
			{
				if (currentFileExtension == ".PlainObject")
					savePlainObjectSettings(fileName);
				else if (currentFileExtension == ".PlainObjectListView")
					savePlainObjectListViewSettings(fileName);
				else if (currentFileExtension == ".MultiversionDocument")
					saveMultiversionDocumentSettings(fileName);
				else if (currentFileExtension == ".MultiversionDocumentListView")
					saveMultiversionDocumentListViewSettings(fileName);
				else if (currentFileExtension == ".StoredQuery")
					saveStoredQuerySettings(fileName);
				currentFileName = fileName;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void savePlainObjectSettings(string fileName)
		{
			constructorSettings = ((PlainObjectSettingsEditor)currentEditor).ConstructorSettings;
			PlainObjectConstructorSettings.Save((PlainObjectConstructorSettings)constructorSettings, fileName);
		}

		private void savePlainObjectListViewSettings(string fileName)
		{
			constructorSettings = ((PlainObjectListViewSettingsEditor)currentEditor).ConstructorSettings;
			PlainObjectListViewConstructorSettings.Save((PlainObjectListViewConstructorSettings)constructorSettings, fileName);
		}

		private void saveMultiversionDocumentSettings(string fileName)
		{
			constructorSettings = ((MultiversionDocumentSettingsEditor)currentEditor).ConstructorSettings;
			MultiversionDocumentConstructorSettings.Save((MultiversionDocumentConstructorSettings)constructorSettings, fileName);
		}

		private void saveMultiversionDocumentListViewSettings(string fileName)
		{
			constructorSettings = ((MultiversionDocumentListViewSettingsEditor)currentEditor).ConstructorSettings;
			MultiversionDocumentListViewConstructorSettings.Save((MultiversionDocumentListViewConstructorSettings)constructorSettings, fileName);
		}

		private void saveStoredQuerySettings(string fileName)
		{
			constructorSettings = ((StoredQuerySettingsEditor)currentEditor).ConstructorSettings;
			StoredQueryConstructorSettings.Save((StoredQueryConstructorSettings)constructorSettings, fileName);
		}

		#endregion

		#region Load settings

		private void loadSettings()
		{
			if (openFileDialog.ShowDialog() != DialogResult.OK) return;
			try
			{
				currentFileExtension = Path.GetExtension(openFileDialog.FileName);
				if (currentFileExtension == ".PlainObject")
					loadPlainObjectSettings();
				else if (currentFileExtension == ".PlainObjectListView")
					loadPlainObjectListViewSettings();
				else if (currentFileExtension == ".MultiversionDocument")
					loadMultiversionDocumentSettings();
				else if (currentFileExtension == ".MultiversionDocumentListView")
					loadMultiversionDocumentListViewSettings();
				else if (currentFileExtension == ".StoredQuery")
					loadStoredQuerySettings();
				currentFileName = openFileDialog.FileName;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void loadPlainObjectSettings()
		{
			constructorSettings = PlainObjectConstructorSettings.Load(openFileDialog.FileName);
			var editor = new PlainObjectSettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (PlainObjectConstructorSettings)constructorSettings;
		}

		private void loadPlainObjectListViewSettings()
		{
			constructorSettings = PlainObjectListViewConstructorSettings.Load(openFileDialog.FileName);
			var editor = new PlainObjectListViewSettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (PlainObjectListViewConstructorSettings)constructorSettings;
		}

		private void loadMultiversionDocumentSettings()
		{
			constructorSettings = MultiversionDocumentConstructorSettings.Load(openFileDialog.FileName);
			var editor = new MultiversionDocumentSettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (MultiversionDocumentConstructorSettings)constructorSettings;
		}

		private void loadMultiversionDocumentListViewSettings()
		{
			constructorSettings = MultiversionDocumentListViewConstructorSettings.Load(openFileDialog.FileName);
			var editor = new MultiversionDocumentListViewSettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (MultiversionDocumentListViewConstructorSettings)constructorSettings;
		}

		private void loadStoredQuerySettings()
		{
			constructorSettings = StoredQueryConstructorSettings.Load(openFileDialog.FileName);
			var editor = new StoredQuerySettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (StoredQueryConstructorSettings)constructorSettings;
		}

		#endregion

		#region New settings

		private void newPlainObjectSettings()
		{
			constructorSettings = new PlainObjectConstructorSettings();
			var editor = new PlainObjectSettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (PlainObjectConstructorSettings)constructorSettings;
			currentFileExtension = ".PlainObject";
		}

		private void newPlainObjectListViewSettings()
		{
			constructorSettings = new PlainObjectListViewConstructorSettings();
			var editor = new PlainObjectListViewSettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (PlainObjectListViewConstructorSettings)constructorSettings;
			currentFileExtension = ".PlainObjectListView";
		}

		private void newMultiversionDocumentSettings()
		{
			constructorSettings = new MultiversionDocumentConstructorSettings();
			var editor = new MultiversionDocumentSettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (MultiversionDocumentConstructorSettings)constructorSettings;
			currentFileExtension = ".MultiversionDocument";
		}

		private void newMultiversionDocumentListViewSettings()
		{
			constructorSettings = new MultiversionDocumentListViewConstructorSettings();
			var editor = new MultiversionDocumentListViewSettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (MultiversionDocumentListViewConstructorSettings)constructorSettings;
			currentFileExtension = ".MultiversionDocumentListView";
		}

		private void newStoredQuerySettings()
		{
			constructorSettings = new StoredQueryConstructorSettings();
			var editor = new StoredQuerySettingsEditor();
			setEditor(editor);
			editor.ConstructorSettings = (StoredQueryConstructorSettings)constructorSettings;
			currentFileExtension = ".StoredQuery";
		}

		#endregion

		#region Status message

		private void setStatusMessage(string message)
		{
			toolStripStatusLabel.Text = message;
		}

		#endregion

		#region Main menu event handlers

		private void toolStripButtonOpen_Click(object sender, EventArgs e)
		{
			loadSettings();
		}

		#endregion

		private void toolStripButtonSave_Click(object sender, EventArgs e)
		{
			saveSettings();
		}

		private void toolStripButtonPersist_Click(object sender, EventArgs e)
		{
			persistBusinessObject();
		}

		private void toolStripButtonRender_Click(object sender, EventArgs e)
		{
			generateProcedures();
		}

		private void toolStripButtonUpdateMetadataStore_Click(object sender, EventArgs e)
		{
			updateMetadataStore();
		}

		private void simpleProjectManagerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			startSimpleProjectManager();
		}

		private static void startSimpleProjectManager()
		{
			new SimpleProjectManagerForm().ShowDialog();
		}

		#region Environments

		private Dictionary<string, EnvironmentSettings> environmentList = new Dictionary<string, EnvironmentSettings>();
		private EnvironmentSettings activeEnvironmentSettings;
		private EnvironmentSettings currentEnvironmentSettings;

		private void fillEnvironmentComboBox()
		{
			clearEnvironmentEditor();
			var xml = File.Exists("environments.xml") ? File.ReadAllText("environments.xml") : null;
			environmentList = new Dictionary<string, EnvironmentSettings>();
			if (xml != null)
			{
				var serializer = new XmlSerializer(typeof(List<EnvironmentSettings>));
				var list = (List<EnvironmentSettings>)serializer.Deserialize(new StringReader(xml));
				foreach (var environmentSettings in list)
				{
					environmentList.Add(environmentSettings.Name, environmentSettings);
				}
			}

			comboBoxEnvironment.DataSource = new List<string>(environmentList.Keys);
			// ReSharper disable PossibleNullReferenceException
			toolStripComboBoxEnvironment.ComboBox.DataSource = new List<string>(environmentList.Keys);
			// ReSharper restore PossibleNullReferenceException
		}

		private void fillEnvironmentEditor()
		{
			textBoxName.Text = currentEnvironmentSettings.Name;
			textBoxConnectionStringToDatabase.Text = currentEnvironmentSettings.ConnectionStringToDatabase;
			textBoxConnectionStringToMetadataStore.Text = currentEnvironmentSettings.ConnectionStringToMetadataStore;
			textBoxOutputDirectory.Text = currentEnvironmentSettings.OutputDirectory;
			textBoxMetadataFileDirectory.Text = currentEnvironmentSettings.MetadataFileDirectory;
		}

		private void clearEnvironmentEditor()
		{
			textBoxName.Clear();
			textBoxConnectionStringToDatabase.Clear();
			textBoxConnectionStringToMetadataStore.Clear();
			textBoxOutputDirectory.Clear();
			textBoxMetadataFileDirectory.Clear();
		}

		private void saveCurrentEnvironment()
		{
			currentEnvironmentSettings = new EnvironmentSettings
			                             	{
			                             		Name = textBoxName.Text,
			                             		ConnectionStringToDatabase = textBoxConnectionStringToDatabase.Text,
			                             		ConnectionStringToMetadataStore = textBoxConnectionStringToMetadataStore.Text,
			                             		OutputDirectory = textBoxOutputDirectory.Text,
			                             		MetadataFileDirectory = textBoxMetadataFileDirectory.Text
			                             	};
			environmentList[currentEnvironmentSettings.Name] = currentEnvironmentSettings;
		}

		private void comboBoxEnvironment_SelectedIndexChanged(object sender, EventArgs e)
		{
			clearEnvironmentEditor();
			currentEnvironmentSettings = environmentList[(string)comboBoxEnvironment.SelectedItem];
			fillEnvironmentEditor();
		}

		private void toolStripComboBoxEnvironment_SelectedIndexChanged(object sender, EventArgs e)
		{
			activeEnvironmentSettings = environmentList[(string)toolStripComboBoxEnvironment.SelectedItem];
		}

		private void buttonSaveEnvironment_Click(object sender, EventArgs e)
		{
			saveCurrentEnvironment();
			saveEnvironmentConfiguration();
		}

		private void saveEnvironmentConfiguration()
		{
			var list = new List<EnvironmentSettings>(environmentList.Values);
			var serializer = new XmlSerializer(typeof(List<EnvironmentSettings>));
			var xml = new StringBuilder();
			serializer.Serialize(new StringWriter(xml), list);
			File.WriteAllText("environments.xml", xml.ToString());
		}

		private void buttonRestoreEnvironment_Click(object sender, EventArgs e)
		{
			fillEnvironmentEditor();
		}


		private void buttonTestEnvironment_Click(object sender, EventArgs e)
		{
			try
			{
				MetadataContainer metadataContext = MetadataContextFactory.Create(currentEnvironmentSettings.ConnectionStringToMetadataStore);
				metadataContext.Connection.Open();
				setStatusMessage("Connection test OK");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonDeleteEnvironment_Click(object sender, EventArgs e)
		{
			if (currentEnvironmentSettings == null) return;
			environmentList.Remove(currentEnvironmentSettings.Name);
			saveEnvironmentConfiguration();
			fillEnvironmentComboBox();
		}

		private void buttonReloadEnvironmentList_Click(object sender, EventArgs e)
		{
			fillEnvironmentComboBox();
		}

		private void toolStripButtonReload_Click(object sender, EventArgs e)
		{
			fillEnvironmentComboBox();
		}

		#endregion

	}
}