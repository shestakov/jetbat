using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Definitions
{
	public class ObjectActionDefinition : INamedObject
	{
		public string Description { get; private set; }
		public bool Enabled { get; private set; }
		public string FriendlyName { get; private set; }
		public string Name { get; private set; }
		public string UIBriefText { get; private set; }
		public string UIFullText { get; private set; }

		#region Инициализация

		public ObjectActionDefinition(Simple.ObjectAction action)
		{
			Name = action.Name;
			FriendlyName = action.FriendlyName;
			Description = action.Description;
			Enabled = action.Enabled;
			UIFullText = action.UIFullText;
			UIBriefText = action.UIBriefText;
		}

		#endregion

		#region Прочее

		public override string ToString()
		{
			return FriendlyName;
		}

		#endregion
	}
}