using JetBat.Client.Entities;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using NUnit.Framework;

namespace JetBat.Test.BusinessObjectTest
{
	public class GoodTest : PlainObjectTest
	{
		private int goodCategoryIdInsert;
		private int goodCategoryIdUpdate;

		public GoodTest(IAccessAdapter adapter)
			: base(adapter)
		{
		}

		protected override void beforeInsert()
		{
			goodCategoryIdInsert = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "GoodCategory",
																		  delegate(ObjectInstance instance)
																		  {
																			  instance["Name"] = "Хлебобулочные изделия";
																		  });
			goodCategoryIdUpdate = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "GoodCategory",
																		  delegate(ObjectInstance instance)
																		  {
																			  instance["Name"] = "Макаронные изделия";
																		  });
		}

		protected override void afterDelete()
		{
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "GoodCategory", goodCategoryIdInsert, false, null);
			BusinessObjectHelper.DeletePlainObject(adapter, "MealCalc", "GoodCategory", goodCategoryIdUpdate, false, null);
		}

		protected override void setObjectAttributesToInsert(PlainObjectInstance instance)
		{
			instance["Name"] = "Хлеб ржаной";
			instance["GoodCategoryID"] = goodCategoryIdInsert;
		}

		protected override void checkObjectAttributesAfterInsert(PlainObjectInstance instance)
		{
			Assert.AreEqual("Хлеб ржаной", instance["Name"]);
			Assert.AreEqual(goodCategoryIdInsert, instance["GoodCategoryID"]);
		}

		protected override void setObjectAttributesToUpdate(PlainObjectInstance instance)
		{
			instance["Name"] = "Хлеб пшеничный";
			instance["GoodCategoryID"] = goodCategoryIdUpdate;
		}

		protected override void checkObjectAttributesAfterUpdate(PlainObjectInstance instance)
		{
			Assert.AreEqual("Хлеб пшеничный", instance["Name"]);
			Assert.AreEqual(goodCategoryIdUpdate, instance["GoodCategoryID"]);
		}

		protected override string entityNamespace
		{
			get { return "MealCalc"; }
		}

		protected override string entityName
		{
			get { return "Good"; }
		}
	}
}