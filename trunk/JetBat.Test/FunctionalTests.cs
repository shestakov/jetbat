using JetBat.Test.BusinessObjectTest;
using JetBat.Test.FunctionalTest;
using NUnit.Framework;

namespace JetBat.Test
{
	[TestFixture]
	public class FunctionalTests : TestFixtureBase
	{
		[Test]
		public void Supplier()
		{
			using(var adapter = CreateSqlAccessAdapter())
			{
				new SupplierTest(adapter).Test(false, null);
			}
		}

		[Test]
		public void GoodCategory()
		{
			using(var adapter = CreateSqlAccessAdapter())
			{
				new GoodCategoryTest(adapter).Test(false, null);
			}
		}

		[Test]
		public void Menu()
		{
			using(var adapter = CreateSqlAccessAdapter())
			{
				new MenuTest(adapter).Test(false, null);
			}
		}

		[Test]
		public void DishCategory()
		{
			using(var adapter = CreateSqlAccessAdapter())
			{
				new DishCategoryTest(adapter).Test(false, null);
			}
		}

		[Test]
		public void Good()
		{
			using(var adapter = CreateSqlAccessAdapter())
			{
				new GoodTest(adapter).Test(false, null);
			}
		}

		[Test]
		public void Dish()
		{
			using(var adapter = CreateSqlAccessAdapter())
			{
				new DishTest(adapter).Test(false, null);
			}
		}

		[Test]
		public void DishGood()
		{
			using(var adapter = CreateSqlAccessAdapter())
			{
				new DishGoodTest(adapter).Test(false, null);
			}
		}
		[Test]
		public void WriteOff()
		{
			using (var adapter = CreateSqlAccessAdapter())
			{
				new WriteOffTest(adapter).Test();
			}
		}

		[Test]
		public void TwoGoodsComplete()
		{
			using (var adapter = CreateSqlAccessAdapter())
			{
				new TwoGoodsCompleteTest(adapter).Test();
			}
		}

		[Test]
		public void TwoGoodsLateIncomeComplete()
		{
			using (var adapter = CreateSqlAccessAdapter())
			{
				new TwoGoodsLateIncomeCompleteTest(adapter).Test();
			}
		}

		[Test]
		public void NotEnoughGoodForReservation()
		{
			using (var adapter = CreateSqlAccessAdapter())
			{
				new NotEnoughGoodForReservationTest(adapter).Test();
			}
		}
	}
}