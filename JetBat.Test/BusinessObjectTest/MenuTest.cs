using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using NUnit.Framework;

namespace JetBat.Test.BusinessObjectTest
{
	public class MenuTest : PlainObjectTest
	{
		public MenuTest(IAccessAdapter adapter)
			: base(adapter)
		{
		}

		protected override void setObjectAttributesToInsert(PlainObjectInstance instance)
		{
			instance["Name"] = "Меню №1";
			instance["Active"] = true;
		}

		protected override void checkObjectAttributesAfterInsert(PlainObjectInstance instance)
		{
			Assert.AreEqual("Меню №1", instance["Name"]);
			Assert.AreEqual(true, instance["Active"]);
		}

		protected override void setObjectAttributesToUpdate(PlainObjectInstance instance)
		{
			instance["Name"] = "Меню №2";
			instance["Active"] = false;
		}

		protected override void checkObjectAttributesAfterUpdate(PlainObjectInstance instance)
		{
			Assert.AreEqual("Меню №2", instance["Name"]);
			Assert.AreEqual(false, instance["Active"]);
		}

		protected override string entityNamespace
		{
			get { return "MealCalc"; }
		}

		protected override string entityName
		{
			get { return "Menu"; }
		}
	}
}