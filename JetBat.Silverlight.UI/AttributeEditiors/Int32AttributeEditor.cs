using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public class Int32AttributeEditor : ObjectAttributeEditor
	{
		readonly NumericUpDown numericUpDown = new NumericUpDown();
		private readonly Button buttonSetNull;

		public Int32AttributeEditor(ObjectAttribute attribute) : base(attribute)
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
			gridHeader.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)});
			gridHeader.ColumnDefinitions.Add(new ColumnDefinition{Width = GridLength.Auto});
			
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

			numericUpDown.BorderThickness = new Thickness(1);
			numericUpDown.Margin = new Thickness(1);
			numericUpDown.DecimalPlaces = attribute.Scale;
			numericUpDown.Maximum = Int32.MaxValue;
			numericUpDown.Minimum = Int32.MinValue;
			numericUpDown.IsEnabled = !attribute.IsReadOnly;
			numericUpDown.ValueChanged += NumericUpDownOnValueChanged;
			
			Border innerBorder = new Border();
			innerBorder.BorderThickness = new Thickness(0);
			innerBorder.Background = new SolidColorBrush(Colors.White);
			innerBorder.Child = numericUpDown;

			stackPanel.Children.Add(innerBorder);

			Content = border;
		}

		private void NumericUpDownOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args)
		{
			isNull = false;
			numericUpDown.Foreground = new SolidColorBrush(Colors.Black);
		}

		public override object Value
		{
			get { return isNull ? (object)null : Convert.ToInt32(numericUpDown.Value); }
			set
			{
				if (value == null)
				{
					isNull = true;
					this.value = null;
					numericUpDown.Value = 0;
					numericUpDown.Foreground = new SolidColorBrush(Colors.Gray);
				}
				else
				{
					this.value = value;
					numericUpDown.Value = Convert.ToDouble(value);
					numericUpDown.Foreground = new SolidColorBrush(Colors.Black);
				}
			}
		}

		public override void SetNull()
		{
			numericUpDown.Foreground = new SolidColorBrush(Colors.Gray);
			isNull = true;
		}

		public override bool IsReadOnly
		{
			set
			{
				numericUpDown.IsEnabled = !base.IsReadOnly;
				buttonSetNull.IsEnabled = !base.IsReadOnly;
				base.IsReadOnly = value;
			}
		}
	}
}