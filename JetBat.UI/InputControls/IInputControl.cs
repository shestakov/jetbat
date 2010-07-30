using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace JetBat.UI.InputControls
{
	public interface IInputControl
	{
		bool IsNull { get; }

		[Browsable(false)]
		bool UserChanged { get; }

		[Browsable(false)]
		object InitialValue { get; }

		[Browsable(false)]
		object Value { get; set; }

		ErrorProvider ErrorProvider { get; set; }
		string AttributeName { get; set; }

		[Description("Отображение TabOrder в заголовке")]
		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		bool ShowTabOrder { get; set; }

		[Description("Информативная подпись")]
		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		bool InformativeLabel { get; set; }

		[Description("Отображение поясняющей надписи")]
		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		bool ShowLabel { get; set; }

		[Description("Заголовок")]
		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue("Заголовок")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		string Text { get; set; }

		[DefaultValue(false)]
		bool AllowNull { get; set; }

		bool ReadOnly { get; set; }
		Color FocusedBackColor { get; set; }
		bool ValidateValue();
		void ResetToNull();
	}
}