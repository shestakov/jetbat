using System;
using System.Data.Common;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Entities
{
	public class DocumentInstance : ObjectInstance
	{
		protected DocumentDefinition documentDefinition;
		private int documentID;
		private int versionID;

		#region Конструкторы, инициализация, деструкторы

		internal DocumentInstance(DocumentDefinition definition, IAccessProvider sqlAccessProvider)
			: base(sqlAccessProvider, definition)
		{
			documentDefinition = definition;
		}

		#endregion

		#region Создать документ

		public void Create(DbTransaction transaction)
		{
			AttributeValueSet parameterValues = new AttributeValueSet();
			parameterValues.Add("DocumentID", DBNull.Value);
			parameterValues.Add("VersionID", DBNull.Value);
			sqlAccessProvider.ExecuteProcedure(documentDefinition.Namespace, documentDefinition.Name, "Create",
											   parameterValues,
											   null);
			documentID = Convert.ToInt32(parameterValues["DocumentID"]);
			versionID = Convert.ToInt32(parameterValues["VersionID"]);

			modified = false;
		}

		#endregion

		#region Создание версии документа для редактирования

		public void StartEdit(DbTransaction transaction)
		{
			AttributeValueSet parameterValues = new AttributeValueSet();
			parameterValues.Add("DocumentID", documentID);
			parameterValues.Add("VersionID", DBNull.Value);
			sqlAccessProvider.ExecuteProcedure(documentDefinition.Namespace, documentDefinition.Name, "StartEdit",
											   parameterValues, null);
			versionID = Convert.ToInt32(parameterValues["VersionID"]);
			modified = false;
		}

		#endregion

		#region Внесение изменений в версию документа

		public void UpdateVersion(DbTransaction transaction)
		{
			AttributeValueSet parameterValues = new AttributeValueSet();
			parameterValues.Add("VersionID", versionID);
			foreach (
				ObjectMethodParameterDefinition parameter in
					documentDefinition.Methods["UpdateVersion"].ParameterDefinitions
				)
			{
				if
					(
					(parameter.Direction == SqlParameterDirection.Input ||
					 parameter.Direction == SqlParameterDirection.InputOutput) &&
					(documentDefinition.Attributes[parameter.AttributeName] != null)
					)
				{
					parameterValues.Add(parameter.AlternativeName, getAttributeValue(parameter.AttributeName));
				}
			}
			sqlAccessProvider.ExecuteProcedure(documentDefinition.Namespace, documentDefinition.Name, "UpdateVersion",
											   parameterValues, null);
			foreach (
				ObjectMethodParameterDefinition parameter in
					documentDefinition.Methods["UpdateVersion"].ParameterDefinitions
				)
			{
				if
					(
					(parameter.Direction == SqlParameterDirection.Output ||
					 parameter.Direction == SqlParameterDirection.InputOutput) &&
					(documentDefinition.Attributes[parameter.AttributeName] != null)
					)
				{
					setAttributeValue(parameter.AttributeName, parameterValues[parameter.Name]);
				}
			}
			modified = false;
		}

		#endregion

		#region Сохранение документа

		public void ConfirmEdit(DbTransaction transaction)
		{
			AttributeValueSet parameterValues = new AttributeValueSet();
			parameterValues.Add("VersionID", versionID);
			sqlAccessProvider.ExecuteProcedure(documentDefinition.Namespace, documentDefinition.Name, "ConfirmEdit",
											   parameterValues, null);

			modified = false;
		}

		#endregion

		#region Отмена изменений

		public void CancelEdit(DbTransaction transaction)
		{
			AttributeValueSet parameterValues = new AttributeValueSet();
			parameterValues.Add("VersionID", versionID);
			sqlAccessProvider.ExecuteProcedure(documentDefinition.Namespace, documentDefinition.Name, "CancelEdit",
											   parameterValues, null);

			modified = false;
		}

		#endregion

		#region Удаление документа

		public void Delete(DbTransaction transaction)
		{
			AttributeValueSet parameterValues = new AttributeValueSet();
			parameterValues.Add("DocumentID", documentID);
			sqlAccessProvider.ExecuteProcedure(documentDefinition.Namespace, documentDefinition.Name, "Delete",
											   parameterValues,
											   null);

			modified = false;
		}

		#endregion

		#region Загрузка документа

		public ErrorMessageCollection Load(int targetDocumentID, DbTransaction transaction)
		{
			documentID = targetDocumentID;
			AttributeValueSet parameterValues = new AttributeValueSet();
			parameterValues.Add("DocumentID", documentID);
			parameterValues.Add("CurrentVersionID", DBNull.Value);
			foreach (
				ObjectMethodParameterDefinition parameter in documentDefinition.Methods["Load"].ParameterDefinitions)
			{
				if
					(
					(parameter.Direction == SqlParameterDirection.Input ||
					 parameter.Direction == SqlParameterDirection.InputOutput) &&
					(documentDefinition.Attributes[parameter.AttributeName] != null && !documentDefinition.Attributes[parameter.AttributeName].Equals(DBNull.Value))
					)
				{
					parameterValues.Add(parameter.AlternativeName, getAttributeValue(parameter.AttributeName));
				}
			}
			ErrorMessageCollection result = sqlAccessProvider.ExecuteProcedure(documentDefinition.Namespace, documentDefinition.Name, "Load",
											   parameterValues,
											   null);
			versionID = Convert.ToInt32(parameterValues["CurrentVersionID"]);
			foreach (
				ObjectMethodParameterDefinition parameter in documentDefinition.Methods["Load"].ParameterDefinitions)
			{
				if
					(
					(parameter.Direction == SqlParameterDirection.Output ||
					 parameter.Direction == SqlParameterDirection.InputOutput) &&
					(documentDefinition.Attributes[parameter.AttributeName] != null && !documentDefinition.Attributes[parameter.AttributeName].Equals(DBNull.Value))
					)
				{
					setAttributeValue(parameter.AttributeName, parameterValues[parameter.AlternativeName]);
				}
			}
			modified = false;
			return result;
		}

		#endregion

		#region Проведение документа

		public void Commit(DbTransaction transaction)
		{
			AttributeValueSet parameterValues = new AttributeValueSet();
			parameterValues.Add("DocumentID", documentID);
			sqlAccessProvider.ExecuteProcedure(documentDefinition.Namespace, documentDefinition.Name, "Commit",
											   parameterValues,
											   null);

			modified = false;
		}

		#endregion

		#region Отмена проведение документа

		public void Rollback(DbTransaction transaction)
		{
			AttributeValueSet parameterValues = new AttributeValueSet();
			parameterValues.Add("DocumentID", documentID);
			sqlAccessProvider.ExecuteProcedure(documentDefinition.Namespace, documentDefinition.Name, "Rollback",
											   parameterValues,
											   null);

			modified = false;
		}

		#endregion

		#region Method overloads without parameters

		public virtual void Create()
		{
			Create(null);
		}

		public virtual void StartEdit()
		{
			StartEdit(null);
		}

		public virtual void UpdateVersion()
		{
			UpdateVersion(null);
		}

		public virtual void ConfirmEdit()
		{
			ConfirmEdit(null);
		}

		public virtual void CancelEdit()
		{
			CancelEdit(null);
		}

		public virtual void Delete()
		{
			Delete(null);
		}

		public ErrorMessageCollection Load(int targetDocumentID)
		{
			return Load(targetDocumentID, null);
		}

		public virtual void Commit()
		{
			Commit(null);
		}

		public virtual void Rollback()
		{
			Rollback(null);
		}

		#endregion

		#region Свойства

		public int DocumentID
		{
			get { return documentID; }
		}

		public int VersionID
		{
			get { return versionID; }
		}

		public DocumentDefinition DocumentDefinition
		{
			get { return documentDefinition; }
		}

		#endregion
	}
}