using System.Collections.Specialized;

namespace JetBat.Client.SqlServer.Concrete
{
	internal class IntermediateComplexAttributeDefinition
	{
		public readonly string Description;
		public readonly string FriendlyName;
		public readonly int ID;
		public readonly NameValueCollection MemberColumns = new NameValueCollection();
		public readonly string Name;
		public readonly string UILabel;

		internal IntermediateComplexAttributeDefinition(int id, string name, string friendlyName, string description,
														string uiLabel)
		{
			ID = id;
			Name = name;
			FriendlyName = friendlyName;
			Description = description;
			UILabel = uiLabel;
		}
	}
}