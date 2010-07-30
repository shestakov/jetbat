using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using NUnit.Framework;

namespace JetBat.Test.BusinessObjectTest
{
	public class DishCategoryTest : PlainObjectTest
	{
		public DishCategoryTest(IAccessAdapter adapter)
			: base(adapter)
		{
		}

		protected override void setObjectAttributesToInsert(PlainObjectInstance instance)
		{
			instance["Name"] = "Первые блюда";
			instance["IndexNumber"] = 1;
		}

		protected override void checkObjectAttributesAfterInsert(PlainObjectInstance instance)
		{
			Assert.AreEqual("Первые блюда", instance["Name"]);
			Assert.AreEqual(1, instance["IndexNumber"]);
		}

		protected override void setObjectAttributesToUpdate(PlainObjectInstance instance)
		{
			instance["Name"] = "Вторые блюда";
			instance["IndexNumber"] = 2;
		}

		protected override void checkObjectAttributesAfterUpdate(PlainObjectInstance instance)
		{
			Assert.AreEqual("Вторые блюда", instance["Name"]);
			Assert.AreEqual(2, instance["IndexNumber"]);
		}

		protected override string entityNamespace
		{
			get { return "MealCalc"; }
		}

		protected override string entityName
		{
			get { return "DishCategory"; }
		}
	}
}