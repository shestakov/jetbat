using JetBat.Client.Entities;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Misc;
using NUnit.Framework;
using System.Linq;

namespace JetBat.Test
{
	public class BusinessObjectHelper
	{
		public static DocumentInstance CreateDocument(IAccessAdapter adapter, string entityNamespace, string entityName, SetBusinessObjectInstanceAttributes setDelegate)
		{
			var instance = (DocumentInstance)adapter.ObjectFactory.New<DocumentDefinition>(entityNamespace, entityName);
			setDelegate(instance);
			instance.Create();
			return instance;
		}

		public static int CreatePlainObject(IAccessAdapter adapter, string entityNamespace, string entityName, SetBusinessObjectInstanceAttributes setDelegate)
		{
			var instance = (PlainObjectInstance)adapter.ObjectFactory.New<PlainObjectDefinition>(entityNamespace, entityName);
			setDelegate(instance);
			var result = instance.Insert();
			Assert.AreEqual(0, result.Count, string.Join(", ", result.Where(message => message.Severity > 1)
			                                                   	.Select(message => message.Text).ToArray()));
			return (int)instance["ID"];
		}

		public static void DeletePlainObject(IAccessAdapter adapter, string entityNamespace, string entityName, int id, bool logicalDeletion, string logicalDeletionAttribute)
		{
			var instance = (PlainObjectInstance)adapter.ObjectFactory.New<PlainObjectDefinition>(entityNamespace, entityName);
			var pk = new AttributeValueSet();
			pk["ID"] = id;
			var result = instance.Load(pk);
			Assert.AreEqual(0, result.Count);
			result = instance.Delete();
			Assert.AreEqual(0, result.Count);
			instance = (PlainObjectInstance)adapter.ObjectFactory.New<PlainObjectDefinition>(entityNamespace, entityName);
			result = instance.Load(pk);
			if (!logicalDeletion)
			{
				Assert.AreEqual(1, result.Count);
				Assert.AreEqual(string.Format("Объект не найден: [{0}] {1}", entityNamespace, entityName),
				                result[0].Text);
			}
			else
			{
				Assert.AreEqual(0, result.Count);
				Assert.AreEqual(true, instance[logicalDeletionAttribute]);
			}
		}
	}

	public delegate void SetBusinessObjectInstanceAttributes(ObjectInstance instance);
}


