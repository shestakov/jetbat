using System;
using System.Windows.Forms;

namespace JetBat.UI.DataControls.Messages
{
	public sealed partial class MessageWindowForm : Form
	{
		private MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK;
		private MessageBoxDefaultButton messageBoxDefaultButton = MessageBoxDefaultButton.Button1;
		private MessageBoxIcon messageBoxIcon = MessageBoxIcon.None;

		public MessageWindowForm()
		{
			InitializeComponent();
		}

		public DialogResult ShowDialog(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
		                               MessageBoxDefaultButton defaultButton)
		{
			Text = caption;
			textBoxMessage.Text = text;
			messageBoxButtons = buttons;
			messageBoxIcon = icon;
			messageBoxDefaultButton = defaultButton;

			switch (messageBoxButtons)
			{
				case MessageBoxButtons.OK:
					button1.Text = "Закрыть";
					button2.Text = "";
					button3.Text = "";
					button1.Visible = true;
					button2.Visible = false;
					button3.Visible = false;
					AcceptButton = button1;
					CancelButton = null;
					break;
				case MessageBoxButtons.OKCancel:
					button1.Text = "Отмена";
					button2.Text = "ОК";
					button3.Text = "";
					button1.Visible = true;
					button2.Visible = true;
					button3.Visible = false;
					AcceptButton = button2;
					CancelButton = button1;
					break;
				case MessageBoxButtons.YesNo:
					button1.Text = "Да";
					button2.Text = "Нет";
					button3.Text = "";
					button1.Visible = true;
					button2.Visible = true;
					button3.Visible = false;
					AcceptButton = null;
					CancelButton = null;
					break;
				case MessageBoxButtons.YesNoCancel:
					button1.Text = "Да";
					button2.Text = "Нет";
					button3.Text = "Отмена";
					button1.Visible = true;
					button2.Visible = true;
					button3.Visible = true;
					AcceptButton = null;
					CancelButton = button3;
					break;
				case MessageBoxButtons.AbortRetryIgnore:
					button1.Text = "Прервать";
					button2.Text = "Повторить";
					button3.Text = "Игнорировать";
					button1.Visible = true;
					button2.Visible = true;
					button3.Visible = true;
					AcceptButton = null;
					CancelButton = null;
					break;
				case MessageBoxButtons.RetryCancel:
					button1.Text = "Отмена";
					button2.Text = "Повторить";
					button3.Text = "";
					button1.Visible = true;
					button2.Visible = true;
					button3.Visible = false;
					AcceptButton = button1;
					CancelButton = null;
					break;
				default:
					button1.Text = "Закрыть";
					button2.Text = "";
					button3.Text = "";
					button1.Visible = true;
					button2.Visible = false;
					button3.Visible = false;
					AcceptButton = button1;
					CancelButton = null;
					break;
			}

			switch (messageBoxIcon)
			{
				case MessageBoxIcon.Information:
					buttonError.ImageIndex = 2;
					break;
				case MessageBoxIcon.Error:
					buttonError.ImageIndex = 1;
					break;
				case MessageBoxIcon.Warning:
					buttonError.ImageIndex = 0;
					break;
				case MessageBoxIcon.Question:
					buttonError.ImageIndex = 3;
					break;
				default:
					buttonError.ImageIndex = 2;
					break;
			}

			return ShowDialog();
		}

		private void MessageWindowForm_Shown(object sender, EventArgs e)
		{
			textBoxMessage.SelectionLength = 0;
			switch (messageBoxDefaultButton)
			{
				case MessageBoxDefaultButton.Button1:
					button1.Focus();
					break;
				case MessageBoxDefaultButton.Button2:
					button2.Focus();
					break;
				case MessageBoxDefaultButton.Button3:
					button3.Focus();
					break;
			}
		}

		private void buttonError_Click(object sender, EventArgs e)
		{
			textBoxMessage.SelectionStart = 0;
			textBoxMessage.SelectionLength = textBoxMessage.Text.Length;
			textBoxMessage.Copy();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			switch (messageBoxButtons)
			{
				case MessageBoxButtons.OK:
					DialogResult = DialogResult.OK;
					break;
				case MessageBoxButtons.OKCancel:
					DialogResult = DialogResult.Cancel;
					break;
				case MessageBoxButtons.YesNo:
					DialogResult = DialogResult.Yes;
					break;
				case MessageBoxButtons.YesNoCancel:
					DialogResult = DialogResult.Yes;
					break;
				case MessageBoxButtons.AbortRetryIgnore:
					DialogResult = DialogResult.Abort;
					break;
				case MessageBoxButtons.RetryCancel:
					DialogResult = DialogResult.Cancel;
					break;
				default:
					DialogResult = DialogResult.OK;
					break;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			switch (messageBoxButtons)
			{
				case MessageBoxButtons.OK:
					DialogResult = DialogResult.None;
					break;
				case MessageBoxButtons.OKCancel:
					DialogResult = DialogResult.OK;
					break;
				case MessageBoxButtons.YesNo:
					DialogResult = DialogResult.No;
					break;
				case MessageBoxButtons.YesNoCancel:
					DialogResult = DialogResult.No;
					break;
				case MessageBoxButtons.AbortRetryIgnore:
					DialogResult = DialogResult.Retry;
					break;
				case MessageBoxButtons.RetryCancel:
					DialogResult = DialogResult.Retry;
					break;
				default:
					DialogResult = DialogResult.None;
					break;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			switch (messageBoxButtons)
			{
				case MessageBoxButtons.OK:
					DialogResult = DialogResult.None;
					break;
				case MessageBoxButtons.OKCancel:
					DialogResult = DialogResult.None;
					break;
				case MessageBoxButtons.YesNo:
					DialogResult = DialogResult.None;
					break;
				case MessageBoxButtons.YesNoCancel:
					DialogResult = DialogResult.Cancel;
					break;
				case MessageBoxButtons.AbortRetryIgnore:
					DialogResult = DialogResult.Ignore;
					break;
				case MessageBoxButtons.RetryCancel:
					DialogResult = DialogResult.None;
					break;
				default:
					DialogResult = DialogResult.None;
					break;
			}
		}
	}

	public class MessageBox
	{
		public static DialogResult Show(string text)
		{
			return Show(text, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
		}

		public static DialogResult Show(string text, string caption)
		{
			return Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
		                                MessageBoxDefaultButton defaultButton)
		{
			MessageWindowForm form = new MessageWindowForm();
			return form.ShowDialog(text, caption, buttons, icon, defaultButton);
		}
	}
}