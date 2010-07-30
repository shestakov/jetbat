namespace JetBat.Test
{
	public partial class TestFixtureBase
	{
		private const string CreateDatabaseScriptFileName = @"..\..\..\SampleSqlScripts\Database\CreateDatabase.sql";
		private const string BusinessObjectFilesPath = @"..\..\..\SampleObjects\";
		private const string AdditionalScriptFolder = @"..\..\..\SampleSqlScripts\Additional\";
		private const string ServerName = @".\SQLEXPRESS";
		private const string ConnectionString = @"Data Source=.\SQLEXPRESS;Integrated Security = true";
		private const string DatabaseName = "TestMealCalc";
		private const string MetadataStoreName = "TestMealCalc";
		private const string DatabaseBackupDirectory = @"D:\Downloads\";
		private const string BackupFileName = @"TestMealCalc.bak";

		private const string AspNetRegSqlPath = @"C:\Windows\Microsoft.NET\Framework\v2.0.50727\aspnet_regsql.exe";
		private const string AspNetRegSqlArgumenstsAddApplicationServcesObjects = @"-S .\SQLEXPRESS -E -d TestMealCalc -A all";
		//private const string AspNetRegSqlArgumenstsRemoveApplicationServcesObjects = @"-S .\SQLEXPRESS -E -d TestMealCalc -R all -Q";
	}
}


