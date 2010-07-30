using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Entities
{
	public sealed class StoredQueryDefinition : ObjectListViewDefinition
	{
		#region Initialization

		public StoredQueryDefinition (StoredQuery storedQuery)
			: base(storedQuery)
		{
		}

		#endregion

		public override bool AddUnexpectedAttributes
		{
			get { return true; }
		}
	}
}