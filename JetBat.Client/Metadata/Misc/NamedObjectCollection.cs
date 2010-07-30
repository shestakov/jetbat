using System.Collections.ObjectModel;

namespace JetBat.Client.Metadata.Misc
{
	public class NamedObjectCollection<T> : KeyedCollection<string, T> where T : INamedObject
	{
		protected override string GetKeyForItem(T item)
		{
			return item.Name;
		}
	}
}