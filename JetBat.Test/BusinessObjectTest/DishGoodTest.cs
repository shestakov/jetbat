using JetBat.Client.Entities;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using NUnit.Framework;

namespace JetBat.Test.BusinessObjectTest
{
	public class DishGoodTest : PlainObjectTest
	{
		private int dishCategoryId;
		private int dishIdInsert;
		private int dishIdUpdate;
		private int goodCategoryId;
		private int goodIdInsert;
		private int goodIdUpdate;

		public DishGoodTest(IAccessAdapter adapter)
			: base(adapter)
		{
		}

		protected override void beforeInsert()
		{
			dishCategoryId = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishCategory",
			                                                        delegate(ObjectInstance instance)
			                                                        	{
			                                                        		instance["Name"] = "Первые блюда";
			                                                        		instance["IndexNumber"] = 1;
			                                                        	});
			dishIdInsert = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Dish",
			                                                      delegate(ObjectInstance instance)
			                                                      	{
			                                                      		instance["Name"] = "Борщ";
			                                                      		instance["DishCategoryID"] = dishCategoryId;
			                                                      		instance["ReceiptCode"] = "01234567890123456789";
			                                                      		instance["PortionCount"] = 1;
			                                                      		instance["WorkOut"] = "тарелка супу";
			                                                      		instance["PortionWeight"] = 250;
			                                                      	});
			dishIdUpdate = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Dish",
			                                                      delegate(ObjectInstance instance)
			                                                      	{
			                                                      		instance["Name"] = "Картофель отварной";
			                                                      		instance["DishCategoryID"] = dishCategoryId;
			                                                      		instance["ReceiptCode"] = "12345678901234567890";
			                                                      		instance["PortionCount"] = 2;
			                                                      		instance["WorkOut"] = "тарелка картошки";
			                                                      		instance["PortionWeight"] = 200;
			                                                      	});
			goodCategoryId = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "GoodCategory",
			                                                        delegate(ObjectInstance instance)
			                                                        	{
			                                                        		instance["Name"] = "Хлебобулочные изделия";
			                                                        	});
			goodIdInsert = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Good",
			                                                      delegate(ObjectInstance instance)
			                                                      	{
			                                                      		instance["Name"] = "Хлеб ржаной";
			                                                      		instance["GoodCategoryID"] = goodCategoryId;
			                                                      	});
			goodIdUpdate = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Good",
			                                                      delegate(ObjectInstance instance)
			                                                      	{
			                                                      		instance["Name"] = "Хлеб пшеничный";
			                                                      		instance["GoodCategoryID"] = goodCategoryId;
			                                                      	});
		}

		protected override void afterDelete()
		{
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "Good", goodIdUpdate, false, null);
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "Good", goodIdInsert, false, null);
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "GoodCategory", goodCategoryId, false, null);
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "Dish", dishIdUpdate, false, null);
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "Dish", dishIdInsert, false, null);
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "DishCategory", dishCategoryId, false, null);
		}

		protected override void setObjectAttributesToInsert(PlainObjectInstance instance)
		{
			instance["DishID"] = dishIdInsert;
			instance["GoodID"] = goodIdInsert;
			instance["Quantity"] = (decimal)1;
		}

		protected override void checkObjectAttributesAfterInsert(PlainObjectInstance instance)
		{
			Assert.AreEqual(dishIdInsert, instance["DishID"]);
			Assert.AreEqual(goodIdInsert, instance["GoodID"]);
			Assert.AreEqual((decimal)1, instance["Quantity"]);
		}

		protected override void setObjectAttributesToUpdate(PlainObjectInstance instance)
		{
			instance["DishID"] = dishIdUpdate;
			instance["GoodID"] = goodIdUpdate;
			instance["Quantity"] = (decimal)2;
		}

		protected override void checkObjectAttributesAfterUpdate(PlainObjectInstance instance)
		{
			Assert.AreEqual(dishIdUpdate, instance["DishID"]);
			Assert.AreEqual(goodIdUpdate, instance["GoodID"]);
			Assert.AreEqual((decimal)2, instance["Quantity"]);
		}

		protected override string entityNamespace
		{
			get { return "MealCalc"; }
		}

		protected override string entityName
		{
			get { return "DishGood"; }
		}
	}
}