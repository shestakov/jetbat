// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable ConvertToAutoProperty
using System;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;
using System.Xml.Serialization;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;

namespace JetBat.UI.DataControls.Grid
{
	public class GridViewSettings : ApplicationSettingsBase
	{
		[UserScopedSetting]
		public GridViewListSettingsCollection GridViewListSettingsCollection
		{
			get { return (GridViewListSettingsCollection) this["GridViewListSettingsCollection"]; }
			set { this["GridViewListSettingsCollection"] = value; }
		}
	}

	[Serializable]
	public class GridViewAttributeSettings
	{
		public int ColumnIndex;
		public string ColumnLabel;
		public string ColumnName;
		public bool ColumnVisible;
		public int ColumnWidth = 100;
		public int SortIndex;
		public SortOrder SortOrder = SortOrder.None;

		public override string ToString()
		{
			string result = ColumnLabel;
			if (SortOrder != SortOrder.None)
				result += " [" + (SortOrder == SortOrder.Ascending ? "À > ß" : "ß > À") + "]";
			return result;
		}
	}

	[Serializable]
	[XmlInclude(typeof (GridViewAttributeSettings))]
	public class GridViewListSettings
	{
		public bool AllowCellSelection;
		public bool AllowColumnReorder = true;
		public bool AllowColumnWidthChange = true;
		public bool AllowHeaderHeightChange = true;

		private ArrayList attributeSettings = new ArrayList();
		private string listName;
		private string listNamespace;

		public string ListNamespace
		{
			get { return listNamespace; }
			set { listNamespace = value; }
		}

		public string ListName
		{
			get { return listName; }
			set { listName = value; }
		}

		public ArrayList AttributeSettings
		{
			get { return attributeSettings; }
			set { attributeSettings = value; }
		}
	}

	[Serializable]
	[XmlInclude(typeof (GridViewAttributeSettings))]
	[XmlInclude(typeof (GridViewListSettings))]
	public class GridViewListSettingsCollection : CollectionBase
	{
		public GridViewListSettings this[int index]
		{
			get { return (GridViewListSettings) List[index]; }
		}

		public GridViewListSettings this[string listNamespace, string listName]
		{
			get
			{
				foreach (GridViewListSettings obj in List)
					if (obj.ListNamespace == listNamespace && obj.ListName == listName)
						return obj;
				return null;
			}
			set
			{
				bool found = false;
				for (int i = 0; i < List.Count; ++i)
				{
					GridViewListSettings obj = List[i] as GridViewListSettings;
					if (obj != null && obj.ListNamespace == listNamespace && obj.ListName == listName)
					{
						List[i] = value;
						found = true;
						break;
					}
				}
				if (!found)
				{
					List.Add(value);
				}
			}
		}

		public void Fix(IAccessAdapter accessAdapter, string listNamespace, string listName)
		{
			ArrayList listSettingsToDelete = new ArrayList();

			foreach (GridViewListSettings currentGridViewListSettings in List)
			{
				ObjectListViewDefinition objectListViewDefinition =
					accessAdapter.MetadataStore.Get<ObjectListViewDefinition>(currentGridViewListSettings.ListNamespace,
					                                                          currentGridViewListSettings.ListName);

				if (objectListViewDefinition == null)
				{
					listSettingsToDelete.Add(currentGridViewListSettings);
					continue;
				}

				ArrayList attributeSettingsToDelete = new ArrayList();
				foreach (GridViewAttributeSettings attributeSettings in currentGridViewListSettings.AttributeSettings)
				{
					if (objectListViewDefinition.Attributes[attributeSettings.ColumnName] == null)
					{
						attributeSettingsToDelete.Add(attributeSettings);
					}
				}

				foreach (GridViewAttributeSettings attributeSettings in attributeSettingsToDelete)
				{
					currentGridViewListSettings.AttributeSettings.Remove(attributeSettings);
				}

				bool attributeFound = false;
				foreach (ObjectAttributeDefinition attribute in objectListViewDefinition.Attributes)
				{
					foreach (GridViewAttributeSettings attributeSettings in currentGridViewListSettings.AttributeSettings)
					{
						if (attributeSettings.ColumnName == attribute.Name)
						{
							attributeFound = true;
							break;
						}
					}

					if (!attributeFound)
					{
						GridViewAttributeSettings attributeSettings = new GridViewAttributeSettings();
						attributeSettings.ColumnName = attribute.Name;
						attributeSettings.ColumnLabel = attribute.UILabel;
						attributeSettings.ColumnVisible = attribute.IsUserVisible;
						attributeSettings.SortOrder = SortOrder.None;
						currentGridViewListSettings.AttributeSettings.Add(attributeSettings);
						attributeSettings.ColumnIndex = currentGridViewListSettings.AttributeSettings.Count - 1;
						attributeSettings.SortIndex = currentGridViewListSettings.AttributeSettings.Count - 1;
					}
				}
			}

			foreach (GridViewListSettings currentGridViewListSettings in listSettingsToDelete)
			{
				List.Remove(currentGridViewListSettings);
			}

			bool found = false;
			foreach (GridViewListSettings currentGridViewListSettings in List)
			{
				if (currentGridViewListSettings.ListNamespace == listNamespace && currentGridViewListSettings.ListName == listName)
				{
					found = true;
					break;
				}
			}

			if (!found)
			{
				ObjectListViewDefinition objectListViewDefinition =
					accessAdapter.MetadataStore.Get<ObjectListViewDefinition>(listNamespace, listName);

				GridViewListSettings currentGridViewListSettings = new GridViewListSettings();
				currentGridViewListSettings.ListNamespace = listNamespace;
				currentGridViewListSettings.ListName = listName;
				if (objectListViewDefinition == null)
					return;

				foreach (ObjectAttributeDefinition attribute in objectListViewDefinition.Attributes)
				{
					GridViewAttributeSettings attributeSettings = new GridViewAttributeSettings();
					attributeSettings.ColumnName = attribute.Name;
					attributeSettings.ColumnLabel = attribute.UILabel;
					attributeSettings.ColumnVisible = attribute.IsUserVisible;
					attributeSettings.SortOrder = SortOrder.None;
					currentGridViewListSettings.AttributeSettings.Add(attributeSettings);
					attributeSettings.ColumnIndex = currentGridViewListSettings.AttributeSettings.Count - 1;
					attributeSettings.SortIndex = currentGridViewListSettings.AttributeSettings.Count - 1;
				}
				List.Add(currentGridViewListSettings);
			}
		}

		public int Add(GridViewListSettings value)
		{
			return (List.Add(value));
		}

		public void Remove(GridViewListSettings value)
		{
			List.Remove(value);
		}
	}
}

// ReSharper restore ConvertToAutoProperty
// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore SuggestUseVarKeywordEvident