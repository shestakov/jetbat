using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace JetBat.Metadata
{
	public static class Extensions
	{
		public static void ProvideMultiversionDocumentDefinitions(this MetadataContainer metadataContext)
		{
			if (metadataContext.Connection.State != ConnectionState.Open) metadataContext.Connection.Open();
			var connection = (SqlConnection)((EntityConnection)metadataContext.Connection).StoreConnection;
			var command = new SqlCommand("ProvideMultiversionDocumentDefinitions", connection) { CommandType = CommandType.StoredProcedure };
			command.ExecuteNonQuery();
		}

		public static void UpdateMetadata(this MetadataContainer metadataContext)
		{
			if (metadataContext.Connection.State != ConnectionState.Open) metadataContext.Connection.Open();
			var connection = (SqlConnection)((EntityConnection)metadataContext.Connection).StoreConnection;
			var command = new SqlCommand("MetadataEngine_UpdateMetadata", connection) { CommandType = CommandType.StoredProcedure };
			command.ExecuteNonQuery();
		}
	}
}