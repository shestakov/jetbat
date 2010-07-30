using System.Collections.Generic;
using System.ComponentModel;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Silverlight.UI
{
	public class DataGridRow : INotifyPropertyChanged
	{
		public NamedObjectCollection<NameValue> Attributes
		{
			get
			{
				NamedObjectCollection<NameValue> result = new NamedObjectCollection<NameValue>();
				foreach (KeyValuePair<string, object> keyValuePair in data)
				{
					result.Add(new NameValue { Name = keyValuePair.Key, Value = keyValuePair.Value });
				}
				return result;
			}
		}

		public bool CheckColumnValueMatch(NamedObjectCollection<NameValue> columnValues)
		{
			foreach (NameValue columnValue in columnValues)
			{
				if (!data.ContainsKey(columnValue.Name) || !data[columnValue.Name].Equals(columnValue.Value))
				{
					return false;
				}
			}
			return true;
		}

		private readonly Dictionary<string, object> data = new Dictionary<string, object>();

		public object this[string index]
		{
			get { return data.ContainsKey(index) ? data[index] : ""; }
			set
			{
				data[index] = value;
				OnPropertyChanged("Data");
			}
		}

		public object Data
		{
			get
			{
				return this;
			}
			set
			{
				PropertyValueChange setter = value as PropertyValueChange;
				data[setter.PropertyName] = setter.Value;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}
	}
}