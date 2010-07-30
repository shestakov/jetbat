using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;

namespace JetBat.Metadata
{
	public abstract class BusinessObjectConstructor
	{
		protected MetadataContainer context;
		protected string friendlyName;
		protected string objectName;
		protected string objectNamespace;

		protected BusinessObjectConstructor(MetadataContainer context)
		{
			this.context = context;
		}

		public abstract void Load();
		public abstract void Save();
		protected abstract void clear();
		protected abstract void createUserActions();
		protected abstract void createAttributes();
		protected abstract void createComplexAttributes();

		#region Additional method construction

		protected void createAdditionalMethods(BusinessObject objectInstance, BusinessObjectConstructorSettings settings)
		{
			if (settings.Methods == null) return;
			foreach (BusinessObjectMethodDefinition methodDefinition in settings.Methods)
			{
				DatabaseStoredProcedure procedure =
					context.DatabaseStoredProcedure.Include("Parameters").Where("it.Name = @ProcedureName",
																				new ObjectParameter("ProcedureName",
																									methodDefinition.
																										DatabaseStoredProcedureName)).
						FirstOrDefault();
				var method = new ObjectMethod
								{
									Name = methodDefinition.Name,
									FriendlyName = methodDefinition.FriendlyName,
									ReturnsXMLErrorList = methodDefinition.ReturnsXmlErrorList,
									StoredProcedure = procedure
								};
				objectInstance.Methods.Add(method);

				foreach (DatabaseStoredProcedureParameter parameter in procedure.Parameters)
				{
					var binding = new Metadata_DBStoredProcedureParameterBinding
									{
										OwnerObject = objectInstance,
										Metadata_ObjectAttribute = null,
										AlternativeName = parameter.Name.Replace("@", "")
									};
					parameter.Bindings.Add(binding);
				}
			}
		}

		#endregion

		#region Queries from metadata store

		protected DataType loadDataType(string dataTypeName)
		{
			DataType dataType = context.DataType.Where(string.Format("it.Name = '{0}'", dataTypeName)).FirstOrDefault();
			if (dataType == null)
				throw new ObjectNotFoundException(string.Format("There is no data type [{0}]", dataTypeName));
			return dataType;
		}

		protected ObjectType loadObjectType(string typeName)
		{
			ObjectType objectType = context.ObjectType.Where(string.Format("it.Name = '{0}'", typeName)).FirstOrDefault();
			if (objectType == null)
				throw new ObjectNotFoundException(string.Format("There is no object type [{0}]", typeName));
			return objectType;
		}

		protected DatabaseView loadDatabaseView(string viewName)
		{
			DatabaseView databaseView =
				context.DatabaseView.Include("Columns").Include("Columns.DataType").Where(string.Format("it.Name = '{0}'", viewName)).FirstOrDefault();
			if (databaseView == null)
				throw new ObjectNotFoundException(string.Format("There is no database view [{0}]", viewName));
			return databaseView;
		}

		protected DatabaseTable loadDatabaseTable(string tableName)
		{
			DatabaseTable databaseTable =
				context.DatabaseTable.Where(string.Format("it.Name = '{0}'", tableName)).Include("Columns").Include(
					"Columns.DataType").Include("ForeignKeysAsPrimaryKeyTable").FirstOrDefault();
			if (databaseTable == null)
				throw new ObjectNotFoundException(string.Format("There is no database table [{0}]", tableName));
			return databaseTable;
		}

		protected Namespace loadNamespace(string namespaceName)
		{
			var namespaceInstance = context.Namespace.Where(string.Format("it.Name = '{0}'", namespaceName)).FirstOrDefault();
			if (namespaceInstance == null)
			{
				namespaceInstance = new Namespace { Name = namespaceName, FriendlyName = namespaceName };
				context.AddToNamespace(namespaceInstance);
				//throw new ObjectNotFoundException(string.Format("There is no namespace [{0}]", namespaceName));
			}
			return namespaceInstance;
		}

		#endregion

		#region Business object method construction

		protected ObjectMethod createMethod(string methodName, string localizedMethodName,
											CreateParametersDelegate createParametersDelegate, bool returnsXmlErrorList,
											BusinessObject objectInstance)
		{
			string storedProcedureName = string.Format("{0}_{1}_{2}", objectNamespace, objectName, methodName);
			//TODO: Check stored procedure existense in the database
			//TODO: Verify that this procedure is referenced by only one object method?
			var procedure = context.DatabaseStoredProcedure.Include("Parameters").Where("it.Name = @ProcedureName", new ObjectParameter("ProcedureName", storedProcedureName)).FirstOrDefault();
			if (procedure != null)
			{
				var objectsToDelete = new List<object>();
				objectsToDelete.AddRange(procedure.Parameters.ToArray());
				foreach (var objectToDelete in objectsToDelete)
					context.DeleteObject(objectToDelete);
				context.SaveChanges();
			}
			else
			{
				procedure = new DatabaseStoredProcedure { Name = storedProcedureName };
				context.AddToDatabaseStoredProcedure(procedure);
			}

			createParametersDelegate(procedure);

			#region Paremeter for an error message collection

			if (returnsXmlErrorList)
			{
				var parameter = new DatabaseStoredProcedureParameter
									{
										Name = "@ErrorMessages",
										DataType = loadDataType("xml"),
										IsOutput = true,
										MaxLength = 0,
										Precision = 0,
										Scale = 0
									};
				procedure.Parameters.Add(parameter);
				var binding = new Metadata_DBStoredProcedureParameterBinding
								{
									OwnerObject = objectInstance,
									Metadata_ObjectAttribute = null,
									AlternativeName = "ErrorMessages"
								};
				parameter.Bindings.Add(binding);
			}

			#endregion

			ObjectMethod method = null;
			foreach (var objectMethod in objectInstance.Methods)
			{
				if (objectMethod.Name == methodName)
				{
					method = objectMethod;
					break;
				}
			}
			if (method == null)
			{
				method = new ObjectMethod();
				objectInstance.Methods.Add(method);
			}
			method.Name = methodName;
			method.FriendlyName = localizedMethodName;
			method.ReturnsXMLErrorList = returnsXmlErrorList;
			method.StoredProcedure = procedure;

			return method;
		}

		protected delegate void CreateParametersDelegate(DatabaseStoredProcedure procedure);

		#endregion

		#region Protected static methods

		protected static ObjectAttribute getObjectAttribute(string attributeName, BusinessObject objectInstance)
		{
			foreach (var attribute in objectInstance.Attributes)
			{
				if (attribute.Name == attributeName) return attribute;
			}
			return null;
		}

		#endregion

		protected static void addNewAttributeAliases(BusinessObjectConstructorSettings settings, BusinessObject instance)
		{
			if (settings.AttributeAliases == null)
				settings.AttributeAliases = new AttributeAlias[0];
			var aliases = new Dictionary<string, AttributeAlias>(settings.AttributeAliases.Length);
			foreach (var attributeAlias in settings.AttributeAliases)
				aliases.Add(attributeAlias.Name, attributeAlias);

			if (instance != null)
				foreach (var attribute in instance.Attributes)
					if (!aliases.ContainsKey(attribute.Name))
					{
						var alias = new AttributeAlias
										{
											Name = attribute.Name,
											FriendlyName = attribute.FriendlyName,
											UILabel = attribute.UILabel,
											IsUserVisible = attribute.IsUserVisible,
											UIPreferredWidth = attribute.UIPreferredWidth,
											UIPreferredIndex = attribute.UIPreferredIndex,
											UIAllowsMultilineText = attribute.UIAllowMultilineText
										};
						aliases.Add(attribute.Name, alias);
					}
			settings.AttributeAliases = aliases.Values.ToArray();
		}

		protected static void addNewComplexAttributeAliases(BusinessObjectConstructorSettings settings, BusinessObject instance)
		{
			if (settings.ComplexAttributeAliases == null)
				settings.ComplexAttributeAliases = new AttributeAlias[0];
			var aliases = new Dictionary<string, AttributeAlias>(settings.ComplexAttributeAliases.Length);
			foreach (var attributeAlias in settings.ComplexAttributeAliases)
				aliases.Add(attributeAlias.Name, attributeAlias);

			if (instance != null)
				foreach (var attribute in instance.ComplexAttributes)
					if (!aliases.ContainsKey(attribute.Name))
					{
						var alias = new AttributeAlias
										{
											Name = attribute.Name,
											FriendlyName = attribute.FriendlyName,
											UILabel = attribute.UILabel
										};
						aliases.Add(attribute.Name, alias);
					}
			settings.ComplexAttributeAliases = aliases.Values.ToArray();
		}
	}
}