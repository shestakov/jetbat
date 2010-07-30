using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public class DateTimeAttributeEditor : ObjectAttributeEditor
	{
		readonly DatePicker datePicker = new DatePicker();
		private readonly Button buttonSetNull;

		public DateTimeAttributeEditor(ObjectAttribute attribute) : base(attribute)
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
			
			datePicker.BorderThickness = new Thickness(1);
			datePicker.Margin = new Thickness(1);
			datePicker.IsEnabled = !attribute.IsReadOnly;

			Border innerBorder = new Border();
			innerBorder.BorderThickness = new Thickness(0);
			innerBorder.Background = new SolidColorBrush(Colors.White);
			innerBorder.Child = datePicker;

			stackPanel.Children.Add(innerBorder);

			Content = border;
		}

		public override object Value
		{
			get { return datePicker.SelectedDate; }
			set
			{
				if (value == null)
				{
					isNull = true;
					this.value = null;
					datePicker.SelectedDate = null;
				}
				else
				{
					this.value = value;
					datePicker.SelectedDate = Convert.ToDateTime(value);
				}
			}
		}

		public override void SetNull()
		{
			isNull = true;
			datePicker.SelectedDate = null;
		}

		public override bool IsReadOnly
		{
			set
			{
				datePicker.IsEnabled = !base.IsReadOnly;
				buttonSetNull.IsEnabled = !base.IsReadOnly;
				base.IsReadOnly = value;
			}
		}
	}
}