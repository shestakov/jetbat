using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public class TextAttributeEditor : ObjectAttributeEditor
	{
		readonly TextBox textBox = new TextBox();
		private readonly Button buttonSetNull;

		public TextAttributeEditor(ObjectAttribute attribute)
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

			textBox.Background = new SolidColorBrush(Colors.Transparent);
			textBox.BorderThickness = new Thickness(1);
			textBox.Margin = new Thickness(1);
			textBox.IsReadOnly = attribute.IsReadOnly;
			//textBox.AcceptsReturn = true;
			textBox.TextWrapping = TextWrapping.Wrap;
			textBox.TextChanged += TextBoxOnTextChanged;

			Border innerBorder = new Border();
			innerBorder.BorderThickness = new Thickness(0);
			innerBorder.Background = new SolidColorBrush(Colors.White);
			innerBorder.Child = textBox;

			stackPanel.Children.Add(innerBorder);

			textBox.SetBinding(TextBox.TextProperty, new Binding
														{
															Converter = new AttributeValueConverter(),
															ConverterParameter = attribute.Name
														});

			Content = border;
		}

		private void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
		{
			isNull = false;
			textBox.Foreground = new SolidColorBrush(Colors.Black);
		}

		public override object Value
		{
			get { return isNull ? null : textBox.Text; }
			set
			{
				if (value == null)
				{
					isNull = true;
					this.value = null;
					textBox.Text = string.Empty;
					textBox.Foreground = new SolidColorBrush(Colors.Gray);
				}
				else
				{
					this.value = value;
					textBox.Text = (string)value;
					textBox.Foreground = new SolidColorBrush(Colors.Black);
				}
			}
		}

		public override void SetNull()
		{
			isNull = true;
			textBox.Foreground = new SolidColorBrush(Colors.Gray);
		}

		public override bool IsReadOnly
		{
			set
			{
				textBox.IsReadOnly = base.IsReadOnly;
				buttonSetNull.IsEnabled = !base.IsReadOnly;
				base.IsReadOnly = value;
			}
		}
	}
}