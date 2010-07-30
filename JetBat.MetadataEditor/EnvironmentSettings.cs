namespace JetBat.MetadataEditor
{
	public class EnvironmentSettings
	{
		public string Name { get; set; }
		public string ConnectionStringToDatabase { get; set; }
		public string ConnectionStringToMetadataStore { get; set; }
		public string OutputDirectory { get; set; }
		public string MetadataFileDirectory { get; set; }
	}
}