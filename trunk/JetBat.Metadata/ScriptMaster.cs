using System.Diagnostics;
using System.IO;
using System.Text;

namespace JetBat.Metadata
{
	public class ScriptMaster
	{
		private readonly string sqlScriptDirectory;

		public ScriptMaster(string sqlScriptDirectory)
		{
			this.sqlScriptDirectory = sqlScriptDirectory;
		}

		public string CreateMetadataDatabase(string metadataBaseName, string targetDatabaseName, string dataFileName, string logFileName)
		{
			return CreateMetadataDatabase(metadataBaseName, targetDatabaseName, true, dataFileName, logFileName);
		}

		public string CreateMetadataDatabase(string metadataBaseName, string targetDatabaseName)
		{
			return CreateMetadataDatabase(metadataBaseName, targetDatabaseName, false, null, null);
		}

		private string CreateMetadataDatabase(string metadataBaseName, string targetDatabaseName, bool specifyFiles, string dataFileName, string logFileName)
		{
			var builder = new StringBuilder();
			if (specifyFiles)
				builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateMetadataStoreDatabaseSpecifyFiles.sql")), metadataBaseName, targetDatabaseName, dataFileName, logFileName));
			else
				builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateMetadataStoreDatabase.sql")), metadataBaseName, targetDatabaseName));
			builder.AppendLine("GO");
			builder.AppendLine(CreateMetadataDatabaseObjects(metadataBaseName, targetDatabaseName));
			return builder.ToString();
		}

		public string CreateMetadataDatabaseObjects(string metadataBaseName, string targetDatabaseName)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateMetadataStoreObjects.sql")), metadataBaseName, targetDatabaseName));
			builder.AppendLine("GO");
			builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateMetadataUpdateProcedure.sql")), metadataBaseName, targetDatabaseName));
			builder.AppendLine("GO");
			builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateProvideMultiversionDocumentDefinitionsProcedure.sql")), metadataBaseName, targetDatabaseName));
			builder.AppendLine("GO");
			builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateDataTypes.sql")), metadataBaseName, targetDatabaseName));
			builder.AppendLine("GO");
			builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateDateTimeFormats.sql")), metadataBaseName, targetDatabaseName));
			builder.AppendLine("GO");
			builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateObjectTypes.sql")), metadataBaseName, targetDatabaseName));
			builder.AppendLine("GO");
			return builder.ToString();
		}

		public void CreateMetadataDatabase(string scriptFileName, string metadataBaseName, string targetDatabaseName, bool openInEditor)
		{
			string path = Path.GetDirectoryName(scriptFileName);
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			using (TextWriter writer = new StreamWriter(scriptFileName))
			{
				writer.Write(CreateMetadataDatabase(metadataBaseName, targetDatabaseName));
			}
			if (openInEditor) Process.Start(scriptFileName);
		}

		public void GenerateMultiversionDocumentInfrastructrureScript(string scriptFileName, string targetDatabaseName, bool openInEditor)
		{
			using (TextWriter writer = new StreamWriter(scriptFileName))
			{
				writer.Write(GenerateMultiversionDocumentInfrastructrureScript(targetDatabaseName));
			}
			if (openInEditor) Process.Start(scriptFileName);
		}

		public string GenerateMultiversionDocumentInfrastructrureScript(string targetDatabaseName)
		{
			var builder = new StringBuilder();
			builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateDocumentInfrastructure.sql")), targetDatabaseName));
			builder.AppendLine("GO");
			builder.AppendLine(string.Format(File.ReadAllText(Path.Combine(sqlScriptDirectory, "CreateDocumentStatuses.sql")), targetDatabaseName));
			builder.AppendLine("GO");
			return builder.ToString();
		}
	}
}