using System.Windows.Controls;
using System.Windows.Input;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public abstract class ObjectAttributeEditor : ContentControl, IObjectAttributeEditor
	{
		protected ObjectAttribute objectAttribute;
		protected bool isNull = true;
		protected object value;

		protected ObjectAttributeEditor(ObjectAttribute attribute)
		{
			objectAttribute = attribute;
			KeyDown += OnKeyDown;
		}

		private void OnKeyDown(object sender, KeyEventArgs args)
		{
			if (args.Key == Key.Delete && Keyboard.Modifiers == ModifierKeys.Shift)
			{
				SetNull();
				args.Handled = true;
			}
		}

		public abstract object Value { get; set; }

		public abstract void SetNull();
		public string AttributeName { get { return objectAttribute.Name; } }
		public bool IsNull { get { return isNull; } }
		public string UILabel { get { return objectAttribute.UILabel; } }
		public virtual bool IsReadOnly { get; set; }
	}
}