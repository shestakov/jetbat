using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public class BooleanAttributeEditor : ObjectAttributeEditor
	{
		const string TextTrue = "Да";
		const string TextFalse = "Нет";
		const string TextNull = "[не задано]";
		readonly ComboBox comboBox = new ComboBox();
		private readonly Button buttonSetNull;

		public BooleanAttributeEditor(ObjectAttribute attribute)
			: base(attribute)
		{
			Border border = new Border();
			border.Padding = new Thickness(1);
			border.Margin = new Thickness(2);
			border.BorderThickness = new Thickness(1);
			border.CornerRadius = new CornerRadius(3);
			border.BorderBrush = new SolidColorBrush(Colors.Black);
			StackPanel stackPanel = new StackPanel();
			border.Child = stackPanel;

			Grid gridHeader = new Grid();
			gridHeader.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			gridHeader.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

			TextBlock textBlock = new TextBlock();
			textBlock.Text = attribute.UILabel;
			textBlock.Margin = new Thickness(1);
			textBlock.Foreground = new SolidColorBrush(Colors.Blue);

			gridHeader.Children.Add(textBlock);
			Grid.SetColumn(textBlock, 0);

			buttonSetNull = new Button();
			buttonSetNull.Content = new Image { Source = new BitmapImage(new Uri("Images/x.png", UriKind.Relative)) };
			buttonSetNull.VerticalAlignment = VerticalAlignment.Center;
			buttonSetNull.IsTabStop = false;
			buttonSetNull.Click += (sender, args) => SetNull();
			gridHeader.Children.Add(buttonSetNull);
			Grid.SetColumn(buttonSetNull, 1);

			stackPanel.Children.Add(gridHeader);

			comboBox.BorderThickness = new Thickness(1);
			comboBox.Margin = new Thickness(1);
			comboBox.IsEnabled = !attribute.IsReadOnly;

			Border innerBorder = new Border();
			innerBorder.BorderThickness = new Thickness(0);
			innerBorder.Background = new SolidColorBrush(Colors.White);
			innerBorder.Child = comboBox;

			stackPanel.Children.Add(innerBorder);

			comboBox.Items.Add(TextTrue);
			comboBox.Items.Add(TextFalse);
			comboBox.Items.Add(TextNull);
			comboBox.SelectedItem = TextNull;

			Content = border;
		}

		public override object Value
		{
			get
			{
				if (ReferenceEquals(TextTrue, comboBox.SelectedItem)) return true;
				if (ReferenceEquals(TextFalse, comboBox.SelectedItem)) return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					isNull = true;
					this.value = null;
					comboBox.SelectedItem = TextNull;
				}
				else
				{
					this.value = value;
					if ((bool)value)
						comboBox.SelectedItem = TextTrue;
					else if (!(bool)value)
						comboBox.SelectedItem = TextFalse;
					else
						comboBox.SelectedItem = TextNull;
				}
			}
		}

		public override void SetNull()
		{
			comboBox.SelectedItem = TextNull;
			isNull = true;
		}

		public override bool IsReadOnly
		{
			set
			{
				comboBox.IsEnabled = !base.IsReadOnly;
				buttonSetNull.IsEnabled = !base.IsReadOnly;
				base.IsReadOnly = value;
			}
		}
	}
}