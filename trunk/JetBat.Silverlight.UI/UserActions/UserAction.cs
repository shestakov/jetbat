using System;
using System.Windows.Media.Imaging;

namespace JetBat.Silverlight.UI.UserActions
{
	public class UserAction
	{
		private BitmapImage _bitmapImage;
		private string _hint;
		private bool _isActive = true;
		private bool _isEnabled = true;
		private bool _showText;
		private string _text;

		public void Execute()
		{
			if (OnExecute != null)
				OnExecute(this);
		}

		public bool ShowText
		{
			get { return _showText; }
			set
			{
				_showText = value;
				if (Changed != null) Changed(this, EventArgs.Empty);
			}
		}

		public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				if (Changed != null) Changed(this, EventArgs.Empty);
			}
		}

		public string Hint
		{
			get { return _hint; }
			set
			{
				_hint = value;
				if (Changed != null) Changed(this, EventArgs.Empty);
			}
		}

		public BitmapImage BitmapImage
		{
			get { return _bitmapImage; }
			set
			{
				_bitmapImage = value;
				if (Changed != null) Changed(this, EventArgs.Empty);
			}
		}

		public bool IsEnabled
		{
			get { return _isEnabled; }
			set
			{
				_isEnabled = value;
				if (Changed != null) Changed(this, EventArgs.Empty);
			}
		}

		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				_isActive = value;
				if (Changed != null) Changed(this, EventArgs.Empty);
			}
		}

		public UserActionDelegate OnExecute { get; set; }
		public event EventHandler Changed;
	}
}