namespace JetBat.Metadata.ConstructionProject
{
	public class ConstructionProjectItem
	{
		public ConstructionProjectItemType ItemType { get; set; }
		public string ObjectNamespace { get; set; }
		public string ObjectName { get; set; }
		public string FileName { get; set; }
		public string ParentObjectNamespace { get; set; }
		public string ParentObjectName { get; set; }
	}
}