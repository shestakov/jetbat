using System;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Misc;
using NUnit.Framework;

namespace JetBat.Test.BusinessObjectTest
{
	public abstract class PlainObjectTest
	{
		protected readonly IAccessAdapter adapter;

		protected PlainObjectTest(IAccessAdapter adapter)
		{
			this.adapter = adapter;
		}

		protected virtual void beforeInsert()
		{
		}

		protected virtual void afterDelete()
		{
		}

		protected abstract void setObjectAttributesToInsert(PlainObjectInstance instance);

		protected abstract void checkObjectAttributesAfterInsert(PlainObjectInstance instance);

		protected abstract void setObjectAttributesToUpdate(PlainObjectInstance instance);

		protected abstract void checkObjectAttributesAfterUpdate(PlainObjectInstance instance);

		public virtual void Test(bool logicalDeletion, string logicalDeletionAttribute)
		{
			Console.WriteLine(string.Format("Testing {0}.{1}", entityNamespace, entityName));
			ErrorMessageCollection result;

			beforeInsert();

			Console.Write("Insert... ");
			var instance = (PlainObjectInstance)adapter.ObjectFactory.New<PlainObjectDefinition>(entityNamespace, entityName);
			setObjectAttributesToInsert(instance);

			result = instance.Insert();
			Assert.AreEqual(0, result.Count);
			var id = instance["ID"];
			Console.WriteLine("OK");

			Console.Write("Load... ");
			instance = (PlainObjectInstance)adapter.ObjectFactory.New<PlainObjectDefinition>(entityNamespace, entityName);
			var pk = new AttributeValueSet();
			pk["ID"] = id;
			result = instance.Load(pk);
			Assert.AreEqual(0, result.Count);
			checkObjectAttributesAfterInsert(instance);
			Assert.AreEqual(id, instance["ID"]);
			Console.WriteLine("OK");

			Console.Write("Update... ");
			setObjectAttributesToUpdate(instance);
			result = instance.Update();
			Assert.AreEqual(0, result.Count);
			instance = (PlainObjectInstance)adapter.ObjectFactory.New<PlainObjectDefinition>(entityNamespace, entityName);
			result = instance.Load(pk);
			Assert.AreEqual(0, result.Count);
			checkObjectAttributesAfterUpdate(instance);
			Assert.AreEqual(id, instance["ID"]);
			Console.WriteLine("OK");

			Console.Write("Delete... ");
			instance = (PlainObjectInstance)adapter.ObjectFactory.New<PlainObjectDefinition>(entityNamespace, entityName);
			result = instance.Load(pk);
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

			Console.WriteLine("OK");

			afterDelete();

		}

		protected abstract string entityNamespace { get; }
		protected abstract string entityName { get; }
	}
}