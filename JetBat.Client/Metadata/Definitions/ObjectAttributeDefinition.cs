using System;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata.Definitions
{
	public class ObjectAttributeDefinition : INamedObject
	{
		public Type DataType { get; private set; }
		public short DateTimeFormatID { get; private set; }
		public object DefaultValue { get; private set; }
		public string FriendlyName { get; private set; }
		public bool IsExternal { get; private set; }
		public bool IsNullable { get; private set; }
		public bool IsPrimaryKeyMember { get; private set; }
		public bool IsReadOnly { get; private set; }
		public bool IsUserVisible { get; private set; }
		public int MaxLength { get; private set; }
		public string Name { get; private set; }
		public int Precision { get; private set; }
		public int Scale { get; private set; }
		public string SqlDbType { get; private set; }
		public string UILabel { get; private set; }
		public int UIPreferredWidth { get; private set; }
		public int UIPreferredIndex { get; private set; }
		public bool UIAllowMultilineText { get; private set; }

		#region Инициализация

		public ObjectAttributeDefinition(Simple.ObjectAttribute objectAttribute)
		{
			Name = objectAttribute.Name;
			FriendlyName = objectAttribute.FriendlyName;
			DataType = DataTypeEnumeration.GetSystemTypeByName(objectAttribute.DataType);
			SqlDbType = objectAttribute.SqlDbType;
			DateTimeFormatID = objectAttribute.DateTimeFormatID;
			IsNullable = objectAttribute.IsNullable;
			IsReadOnly = objectAttribute.IsReadOnly;
			IsExternal = objectAttribute.IsExternal;
			IsUserVisible = objectAttribute.IsUserVisible;
			IsPrimaryKeyMember = objectAttribute.IsPrimaryKeyMember;
			MaxLength = objectAttribute.MaxLength;
			Precision = objectAttribute.Precision;
			Scale = objectAttribute.Scale;
			UILabel = objectAttribute.UILabel;
			UIPreferredWidth = objectAttribute.UIPreferredWidth;
			UIPreferredIndex = objectAttribute.UIPreferredIndex;
			UIAllowMultilineText = objectAttribute.UIAllowMultilineText;
			DefaultValue = null;
		}

		#endregion

		#region Прочее

		public override string ToString()
		{
			return UILabel;
		}

		#endregion
	}
}