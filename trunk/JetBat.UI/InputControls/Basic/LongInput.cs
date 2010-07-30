using System;
using System.ComponentModel;

namespace JetBat.UI.InputControls.Basic
{
	public class LongInput : NumericInput
	{
		public LongInput()
		{
			InitializeComponent();
			numericUpDown.Minimum = Convert.ToDecimal(Int64.MinValue);
			numericUpDown.Maximum = Convert.ToDecimal(Int64.MaxValue);
		}

		#region Designer generated code

		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// numericUpDown
			// 
			this.numericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			// 
			// LongInput
			// 
			this.Name = "LongInput";
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown)).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		#region Значение

		public override object Value
		{
			get { return base.Value; }
			set
			{
				if (value != null && value != DBNull.Value && !(value is Int64))
					throw new Exception("Неверный тип значения. Присваиваемое значение должно иметь тип System.Int64.");
				base.Value = value;
			}
		}

		public long ValueLong
		{
			get { return (userValueIsNull || userValue == null) ? 0 : (Int64) Value; }
			set { Value = value; }
		}

		protected override void pickUserValue()
		{
			userValueIsNull = false;
			userValue = Convert.ToInt64(numericUpDown.Value);
			base.pickUserValue();
		}

		#endregion

		#region Свойства

		[DefaultValue(1)]
		public long Increment
		{
			get { return Convert.ToInt64(numericUpDown.Increment); }
			set
			{
				increment = Convert.ToDecimal(value);
				numericUpDown.Increment = readOnly ? 0 : increment;
			}
		}

		[DefaultValue(Int64.MaxValue)]
		public long Maximum
		{
			get { return Convert.ToInt64(numericUpDown.Maximum); }
			set { numericUpDown.Maximum = value <= Int64.MaxValue ? Convert.ToDecimal(value) : Int64.MaxValue; }
		}

		[DefaultValue(Int64.MinValue)]
		public long Minimum
		{
			get { return Convert.ToInt64(numericUpDown.Minimum); }
			set
			{
				numericUpDown.Minimum = value >= Int64.MinValue ? Convert.ToDecimal(value) : Int64.MinValue;
				if (!allowNegative && (numericUpDown.Minimum < 0))
					numericUpDown.Minimum = 0;
			}
		}

		#endregion
	}
}