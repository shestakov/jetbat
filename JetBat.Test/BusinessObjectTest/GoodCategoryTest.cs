using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using NUnit.Framework;

namespace JetBat.Test.BusinessObjectTest
{
	public class GoodCategoryTest : PlainObjectTest
	{
		public GoodCategoryTest(IAccessAdapter adapter)
			: base(adapter)
		{
		}

		protected override void setObjectAttributesToInsert(PlainObjectInstance instance)
		{
			instance["Name"] = "Хлебобулочные изделия";
		}

		protected override void checkObjectAttributesAfterInsert(PlainObjectInstance instance)
		{
			Assert.AreEqual("Хлебобулочные изделия", instance["Name"]);
		}

		protected override void setObjectAttributesToUpdate(PlainObjectInstance instance)
		{
			instance["Name"] = "Макаронные изделия";
		}

		protected override void checkObjectAttributesAfterUpdate(PlainObjectInstance instance)
		{
			Assert.AreEqual("Макаронные изделия", instance["Name"]);
		}

		protected override string entityNamespace
		{
			get { return "MealCalc"; }
		}

		protected override string entityName
		{
			get { return "GoodCategory"; }
		}
	}
}