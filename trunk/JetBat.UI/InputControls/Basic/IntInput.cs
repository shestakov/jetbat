using System;
using System.ComponentModel;

namespace JetBat.UI.InputControls.Basic
{
	public class IntInput : NumericInput
	{
		public IntInput()
		{
			InitializeComponent();
			numericUpDown.Minimum = Convert.ToDecimal(Int32.MinValue);
			numericUpDown.Maximum = Convert.ToDecimal(Int32.MaxValue);
		}

		#region Designer generated code

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// labelHeader
			// 
			this.labelHeader.Name = "labelHeader";
			// 
			// IntInput
			// 
			this.Controls.Add(this.numericUpDown);
			this.Name = "IntInput";
			this.Controls.SetChildIndex(this.numericUpDown, 0);
			this.Controls.SetChildIndex(this.labelHeader, 0);
			this.ResumeLayout(false);
		}

		#endregion

		#region Значение

		public override object Value
		{
			get { return base.Value; }
			set
			{
				if (value != null && value != DBNull.Value && !(value is Int32))
					throw new Exception("Неверный тип значения. Присваиваемое значение должно иметь тип System.Int32.");
				base.Value = value;
			}
		}

		public int ValueInt
		{
			get { return (userValueIsNull || userValue == null) ? 0 : (Int32) Value; }
			set { Value = value; }
		}

		protected override void pickUserValue()
		{
			userValueIsNull = false;
			userValue = Convert.ToInt32(numericUpDown.Value);
			base.pickUserValue();
		}

		#endregion

		#region Свойства

		[DefaultValue(1)]
		public int Increment
		{
			get { return Convert.ToInt32(numericUpDown.Increment); }
			set
			{
				increment = Convert.ToDecimal(value);
				numericUpDown.Increment = readOnly ? 0 : increment;
			}
		}

		[DefaultValue(Int32.MaxValue)]
		public int Maximum
		{
			get { return Convert.ToInt32(numericUpDown.Maximum); }
			set { numericUpDown.Maximum = value <= Int32.MaxValue ? Convert.ToDecimal(value) : Int32.MaxValue; }
		}

		[DefaultValue(Int32.MinValue)]
		public int Minimum
		{
			get { return Convert.ToInt32(numericUpDown.Minimum); }
			set
			{
				numericUpDown.Minimum = value >= Int32.MinValue ? Convert.ToDecimal(value) : Int32.MinValue;
				if (!allowNegative && (numericUpDown.Minimum < 0))
					numericUpDown.Minimum = 0;
			}
		}

		#endregion
	}
}