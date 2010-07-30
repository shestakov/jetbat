using System;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.Metadata.Definitions
{
	public abstract class ObjectListViewDefinition : ObjectDefinition
	{
		#region Атрибуты

		protected string basicObjectName = String.Empty;
		protected string basicObjectNamespace = String.Empty;
		protected string objectActionNameLoadList = String.Empty;
		protected string objectMethodNameLoadList = String.Empty;
		protected string uiListCaption = String.Empty;

		#endregion

		protected ObjectListViewDefinition(ObjectListView objectListView)
			: base(objectListView)
		{
			uiListCaption = objectListView.UIListCaption;
			basicObjectNamespace = objectListView.BasicObjectNamespace;
			basicObjectName = objectListView.BasicObjectName;
			objectActionNameLoadList = objectListView.ObjectActionNameLoadList;
			objectMethodNameLoadList = objectListView.ObjectMethodNameLoadList;
		}

		public void AddUnexpectedAttribute(ObjectAttributeDefinition attribute)
		{
			if (!AddUnexpectedAttributes)
				throw new NotSupportedException("Objects of this type do not support addition of (unexpected) attributes");
			innerAttributeList.Add(attribute);
		}

		#region Свойства

		public string BasicObjectNamespace
		{
			get { return basicObjectNamespace; }
		}

		public string BasicObjectName
		{
			get { return basicObjectName; }
		}

		public string UIListCaption
		{
			get { return uiListCaption; }
		}

		public string ActionUIFullTextLoadList
		{
			get
			{
				return
					Actions[objectActionNameLoadList] != null ? Actions[objectActionNameLoadList].UIFullText : "";
			}
		}

		public string ActionUIBriefTextLoadList
		{
			get
			{
				return
					Actions[objectActionNameLoadList] != null ? Actions[objectActionNameLoadList].UIBriefText : "";
			}
		}

		public string ActionNameLoadList
		{
			get { return Actions[objectActionNameLoadList] != null ? Actions[objectActionNameLoadList].Name : ""; }
		}

		public string ProcedureNameLoadList
		{
			get
			{
				return
					Methods[objectMethodNameLoadList] != null
						? Methods[objectMethodNameLoadList].StoredProcedureName
						: "";
			}
		}

		public NamedObjectReadOnlyCollection<ObjectMethodParameterDefinition> MethodParameterDefinitionsLoadList
		{
			get
			{
				return
					Methods[objectMethodNameLoadList] != null
						? Methods[objectMethodNameLoadList].ParameterDefinitions
						: null;
			}
		}

		public virtual bool AddUnexpectedAttributes
		{
			get { return false; }
		}

		#endregion
	}
}