using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;
using JetBat.Silverlight.UI.WebService;

namespace JetBat.Silverlight.UI
{
	public class ListViewLoader
	{
		public delegate void OnLoadCompleteDelegate(Exception exception, List<DataGridRow> rows);
		public event OnLoadCompleteDelegate OnLoadComplete;

		public bool IsBusy { get; private set; }
		
		//private readonly Dictionary<int, List<DataGridRow>> cache = new Dictionary<int, List<DataGridRow>>();
		//public bool UseCache { get; set; }

		public void Load(string objectNamespace, string objectName, NamedObjectCollection<NameValue> parameters)
		{
			IsBusy = true;
			XmlSerializer serializer = new XmlSerializer(typeof(NamedObjectCollection<NameValue>));
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
				serializer.Serialize(xmlWriter, parameters);
			string serializedParameters = stringBuilder.ToString();

			//if(UseCache)
			//    lock(cache)
			//    {
			//        string key = string.Format("{0}.{1}({2})", objectNamespace, objectName, serializedParameters);
			//        if(cache.ContainsKey(key.GetHashCode()) && OnLoadComplete != null)
			//        {
			//            OnLoadComplete(null, cache[key.GetHashCode()]);
			//            return;
			//        }
			//    }

			currentObjectNamespace = objectNamespace;
			currentObjectName = objectName;
			//currentSerializedParameters = serializedParameters;
			currentParameters = parameters;

			proxy = WebServiceProxyHelper.CreateProxy();
			proxy.ExecuteMethodCompleted += proxyOnExecuteMethodCompleted;
			proxy.ExecuteMethodAsync(objectNamespace, objectName, "LoadList", serializedParameters);
		}

		private void proxyOnExecuteMethodCompleted(object sender, ExecuteMethodCompletedEventArgs e)
		{
			proxy.ExecuteMethodCompleted -= proxyOnExecuteMethodCompleted;

			exception = e.Error;
			List<DataGridRow> rows = new List<DataGridRow>();

			if(exception != null)
			{
				currentObjectNamespace = null;
				currentObjectName = null;
				//currentSerializedParameters = null;
				currentParameters = null;
				if(OnLoadComplete != null)
					OnLoadComplete(exception, rows);
				return;
			}

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObjectMethodResult));
			ObjectMethodResult objectMethodResult;
			using(StringReader reader = new StringReader(e.Result))
			{
				objectMethodResult = (ObjectMethodResult)xmlSerializer.Deserialize(reader);
			}
			if(objectMethodResult.Recordset == null)
				objectMethodResult.Recordset = new List<List<NameValue>>(0);

			if(objectMethodResult.AuthenticationRequired)
			{
				StaticAuthenticator.OnAuthenticationFinished += StaticAuthentificatorOnOnAuthenticationFinished;
				StaticAuthenticator.RequireAuthentication();
				return;
			}

			currentObjectNamespace = null;
			currentObjectName = null;
			//currentSerializedParameters = null;
			currentParameters = null;

			foreach(List<NameValue> rowNamesAndValues in objectMethodResult.Recordset)
			{
				DataGridRow dataGridRow = new DataGridRow();
				foreach(NameValue nameValue in rowNamesAndValues)
				{
					dataGridRow[nameValue.Name] = nameValue.Value is DBNull ? null : nameValue.Value;
				}
				rows.Add(dataGridRow);
			}

			//if(UseCache)
			//    lock(cache)
			//    {
			//        string key = string.Format("{0}.{1}({2})", objectNamespace, objectName, serializedParameters);
			//        cache[key.GetHashCode()] = rows;
			//    }

			if(OnLoadComplete != null)
				OnLoadComplete(exception, rows);
			IsBusy = false;
		}

		private void StaticAuthentificatorOnOnAuthenticationFinished(bool succeeded)
		{
			StaticAuthenticator.OnAuthenticationFinished -= StaticAuthentificatorOnOnAuthenticationFinished;
			if(!succeeded && OnLoadComplete != null)
			{
				OnLoadComplete(new SecurityException("Сервис требует аутенификации"), new List<DataGridRow>());
				IsBusy = false;
				return;
			}
			currentObjectNamespace = null;
			currentObjectName = null;
			//currentSerializedParameters = null;
			currentParameters = null;
			IsBusy = false;
			Load(currentObjectNamespace, currentObjectName, currentParameters);
		}

		private string currentObjectNamespace;
		private string currentObjectName;
		//private string currentSerializedParameters;
		private Exception exception;
		private ServiceSoapClient proxy;
		private NamedObjectCollection<NameValue> currentParameters;
	}
}