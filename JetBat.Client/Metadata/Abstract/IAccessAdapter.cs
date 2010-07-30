using System;
using System.ComponentModel;

namespace JetBat.Client.Metadata.Abstract
{
	public interface IAccessAdapter : IDisposable
	{
		[Browsable(false)]
		IObjectFactory ObjectFactory { get; }

		[Browsable(false)]
		MetadataStore MetadataStore { get; }

		[Browsable(false)]
		IAccessProvider AccessProvider { get; }

		void Open(string context, string metadataStoreName);
		void Close();
	}
}