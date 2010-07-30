using System;
using System.Collections;
using System.Windows.Forms;
using JetBat.Client.Metadata.Definitions;

namespace JetBat.UI.DataControls.Grid
{
	public class ColumnIndexSorter : IComparer
	{
		#region IComparer Members

		int IComparer.Compare(Object x, Object y)
		{
			if ((x is GridViewAttributeSettings) && (y is GridViewAttributeSettings))
				return (x as GridViewAttributeSettings).ColumnIndex < (y as GridViewAttributeSettings).ColumnIndex ? -1 : 1;
			return 0;
		}

		#endregion
	}

	public class SortIndexSorter : IComparer
	{
		#region IComparer Members

		int IComparer.Compare(Object x, Object y)
		{
			if ((x is GridViewAttributeSettings) && (y is GridViewAttributeSettings))
				return (x as GridViewAttributeSettings).SortIndex < (y as GridViewAttributeSettings).SortIndex ? -1 : 1;
			return 0;
		}

		#endregion
	}

	public class GridViewColumn : DataGridViewColumn
	{
		private readonly ObjectAttributeDefinition objectAttributeDefinition;
		private SortOrder sortOrder = SortOrder.None;

		public GridViewColumn(ObjectAttributeDefinition objectAttributeDefinition)
		{
			base.CellTemplate = new DataGridViewTextBoxCell();
			this.objectAttributeDefinition = objectAttributeDefinition;
			Name = objectAttributeDefinition.Name;
			DataPropertyName = objectAttributeDefinition.Name;
			HeaderText = objectAttributeDefinition.UILabel;
			Width = objectAttributeDefinition.UIPreferredWidth;
			ValueType = objectAttributeDefinition.DataType;
			base.Visible = objectAttributeDefinition.IsUserVisible;
		}

		public ObjectAttributeDefinition ObjectAttributeDefinition
		{
			get { return objectAttributeDefinition; }
		}

		public string ColumnName
		{
			get { return objectAttributeDefinition.Name; }
		}

		public SortOrder SortOrder
		{
			get { return sortOrder; }
			set { sortOrder = value; }
		}

		public bool UserVisible
		{
			get { return objectAttributeDefinition.IsUserVisible; }
		}
	}
}