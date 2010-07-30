namespace JetBat.UI.InputControls
{
	public interface IDataObjectPickListControl : IDataObjectPickControl
	{
		string ListNamespace { get; set; }
		string ListName { get; set; }
	}
}