using System;
using System.Drawing;
using System.Windows.Forms;

namespace JetBat.UI.InputControls.Basic
{
	public class TextInput : CustomInput, ITextInput
	{
		private TextBox textBox;

		public TextInput()
		{
			InitializeComponent();
		}

		private void textBox_Leave(object sender, EventArgs e)
		{
			textBox.BackColor = SystemColors.Window;
		}

		#region Designer generated code

		private void InitializeComponent()
		{
			this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// labelHeader
			// 
			this.labelHeader.Size = new System.Drawing.Size(200, 16);
			this.labelHeader.VisibleChanged += new System.EventHandler(this.labelHeader_VisibleChanged);
			this.labelHeader.SizeChanged += new System.EventHandler(this.labelHeader_SizeChanged);
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(0, 16);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(200, 20);
			this.textBox.TabIndex = 7;
			this.textBox.Enter += new System.EventHandler(this.textBox_Enter);
			this.textBox.Leave += new System.EventHandler(this.textBox_Leave);
			this.textBox.SizeChanged += new System.EventHandler(this.textBox_SizeChanged);
			this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
			this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
			// 
			// TextInput
			// 
			this.Controls.Add(this.textBox);
			this.Name = "TextInput";
			this.Size = new System.Drawing.Size(200, 36);
			this.SizeChanged += new System.EventHandler(this.TextInput_SizeChanged);
			this.Controls.SetChildIndex(this.textBox, 0);
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
				if (value != null && value != DBNull.Value && !(value is String))
					throw new Exception("Неверный тип значения. Присваиваемое значение должно быть строкой.");
				base.Value = value;
			}
		}

		public string ValueString
		{
			get { return (userValueIsNull || userValue == null) ? String.Empty : (string) Value; }
			set { Value = value; }
		}

		protected override void renderUserValue()
		{
			base.renderUserValue();
			textBox.TextChanged -= textBox_TextChanged;
			textBox.Text = userValueIsNull ? string.Empty : userValue.ToString();
			textBox.TextChanged += textBox_TextChanged;
		}

		protected override void pickUserValue()
		{
			userValueIsNull = (allowNull && textBox.Text == String.Empty);
			userValue = userValueIsNull ? null : textBox.Text;
			base.pickUserValue();
		}

		#endregion

		#region Внешний вид и поведение

		private void realign()
		{
			SizeChanged -= TextInput_SizeChanged;
			labelHeader.SizeChanged -= labelHeader_SizeChanged;
			textBox.SizeChanged -= textBox_SizeChanged;

			textBox.Top = labelHeader.Visible ? labelHeader.Height : 0;

			if (!textBox.Multiline || (Height < labelHeader.Height))
			{
				Height = (labelHeader.Visible ? labelHeader.Height : 0) + textBox.Height;
			}
			else
			{
				textBox.Height = Height - (labelHeader.Visible ? labelHeader.Height : 0);
			}


			Height = (labelHeader.Visible ? labelHeader.Height : 0) + textBox.Height;
			textBox.Width = Width;

			SizeChanged += TextInput_SizeChanged;
			labelHeader.SizeChanged += labelHeader_SizeChanged;
			textBox.SizeChanged += textBox_SizeChanged;
		}

		private void labelHeader_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void TextInput_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void textBox_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void labelHeader_VisibleChanged(object sender, EventArgs e)
		{
			realign();
		}

		#endregion

		#region Обработка событий

		private void textBox_Enter(object sender, EventArgs e)
		{
			textBox.SelectAll();
			textBox.BackColor = Color.FromArgb(255, 255, 150);
		}

		private void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && e.Shift && allowNull)
			{
				resetToNull();
			}
		}

		private void textBox_TextChanged(object sender, EventArgs e)
		{
			pickUserValue();
		}

		#endregion

		#region Свойства

		public override bool ReadOnly
		{
			get { return readOnly; }
			set
			{
				textBox.ReadOnly = value;
				textBox.BackColor = Color.White;
				base.ReadOnly = value;
			}
		}

		public int MaxLength
		{
			get { return textBox.MaxLength; }
			set { textBox.MaxLength = value; }
		}

		public bool Multiline
		{
			get { return textBox.Multiline; }
			set { textBox.Multiline = value; }
		}

		#endregion
	}
}