using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using JetBat.Metadata;
using JetBat.Metadata.ConstructionProject;

namespace JetBat.MetadataEditor
{
	public partial class SimpleProjectManagerForm : Form
	{
		readonly ConstructionProject project = new ConstructionProject();
		private MetadataContainer metadataStoreContext;
		//private string targetDatabaseName;
		//private string connectionStringToMetadataStore;
		private const string MetadataStoreContextConnectionStringName = "AdvancedMealCalc_Metadata";
		private const string LogFileNamePersistense = @"D:\Downloads\LogPersistense.txt";
		private const string LogFileNameRendering = @"D:\Downloads\LogRendering.txt";
		private const string SqlOutputFileName = @"D:\Downloads\Procedures.sql";
		private const string ObjectSettingsPath = @"w:\Projects\MealCalc2\BusinessObjects";

		private void getSettings()
		{
			metadataStoreContext = MetadataContextFactory.Create(MetadataStoreContextConnectionStringName);
		}

		public SimpleProjectManagerForm()
		{
			InitializeComponent();
		}

		private void buttonCreate_Click(object sender, EventArgs e)
		{
			getSettings();
			//project.ConnectionStringToMetadataStore = connectionStringToMetadataStore;
			//project.TargetDatabaseName = targetDatabaseName;

			createProject();
		}

		private void createProject()
		{
			try
			{
				var files = Directory.GetFiles(ObjectSettingsPath, "*.*", SearchOption.AllDirectories);
				foreach (var fileName in files) project.AddItem(fileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void persistProject()
		{
			try
			{
				var result = project.Persist(metadataStoreContext);
				File.WriteAllText(LogFileNamePersistense, result);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void renderProject()
		{
			try
			{
				var output = new StringBuilder();
				var result = project.RenderMethods(metadataStoreContext, output);
				File.WriteAllText(LogFileNameRendering, result);
				File.WriteAllText(SqlOutputFileName, output.ToString());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void buttonPersist_Click(object sender, EventArgs e)
		{
			persistProject();
		}

		private void buttonRender_Click(object sender, EventArgs e)
		{
			renderProject();
		}
	}
}