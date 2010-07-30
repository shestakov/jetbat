using System;
using System.ComponentModel;

namespace JetBat.UI.InputControls.Basic
{
	public class DecimalInput : NumericInput, IDecimalInput
	{
		protected int decimalPrecision = 3;

		public DecimalInput()
		{
			InitializeComponent();
			numericUpDown.Minimum = Decimal.MinValue;
			numericUpDown.Maximum = Decimal.MaxValue;
		}

		#region Designer generated code

		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// DecimalInput
			// 
			this.Name = "DecimalInput";
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
				if (value != null && value != DBNull.Value && !(value is Decimal))
					throw new Exception("Неверный тип значения. Присваиваемое значение должно иметь тип System.Decimal.");
				base.Value = value;
			}
		}

		public decimal ValueDecimal
		{
			get { return (userValueIsNull || userValue == null) ? 0 : (decimal) Value; }
			set { Value = value; }
		}

		protected override void pickUserValue()
		{
			userValueIsNull = false;
			userValue = numericUpDown.Value;
			base.pickUserValue();
		}

		#endregion

		#region Свойства

		[DefaultValue(3)]
		public int DecimalPrecision
		{
			get { return decimalPrecision; }
			set
			{
				decimalPrecision = value;
				decimal val = Convert.ToDecimal((Math.Pow(10, decimalPrecision) - 1)/(Math.Pow(10, numericUpDown.DecimalPlaces)));
				numericUpDown.Maximum = val;
				numericUpDown.Minimum = allowNegative ? -val : 0;
			}
		}

		[DefaultValue(0)]
		public int DecimalScale
		{
			get { return numericUpDown.DecimalPlaces; }
			set
			{
				numericUpDown.DecimalPlaces = value;
				decimal val = Convert.ToDecimal((Math.Pow(10, decimalPrecision) - 1)/(Math.Pow(10, numericUpDown.DecimalPlaces)));
				numericUpDown.Maximum = val;
				numericUpDown.Minimum = allowNegative ? -val : 0;
			}
		}

		[DefaultValue(1)]
		public decimal Increment
		{
			get { return numericUpDown.Increment; }
			set
			{
				increment = value;
				numericUpDown.Increment = readOnly ? 0 : increment;
			}
		}

		[DefaultValue(999)]
		public decimal Maximum
		{
			get { return numericUpDown.Maximum; }
			set
			{
				decimal val = Convert.ToDecimal((Math.Pow(10, decimalPrecision) - 1)/(Math.Pow(10, numericUpDown.DecimalPlaces)));
				numericUpDown.Maximum = value <= val ? value : val;
			}
		}

		[DefaultValue(0)]
		public decimal Minimum
		{
			get { return numericUpDown.Minimum; }
			set
			{
				decimal val = -Convert.ToDecimal((Math.Pow(10, decimalPrecision) - 1)/(Math.Pow(10, numericUpDown.DecimalPlaces)));
				numericUpDown.Minimum = value >= val ? value : val;
				if (!allowNegative && (numericUpDown.Minimum < 0))
					numericUpDown.Minimum = 0;
			}
		}

		#endregion
	}
}