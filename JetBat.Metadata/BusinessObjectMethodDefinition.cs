namespace JetBat.Metadata
{
	public class BusinessObjectMethodDefinition
	{
		public string Name { get; set; }
		public string FriendlyName { get; set; }
		public bool ReturnsXmlErrorList { get; set; }
		public string DatabaseStoredProcedureName { get; set; }

		public BusinessObjectMethodDefinition()
		{
		}

		public BusinessObjectMethodDefinition(string name, string friendlyName, bool returnsXmlErrorList, string databaseStoredProcedureName)
		{
			Name = name;
			FriendlyName = friendlyName;
			ReturnsXmlErrorList = returnsXmlErrorList;
			DatabaseStoredProcedureName = databaseStoredProcedureName;
		}

		public override string ToString()
		{
			return string.Format("[{0}] ([{1}]) - [{2}]", Name, FriendlyName, DatabaseStoredProcedureName);
		}
	}
}