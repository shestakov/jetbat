using System.ComponentModel;
using System.Data.SqlClient;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;

namespace JetBat.Client.SqlServer.Common
{
	public class SqlAccessAdapter : Component, IAccessAdapter
	{
		protected readonly bool EnableCaching;
		protected SqlConnection sqlConnection;
		protected IAccessProvider accessProvider;
		private Container components;
		protected IMetadataProvider metadataProvider;
		protected MetadataStore metadataStore;
		protected ObjectFactory objectFactory;
		protected string connectionStringMetadataStore;

		#region Инициализация и освобождение ресурсов

		public SqlAccessAdapter(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public SqlAccessAdapter()
		{
			InitializeComponent();
		}

		public SqlAccessAdapter(bool enableCaching)
		{
			EnableCaching = enableCaching;
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Close();
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		#endregion

		#region Открытые методы

		public virtual void Open(string context, string metadataStoreName)
		{
			//metadataProvider = new SqlMetadataProvider(connectionStringMetadataStore, EnableCaching);
			//metadataStore = new MetadataStore(metadataProvider);
			//accessProvider = new SqlAccessProvider(metadataStore, sqlConnection);
			//objectFactory = new ObjectFactory(metadataStore, accessProvider);
		}

		public void Close()
		{
			accessProvider.Close();
		}

		#endregion

		#region Свойства

		[Browsable(false)]
		public IObjectFactory ObjectFactory
		{
			get { return objectFactory; }
		}

		[Browsable(false)]
		public MetadataStore MetadataStore
		{
			get { return metadataStore; }
		}

		[Browsable(false)]
		public IAccessProvider AccessProvider
		{
			get { return accessProvider; }
		}

		#endregion
	}
}