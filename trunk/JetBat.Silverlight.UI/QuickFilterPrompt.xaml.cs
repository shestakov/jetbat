using System;
using System.Windows;
using System.Windows.Controls;

namespace JetBat.Silverlight.UI
{
	public partial class QuickFilterPrompt
	{
		public QuickFilterPrompt()
		{
			InitializeComponent();
		}

		public event EventHandler FilterPatternChanged;

		private void ToggleButtonSearch_OnClick(object sender, RoutedEventArgs e)
		{
			if (true == toggleButtonSearch.IsChecked)
			{
				columnTextBox.Width = new GridLength(1, GridUnitType.Star);
				FilterPattern = textBoxPattern.Text;
				if (FilterPatternChanged != null)
					FilterPatternChanged(this, EventArgs.Empty);
				textBoxPattern.TextChanged += TextBoxPattern_OnTextChanged;
				textBoxPattern.SelectAll();
				textBoxPattern.Focus();
			}
			else
			{
				columnTextBox.Width = new GridLength(0, GridUnitType.Pixel);
				textBoxPattern.TextChanged -= TextBoxPattern_OnTextChanged;
				FilterPattern = null;
				if (FilterPatternChanged != null)
					FilterPatternChanged(this, EventArgs.Empty);
			}
		}

		private void TextBoxPattern_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			FilterPattern = textBoxPattern.Text;
			if (FilterPatternChanged != null)
				FilterPatternChanged(this, EventArgs.Empty);
		}

		public string FilterPattern { get; private set; }
	}
}
