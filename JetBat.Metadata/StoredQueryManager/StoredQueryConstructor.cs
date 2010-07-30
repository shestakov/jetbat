using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;

namespace JetBat.Metadata.StoredQueryManager
{
	public class StoredQueryConstructor : BusinessObjectOnTableAndViewConstructor
	{
		private readonly StoredQueryConstructorSettings settings;
		private StoredQuery instance;


		public StoredQueryConstructor(StoredQueryConstructorSettings settings, MetadataContainer context)
			: base(context)
		{
			this.settings = settings;
			objectNamespace = settings.EntityNamespace;
			objectName = settings.EntityName;
			friendlyName = settings.EntityName;
		}

		#region Business object construction

		public override void Load()
		{
			instance = context.BusinessObject.OfType<StoredQuery>()
				.Include("Namespace")
				.Include("Attributes")
				.Include("Attributes.StoredProcedureParameterBindings")
				.Include("ComplexAttributes")
				.Include("Actions")
				.Include("Methods")
				.Include("DatabaseStoredProcedureParameterBindings")
				.Where(string.Format("it.name = '{0}' and it.Namespace.Name = '{1}'", objectName, objectNamespace)).FirstOrDefault();
			addNewAttributeAliases(settings, instance);
			addNewComplexAttributeAliases(settings, instance);
		}

		public override void Save()
		{
			if (context.Connection.State != ConnectionState.Open)
				context.Connection.Open();
			var transaction = context.Connection.BeginTransaction();
			persist();
			CreateMethodLoadList();
			transaction.Commit();
		}

		private void persist()
		{
			Load();
			if (instance != null)
				clear();
			else
			{
				instance = new StoredQuery
				           	{
				           		ObjectType = loadObjectType("StoredQuery"),
				           		Namespace = loadNamespace(objectNamespace),
				           		Name = objectName,
				           		FriendlyName = friendlyName,
				           		UIListCaption = settings.UIListCaption,
				           		PredefinedAttributes = settings.PredefinedAttributes
				           	};
				context.AddToBusinessObject(instance);
			}

			createAttributes();
			createComplexAttributes();
			createUserActions();
			createAdditionalMethods(instance, settings);

			context.SaveChanges();
		}

		protected override void clear()
		{
			var objectsToDelete = new List<object>();
			objectsToDelete.AddRange(instance.Attributes.ToArray());
			objectsToDelete.AddRange(instance.ComplexAttributes.ToArray());
			objectsToDelete.AddRange(instance.Actions.ToArray());
			objectsToDelete.AddRange(instance.Methods.ToArray());
			objectsToDelete.AddRange(instance.DatabaseStoredProcedureParameterBindings.ToArray());

			foreach (var objectToDelete in objectsToDelete)
			{
				context.DeleteObject(objectToDelete);
			}
		}

		protected override void createUserActions()
		{
			var action = new ObjectAction
			             	{
			             		Name = "LoadList",
			             		FriendlyName = "LoadList",
			             		UIFullText = "Загрузить",
			             		UIBriefText = "Загрузить",
			             		Enabled = true
			             	};
			instance.Actions.Add(action);
			instance.ActionLoadList = action;
		}

		protected override void createComplexAttributes()
		{
			//var aliases = new Dictionary<string, AttributeAlias>(settings.ComplexAttributeAliases.Length);
			//foreach (var attributeAlias in settings.ComplexAttributeAliases)
			//    aliases.Add(attributeAlias.Name, attributeAlias);
			//aliases.ContainsKey(foreignKey.Name) ? aliases[foreignKey.Name].FriendlyName : foreignKey.Name,
		}

		protected override void createAttributes()
		{
			//var aliases = new Dictionary<string, AttributeAlias>(settings.AttributeAliases.Length);
			//foreach (var attributeAlias in settings.AttributeAliases)
			//    aliases.Add(attributeAlias.Name, attributeAlias);
			//FriendlyName = aliases.ContainsKey(viewColumn.Name) ? aliases[viewColumn.Name].FriendlyName : viewColumn.Name,
			//UILabel = aliases.ContainsKey(viewColumn.Name) ? aliases[viewColumn.Name].UILabel : viewColumn.Name,
		}

		#endregion

		#region Method construction

		private void CreateMethodLoadList()
		{
			var procedure = context.DatabaseStoredProcedure.Include("Parameters").Where("it.Name = @ProcedureName", new ObjectParameter("ProcedureName", settings.StoredProcedureName)).FirstOrDefault();
			if (procedure == null)
				throw new Exception(string.Format("Stored procedure [{0}] not found", settings.StoredProcedureName));
			ObjectMethod method = new ObjectMethod();
			instance.Methods.Add(method);
			method.Name = "LoadList";
			method.FriendlyName = "Загрузить";
			method.ReturnsXMLErrorList = false;
			method.StoredProcedure = procedure;
			
			foreach (DatabaseStoredProcedureParameter parameter in procedure.Parameters)
			{
				var binding = new Metadata_DBStoredProcedureParameterBinding
				{
					OwnerObject = instance,
					Metadata_ObjectAttribute = null,
					AlternativeName = parameter.Name.Replace("@", "")
				};
				parameter.Bindings.Add(binding);
			}

			instance.MethodLoadList = method;
			context.SaveChanges();
		}

		#endregion

		protected override bool columnAllowsNull(string columnName)
		{
			return true;
		}
	}
}