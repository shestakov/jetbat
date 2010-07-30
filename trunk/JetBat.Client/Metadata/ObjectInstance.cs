using System;
using System.Collections;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata
{
	public class ObjectInstance
	{
		private readonly Hashtable attributeValues;
		private readonly ObjectDefinition objectDefinition;
		protected bool modified;
		protected IAccessProvider sqlAccessProvider;

		protected ObjectInstance(IAccessProvider sqlAccessProvider, ObjectDefinition definition)
		{
			this.sqlAccessProvider = sqlAccessProvider;
			objectDefinition = definition;
			attributeValues = new Hashtable(definition.Attributes.Count);
			foreach (ObjectAttributeDefinition attrbiute in objectDefinition.Attributes)
			{
				attributeValues.Add(attrbiute.Name, DBNull.Value);
			}
		}

		public AttributeValueSet PrimaryKey
		{
			get
			{
				AttributeValueSet primaryKey = new AttributeValueSet();
				foreach (ObjectAttributeDefinition attribute in objectDefinition.Attributes)
					if (attribute.IsPrimaryKeyMember)
						primaryKey.Add(attribute.Name, attributeValues[attribute.Name]);
				return primaryKey;
			}
		}

		public virtual object this[string columnName]
		{
			get { return getAttributeValue(columnName); }
			set
			{
				if (setAttributeValue(columnName, value))
				{
					modified = true;
				}
			}
		}

		protected object getAttributeValue(string columnName)
		{
			if (!attributeValues.ContainsKey(columnName))
				throw new Exception(
					string.Format("Объект [{0}] {1} не содержит атрибута [{2}]", objectDefinition.Namespace,
								  objectDefinition.Name,
								  columnName));

			return attributeValues[columnName];
		}

		protected bool setAttributeValue(string columnName, object value)
		{
			if (!attributeValues.ContainsKey(columnName))
				throw new Exception(
					string.Format("Объект [{0}] {1} не содержит атрибута {2}", objectDefinition.Namespace,
								  objectDefinition.Name,
								  columnName));

			if (value == null)
				value = DBNull.Value;
			else if (value.GetType() != objectDefinition.Attributes[columnName].DataType && value != DBNull.Value)
				throw new Exception(
					string.Format("Несовпадение типов: атрибут {0} объекта [{1}] {2} имеет тип {3}", columnName,
								  objectDefinition.Namespace, objectDefinition.Name,
								  objectDefinition.Attributes[columnName].DataType));

			if (attributeValues[columnName] != value)
			{
				attributeValues[columnName] = value;
				return true;
			}
			return false;
		}

		protected void setPrimaryKey(AttributeValueSet primaryKeyValueSet)
		{
			foreach (ObjectAttributeDefinition attribute in objectDefinition.Attributes)
				if (attribute.IsPrimaryKeyMember)
					attributeValues[attribute.Name] = primaryKeyValueSet[attribute.Name];
		}
	}
}