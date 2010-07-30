using System.Collections.Specialized;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Definitions
{
	public class ObjectComplexAttributeDefinition : INamedObject
	{
		#region Атрибуты

		public string Description { get; private set; }
		public string FriendlyName { get; private set; }
		public int ID { get; private set; }
		public NameValueCollection MemberColumns { get; private set; }
		public string Name { get; private set; }
		public string UILabel { get; private set; }
		public int UIPreferredIndex { get; private set; }

		#endregion

		#region Инициализация

		public ObjectComplexAttributeDefinition(Simple.ObjectComplexAttribute complexAttribute)
		{
			ID = complexAttribute.ID;
			Name = complexAttribute.Name;
			FriendlyName = complexAttribute.FriendlyName;
			Description = complexAttribute.Description;
			UILabel = complexAttribute.UILabel;
			UIPreferredIndex = complexAttribute.UIPreferredIndex;
			NameValueCollection memberColumns = new NameValueCollection(complexAttribute.MemberColumns.Count);
			foreach (var pair in complexAttribute.MemberColumns)
				memberColumns.Add(pair.PrimaryKeyColumnName, pair.ForeignKeyColumnName);
			MemberColumns = new NameValueCollection(memberColumns);
		}

		#endregion
	}
}