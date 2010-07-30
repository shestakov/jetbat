using System;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using JetBat.Client.SqlServer.Concrete;
using JetBat.Metadata;
using JetBat.Metadata.ConstructionProject;
using NUnit.Framework;

namespace JetBat.Test
{
	public partial class TestFixtureBase
	{
		private const bool recreateDb = true;
		private string connectionStringToMetadataStore;
		private ConstructionProject constructionProject;
		private readonly SqlConnection connection = new SqlConnection(ConnectionString);
		private string initialDatabaseName;
		private MetadataContainer metadataStoreContext;

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			createMetadataStoreContext();
			connection.Open();
			initialDatabaseName = connection.Database;
			if (recreateDb)
			{
				backupDatadase(DatabaseName,
							   string.Format("{0}_{1}.bak", DatabaseName, DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss")));
				dropDatabaseIfExists(MetadataStoreName);
				dropDatabaseIfExists(DatabaseName);
				createDatabase();
				connection.ChangeDatabase(DatabaseName);
				createAspNetMembershipProviderObjects();
				MembershipTestHelper.CreateApplication(connection);
				createMetadataStore();
				createDocumentInfrastructure();
				createTablesAndViews();
				executeCustomScripts();
				createConstructionProject();
				persistMetadataProject();
				renderMetadataProject();
				provideDocumentDefinitions();
				metadataStoreContext.Connection.Close();
				//MembershipTestHelper.CreateDefaultUsersAndRoles(CreateSqlAccessAdapter(), connection);
				backupDatadase();
			}
			SqlConnection.ClearAllPools();
			Console.Write("Executing tests...");
		}

		[SetUp]
		public void SetUp()
		{
			restoreDatabase();
		}

		private void restoreDatabase()
		{
			const string sqlScript = @"use master;
					--declare @spid int;
					--declare @sql nvarchar(1000);
					--while exists(SELECT 1 FROM sys.sysprocesses as q1 WHERE db_name(q1.dbid) = '{0}' and q1.spid >= 51)
					--begin
					--	SELECT TOP 1 @spid = q1.spid FROM sys.sysprocesses as q1 WHERE db_name(q1.dbid) = '{0}' and q1.spid >= 51;
					--	SET @sql = 'kill ' + cast(@spid as nvarchar);
					--	exec sys.sp_executesql @sql;
					--end;
					ALTER DATABASE [{0}] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE;
					use master;
					RESTORE DATABASE [{0}] FROM DISK = '{1}' WITH REPLACE;
					ALTER DATABASE [{0}] SET  MULTI_USER;";
			string createDatabaseCommand = String.Format(sqlScript, DatabaseName, DatabaseBackupDirectory + BackupFileName);
			connection.ChangeDatabase(initialDatabaseName);
			using (var command = new SqlCommand(createDatabaseCommand, connection))
			{
				command.ExecuteNonQuery();
				Console.WriteLine(string.Format("Restored database {0}", DatabaseName));
			}
			connection.ChangeDatabase(DatabaseName);
		}

		private void backupDatadase()
		{
			if (File.Exists(DatabaseBackupDirectory + BackupFileName)) File.Delete(DatabaseBackupDirectory + BackupFileName);
			string commandText = String.Format("BACKUP DATABASE [{0}] TO DISK = '{1}'", DatabaseName, DatabaseBackupDirectory + BackupFileName);
			using (var command = new SqlCommand(commandText, connection))
			{
				command.ExecuteNonQuery();
				Console.WriteLine(string.Format("Backed up database {0}", DatabaseName));
			}
		}

		private void backupDatadase(string databaseName, string backupFileName)
		{
			if (File.Exists(DatabaseBackupDirectory + backupFileName)) File.Delete(DatabaseBackupDirectory + backupFileName);
			string commandText = String.Format("IF EXISTS (SELECT 0 FROM sys.databases WHERE name = '{0}') BACKUP DATABASE [{0}] TO DISK = '{1}'", databaseName, DatabaseBackupDirectory + backupFileName);
			using (var command = new SqlCommand(commandText, connection))
			{
				command.ExecuteNonQuery();
				Console.WriteLine(string.Format("Backed up database {0}", databaseName));
			}
		}

		[TestFixtureTearDown]
		public void FixtureTearDown()
		{
			Console.WriteLine(" Done");
			connection.ChangeDatabase(initialDatabaseName);
			connection.Close();
		}

		private void executeCustomScripts()
		{
			Console.Write(string.Format("Creating custom stored procedures..."));
			var files = Directory.GetFiles(AdditionalScriptFolder, "*.sql", SearchOption.AllDirectories);
			foreach (var fileName in files)
				splitAndExecuteBatches(File.ReadAllText(fileName, Encoding.Default));
			Console.WriteLine(" Done");
		}

		private void persistMetadataProject()
		{
			Console.Write("Persisting business objects...");
			string log = constructionProject.Persist(metadataStoreContext);
			Console.WriteLine(log);
			Console.WriteLine(" Done");
		}

		private void renderMetadataProject()
		{
			Console.Write("Rendering methods...");
			var builder = new StringBuilder();
			string log = constructionProject.RenderMethods(metadataStoreContext, builder);
			Console.WriteLine(log);
			Console.WriteLine(" Done");
			Console.Write("Execute script...");
			splitAndExecuteBatches(builder.ToString());
			Console.WriteLine(" Done");
		}

		private void provideDocumentDefinitions()
		{
			Console.Write("Providing Multiversion Document Definitions...");
			metadataStoreContext.ProvideMultiversionDocumentDefinitions();
			Console.WriteLine(" Done");
		}


		private static void createAspNetMembershipProviderObjects()
		{
			Console.Write(string.Format("Creating ASP.NET Membership Provider database objects..."));
			var processStartInfo = new ProcessStartInfo(AspNetRegSqlPath, AspNetRegSqlArgumenstsAddApplicationServcesObjects)
				{
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardOutput = true,
					StandardOutputEncoding = Encoding.GetEncoding(1251)
				};
			Process process = Process.Start(processStartInfo);
			if (process == null) throw new Exception(string.Format("Failed to start {0}", AspNetRegSqlPath));
			while (!process.StandardOutput.EndOfStream)
				Console.WriteLine(process.StandardOutput.ReadLine());
			process.WaitForExit();
			if (process.ExitCode != 0)
				throw new Exception(string.Format("{0} failed to create ASP.NET Applications Services data structure.", AspNetRegSqlPath));
			Console.WriteLine(" Done");
		}

		private void createMetadataStore()
		{
			Console.Write(string.Format("Creating metadata store {0}...", MetadataStoreName));
			string script = new ScriptMaster("SqlScripts").CreateMetadataDatabaseObjects(MetadataStoreName, DatabaseName);
			splitAndExecuteBatches(script);
			Console.WriteLine(" Done");
		}

		private void createDocumentInfrastructure()
		{
			Console.Write(string.Format("Creating document infrastructure..."));
			string script = new ScriptMaster("SqlScripts").GenerateMultiversionDocumentInfrastructrureScript(DatabaseName);
			splitAndExecuteBatches(script);
			Console.WriteLine(" Done");
		}

		private void createTablesAndViews()
		{
			Console.Write(string.Format("Creating data structure..."));
			string script = File.ReadAllText(CreateDatabaseScriptFileName, Encoding.Default);
			splitAndExecuteBatches(script);
			Console.WriteLine(" Done");
		}

		private void splitAndExecuteBatches(string script)
		{
			string[] batches = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var batch in batches)
			{
				using (var command = new SqlCommand(batch, connection))
				{
					//Console.WriteLine(string.Format("Executing batch {0}", batch));
					try
					{
						command.ExecuteNonQuery();
					}
					catch (Exception)
					{
						Console.WriteLine(string.Format("Error executing batch {0}", batch));
						throw;
					}
				}
			}
		}

		private void dropDatabaseIfExists(string databaseNameToDrop)
		{
			const string dropDatabaseCommandText = @"
IF EXISTS (SELECT 0 FROM sys.databases WHERE name = '{0}')
BEGIN
	ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [{0}]
END";
			string dropDatabaseCommand = String.Format(dropDatabaseCommandText, databaseNameToDrop);
			using (var command = new SqlCommand(dropDatabaseCommand, connection))
			{
				command.ExecuteNonQuery();
				Console.WriteLine(string.Format("Dropped database {0}", databaseNameToDrop));
			}
		}

		private void createDatabase()
		{
			string createDatabaseCommand = String.Format("CREATE DATABASE [{0}]", DatabaseName);
			using (var command = new SqlCommand(createDatabaseCommand, connection))
			{
				command.ExecuteNonQuery();
				Console.WriteLine(string.Format("Created database {0}", DatabaseName));
			}
		}

		private void createConstructionProject()
		{
			constructionProject = new ConstructionProject()
				{
					ConnectionStringToMetadataStore = connectionStringToMetadataStore,
					TargetDatabaseName = DatabaseName
				};
			var files = Directory.GetFiles(BusinessObjectFilesPath, "*.*", SearchOption.AllDirectories);
			foreach (var fileName in files) constructionProject.AddItem(fileName);
		}

		public static SqlAccessAdapter CreateSqlAccessAdapter()
		{
			var adapter = new SqlAccessAdapter(false);
			var builder = new SqlConnectionStringBuilder
				{
					DataSource = ServerName,
					InitialCatalog = DatabaseName,
					IntegratedSecurity = true,
					Pooling = false
				};
			adapter.Open(builder.ToString(), MetadataStoreName);
			return adapter;
		}

		private void createMetadataStoreContext()
		{
			var sqlBuilder = new SqlConnectionStringBuilder(ConnectionString)
				{
					InitialCatalog = MetadataStoreName,
					Pooling = false
				};
			connectionStringToMetadataStore = sqlBuilder.ToString();
			var entityBuilder = new EntityConnectionStringBuilder
				{
					ProviderConnectionString = connectionStringToMetadataStore,
					Provider = "System.Data.SqlClient",
					Metadata = @"res://*/MetadataModel.csdl|res://*/MetadataModel.ssdl|res://*/MetadataModel.msl"
				};
			metadataStoreContext = new MetadataContainer(entityBuilder.ToString());
		}
	}
}