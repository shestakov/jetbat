namespace JetBat.BusinessLogic
{
	public interface IPermissionProvider
	{
		void CheckPremission(string userName, string objectNamespace, string objectName, string methodName);
	}
}