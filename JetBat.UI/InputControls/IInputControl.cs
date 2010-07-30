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

		[Description("����������� TabOrder � ���������")]
		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		bool ShowTabOrder { get; set; }

		[Description("������������� �������")]
		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		bool InformativeLabel { get; set; }

		[Description("����������� ���������� �������")]
		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		bool ShowLabel { get; set; }

		[Description("���������")]
		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue("���������")]
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