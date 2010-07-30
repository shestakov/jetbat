using System;
using System.Data.Common;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Entities
{
	public class PlainObjectInstance : ObjectInstance
	{
		#region Конструкторы, инициализация, деструкторы

		internal PlainObjectInstance(PlainObjectDefinition definition, IAccessProvider sqlAccessProvider)
			: base(sqlAccessProvider, definition)
		{
			plainObjectDefinition = definition;
		}

		#endregion

		#region Загрузка объекта

		public ErrorMessageCollection Load(AttributeValueSet primaryKeyValueSet, DbTransaction transaction)
		{
			setPrimaryKey(primaryKeyValueSet);
			ObjectMethodDefinition method = plainObjectDefinition.Methods["Load"];
			if (method == null) throw new NullReferenceException("Вызов метода Load невозможен: метод не найден.");
			ErrorMessageCollection result = executeProcedure(method, transaction);
			modified = false;
			return result;
		}

		#endregion

		#region Добавление объекта

		public ErrorMessageCollection Insert(DbTransaction transaction)
		{
			ObjectMethodDefinition method = plainObjectDefinition.Methods["Insert"];
			if (method == null) throw new NullReferenceException("Вызов метода Insert невозможен: метод не найден.");
			ErrorMessageCollection result = executeProcedure(method, transaction);
			modified = false;
			return result;
		}

		#endregion

		#region Изменение объекта

		public ErrorMessageCollection Update(DbTransaction transaction)
		{
			if (!modified) return new ErrorMessageCollection();
			ObjectMethodDefinition method = plainObjectDefinition.Methods["Update"];
			if (method == null) throw new NullReferenceException("Вызов метода Update невозможен: метод не найден.");
			ErrorMessageCollection result = executeProcedure(method, transaction);
			modified = false;
			return result;
		}

		#endregion

		#region Удаление объекта

		public ErrorMessageCollection Delete(DbTransaction transaction)
		{
			ObjectMethodDefinition method = plainObjectDefinition.Methods["Delete"];
			if (method == null) throw new NullReferenceException("Вызов метода Delete невозможен: метод не найден.");
			ErrorMessageCollection result = executeProcedure(method, transaction);
			modified = false;
			return result;
		}

		#endregion

		#region Stored procedure call

		private AttributeValueSet prepareProcedureParameterSet(ObjectMethodDefinition methodDefinition)
		{
			AttributeValueSet result = new AttributeValueSet();
			foreach (ObjectMethodParameterDefinition parameter in methodDefinition.ParameterDefinitions)
			{
				if
					(
					(parameter.Direction == SqlParameterDirection.Input ||
					 parameter.Direction == SqlParameterDirection.InputOutput) &&
					(plainObjectDefinition.Attributes[parameter.AttributeName] != null)
					)
				{
					result.Add(parameter.AlternativeName, getAttributeValue(parameter.AttributeName)); //*
				}
			}
			return result;
		}

		private ErrorMessageCollection executeProcedure(ObjectMethodDefinition methodDefinition,
		                                                DbTransaction transaction)
		{
			AttributeValueSet parameterValues = prepareProcedureParameterSet(methodDefinition);
			ErrorMessageCollection result =
				sqlAccessProvider.ExecuteProcedure(plainObjectDefinition.Namespace, plainObjectDefinition.Name,
				                                   methodDefinition.Name, parameterValues, transaction);
			foreach (ObjectMethodParameterDefinition parameter in methodDefinition.ParameterDefinitions)
			{
				if
					(
					(parameter.Direction == SqlParameterDirection.Input ||
					 parameter.Direction == SqlParameterDirection.InputOutput) &&
					(plainObjectDefinition.Attributes[parameter.AttributeName] != null)
					)
				{
					setAttributeValue(parameter.AttributeName, parameterValues[parameter.AlternativeName]);
				}
			}
			return result;
		}

		#endregion

		#region Method overloads without transactions

		public ErrorMessageCollection Load(AttributeValueSet primaryKeyValueSet)
		{
			return Load(primaryKeyValueSet, null);
		}

		public virtual ErrorMessageCollection Insert()
		{
			return Insert(null);
		}

		public virtual ErrorMessageCollection Update()
		{
			return Update(null);
		}

		public virtual ErrorMessageCollection Delete()
		{
			return Delete(null);
		}

		#endregion

		#region Misc

		private readonly PlainObjectDefinition plainObjectDefinition;

		public PlainObjectDefinition PlainObjectDefinition
		{
			get { return plainObjectDefinition; }
		}

		#endregion
	}
}