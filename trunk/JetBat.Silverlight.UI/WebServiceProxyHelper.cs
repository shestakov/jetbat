using System.ServiceModel;
using System.Windows.Interop;
using JetBat.Silverlight.UI.WebService;

namespace JetBat.Silverlight.UI
{
	public static class WebServiceProxyHelper
	{
		public static string HostUrl { get; private set; }

		public static void SetHostUrl(SilverlightHost host)
		{
			//if (host.Source.Host == "localhost")
			//    return;
			string localPath = host.Source.LocalPath;
			HostUrl = string.Format("{0}:{1}{2}", host.Source.Host, host.Source.Port, localPath.Substring(0, localPath.IndexOf("ClientBin") - 1));
		}

		public static ServiceSoapClient CreateProxy()
		{
			var proxy = new ServiceSoapClient("ServiceSoap");
			if (!string.IsNullOrEmpty(HostUrl))
			{
				var newAddress = string.Format("http://{0}/WebServices/Service.asmx", HostUrl);
				proxy.Endpoint.Address = new EndpointAddress(newAddress);
			}
			return proxy;
		}
	}
}