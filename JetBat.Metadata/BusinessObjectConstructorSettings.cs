namespace JetBat.Metadata
{
	public abstract class BusinessObjectConstructorSettings
	{
		public string EntityNamespace{ get; set; }
		public string EntityName{ get; set; }
		public BusinessObjectMethodDefinition[] Methods { get; set; }
		public AttributeAlias[] AttributeAliases { get; set; }
		public AttributeAlias[] ComplexAttributeAliases { get; set; }
	}
}