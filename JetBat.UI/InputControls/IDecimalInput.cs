using System.ComponentModel;

namespace JetBat.UI.InputControls
{
	public interface IDecimalInput
	{
		[DefaultValue(3)]
		int DecimalPrecision { get; set; }

		[DefaultValue(0)]
		int DecimalScale { get; set; }
	}
}