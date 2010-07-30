using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JetBat.UI.InputControls.Basic
{
	public enum DateTimeInputDateFormat
	{
		None,
		Full,
		Short,
		MonthYear
	}

	public enum DateTimeInputTimeFormat
	{
		None,
		HoursMinutes,
		HoursMinutesSeconds
	}

	public class DateTimeInput : CustomInput
	{
		private bool droppedDown;
		private TextBox textBox;
		private string textIsNull = "[не указано]";

		public DateTimeInput()
		{
			InitializeComponent();
			Value = DateTime.Now;
		}

		#region Designer generated code

		private void InitializeComponent()
		{
			this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// labelHeader
			// 
			this.labelHeader.Size = new System.Drawing.Size(120, 16);
			this.labelHeader.VisibleChanged += new System.EventHandler(this.labelHeader_VisibleChanged);
			this.labelHeader.SizeChanged += new System.EventHandler(this.labelHeader_SizeChanged);
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
			this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker.Location = new System.Drawing.Point(0, 16);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new System.Drawing.Size(120, 20);
			this.dateTimePicker.TabIndex = 7;
			this.dateTimePicker.Value = new System.DateTime(2006, 1, 1, 0, 0, 0, 0);
			this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
			this.dateTimePicker.DropDown += new System.EventHandler(this.dateTimePicker_DropDown);
			this.dateTimePicker.CloseUp += new System.EventHandler(this.dateTimePicker_CloseUp);
			this.dateTimePicker.SizeChanged += new System.EventHandler(this.dateTimePicker_SizeChanged);
			this.dateTimePicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker_KeyDown);
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(0, 16);
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.Size = new System.Drawing.Size(120, 20);
			this.textBox.TabIndex = 8;
			// 
			// DateTimeInput
			// 
			this.Controls.Add(this.dateTimePicker);
			this.Controls.Add(this.textBox);
			this.Name = "DateTimeInput";
			this.Size = new System.Drawing.Size(120, 36);
			this.Enter += new System.EventHandler(this.DateTimeInput_Enter);
			this.Leave += new System.EventHandler(this.DateTimeInput_Leave);
			this.SizeChanged += new System.EventHandler(this.DateTimeInput_SizeChanged);
			this.Controls.SetChildIndex(this.textBox, 0);
			this.Controls.SetChildIndex(this.dateTimePicker, 0);
			this.Controls.SetChildIndex(this.labelHeader, 0);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		#region Внешний вид

		private void realign()
		{
			SizeChanged -= DateTimeInput_SizeChanged;
			labelHeader.SizeChanged -= labelHeader_SizeChanged;
			dateTimePicker.SizeChanged -= dateTimePicker_SizeChanged;

			dateTimePicker.Top = labelHeader.Visible ? labelHeader.Height : 0;
			Height = (labelHeader.Visible ? labelHeader.Height : 0) + dateTimePicker.Height;
			dateTimePicker.Width = Width;

			textBox.Top = labelHeader.Visible ? labelHeader.Height : 0;
			textBox.Width = Width;

			SizeChanged += DateTimeInput_SizeChanged;
			labelHeader.SizeChanged += labelHeader_SizeChanged;
			dateTimePicker.SizeChanged += dateTimePicker_SizeChanged;
		}

		private void labelHeader_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void DateTimeInput_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void dateTimePicker_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void labelHeader_VisibleChanged(object sender, EventArgs e)
		{
			realign();
		}


		private void dateTimePicker_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && e.Shift && allowNull)
			{
				resetToNull();
			}
		}

		#endregion

		#region Обработка событий

		private void dateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!droppedDown)
				pickUserValue();
		}

		private void dateTimePicker_DropDown(object sender, EventArgs e)
		{
			droppedDown = true;
		}

		private void dateTimePicker_CloseUp(object sender, EventArgs e)
		{
			pickUserValue();
			droppedDown = false;
		}

		#endregion

		#region Значение

		public override object Value
		{
			get { return base.Value; }
			set
			{
				if (value != null && value != DBNull.Value && !(value is DateTime))
					throw new Exception("Неверный тип значения. Присваиваемое значение должно иметь тип DateTime.");
				base.Value = value;
			}
		}

		public DateTime ValueDateTime
		{
			get { return (userValueIsNull || userValue == null) ? DateTime.MinValue : (DateTime) Value; }
			set { Value = value; }
		}

		protected override void setUserValueNull()
		{
			base.setUserValueNull();
			dateTimePicker.ValueChanged -= dateTimePicker_ValueChanged;
			dateTimePicker.Value = DateTime.Now;
			dateTimePicker.ValueChanged += dateTimePicker_ValueChanged;
			textBox.Text = dateTimePicker.Value.ToString(dateTimePicker.CustomFormat);
		}

		protected override void renderUserValue()
		{
			base.renderUserValue();
			dateTimePicker.ValueChanged -= dateTimePicker_ValueChanged;
			dateTimePicker.Value = userValueIsNull ? DateTime.Now : Convert.ToDateTime(userValue);
			dateTimePicker.ValueChanged += dateTimePicker_ValueChanged;
			setDateTimeFormat();
		}

		protected override void pickUserValue()
		{
			userValueIsNull = false;
			DateTime dt = dateTimePicker.Value;
			userValue = new DateTime(
				dt.Year,
				dt.Month,
				dateFormat != DateTimeInputDateFormat.MonthYear ? dt.Day : 1,
				timeFormat != DateTimeInputTimeFormat.None ? dt.Hour : 0,
				timeFormat != DateTimeInputTimeFormat.None ? dt.Minute : 0,
				timeFormat == DateTimeInputTimeFormat.HoursMinutesSeconds ? dt.Second : 0);
			base.pickUserValue();
			setDateTimeFormat();
		}

		#endregion

		#region Свойства

		public override bool ReadOnly
		{
			get { return readOnly; }
			set
			{
				base.ReadOnly = value;
				dateTimePicker.Enabled = !value;
				dateTimePicker.Visible = !value;
				textBox.Visible = value;
				textBox.BackColor = Color.White;
			}
		}

		[Browsable(true)]
		[DefaultValue("[не указано]")]
		public string TextIsNull
		{
			get { return textIsNull; }
			set
			{
				textIsNull = "'" + value.Replace("'", "\'") + "'";
				setDateTimeFormat();
			}
		}

		#endregion

		#region Формат даты и времени

		private DateTimeInputDateFormat dateFormat = DateTimeInputDateFormat.Short;
		private DateTimePicker dateTimePicker;
		private string dateTimePickerCustomFormat;
		private DateTimeInputTimeFormat timeFormat = DateTimeInputTimeFormat.HoursMinutes;

		public DateTimeInputDateFormat DateFormat
		{
			get { return dateFormat; }
			set
			{
				if (dateFormat == value)
					return;

				dateFormat = value;
				setDateTimeFormat();
			}
		}

		public DateTimeInputTimeFormat TimeFormat
		{
			get { return timeFormat; }
			set
			{
				if (timeFormat == value)
					return;

				timeFormat = value;
				setDateTimeFormat();
			}
		}

		public string CustomFormat
		{
			get { return dateTimePickerCustomFormat; }
			set
			{
				if (dateTimePickerCustomFormat == value)
					return;

				dateTimePickerCustomFormat = value;
				setDateTimeFormat();
			}
		}

		private void setDateTimeFormat()
		{
			if (IsNull && !weHaveFocuse)
			{
				dateTimePicker.CustomFormat = "'" + textIsNull.Replace("'", "\'") + "'";
			}
			else if (dateFormat == DateTimeInputDateFormat.None && timeFormat == DateTimeInputTimeFormat.None)
			{
				dateTimePicker.CustomFormat = dateTimePickerCustomFormat;
			}
			else
			{
				StringBuilder format = new StringBuilder();
				switch (dateFormat)
				{
					case DateTimeInputDateFormat.Full:
						format.Append("dd MMMM yyyy");
						break;
					case DateTimeInputDateFormat.Short:
						format.Append("dd.MM.yyyy");
						break;
					case DateTimeInputDateFormat.MonthYear:
						format.Append("MMMM yyyy");
						break;
					default:
						break;
				}
				if (dateFormat != DateTimeInputDateFormat.None && timeFormat != DateTimeInputTimeFormat.None)
					format.Append(" ");

				if (timeFormat != DateTimeInputTimeFormat.None)
					format.Append(timeFormat == DateTimeInputTimeFormat.HoursMinutesSeconds ? "hh:mm:ss" : "hh:mm");

				dateTimePicker.CustomFormat = format.ToString();

				if (dateFormat == DateTimeInputDateFormat.Full || dateFormat == DateTimeInputDateFormat.Short)
					dateTimePicker.ShowUpDown = false;
				else
					dateTimePicker.ShowUpDown = true;
			}
			textBox.Text = dateTimePicker.Value.ToString(dateTimePicker.CustomFormat);
		}

		#endregion

		#region Получение и утрата фокуса

		private bool weHaveFocuse;

		private void DateTimeInput_Enter(object sender, EventArgs e)
		{
			weHaveFocuse = true;
			setDateTimeFormat();
			textBox.BackColor = Color.FromArgb(255, 255, 150);
		}

		private void DateTimeInput_Leave(object sender, EventArgs e)
		{
			weHaveFocuse = false;
			setDateTimeFormat();
			textBox.BackColor = Color.White;
		}

		#endregion
	}
}