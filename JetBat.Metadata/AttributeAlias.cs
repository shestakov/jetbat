using System;

namespace JetBat.Metadata
{
	[Serializable]
	public class AttributeAlias
	{
		public AttributeAlias()
		{
			IsUserVisible = true;
		}

		public string Name { get; set; }
		public string FriendlyName { get; set; }
		public string UILabel { get; set; }
		public bool IsUserVisible { get; set; }
		public int UIPreferredWidth { get; set; }
		public int UIPreferredIndex { get; set; }
		public bool UIAllowsMultilineText { get; set; }
	}
}