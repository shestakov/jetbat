using System;
using System.Globalization;
using System.Windows.Data;

namespace JetBat.Silverlight.UI
{
	public class DataGridRowIndexConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			DataGridRow dataGridRow = value as DataGridRow;
			string index = parameter as string;
			object columnValue = dataGridRow[index];
			if (columnValue is DateTime)
			{
				DateTime dateTimeValue = (DateTime)columnValue;
				if (dateTimeValue.Date == dateTimeValue)
					return dateTimeValue.ToString("dd.MM.yyyy");
				return dateTimeValue.ToString("dd.MM.yyyy HH:mm:ss");
			}
			if (columnValue is Boolean && !targetType.Equals(typeof(Boolean?)))
			{
				return (bool)columnValue ? "да" : "нет";
			}
			return columnValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return new PropertyValueChange(parameter as string, value);
		}
	}

	public class PropertyValueChange
	{
		private readonly string propertyName;
		private readonly object value;

		public object Value
		{
			get { return value; }
		}

		public string PropertyName
		{
			get { return propertyName; }
		}

		public PropertyValueChange(string propertyName, object value)
		{
			this.propertyName = propertyName;
			this.value = value;
		}
	}
}