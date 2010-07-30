using JetBat.Client.Entities;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using NUnit.Framework;

namespace JetBat.Test.BusinessObjectTest
{
	public class DishTest : PlainObjectTest
	{
		private int dishCategoryIdInsert;
		private int dishCategoryIdUpdate;

		public DishTest(IAccessAdapter adapter)
			: base(adapter)
		{
		}

		protected override void beforeInsert()
		{
			dishCategoryIdInsert = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishCategory",
			                                                              delegate(ObjectInstance instance)
			                                                              	{
			                                                              		instance["Name"] = "Первые блюда";
			                                                              		instance["IndexNumber"] = 1;
			                                                              	});
			dishCategoryIdUpdate = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishCategory",
			                                                              delegate(ObjectInstance instance)
			                                                              	{
			                                                              		instance["Name"] = "Вторые блюда";
			                                                              		instance["IndexNumber"] = 2;
			                                                              	});
		}

		protected override void afterDelete()
		{
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "DishCategory", dishCategoryIdInsert, false, null);
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "DishCategory", dishCategoryIdUpdate, false, null);
		}

		protected override void setObjectAttributesToInsert(PlainObjectInstance instance)
		{
			instance["Name"] = "Борщ";
			instance["DishCategoryID"] = dishCategoryIdInsert;
			instance["ReceiptCode"] = "01234567890123456789";
			instance["PortionCount"] = 1;
			instance["WorkOut"] = "тарелка супу";
			instance["PortionWeight"] = 250;
		}

		protected override void checkObjectAttributesAfterInsert(PlainObjectInstance instance)
		{
			Assert.AreEqual("Борщ", instance["Name"]);
			Assert.AreEqual(dishCategoryIdInsert, instance["DishCategoryID"]);
			Assert.AreEqual("01234567890123456789", instance["ReceiptCode"]);
			Assert.AreEqual(1, instance["PortionCount"]);
			Assert.AreEqual("тарелка супу", instance["WorkOut"]);
			Assert.AreEqual(250, instance["PortionWeight"]);
		}

		protected override void setObjectAttributesToUpdate(PlainObjectInstance instance)
		{
			instance["Name"] = "Картофель отварной";
			instance["DishCategoryID"] = dishCategoryIdUpdate;
			instance["ReceiptCode"] = "12345678901234567890";
			instance["PortionCount"] = 2;
			instance["WorkOut"] = "тарелка картошки";
			instance["PortionWeight"] = 200;
		}

		protected override void checkObjectAttributesAfterUpdate(PlainObjectInstance instance)
		{
			Assert.AreEqual("Картофель отварной", instance["Name"]);
			Assert.AreEqual(dishCategoryIdUpdate, instance["DishCategoryID"]);
			Assert.AreEqual("12345678901234567890", instance["ReceiptCode"]);
			Assert.AreEqual(2, instance["PortionCount"]);
			Assert.AreEqual("тарелка картошки", instance["WorkOut"]);
			Assert.AreEqual(200, instance["PortionWeight"]);
		}

		protected override string entityNamespace
		{
			get { return "MealCalc"; }
		}

		protected override string entityName
		{
			get { return "Dish"; }
		}
	}
}