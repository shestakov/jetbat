using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace JetBat.UI.InputControls.Basic
{
	public class BoolInput : CustomInput
	{
		private ComboBox comboBox;

		private TextBox textBox;
		private string textFalse = "нет";
		private string textNull = "[не задано]";
		private string textTrue = "да";

		public BoolInput()
		{
			InitializeComponent();
			fillList();
		}

		#region Designer generated code

		private void InitializeComponent()
		{
			this.comboBox = new System.Windows.Forms.ComboBox();
			this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// labelHeader
			// 
			this.labelHeader.VisibleChanged += new System.EventHandler(this.labelHeader_VisibleChanged);
			this.labelHeader.SizeChanged += new System.EventHandler(this.labelHeader_SizeChanged);
			// 
			// comboBox
			// 
			this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox.Location = new System.Drawing.Point(0, 16);
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new System.Drawing.Size(150, 21);
			this.comboBox.TabIndex = 7;
			this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			this.comboBox.SizeChanged += new System.EventHandler(this.comboBox_SizeChanged);
			this.comboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(0, 16);
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.Size = new System.Drawing.Size(150, 20);
			this.textBox.TabIndex = 8;
			// 
			// BoolInput
			// 
			this.Controls.Add(this.comboBox);
			this.Controls.Add(this.textBox);
			this.Name = "BoolInput";
			this.Size = new System.Drawing.Size(150, 37);
			this.Enter += new System.EventHandler(this.BoolInput_Enter);
			this.Leave += new System.EventHandler(this.BoolInput_Leave);
			this.SizeChanged += new System.EventHandler(this.boolInput_SizeChanged);
			this.Controls.SetChildIndex(this.textBox, 0);
			this.Controls.SetChildIndex(this.comboBox, 0);
			this.Controls.SetChildIndex(this.labelHeader, 0);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		#region Значение

		public override object Value
		{
			get { return base.Value; }
			set
			{
				if (value != null && value != DBNull.Value && !(value is Boolean))
					throw new Exception("Неверный тип значения. Присваиваемое значение должно иметь логический тип.");
				base.Value = value;
			}
		}

		[DefaultValue(false)]
		public bool ValueBoolean
		{
			get { return (userValue != null && userValue != DBNull.Value) ? (bool) userValue : false; }
			set { Value = value; }
		}

		protected override void renderUserValue()
		{
			base.renderUserValue();
			comboBox.SelectedIndexChanged -= comboBox_SelectedIndexChanged;
			if ((userValue == null) || !(userValue is Boolean))
				comboBox.SelectedIndex = comboBox.Items.IndexOf(textNull);
			else if ((bool) userValue)
				comboBox.SelectedIndex = comboBox.Items.IndexOf(textTrue);
			else if ((bool) userValue == false)
				comboBox.SelectedIndex = comboBox.Items.IndexOf(textFalse);
			comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
			setExtendedColors();
			textBox.Text = comboBox.Text;
		}

		protected override void pickUserValue()
		{
			if (comboBox.SelectedIndex == comboBox.Items.IndexOf(textTrue))
			{
				userValue = true;
				userValueIsNull = false;
			}
			else if (comboBox.SelectedIndex == comboBox.Items.IndexOf(textFalse))
			{
				userValue = false;
				userValueIsNull = false;
			}
			else
			{
				userValue = null;
				userValueIsNull = true;
			}
			base.pickUserValue();
		}

		protected override void resetToNull()
		{
			base.resetToNull();
			setExtendedColors();
			textBox.Text = comboBox.Text;
		}

		#endregion

		#region Внешний вид

		private void realign()
		{
			SizeChanged -= boolInput_SizeChanged;
			labelHeader.SizeChanged -= labelHeader_SizeChanged;
			comboBox.SizeChanged -= comboBox_SizeChanged;

			comboBox.Top = labelHeader.Visible ? labelHeader.Height : 0;
			Height = (labelHeader.Visible ? labelHeader.Height : 0) + comboBox.Height;
			comboBox.Width = Width;

			textBox.Top = labelHeader.Visible ? labelHeader.Height : 0;
			textBox.Width = Width;

			SizeChanged += boolInput_SizeChanged;
			labelHeader.SizeChanged += labelHeader_SizeChanged;
			comboBox.SizeChanged += comboBox_SizeChanged;
		}

		private void labelHeader_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void boolInput_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void comboBox_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}


		private void labelHeader_VisibleChanged(object sender, EventArgs e)
		{
			realign();
		}

		#endregion

		#region Обработка событий

		private void comboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && e.Shift && allowNull)
			{
				resetToNull();
			}
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			pickUserValue();
		}

		#endregion

		#region Инициализация

		private void fillList()
		{
			comboBox.Items.Clear();
			if (allowNull) comboBox.Items.Add(textNull);
			comboBox.Items.Add(textTrue);
			comboBox.Items.Add(textFalse);
			resetToNull();
		}

		#endregion

		#region Свойства

		[DefaultValue("да")]
		public string TextTrue
		{
			get { return textTrue; }
			set
			{
				if (textTrue != value)
				{
					textTrue = value;
					fillList();
					textBox.Text = comboBox.Text;
				}
			}
		}

		[DefaultValue("нет")]
		public string TextFalse
		{
			get { return textFalse; }
			set
			{
				if (textFalse != value)
				{
					textFalse = value;
					fillList();
					textBox.Text = comboBox.Text;
				}
			}
		}

		[DefaultValue("[не задано]")]
		public string TextNull
		{
			get { return textNull; }
			set
			{
				if (textNull != value)
				{
					textNull = value;
					fillList();
					textBox.Text = comboBox.Text;
				}
			}
		}

		public override bool ReadOnly
		{
			get { return readOnly; }
			set
			{
				comboBox.Enabled = !value;
				comboBox.Visible = !value;
				textBox.Visible = value;
				textBox.BackColor = Color.White;

				base.ReadOnly = value;
			}
		}

		#endregion

		#region Цветовое оформление

		private Color backColorFalse = Color.Red;
		private Color backColorNull = Color.LightGray;
		private Color backColorTrue = Color.LightGreen;
		private Color foreColorFalse = SystemColors.WindowText;
		private Color foreColorNull = SystemColors.WindowText;
		private Color foreColorTrue = SystemColors.WindowText;
		private bool useExtendedColors;

		public bool UseExtendedColors
		{
			get { return useExtendedColors; }
			set
			{
				useExtendedColors = value;
				setExtendedColors();
			}
		}

		public Color BackColorTrue
		{
			get { return backColorTrue; }
			set
			{
				backColorTrue = value;
				setExtendedColors();
			}
		}

		public Color BackColorFalse
		{
			get { return backColorFalse; }
			set
			{
				backColorFalse = value;
				setExtendedColors();
			}
		}

		public Color BackColorNull
		{
			get { return backColorNull; }
			set
			{
				backColorNull = value;
				setExtendedColors();
			}
		}

		public Color ForeColorTrue
		{
			get { return foreColorTrue; }
			set
			{
				foreColorTrue = value;
				setExtendedColors();
			}
		}

		public Color ForeColorFalse
		{
			get { return foreColorFalse; }
			set
			{
				foreColorFalse = value;
				setExtendedColors();
			}
		}

		public Color ForeColorNull
		{
			get { return foreColorNull; }
			set
			{
				foreColorNull = value;
				setExtendedColors();
			}
		}

		private void setExtendedColors()
		{
			if (!useExtendedColors)
			{
				comboBox.BackColor = SystemColors.Window;
				comboBox.ForeColor = SystemColors.WindowText;
			}
			else if (IsNull)
			{
				comboBox.BackColor = backColorNull;
				comboBox.ForeColor = foreColorNull;
			}
			else if (ValueBoolean)
			{
				comboBox.BackColor = backColorTrue;
				comboBox.ForeColor = foreColorTrue;
			}
			else
			{
				comboBox.BackColor = backColorFalse;
				comboBox.ForeColor = foreColorFalse;
			}
		}

		#endregion

		#region Получение и утрата фокуса

		private void BoolInput_Enter(object sender, EventArgs e)
		{
			comboBox.BackColor = Color.FromArgb(255, 255, 150);
			textBox.BackColor = Color.FromArgb(255, 255, 150);
		}

		private void BoolInput_Leave(object sender, EventArgs e)
		{
			comboBox.BackColor = Color.White;
			textBox.BackColor = Color.White;
		}

		#endregion
	}
}