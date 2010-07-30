using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Entities
{
	public sealed class DocumentDefinition : InstancedObjectDefinition
	{
		#region Конструкторы и инициализация

		public DocumentDefinition(Document document)
			: base(document)
		{
		}

		#endregion

		public override ObjectInstance New(IAccessProvider accessProvider)
		{
			return new DocumentInstance(this, accessProvider);
		}
	}
}