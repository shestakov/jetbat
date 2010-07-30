using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Silverlight.UI.AttributeEditiors
{
	public static class ObjectAttributeEditorFactory
	{
		public static FrameworkElement CreateAttributeEditor(ObjectAttribute attribute)
		{
			if (Type.GetType(attribute.DataType) == typeof(string))
			{
				return new TextAttributeEditor(attribute)
				{
					Value = null,
					HorizontalContentAlignment = HorizontalAlignment.Stretch
				};
			}
			if (Type.GetType(attribute.DataType) == typeof(Int32))
			{
				return new Int32AttributeEditor(attribute)
				{
					Value = null,
					HorizontalContentAlignment = HorizontalAlignment.Stretch
				};
			}
			if (Type.GetType(attribute.DataType) == typeof(DateTime))
			{
				return new DateTimeAttributeEditor(attribute)
				{
					Value = null,
					HorizontalContentAlignment = HorizontalAlignment.Stretch
				};
			}
			if (Type.GetType(attribute.DataType) == typeof(decimal))
			{
				return new DecimalAttributeEditor(attribute)
				{
					Value = null,
					HorizontalContentAlignment = HorizontalAlignment.Stretch
				};
			}
			if (Type.GetType(attribute.DataType) == typeof(bool))
			{
				return new BooleanAttributeEditor(attribute)
				{
					Value = null,
					HorizontalContentAlignment = HorizontalAlignment.Stretch
				};
			}
			
			TextBlock textBlock = new TextBlock();
			textBlock.Text = "Unsupported data type " + attribute.DataType;
			return textBlock;
		}

		public static FrameworkElement CreateComplexAttributeEditor(string objectNamespace, string objectName, ObjectComplexAttribute complexAttribute, GetPlainObjectListQualifiedNamespaceDelegate getPlainObjectListQualifiedNamespace)
		{
			string plainObjectListNamespace;
			string plainObjectListName;
			string displayMemberPath;
			Dictionary<string, string> migratedAttributes;
			getPlainObjectListQualifiedNamespace(objectNamespace, objectName, complexAttribute.Name, out plainObjectListNamespace, out plainObjectListName, out displayMemberPath, out migratedAttributes);
			return new ComplexAttributeComboBox(complexAttribute, plainObjectListNamespace, plainObjectListName, displayMemberPath, migratedAttributes) { HorizontalContentAlignment = HorizontalAlignment.Stretch };
		}

		public static IComplexAttributeEditor CreateComplexAttributeEditorDocumentListComboBox(string objectNamespace, string objectName, ObjectComplexAttribute complexAttribute, GetDocumentListQualifiedNamespaceDelegate getDocumentListQualifiedNamespace)
		{
			string documentListNamespace;
			string documentListName;
			string displayMemberPath;
			Dictionary<string, string> migratedAttributes;
			getDocumentListQualifiedNamespace(objectNamespace, objectName, complexAttribute.Name, out documentListNamespace, out documentListName, out displayMemberPath, out migratedAttributes);
			return new ComplexAttributeComboBoxDocumentListView(complexAttribute, documentListNamespace, documentListName, displayMemberPath, migratedAttributes) { HorizontalContentAlignment = HorizontalAlignment.Stretch };
		}
	}
}