using System.Data;
using JetBat.Client.Metadata.Definitions;

namespace JetBat.UI.InputControls
{
	public class DataObjectListItem
	{
		private readonly DataRow dataRow;
		private readonly ObjectDefinition plainObjectListViewDefinition;
		private string text;

		public DataObjectListItem(DataRow dataRow, ObjectDefinition plainObjectListViewDefinition)
		{
			this.dataRow = dataRow;
			this.plainObjectListViewDefinition = plainObjectListViewDefinition;
		}

		public DataObjectListItem(string text, DataRow dataRow, ObjectDefinition plainObjectListViewDefinition)
		{
			this.text = text;
			this.dataRow = dataRow;
			this.plainObjectListViewDefinition = plainObjectListViewDefinition;
		}

		public DataRow DataRow
		{
			get { return dataRow; }
		}

		public ObjectDefinition PlainObjectListViewDefinition
		{
			get { return plainObjectListViewDefinition; }
		}

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public override string ToString()
		{
			return text;
		}
	}
}