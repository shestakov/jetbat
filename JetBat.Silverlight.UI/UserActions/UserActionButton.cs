using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace JetBat.Silverlight.UI.UserActions
{
	public class UserActionButton : Button
	{
		private UserAction userAction;
		private bool showText = true;

		[DefaultValue(true)]
		public bool ShowText
		{
			get { return showText; }
			set
			{
				if (showText == value) return;
				showText = value;
				setUpButton();
			}
		}

		public UserAction UserAction
		{
			get { return userAction; }
			set
			{
				if (userAction != null)
					userAction.Changed -= UserActionOnChanged;
				userAction = value;
				showText = userAction.ShowText;
				userAction.Changed += UserActionOnChanged;
				setUpButton();
				Click += OnClick;
			}
		}

		private void OnClick(object sender, RoutedEventArgs args)
		{
			if (userAction == null) return;
			if (userAction.OnExecute != null)
				userAction.OnExecute(sender);
		}

		private void UserActionOnChanged(object sender, EventArgs args)
		{
			setUpButton();
		}

		private void setUpButton()
		{
			IsEnabled = userAction.IsActive;
			Visibility = userAction.IsEnabled ? Visibility.Visible : Visibility.Collapsed;
			ToolTipService.SetToolTip(this, userAction.Hint);
			if (showText)
			{
				StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
				Image image = new Image { Source = userAction.BitmapImage, Margin = new Thickness(2) };
				stackPanel.Children.Add(image);
				TextBlock textBlock = new TextBlock { Text = userAction.Text, Margin = new Thickness(2) };
				stackPanel.Children.Add(textBlock);
				Content = stackPanel;
			}
			else
			{
				Content = new Image { Source = userAction.BitmapImage };
			}
		}
	}
}