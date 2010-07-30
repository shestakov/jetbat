using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;
using JetBat.Silverlight.UI.WebService;
using NameValue = JetBat.Client.Metadata.Misc.NameValue;

namespace JetBat.Silverlight.UI
{
	public class PlainObjectInstance
	{
		#region Load

		public void LoadAsync(NamedObjectCollection<NameValue> primaryKey)
		{
			ExecuteMethodAsync("Load", primaryKey);
		}

		#endregion

		#region Method Execution

		private void ExecuteMethodAsync(string methodName, KeyedCollection<string, NameValue> actualParameters)
		{
			if(plainObject == null) throw new NullReferenceException("Plain Object Metadata not set");
			currentMethodName = methodName;
			currentParameters = actualParameters;
			proxy = WebServiceProxyHelper.CreateProxy();
			NamedObjectCollection<NameValue> parameters = new NamedObjectCollection<NameValue>();
			foreach(ObjectMethodParameter parameter in plainObject.Methods[methodName].ParameterDefinitions)
			{
				string parameterName = parameter.Name.Substring(1);
				NameValue actualParameter = new NameValue { Name = parameterName };
				if(actualParameters.Contains(parameterName))
					actualParameter.Value = actualParameters[parameterName].Value;
				else if(Attributes.Contains(parameterName))
					actualParameter.Value = Attributes[parameterName].Value;
				else
					actualParameter.Value = null;
				parameters.Add(actualParameter);
			}
			XmlSerializer serializer = new XmlSerializer(typeof(NamedObjectCollection<NameValue>));
			StringBuilder stringBuilder = new StringBuilder();
			using(XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
				serializer.Serialize(xmlWriter, parameters);
			proxy.ExecuteMethodCompleted += ProxyOnExecuteMethodCompleted;
			proxy.ExecuteMethodAsync(plainObject.ObjectNamespace, plainObject.ObjectName, methodName, stringBuilder.ToString(), methodName);
		}

		private void ProxyOnExecuteMethodCompleted(object sender, ExecuteMethodCompletedEventArgs e)
		{
			proxy.ExecuteMethodCompleted -= ProxyOnExecuteMethodCompleted;
			if(e.Error != null)
				throw e.Error;

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObjectMethodResult));
			ObjectMethodResult objectMethodResult;
			using(StringReader reader = new StringReader(e.Result))
			{
				objectMethodResult = (ObjectMethodResult)xmlSerializer.Deserialize(reader);
			}

			if(objectMethodResult.AuthenticationRequired)
			{
				StaticAuthenticator.OnAuthenticationFinished += StaticAuthentificatorOnOnAuthenticationFinished;
				StaticAuthenticator.RequireAuthentication();
				return;
			}

			currentMethodName = null;
			currentParameters = null;

			//if (objectMethodResult.ErrorMessages != null && objectMethodResult.ErrorMessages.Count > 0)
			//{
			//    MessageBox.Show(objectMethodResult.ErrorMessages[0].Text);
			//}

			foreach(ObjectMethodParameter methodParameter in plainObject.Methods[(string)e.UserState].ParameterDefinitions)
			{
				if(objectMethodResult.Parameters.Contains(methodParameter.AttributeName))
					Attributes[methodParameter.AttributeName].Value = objectMethodResult.Parameters[methodParameter.AttributeName].Value;
			}

			if(ExecuteMethodComplete != null)
			{
				ExecuteMethodCompleteEventArgs executeMethodCompleteEventArgs = new ExecuteMethodCompleteEventArgs
																					{
																						MethodName = (string)e.UserState,
																						Exception = objectMethodResult.Exception,
																						ErrorMessages = objectMethodResult.ErrorMessages
																					};
				ExecuteMethodComplete(sender, executeMethodCompleteEventArgs);
			}
		}

		private void StaticAuthentificatorOnOnAuthenticationFinished(bool succeeded)
		{
			StaticAuthenticator.OnAuthenticationFinished -= StaticAuthentificatorOnOnAuthenticationFinished;
			if(!succeeded && ExecuteMethodComplete != null)
			{
				ExecuteMethodCompleteEventArgs eventErgs = new ExecuteMethodCompleteEventArgs
					{
						CallException = new SecurityException("Сервис требует аутенификации")
					};
				currentMethodName = null;
				currentParameters = null;
				ExecuteMethodComplete(this, eventErgs);
				return;
			}
			ExecuteMethodAsync(currentMethodName, currentParameters);
		}

		#endregion

		#region Constructor

		public PlainObjectInstance(PlainObject plainObject)
		{
			this.plainObject = plainObject;
			Attributes = new NamedObjectCollection<NameValue>();
			foreach(ObjectAttribute attribute in plainObject.Attributes)
			{
				Attributes.Add(new NameValue { Name = attribute.Name, Value = null });
			}
		}

		#endregion

		public NamedObjectCollection<NameValue> GetComplexAttributeValue(string complexAttributeName)
		{
			NamedObjectCollection<NameValue> result = new NamedObjectCollection<NameValue>();
			var complexAttribute = plainObject.ComplexAttributes[complexAttributeName];
			foreach(ComplexAttributeColumnPair columnPair in complexAttribute.MemberColumns)
			{
				result.Add(new NameValue { Name = columnPair.PrimaryKeyColumnName, Value = Attributes[columnPair.ForeignKeyColumnName].Value });
			}
			return result;
		}

		private readonly PlainObject plainObject;
		private ServiceSoapClient proxy;
		private string currentMethodName;
		private KeyedCollection<string, NameValue> currentParameters;
		public NamedObjectCollection<NameValue> Attributes { get; private set; }

		public event EventHandler<ExecuteMethodCompleteEventArgs> ExecuteMethodComplete;

		public void InsertAsync()
		{
			ExecuteMethodAsync("Insert", new NamedObjectCollection<NameValue>());
		}

		public void UpdateAsync()
		{
			ExecuteMethodAsync("Update", new NamedObjectCollection<NameValue>());
		}

		public void DeleteAsync()
		{
			ExecuteMethodAsync("Delete", new NamedObjectCollection<NameValue>());
		}

		public void RestoreAsync()
		{
			ExecuteMethodAsync("Restore", new NamedObjectCollection<NameValue>());
		}

		public void SetComplexAttribute(string complexAttributeName, NamedObjectCollection<NameValue> primaryKeyValues)
		{
			var complexAttribute = plainObject.ComplexAttributes[complexAttributeName];
			if(primaryKeyValues != null)
			{
				foreach(ComplexAttributeColumnPair columnPair in complexAttribute.MemberColumns)
				{
					Attributes[columnPair.ForeignKeyColumnName].Value = primaryKeyValues[columnPair.PrimaryKeyColumnName].Value;
				}
			}
			else
			{
				foreach(ComplexAttributeColumnPair columnPair in complexAttribute.MemberColumns)
				{
					Attributes[columnPair.ForeignKeyColumnName].Value = null;
				}
			}
		}
	}
}