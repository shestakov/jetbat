using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;
using JetBat.Silverlight.UI.WebService;
using NameValue = JetBat.Client.Metadata.Misc.NameValue;

namespace JetBat.Silverlight.UI
{
	public class DocumentInstance
	{
		public NameValue VersionID = new NameValue{Name = "VersionID",Value = null};
		public NameValue CurrentVersionID = new NameValue { Name = "CurrentVersionID", Value = null };
		public NameValue DocumentID = new NameValue { Name = "DocumentID", Value = null };

		#region Load

		public void LoadAsync()
		{
			NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
			parameters.Add(DocumentID);
			executeMethodAsync("Load", parameters);
		}

		public void LoadAsync(NamedObjectCollection<NameValue> parameters)
		{
			if (parameters != null)
			{
				executeMethodAsync("Load", parameters);
			}
			else
			{
				LoadAsync();
			}
			
		}

		public void StartEdit(NamedObjectCollection<NameValue> primaryKey)
		{
			DocumentID = primaryKey["DocumentID"];
			primaryKey.Add(VersionID);
			executeMethodAsync("StartEdit", primaryKey);
		}

		#endregion

		#region Method Execution

		private void executeMethodAsync(string methodName, KeyedCollection<string, NameValue> actualParameters)
		{
			if (document == null) throw new NullReferenceException("Document Metadata not set");
			proxy = WebServiceProxyHelper.CreateProxy();
			NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
			foreach (ObjectMethodParameter parameter in document.Methods[methodName].ParameterDefinitions)
			{
				string parameterName = parameter.Name.Substring(1);
				NameValue actualParameter = new NameValue { Name = parameterName };
				if (actualParameters.Contains(parameterName))
					actualParameter.Value = actualParameters[parameterName].Value;
				else if (Attributes.Contains(parameterName))
					actualParameter.Value = Attributes[parameterName].Value;
				else
					actualParameter.Value = null;
				parameters.Add(actualParameter);
			}
			XmlSerializer serializer = new XmlSerializer(typeof(NamedObjectCollection<NameValue>));
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
				serializer.Serialize(xmlWriter, parameters);
			proxy.ExecuteMethodCompleted += ProxyOnExecuteMethodCompleted;
			proxy.ExecuteMethodAsync(document.ObjectNamespace, document.ObjectName, methodName, stringBuilder.ToString(), methodName);
		}

		private void ProxyOnExecuteMethodCompleted(object sender, ExecuteMethodCompletedEventArgs e)
		{
			proxy.ExecuteMethodCompleted -= ProxyOnExecuteMethodCompleted;
			if (e.Error != null)
				throw e.Error;

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObjectMethodResult));
			ObjectMethodResult objectMethodResult;
			using (StringReader reader = new StringReader(e.Result))
				objectMethodResult = (ObjectMethodResult) xmlSerializer.Deserialize(reader);

			if(objectMethodResult.AuthenticationRequired)
				throw new SecurityException("Сервис требует авторизации");
			
			if (objectMethodResult.ErrorMessages != null && objectMethodResult.ErrorMessages.Count > 0)
			{
				MessageBox.Show(objectMethodResult.ErrorMessages[0].Text);
			}

			string methodName = (string)e.UserState;
			foreach (ObjectMethodParameter methodParameter in document.Methods[methodName].ParameterDefinitions)
			{
				if (objectMethodResult.Parameters.Contains(methodParameter.AttributeName))
					Attributes[methodParameter.AttributeName].Value = objectMethodResult.Parameters[methodParameter.AttributeName].Value;
				if(objectMethodResult.Parameters.Contains(DocumentID.Name))
				{
					DocumentID.Value = objectMethodResult.Parameters[DocumentID.Name].Value;
				}
				if (methodName == "StartEdit")
				{
					VersionID.Value = objectMethodResult.Parameters[VersionID.Name].Value;
				}
				else if (methodName == "Load")
				{
					CurrentVersionID.Value = objectMethodResult.Parameters[CurrentVersionID.Name].Value;
				}
			}

			if (ExecuteMethodComplete != null)
			{
				ExecuteMethodCompleteEventArgs executeMethodCompleteEventArgs = new ExecuteMethodCompleteEventArgs
				{
					MethodName = methodName,
					Exception = objectMethodResult.Exception,
					ErrorMessages = objectMethodResult.ErrorMessages
				};
				ExecuteMethodComplete(sender, executeMethodCompleteEventArgs);
			}
		}

		#endregion

		#region Constructor

		public DocumentInstance(Document document)
		{
			this.document = document;
			Attributes = new NamedObjectCollection<NameValue>();
			foreach (ObjectAttribute attribute in document.Attributes)
			{
				Attributes.Add(new NameValue { Name = attribute.Name, Value = null });
			}
		}

		#endregion

		public NamedObjectCollection<NameValue> GetComplexAttributeValue(string complexAttributeName)
		{
			NamedObjectCollection<NameValue> result = new NamedObjectCollection<NameValue>();
			var complexAttribute = document.ComplexAttributes[complexAttributeName];
			foreach (ComplexAttributeColumnPair columnPair in complexAttribute.MemberColumns)
			{
				result.Add(new NameValue { Name = columnPair.PrimaryKeyColumnName, Value = Attributes[columnPair.ForeignKeyColumnName].Value });
			}
			return result;
		}

		private readonly Document document;
		private ServiceSoapClient proxy;
		public NamedObjectCollection<NameValue> Attributes { get; private set; }

		
		public event EventHandler<ExecuteMethodCompleteEventArgs> ExecuteMethodComplete;

		public void ExecuteAsync(string methodName, NamedObjectCollection<NameValue> actualParameters)
		{
			executeMethodAsync(methodName, actualParameters);
		}

		public void Delete(NamedObjectCollection<NameValue> primaryKey)
		{
			executeMethodAsync("Delete", primaryKey);
		}

		public void SetComplexAttribute(string complexAttributeName, NamedObjectCollection<NameValue> primaryKeyValues)
		{
			var complexAttribute = document.ComplexAttributes[complexAttributeName];
			if (primaryKeyValues != null)
			{
				foreach (ComplexAttributeColumnPair columnPair in complexAttribute.MemberColumns)
				{
					Attributes[columnPair.ForeignKeyColumnName].Value = primaryKeyValues[columnPair.PrimaryKeyColumnName].Value;
				}
			}
			else
			{
				foreach (ComplexAttributeColumnPair columnPair in complexAttribute.MemberColumns)
				{
					Attributes[columnPair.ForeignKeyColumnName].Value = null;
				}
			}
		}


		public void UpdateVersion()
		{
			var parameters = new NamedObjectCollection<NameValue> {VersionID};
			executeMethodAsync("UpdateVersion", parameters);
		}

		public void ConfirmEdit()
		{
			var parameters = new NamedObjectCollection<NameValue> {VersionID};
			executeMethodAsync("ConfirmEdit", parameters);
		}

		public void Commit()
		{
			var parameters = new NamedObjectCollection<NameValue> {DocumentID};
			executeMethodAsync("Commit", parameters);
		}

		public void Delete()
		{
			var parameters = new NamedObjectCollection<NameValue> {DocumentID};
			executeMethodAsync("Delete", parameters);
		}

		public void Rollback()
		{
			var parameters = new NamedObjectCollection<NameValue> {DocumentID};
			executeMethodAsync("Rollback", parameters);
		}

		public void CancelEdit()
		{
			var parameters = new NamedObjectCollection<NameValue> {VersionID};
			executeMethodAsync("CancelEdit", parameters);
		}
	}
}