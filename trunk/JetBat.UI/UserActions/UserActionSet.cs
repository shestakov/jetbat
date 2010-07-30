using System;
using System.ComponentModel;

namespace JetBat.UI.UserActions
{
	[Serializable]
	public partial class UserActionSet : Component
	{
		#region Конструкторы

		public UserActionSet()
		{
			InitializeComponent();
		}

		public UserActionSet(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		#endregion

		private UserActionCollection items = new UserActionCollection();

		public void SetActionPermittedStates()
		{
			foreach (UserAction action in items)
			{
				action.Permitted = true;
			}
		}

		#region Свойства

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public UserActionCollection Items
		{
			get { return items; }
			set { items = value; }
		}

		#endregion
	}
}