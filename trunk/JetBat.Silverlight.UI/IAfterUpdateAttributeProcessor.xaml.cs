namespace JetBat.Silverlight.UI
{
	public interface IAfterUpdateAttributeProcessor
	{
		void Process(PlainObjectInstance instance, string methodName);
	}
}