using System;
using System.Data;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using NUnit.Framework;

namespace JetBat.Test.FunctionalTest
{
	public class WriteOffTest : MealCalcFunctionalTest
	{
		private int incomeDocument;
		private int calcDayMenu;
		private int calcDayMealOne;
		private int calcDayMealTwo;
		private int calcDayMealThree;
		private int mealOneDishOne;
		private int mealOneDishTwo;
		private int mealTwoDishOne;
		private int mealTwoDishTwo;
		private int mealTwoDishThree;
		private int mealThreeDishTwo;
		private int mealThreeDishThree;

		public WriteOffTest(IAccessAdapter adapter) : base(adapter)
		{
		}

		private void makeIncome()
		{
			var documentInstance = BusinessObjectHelper.CreateDocument(adapter, "MealCalc", "GoodIncomeDocument",
			                                                           delegate(ObjectInstance instance)
			                                                           	{
			                                                           		instance["SupplierID"] = supplier;
			                                                           		instance["IncomeCalcDayID"] = calcDay;
			                                                           		instance["DocumentDateTime"] = DateTime.Today;
			                                                           		instance["InvoiceNumber"] = "123456789012345678901234567890";
			                                                           	});
			incomeDocument = documentInstance.DocumentID;

			BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "GoodIncomeDocumentDetail",
			                                       delegate(ObjectInstance instance)
			                                       	{
			                                       		instance["DocumentVersionID"] = documentInstance.VersionID;
			                                       		instance["GoodID"] = goodOne;
			                                       		instance["GoodPackingUnitID"] = goodOneDefaultPackingUnit;
			                                       		instance["GoodCommodityName"] = "Торговое наименование первого товара";
			                                       		instance["Price"] = (decimal)10;
			                                       		instance["Quantity"] = (decimal)2.25;
			                                       		instance["Comment"] = "Комментарий к товарной позиции";
			                                       		instance["OrderNumber"] = DBNull.Value;
			                                       	});

			BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "GoodIncomeDocumentDetail",
			                                       delegate(ObjectInstance instance)
			                                       	{
			                                       		instance["DocumentVersionID"] = documentInstance.VersionID;
			                                       		instance["GoodID"] = goodOne;
			                                       		instance["GoodPackingUnitID"] = goodOneDefaultPackingUnit;
			                                       		instance["GoodCommodityName"] = "Торговое наименование первого товара";
			                                       		instance["Price"] = (decimal)10;
			                                       		instance["Quantity"] = (decimal)3.25;
			                                       		instance["Comment"] = "Комментарий к товарной позиции";
			                                       		instance["OrderNumber"] = DBNull.Value;
			                                       	});

			BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "GoodIncomeDocumentDetail",
			                                       delegate(ObjectInstance instance)
			                                       	{
			                                       		instance["DocumentVersionID"] = documentInstance.VersionID;
			                                       		instance["GoodID"] = goodTwo;
			                                       		instance["GoodPackingUnitID"] = goodTwoDefaultPackingUnit;
			                                       		instance["GoodCommodityName"] = "Торговое наименование второго товара";
			                                       		instance["Price"] = (decimal)200;
			                                       		instance["Quantity"] = (decimal)45;
			                                       		instance["Comment"] = "Комментарий к товарной позиции";
			                                       		instance["OrderNumber"] = DBNull.Value;
			                                       	});
			documentInstance.UpdateVersion();
			documentInstance.ConfirmEdit();
			documentInstance.Commit();
		}

		private void checkGoodBalance()
		{
			ObjectListViewDefinition storedQueryDefinition;
			DataTable result = adapter.ObjectFactory.LoadObjectListView("MealCalc", "GoodRemains", new AttributeValueSet(), out storedQueryDefinition);
			Assert.AreEqual(2, result.Rows.Count);
			Assert.AreEqual(5, storedQueryDefinition.Attributes.Count);
			Assert.AreEqual("Good One", result.Rows[0]["GoodName"]);
			Assert.AreEqual((decimal)5.5, result.Rows[0]["QuantityLeft"]);
			Assert.AreEqual((decimal)0, result.Rows[0]["QuantityReserved"]);
			Assert.AreEqual("Good Two", result.Rows[1]["GoodName"]);
			Assert.AreEqual((decimal)45, result.Rows[1]["QuantityLeft"]);
			Assert.AreEqual((decimal)0, result.Rows[1]["QuantityReserved"]);
		}

		private void checkGoodReserve()
		{
			ObjectListViewDefinition storedQueryDefinition;
			DataTable result = adapter.ObjectFactory.LoadObjectListView("MealCalc", "GoodRemains", new AttributeValueSet(), out storedQueryDefinition);
			Assert.AreEqual(2, result.Rows.Count);
			Assert.AreEqual(5, storedQueryDefinition.Attributes.Count);
			Assert.AreEqual("Good One", result.Rows[0]["GoodName"]);
			Assert.AreEqual((decimal)5.5, result.Rows[0]["QuantityLeft"]);
			Assert.AreEqual((decimal)4.5, result.Rows[0]["QuantityReserved"]);
			Assert.AreEqual("Good Two", result.Rows[1]["GoodName"]);
			Assert.AreEqual((decimal)45, result.Rows[1]["QuantityLeft"]);
			Assert.AreEqual((decimal)45, result.Rows[1]["QuantityReserved"]);
		}

		private void createCalcDayMenu()
		{
			#region CalcDayMenu (1)

			calcDayMenu = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMenu",
			                                                     delegate(ObjectInstance instance)
			                                                     	{
			                                                     		instance["CalcDayID"] = calcDay;
			                                                     		instance["MenuID"] = menu;
			                                                     		instance["Comment"] = "No comment";
			                                                     	});

			#endregion

			#region Get CalcDayMeal ID's

			ObjectListViewDefinition storedQueryDefinition;
			var parameters = new AttributeValueSet { { "CalcDayMenuID", calcDayMenu } };
			DataTable result = adapter.ObjectFactory.LoadObjectListView("MealCalc", "CalcDayMealList", parameters, out storedQueryDefinition);
			Assert.AreEqual(3, result.Rows.Count);

			calcDayMealOne = (int)result.Select("OrderIndex = 1")[0]["ID"];
			calcDayMealTwo = (int)result.Select("OrderIndex = 2")[0]["ID"];
			calcDayMealThree = (int)result.Select("OrderIndex = 3")[0]["ID"];

			#endregion
		}

		private void addCalcDayMealDishes()
		{
			#region Dishes for calcDayMealOne

			mealOneDishOne = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
			                                                        delegate(ObjectInstance instance)
			                                                        	{
			                                                        		instance["CalcDayMealID"] = calcDayMealOne;
			                                                        		instance["DishID"] = dishOne;
			                                                        		instance["PortionCount"] = 10;
			                                                        		instance["Comment"] = "No comment";
			                                                        	});

			mealOneDishTwo = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
			                                                        delegate(ObjectInstance instance)
			                                                        	{
			                                                        		instance["CalcDayMealID"] = calcDayMealOne;
			                                                        		instance["DishID"] = dishTwo;
			                                                        		instance["PortionCount"] = 10;
			                                                        		instance["Comment"] = "No comment";
			                                                        	});

			mealOneDishTwo = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
			                                                        delegate(ObjectInstance instance)
			                                                        	{
			                                                        		instance["CalcDayMealID"] = calcDayMealOne;
			                                                        		instance["DishID"] = dishThree;
			                                                        		instance["PortionCount"] = 10;
			                                                        		instance["Comment"] = "No comment";
			                                                        	});

			#endregion

			#region Dishes for calcDayMealTwo

			mealTwoDishOne = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
			                                                        delegate(ObjectInstance instance)
			                                                        	{
			                                                        		instance["CalcDayMealID"] = calcDayMealTwo;
			                                                        		instance["DishID"] = dishOne;
			                                                        		instance["PortionCount"] = 10;
			                                                        		instance["Comment"] = "No comment";
			                                                        	});

			mealTwoDishTwo = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
			                                                        delegate(ObjectInstance instance)
			                                                        	{
			                                                        		instance["CalcDayMealID"] = calcDayMealTwo;
			                                                        		instance["DishID"] = dishTwo;
			                                                        		instance["PortionCount"] = 10;
			                                                        		instance["Comment"] = "No comment";
			                                                        	});

			mealTwoDishThree = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
			                                                          delegate(ObjectInstance instance)
			                                                          	{
			                                                          		instance["CalcDayMealID"] = calcDayMealTwo;
			                                                          		instance["DishID"] = dishThree;
			                                                          		instance["PortionCount"] = 10;
			                                                          		instance["Comment"] = "No comment";
			                                                          	});

			#endregion

			#region Dishes for calcDayMealThree

			mealThreeDishTwo = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
			                                                          delegate(ObjectInstance instance)
			                                                          	{
			                                                          		instance["CalcDayMealID"] = calcDayMealThree;
			                                                          		instance["DishID"] = dishOne;
			                                                          		instance["PortionCount"] = 10;
			                                                          		instance["Comment"] = "No comment";
			                                                          	});

			mealThreeDishTwo = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
			                                                          delegate(ObjectInstance instance)
			                                                          	{
			                                                          		instance["CalcDayMealID"] = calcDayMealThree;
			                                                          		instance["DishID"] = dishTwo;
			                                                          		instance["PortionCount"] = 10;
			                                                          		instance["Comment"] = "No comment";
			                                                          	});

			mealThreeDishThree = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
			                                                            delegate(ObjectInstance instance)
			                                                            	{
			                                                            		instance["CalcDayMealID"] = calcDayMealThree;
			                                                            		instance["DishID"] = dishThree;
			                                                            		instance["PortionCount"] = 10;
			                                                            		instance["Comment"] = "No comment";
			                                                            	});

			#endregion
		}

		private void approveCalcDayMenu()
		{
			var parameters = new AttributeValueSet { { "ID", calcDayMenu } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMenu", "Approve", parameters, null);
		}

		public void Test()
		{
			prepare();
			composeDishes();
			createMenu();
			openCalcDay();
			makeIncome();
			checkGoodBalance();
			createCalcDayMenu();
			addCalcDayMealDishes();
			checkGoodReserve();
			approveCalcDayMenu();
		}
	}
}