using System;
using System.Collections;
using System.ComponentModel;

namespace JetBat.UI.UserActions
{
	[Designer("System.ComponentModel.Design.CollectionEditor.CollectionForm")]
	[Serializable]
	public sealed class UserActionCollection : CollectionBase
	{
		#region Делегаты и события

		#region Delegates

		public delegate void UserActionEventHandler(object sender, UserActionEventArgs eventArgs);

		#endregion

		public event UserActionEventHandler ActionNameChanged;
		public event UserActionEventHandler ActionTextChanged;
		public event UserActionEventHandler ActionBriefTextChanged;
		public event UserActionEventHandler ActionHintChanged;
		public event UserActionEventHandler ActionImageChanged;
		public event UserActionEventHandler ActionShortcutChanged;
		public event UserActionEventHandler ActionEnabledChanged;
		public event UserActionEventHandler ActionActiveChanged;
		public event UserActionEventHandler ActionTagChanged;
		public event UserActionEventHandler ActionPermittedChanged;
		public event UserActionEventHandler ActionExecute;

		public event UserActionEventHandler ActionAdded;
		public event UserActionEventHandler ActionRemoved;
		public event UserActionEventHandler ActionMoved;
		public event UserActionEventHandler ActionSetChanged;

		private void Action_OnNameChanging(object sender, UserAction.NameChangingEventArgs e)
		{
			e.Allowed = true;

			foreach (UserAction action in InnerList)
			{
				if ((action.Name == e.NewName) && (sender is UserAction) && (action.Name != (sender as UserAction).Name))
				{
					e.Allowed = false;
					return;
				}
			}
		}

		private void Action_OnNameChanged(object sender, EventArgs e)
		{
			if (ActionNameChanged != null)
			{
				ActionNameChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnTextChanged(object sender, EventArgs e)
		{
			if (ActionTextChanged != null)
			{
				ActionTextChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnBriefTextChanged(object sender, EventArgs e)
		{
			if (ActionBriefTextChanged != null)
			{
				ActionBriefTextChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnHintChanged(object sender, EventArgs e)
		{
			if (ActionHintChanged != null)
			{
				ActionHintChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnImageChanged(object sender, EventArgs e)
		{
			if (ActionImageChanged != null)
			{
				ActionImageChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnShortcutChanged(object sender, EventArgs e)
		{
			if (ActionShortcutChanged != null)
			{
				ActionShortcutChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnEnabledChanged(object sender, EventArgs e)
		{
			if (ActionEnabledChanged != null)
			{
				ActionEnabledChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnActiveChanged(object sender, EventArgs e)
		{
			if (ActionActiveChanged != null)
			{
				ActionActiveChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnTagChanged(object sender, EventArgs e)
		{
			if (ActionTagChanged != null)
			{
				ActionTagChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnPermittedChanged(object sender, EventArgs e)
		{
			if (ActionPermittedChanged != null)
			{
				ActionPermittedChanged(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		private void Action_OnExecute(object sender, EventArgs e)
		{
			if (ActionExecute != null)
			{
				ActionExecute(this, new UserActionEventArgs((UserAction) sender));
			}
		}

		public class UserActionEventArgs : EventArgs
		{
			public UserAction Action;

			public UserActionEventArgs(UserAction action)
			{
				Action = action;
			}
		}

		#endregion

		#region Операции коллекции

		public UserAction this[int index]
		{
			get { return (UserAction) InnerList[index]; }
		}

		public UserAction this[string name]
		{
			get
			{
				foreach (UserAction action in InnerList)
				{
					if (action.Name == name)
					{
						return action;
					}
				}
				return null;
			}
		}

		public void AddPersistent(UserAction action)
		{
			action.Persistent = true;
			Add(action);
		}

		public void Add(UserAction action)
		{
			foreach (UserAction userAction in InnerList)
			{
				if ((userAction.Name == action.Name) && (action.Name != null))
				{
					throw new Exception("Нарушение уникальности имени действия");
				}
			}

			InnerList.Add(action);

			action.NameChanging += Action_OnNameChanging;
			action.NameChanged += Action_OnNameChanged;
			action.TextChanged += Action_OnTextChanged;
			action.BriefTextChanged += Action_OnBriefTextChanged;
			action.HintChanged += Action_OnHintChanged;
			action.ImageChanged += Action_OnImageChanged;
			action.ShortcutChanged += Action_OnShortcutChanged;
			action.EnabledChanged += Action_OnEnabledChanged;
			action.ActiveChanged += Action_OnActiveChanged;
			action.TagChanged += Action_OnTagChanged;
			action.PermittedChanged += Action_OnPermittedChanged;
			action.ActionExecute += Action_OnExecute;

			foreach (UserAction userAction in InnerList)
			{
				userAction.setSequenceNumber(InnerList.IndexOf(userAction));
			}

			if (ActionAdded != null) ActionAdded(this, new UserActionEventArgs(action));
			if (ActionSetChanged != null) ActionSetChanged(this, new UserActionEventArgs(action));
		}

		public void AddRange(UserAction[] actions)
		{
			foreach (UserAction action in actions) Add(action);
			if (ActionSetChanged != null) ActionSetChanged(this, new UserActionEventArgs(null));
		}

		public void Remove(UserAction action)
		{
			if (action.Persistent)
			{
				throw new Exception(string.Format("Попытка удаления фиксированного действия: {0}", action.Name));
			}

			InnerList.Remove(action);

			action.NameChanging -= Action_OnNameChanging;
			action.NameChanged -= Action_OnNameChanged;
			action.TextChanged -= Action_OnTextChanged;
			action.BriefTextChanged -= Action_OnBriefTextChanged;
			action.HintChanged -= Action_OnHintChanged;
			action.ImageChanged -= Action_OnImageChanged;
			action.ShortcutChanged -= Action_OnShortcutChanged;
			action.EnabledChanged -= Action_OnEnabledChanged;
			action.ActiveChanged -= Action_OnActiveChanged;
			action.TagChanged -= Action_OnTagChanged;
			action.PermittedChanged -= Action_OnPermittedChanged;
			action.ActionExecute -= Action_OnExecute;
			action.setSequenceNumber(-1);

			foreach (UserAction userAction in InnerList)
			{
				userAction.setSequenceNumber(InnerList.IndexOf(userAction));
			}

			if (ActionRemoved != null) ActionRemoved(this, new UserActionEventArgs(action));
			if (ActionSetChanged != null) ActionSetChanged(this, new UserActionEventArgs(action));
		}

		public void Move(UserAction action, int index)
		{
			InnerList.Remove(action);
			InnerList.Insert(index, action);

			if (ActionMoved != null) ActionMoved(this, new UserActionEventArgs(action));
			if (ActionSetChanged != null) ActionSetChanged(this, new UserActionEventArgs(action));
		}

		public new void Clear()
		{
			ArrayList actionsToRemove = new ArrayList(InnerList.Count);
			foreach (UserAction action in InnerList)
				if (action.Persistent)
					actionsToRemove.Add(action);

			foreach (UserAction action in actionsToRemove)
				InnerList.Remove(action);
			if (ActionSetChanged != null) ActionSetChanged(this, new UserActionEventArgs(null));
		}

		#endregion
	}
}