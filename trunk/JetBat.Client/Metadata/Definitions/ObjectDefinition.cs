using System.Collections.Generic;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Metadata.Definitions
{
	public abstract class ObjectDefinition
	{
		#region Атрибуты

		public NamedObjectReadOnlyCollection<ObjectActionDefinition> Actions { get; private set; }
		public NamedObjectReadOnlyCollection<ObjectAttributeDefinition> Attributes { get; private set; }
		public NamedObjectReadOnlyCollection<ObjectComplexAttributeDefinition> ComplexAttributes { get; private set; }
		public string Description { get; private set; }
		public string FriendlyName { get; private set; }
		public NamedObjectReadOnlyCollection<ObjectMethodDefinition> Methods { get; private set; }
		public string Name { get; private set; }
		public string Namespace { get; private set; }
		public int ObjectType { get; private set; }
		protected readonly List<ObjectAttributeDefinition> innerAttributeList;

		#endregion

		#region Инициализация

		internal ObjectDefinition(BusinessObject businessObject)
		{
			Namespace = businessObject.ObjectNamespace;
			Name = businessObject.ObjectName;
			ObjectType = businessObject.ObjectType;
			FriendlyName = businessObject.FriendlyName;
			Description = businessObject.Description;

			List<ObjectAttributeDefinition> attributes = new List<ObjectAttributeDefinition>();
			foreach (ObjectAttribute objectAttribute in businessObject.Attributes)
				attributes.Add(new ObjectAttributeDefinition(objectAttribute));
			innerAttributeList = new List<ObjectAttributeDefinition>(attributes);

			Attributes = new NamedObjectReadOnlyCollection<ObjectAttributeDefinition>(innerAttributeList);
			IList<ObjectComplexAttributeDefinition> complexAttributes = new List<ObjectComplexAttributeDefinition>(businessObject.ComplexAttributes.Count);
			foreach (ObjectComplexAttribute complexAttribute in businessObject.ComplexAttributes)
				complexAttributes.Add(new ObjectComplexAttributeDefinition(complexAttribute));
			ComplexAttributes = new NamedObjectReadOnlyCollection<ObjectComplexAttributeDefinition>(complexAttributes);

			IList<ObjectActionDefinition> actions = new List<ObjectActionDefinition>(businessObject.Actions.Count);
			foreach (ObjectAction action in businessObject.Actions)
				actions.Add(new ObjectActionDefinition(action));
			Actions = new NamedObjectReadOnlyCollection<ObjectActionDefinition>(actions);

			IList<ObjectMethodDefinition> methods = new List<ObjectMethodDefinition>(businessObject.Methods.Count);
			foreach (ObjectMethod method in businessObject.Methods)
				methods.Add(new ObjectMethodDefinition(method));
			Methods = new NamedObjectReadOnlyCollection<ObjectMethodDefinition>(methods);
		}

		#endregion

		#region Свойства

		public QualifiedName QualifiedName
		{
			get { return new QualifiedName(Namespace, Name); }
		}

		#endregion
	}
}