using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Metadata.Misc
{
	public class NamedObjectReadOnlyCollection<T> : ReadOnlyCollection<T> where T : INamedObject
	{
		public NamedObjectReadOnlyCollection(IList<T> list)
			: base(list)
		{
		}

		public bool Contains(string name)
		{
			foreach (T descriptor in this)
				if (descriptor.Name == name)
					return true;
			return false;
		}

		public T this[string name]
		{
			get
			{
				foreach (T descriptor in this)
					if (descriptor.Name == name)
						return descriptor;
				return default(T);
			}
		}
	}
}