using System.Windows;
using System.Windows.Input;

namespace JetBat.Silverlight.UI
{
	public partial class EditorWindow
	{
		public EditorWindow()
		{
			InitializeComponent();
		}

		public PlainObjectEditPanel PlainObjectEditPanel
		{
			get { return plainObjectEditPanel; }
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void Save()
		{
			plainObjectEditPanel.EditComplete += PlainObjectEditPanelOnEditComplete;
			IsEnabled = false;
			plainObjectEditPanel.CommitChanges();
		}

		private void PlainObjectEditPanelOnEditComplete(bool success)
		{
			plainObjectEditPanel.EditComplete -= PlainObjectEditPanelOnEditComplete;
			IsEnabled = true;
			if (success)
				DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void EditorWindow_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				DialogResult = false;
				e.Handled = true;
			}
			if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Enter)
			{
				Save();
				e.Handled = true;
			}
		}
	}
}