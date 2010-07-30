using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace JetBat.UI.InputControls.Basic
{
	public class NumericInput : CustomInput
	{
		protected bool allowNegative = true;
		protected decimal increment = 1;
		protected NumericUpDown numericUpDown;

		public NumericInput()
		{
			InitializeComponent();
		}

		private void numericUpDown_Leave(object sender, EventArgs e)
		{
			numericUpDown.BackColor = Color.White;
		}

		#region Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.numericUpDown = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// labelHeader
			// 
			this.labelHeader.Size = new System.Drawing.Size(150, 16);
			this.labelHeader.SizeChanged += new System.EventHandler(this.labelHeader_SizeChanged);
			// 
			// numericUpDown
			// 
			this.numericUpDown.Anchor =
				((System.Windows.Forms.AnchorStyles)
				 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				    | System.Windows.Forms.AnchorStyles.Left)
				   | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numericUpDown.Location = new System.Drawing.Point(0, 16);
			this.numericUpDown.Maximum = new decimal(new int[]
			                                         	{
			                                         		999,
			                                         		0,
			                                         		0,
			                                         		0
			                                         	});
			this.numericUpDown.Name = "numericUpDown";
			this.numericUpDown.Size = new System.Drawing.Size(150, 20);
			this.numericUpDown.TabIndex = 8;
			this.numericUpDown.Value = new decimal(new int[]
			                                       	{
			                                       		1,
			                                       		0,
			                                       		0,
			                                       		0
			                                       	});
			this.numericUpDown.Enter += new System.EventHandler(this.numericUpDown_Enter);
			this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
			this.numericUpDown.Leave += new System.EventHandler(this.numericUpDown_Leave);
			this.numericUpDown.SizeChanged += new System.EventHandler(this.numericUpDown_SizeChanged);
			this.numericUpDown.TextChanged += new System.EventHandler(this.numericUpDown_TextChanged);
			this.numericUpDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_KeyDown);
			// 
			// NumericInput
			// 
			this.Controls.Add(this.numericUpDown);
			this.Name = "NumericInput";
			this.Size = new System.Drawing.Size(150, 36);
			this.SizeChanged += new System.EventHandler(this.NumInput_SizeChanged);
			this.Controls.SetChildIndex(this.numericUpDown, 0);
			this.Controls.SetChildIndex(this.labelHeader, 0);
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown)).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		#region Внешний вид и поведение

		private void labelHeader_SizeChanged(object sender, EventArgs e)
		{
			numericUpDown.Top = labelHeader.Height;
			Height = labelHeader.Height + numericUpDown.Height;
		}

		private void NumInput_SizeChanged(object sender, EventArgs e)
		{
			if (Height != labelHeader.Height + numericUpDown.Height)
				Height = labelHeader.Height + numericUpDown.Height;
		}


		private void numericUpDown_SizeChanged(object sender, EventArgs e)
		{
			if (Height != labelHeader.Height + numericUpDown.Height)
				Height = labelHeader.Height + numericUpDown.Height;
		}

		private void numericUpDown_Enter(object sender, EventArgs e)
		{
			if (numericUpDown.DecimalPlaces == 0)
				numericUpDown.Select(0, numericUpDown.Text.Length);
			numericUpDown.BackColor = Color.FromArgb(255, 255, 150);
		}

		private void numericUpDown_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && e.Shift && allowNull)
			{
				resetToNull();
			}
		}

		private void numericUpDown_TextChanged(object sender, EventArgs e)
		{
			numericUpDown.ForeColor = SystemColors.WindowText;
			//pickUserValue();
		}

		private void numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			pickUserValue();
		}

		#endregion

		#region Значение

		protected override void renderUserValue()
		{
			base.renderUserValue();
			numericUpDown.TextChanged -= numericUpDown_TextChanged;
			numericUpDown.ValueChanged -= numericUpDown_TextChanged;

			numericUpDown.Value = userValueIsNull ? 0 : Convert.ToDecimal(userValue);

			numericUpDown.ValueChanged += numericUpDown_TextChanged;
			numericUpDown.TextChanged += numericUpDown_TextChanged;
			numericUpDown.ForeColor = userValueIsNull ? SystemColors.InactiveCaptionText : SystemColors.WindowText;
		}

		#endregion

		#region Свойства

		[DefaultValue(true)]
		public bool AllowNegative
		{
			get { return allowNegative; }
			set
			{
				allowNegative = value;
				numericUpDown.Value = !allowNegative && numericUpDown.Value < 0 ? 0 : numericUpDown.Value;
				numericUpDown.Minimum = allowNegative ? -numericUpDown.Maximum : 0;
			}
		}

		[DefaultValue(false)]
		public override bool ReadOnly
		{
			get { return readOnly; }
			set
			{
				base.ReadOnly = value;
				numericUpDown.BackColor = Color.White;
				numericUpDown.ReadOnly = value;
				numericUpDown.Increment = value ? 0 : increment;
			}
		}

		#endregion
	}
}