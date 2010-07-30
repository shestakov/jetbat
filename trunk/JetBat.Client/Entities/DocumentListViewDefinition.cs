using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Entities
{
	public sealed class DocumentListViewDefinition : ObjectListViewDefinition
	{
		#region Initialization

		public DocumentListViewDefinition(DocumentListView documentListView)
			: base(documentListView)
		{
		}

		#endregion
	}
}