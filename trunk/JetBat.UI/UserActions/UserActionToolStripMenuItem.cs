using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace JetBat.UI.UserActions
{
	public partial class UserActionToolStripMenuItem : ToolStripMenuItem
	{
		#region Конструкторы

		public UserActionToolStripMenuItem()
		{
			InitializeComponent();
		}

		public UserActionToolStripMenuItem(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		#endregion

		private UserAction userAction;

		public UserAction UserAction
		{
			get { return userAction; }
			set
			{
				if (userAction != null)
				{
					userAction.BriefTextChanged -= userAction_OnBriefTextChanged;
					userAction.HintChanged -= userAction_OnHintChanged;
					userAction.ImageChanged -= userAction_OnImageChanged;
					userAction.ImageTransparentColorChanged -= userAction_OnImageTransparentColorChanged;
					userAction.EnabledChanged -= userAction_OnEnabledChanged;
					userAction.ActiveChanged -= userAction_OnActiveChanged;
					userAction.PermittedChanged -= userAction_OnPermittedChanged;
				}

				userAction = value;

				if (userAction != null)
				{
					userAction.BriefTextChanged += userAction_OnBriefTextChanged;
					userAction.HintChanged += userAction_OnHintChanged;
					userAction.ImageChanged += userAction_OnImageChanged;
					userAction.ImageTransparentColorChanged += userAction_OnImageTransparentColorChanged;
					userAction.EnabledChanged += userAction_OnEnabledChanged;
					userAction.ActiveChanged += userAction_OnActiveChanged;
					userAction.PermittedChanged += userAction_OnPermittedChanged;

					Text = userAction.BriefText;
					ToolTipText = userAction.Hint;
					Image = userAction.Image;
					ImageTransparentColor = userAction.ImageTransparentColor;
					Visible = DesignMode ? true : userAction.Enabled && userAction.Permitted;
					Enabled = userAction.Active;
				}
			}
		}

		#region Обработка событий

		private void userAction_OnBriefTextChanged(object sender, EventArgs e)
		{
			if (Text != userAction.BriefText)
				Text = userAction.BriefText;
		}

		private void userAction_OnHintChanged(object sender, EventArgs e)
		{
			if (ToolTipText != userAction.Hint)
				ToolTipText = userAction.Hint;
		}

		private void userAction_OnImageChanged(object sender, EventArgs e)
		{
			if (Image != userAction.Image)
				Image = userAction.Image;
		}

		private void userAction_OnImageTransparentColorChanged(object sender, EventArgs e)
		{
			if (ImageTransparentColor != userAction.ImageTransparentColor)
				ImageTransparentColor = userAction.ImageTransparentColor;
		}

		private void userAction_OnEnabledChanged(object sender, EventArgs e)
		{
			if (DesignMode)
				return;

			if (Visible != userAction.Enabled && userAction.Permitted)
				Visible = userAction.Enabled && userAction.Permitted;
		}

		private void userAction_OnActiveChanged(object sender, EventArgs e)
		{
			if (Enabled != userAction.Active)
				Enabled = userAction.Active;
		}

		private void userAction_OnPermittedChanged(object sender, EventArgs e)
		{
			if (DesignMode)
				return;

			if (Visible != userAction.Enabled && userAction.Permitted)
				Visible = userAction.Enabled && userAction.Permitted;
		}

		private void UserActionToolStripMenuItem_TextChanged(object sender, EventArgs e)
		{
			if ((userAction != null) && (Text != userAction.BriefText))
				Text = userAction.BriefText;
		}

		private void UserActionToolStripMenuItem_EnabledChanged(object sender, EventArgs e)
		{
			if ((userAction != null) && (Enabled != UserAction.Active))
				Enabled = userAction.Active;
		}

		private void UserActionToolStripMenuItem_VisibleChanged(object sender, EventArgs e)
		{
			if ((userAction != null) && (Enabled != userAction.Enabled && userAction.Permitted))
				Enabled = userAction.Enabled && userAction.Permitted;
		}

		private void UserActionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (userAction != null && userAction.Enabled && userAction.Permitted)
				userAction.Execute();
		}

		#endregion
	}
}