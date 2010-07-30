using System.Data.SqlClient;
using JetBat.Client.Metadata;
using JetBat.Client.SqlServer.Common;

namespace JetBat.Client.SqlServer.Concrete
{
	public class SqlAccessAdapter : Common.SqlAccessAdapter
	{
		public SqlAccessAdapter(bool enableCaching)
			: base(enableCaching)
		{
		}

		public SqlAccessAdapter()
			: base()
		{

		}

		public override void Open(string context, string metadataStoreName)
		{
			base.Open(context, metadataStoreName);

			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(context);
			sqlConnection = new SqlConnection(builder.ToString());
			builder.InitialCatalog = metadataStoreName;
			connectionStringMetadataStore = builder.ToString();
			
			metadataProvider = new SqlMetadataProvider(connectionStringMetadataStore, EnableCaching);
			metadataStore = new MetadataStore(metadataProvider);
			metadataStore.LoadMetadata();

			sqlConnection.Open();
			accessProvider = new SqlAccessProvider(metadataStore, sqlConnection);
			objectFactory = new ObjectFactory(metadataStore, accessProvider);
		}
	}
}