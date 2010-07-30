using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Entities
{
	public sealed class PlainObjectListViewDefinition : ObjectListViewDefinition
	{
		#region Initialization

		public PlainObjectListViewDefinition(PlainObjectListView plainObjectListView)
			: base(plainObjectListView)
		{
		}

		#endregion
	}
}