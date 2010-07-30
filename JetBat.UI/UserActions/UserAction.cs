// ReSharper disable ConvertToAutoProperty
// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable UseObjectOrCollectionInitializer
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using JetBat.Client.Metadata.Definitions;

namespace JetBat.UI.UserActions
{
	[Serializable]
	[DesignTimeVisible(false)]
	[DefaultProperty("Name")]
	public partial class UserAction : Component
	{
		#region Атрибуты

		private bool active = true;
		private string briefText;
		private bool enabled;
		private string hint;
		private Image image;
		private Color imageTransparentColor;
		private string name;
		private bool permitted = true;
		private Keys shortcut;
		private object tag;
		private string text;

		#endregion

		#region Конструкторы

		public UserAction()
		{
			InitializeComponent();
		}

		public UserAction(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		#endregion

		#region Порядковый номер

		private int squenceNumber;

		public int SquenceNumber
		{
			get { return squenceNumber; }
		}

		internal void setSequenceNumber(int value)
		{
			squenceNumber = value;
			if (SequenceNumberChanged != null)
			{
				SequenceNumberChanged(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Persistence

		private bool persistent;

		public bool Persistent
		{
			get { return persistent; }
			set { persistent = value; }
		}

		#endregion

		#region Выполнение действия

		public void Execute()
		{
			if (enabled && permitted && (ActionExecute != null))
			{
				ActionExecute(this, EventArgs.Empty);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler ActionExecute;

		#endregion

		#region Свойства

		[Browsable(true)]
		[Category("Design")]
		public string Name
		{
			get { return name; }
			set
			{
				if (name != value)
				{
					if (NameChanging != null)
					{
						NameChangingEventArgs e = new NameChangingEventArgs();
						e.NewName = value;
						NameChanging(this, e);
						if (!e.Allowed)
						{
							throw new Exception("Имя действия должно быть уникальным!");
						}
					}
					name = value;
					if (NameChanged != null)
					{
						NameChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		public string Text
		{
			get { return text; }
			set
			{
				if (text == value) return;
				text = value;
				if (TextChanged != null) TextChanged(this, EventArgs.Empty);
			}
		}

		public string BriefText
		{
			get { return briefText; }
			set
			{
				if (briefText == value) return;
				briefText = value;
				if (BriefTextChanged != null) BriefTextChanged(this, EventArgs.Empty);
			}
		}

		public string Hint
		{
			get { return hint; }
			set
			{
				if (hint == value) return;
				hint = value;
				if (HintChanged != null) HintChanged(this, EventArgs.Empty);
			}
		}

		public Image Image
		{
			get { return image; }
			set
			{
				if (image == value) return;
				image = value;
				if (ImageChanged != null) ImageChanged(this, EventArgs.Empty);
			}
		}

		public Color ImageTransparentColor
		{
			get { return imageTransparentColor; }
			set
			{
				if (imageTransparentColor == value) return;
				imageTransparentColor = value;
				if (ImageTransparentColorChanged != null) ImageTransparentColorChanged(this, EventArgs.Empty);
			}
		}

		public Keys Shortcut
		{
			get { return shortcut; }
			set
			{
				if (shortcut == value) return;
				shortcut = value;
				if (ShortcutChanged != null) ShortcutChanged(this, EventArgs.Empty);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public object Tag
		{
			get { return tag; }
			set
			{
				if (tag == value) return;
				tag = value;
				if (TagChanged != null) TagChanged(this, EventArgs.Empty);
			}
		}

		public bool Enabled
		{
			get { return enabled; }
			set
			{
				if (enabled == value) return;
				enabled = value;
				if (EnabledChanged != null) EnabledChanged(this, EventArgs.Empty);
			}
		}

		public bool Active
		{
			get { return active; }
			set
			{
				if (active == value) return;
				active = value;
				if (ActiveChanged != null) ActiveChanged(this, EventArgs.Empty);
			}
		}

		public bool Permitted
		{
			get { return permitted; }
			set
			{
				if (permitted == value) return;
				permitted = value;
				if (PermittedChanged != null) PermittedChanged(this, EventArgs.Empty);
			}
		}

		public void SetupFromObjectActionDefinition(ObjectActionDefinition objectActionDefinition)
		{
			if (objectActionDefinition == null) return;
			Text = objectActionDefinition.UIFullText;
			BriefText = objectActionDefinition.UIBriefText;
		}

		#endregion

		#region События: изменение свойств

		#region Delegates

		public delegate void NameChangingEventHandler(object sender, NameChangingEventArgs e);

		#endregion

		public event NameChangingEventHandler NameChanging;
		public event EventHandler NameChanged;
		public event EventHandler TextChanged;
		public event EventHandler BriefTextChanged;
		public event EventHandler HintChanged;
		public event EventHandler ImageChanged;
		public event EventHandler ImageTransparentColorChanged;
		public event EventHandler ShortcutChanged;
		public event EventHandler TagChanged;
		public event EventHandler SequenceNumberChanged;
		public event EventHandler EnabledChanged;
		public event EventHandler ActiveChanged;
		public event EventHandler PermittedChanged;

		public class NameChangingEventArgs : EventArgs
		{
			public bool Allowed;
			public string NewName;
		}

		#endregion
	}

	public class UserActionOrderIndexSorter : IComparer
	{
		#region IComparer Members

		int IComparer.Compare(Object x, Object y)
		{
			if ((x is UserAction) && (y is UserAction))
				return (x as UserAction).SquenceNumber < (y as UserAction).SquenceNumber ? -1 : 1;
			return 0;
		}

		#endregion
	}
}

// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore SuggestUseVarKeywordEvident
// ReSharper restore ConvertToAutoProperty