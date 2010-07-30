using System;
using System.Data;
using System.Data.Common;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Abstract
{
	public interface IAccessProvider
	{
		ErrorMessageCollection ExecuteProcedure(string objectNamespace, string objectName, string methodName,
												AttributeValueSet parameterValues, out DataTable[] recordSets,
												DbTransaction transaction);

		ErrorMessageCollection ExecuteProcedure(string objectNamespace, string objectName, string methodName,
												AttributeValueSet parameterValues, DbTransaction transaction);

		void Close();
	}
}