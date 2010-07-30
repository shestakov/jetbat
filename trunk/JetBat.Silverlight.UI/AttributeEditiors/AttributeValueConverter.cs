using System;
using System.Globalization;
using System.Windows.Data;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public class AttributeValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			foreach (NameValue nameValue in ((PlainObjectInstance)value).Attributes)
			{
				if (nameValue.Name == (string)parameter)
					return (nameValue.Value);
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}