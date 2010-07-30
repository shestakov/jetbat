using System;
using System.ComponentModel;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Misc;

namespace JetBat.UI.InputControls
{
	public interface IDataObjectPickControl
	{
		AttributeValueSet SelectedObject { get; }

		[
			Description("Заголовок"),
			Category("Appearance"),
			Browsable(true),
			DefaultValue("Заголовок"),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		string Text { get; set; }

		bool IsNull { get; }

		bool AllowNull { get; set; }

		bool Changed { get; }

		bool ReadOnly { get; set; }

		AttributeValueSet Parameters { get; }

		IAccessAdapter AccessAdapter { get; set; }

		string ComplexAttributeName { get; set; }

		PlainObjectDefinition PlainObjectDefinition { get; }

		event EventHandler ValueChanged;
		void Prepare();
		void ResetToNull();
		void SelectObject(AttributeValueSet primaryKey);
	}
}