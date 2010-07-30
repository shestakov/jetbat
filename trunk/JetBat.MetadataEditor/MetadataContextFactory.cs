using System.Data.EntityClient;
using System.Data.SqlClient;
using JetBat.Metadata;

namespace JetBat.MetadataEditor
{
	public static class MetadataContextFactory
	{
		public static MetadataContainer Create(string sqlConnectionString)
		{
			SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(sqlConnectionString);
			EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder
			{
				ProviderConnectionString = sqlBuilder.ToString(),
				Provider = "System.Data.SqlClient",
				Metadata = @"res://*/MetadataModel.csdl|res://*/MetadataModel.ssdl|res://*/MetadataModel.msl"
			};
			return new MetadataContainer(entityBuilder.ToString());
		}
	}
}