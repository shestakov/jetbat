namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public class ComboBoxDataItem
	{
		private readonly string _displayMember;

		public ComboBoxDataItem(DataGridRow dataGridRow, string displayMember)
		{
			DataGridRow = dataGridRow;
			_displayMember = displayMember;
		}

		public DataGridRow DataGridRow { get; private set; }
		public bool IsNullItem { get; private set; }

		public static ComboBoxDataItem CreateNullItem(string displayMember, string displayText)
		{
			DataGridRow dataGridRow = new DataGridRow();
			dataGridRow[displayMember] = displayText;
			ComboBoxDataItem nullItem = new ComboBoxDataItem(dataGridRow, displayMember);
			nullItem.IsNullItem = true;
			return nullItem;
		}

		public override string ToString()
		{
			return DataGridRow[_displayMember].ToString();
		}
	}
}