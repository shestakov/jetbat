using System;
using System.ComponentModel;

namespace JetBat.UI.InputControls.Basic
{
	public class ShortInput : NumericInput
	{
		public ShortInput()
		{
			InitializeComponent();
			numericUpDown.Minimum = Convert.ToDecimal(Int16.MinValue);
			numericUpDown.Maximum = Convert.ToDecimal(Int16.MaxValue);
		}

		#region Designer generated code

		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// ShortInput
			// 
			this.Name = "ShortInput";
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown)).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		#region Внешний вид и поведение

		#endregion

		#region Значение

		public override object Value
		{
			get { return base.Value; }
			set
			{
				if (value != null && value != DBNull.Value && !(value is Int16))
					throw new Exception("Неверный тип значения. Присваиваемое значение должно иметь тип System.Int16.");
				base.Value = value;
			}
		}

		public short ValueShort
		{
			get { return (userValueIsNull || userValue == null) ? (Int16) 0 : (Int16) Value; }
			set { Value = value; }
		}

		protected override void pickUserValue()
		{
			userValueIsNull = false;
			userValue = Convert.ToInt16(numericUpDown.Value);
			base.pickUserValue();
		}

		#endregion

		#region Свойства

		[DefaultValue(1)]
		public short Increment
		{
			get { return Convert.ToInt16(numericUpDown.Increment); }
			set
			{
				increment = Convert.ToDecimal(value);
				numericUpDown.Increment = readOnly ? 0 : increment;
			}
		}

		[DefaultValue(Int16.MaxValue)]
		public int Maximum
		{
			get { return Convert.ToInt16(numericUpDown.Maximum); }
			set { numericUpDown.Maximum = value <= Int16.MaxValue ? Convert.ToDecimal(value) : Int16.MaxValue; }
		}

		[DefaultValue(Int16.MinValue)]
		public int Minimum
		{
			get { return Convert.ToInt16(numericUpDown.Minimum); }
			set
			{
				numericUpDown.Minimum = value >= Int16.MinValue ? Convert.ToDecimal(value) : Int16.MinValue;
				if (!allowNegative && (numericUpDown.Minimum < 0))
					numericUpDown.Minimum = 0;
			}
		}

		#endregion
	}
}