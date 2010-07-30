using System.Collections.Generic;

namespace JetBat.DatabaseSchema
{
	public class StoredProcedureSchema : INamedObject
	{
		public readonly string Description;
		private readonly string name;
		public NamedObjectReadOnlyCollection<StoredProcedureParameterSchema> Parameters;

		public StoredProcedureSchema(string name, string description, IList<StoredProcedureParameterSchema> parameters)
		{
			Parameters = new NamedObjectReadOnlyCollection<StoredProcedureParameterSchema>(parameters);
			this.name = name;
			Description = description;
		}

		#region INamedObject Members

		public string Name
		{
			get { return name; }
		}

		#endregion
	}
}