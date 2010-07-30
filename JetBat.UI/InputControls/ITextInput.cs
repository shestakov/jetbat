namespace JetBat.UI.InputControls
{
	public interface ITextInput : IInputControl
	{
		int MaxLength { get; set; }
		bool Multiline { get; set; }
	}
}