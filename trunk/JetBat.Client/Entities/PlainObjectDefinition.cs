using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Entities
{
	public sealed class PlainObjectDefinition : InstancedObjectDefinition
	{
		#region Members

		public readonly string UIEditorName;

		#endregion

		#region Initialization

		public PlainObjectDefinition(PlainObject plainObject)
			: base(plainObject)
		{
			UIEditorName = plainObject.UIEditorName;
		}

		#endregion

		public override ObjectInstance New(IAccessProvider accessProvider)
		{
			return new PlainObjectInstance(this, accessProvider);
		}
	}
}