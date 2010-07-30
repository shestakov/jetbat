using System.Collections;

namespace JetBat.Client.Metadata.Misc
{
	/// <summary>
	/// Набор значений атрибутов
	/// </summary>
	public class AttributeValueSet : Hashtable
	{
		public void Add(AttributeValueSet avs)
		{
			foreach (object key in avs.Keys)
			{
				base[key] = avs[key];
			}
		}
	}
}