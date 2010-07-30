using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Metadata.Definitions
{
	public abstract class InstancedObjectDefinition : ObjectDefinition
	{
		protected InstancedObjectDefinition(InstancedObject instancedObject)
			: base(instancedObject)
		{
		}

		public abstract ObjectInstance New(IAccessProvider accessProvider);
	}
}