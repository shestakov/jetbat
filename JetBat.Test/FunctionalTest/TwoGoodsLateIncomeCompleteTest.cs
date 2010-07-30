using System;
using System.Data;
using JetBat.Client.Entities;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using NUnit.Framework;

namespace JetBat.Test.FunctionalTest
{
	public class TwoGoodsLateIncomeCompleteTest : MealCalcFunctionalTest
	{
		private int incomeDocument;
		private int calcDayMenu;
		private int calcDayMealOne;
		private int calcDayMealTwo;
		private int calcDayMealThree;
		private int mealOneDishOne;
		private int mealOneDishTwo;
		private int mealOneDishThree;
		private int mealTwoDishOne;
		private int mealTwoDishTwo;
		private int mealTwoDishThree;
		private int mealThreeDishOne;
		private int mealThreeDishTwo;
		private int mealThreeDishThree;

		public TwoGoodsLateIncomeCompleteTest(IAccessAdapter adapter) : base(adapter)
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
			                                       		instance["Quantity"] = (decimal)4.5;
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
			Assert.AreEqual((decimal)4.5, result.Rows[0]["QuantityLeft"]);
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
			Assert.AreEqual((decimal)4.5, result.Rows[0]["QuantityLeft"]);
			Assert.AreEqual((decimal)4.5, result.Rows[0]["QuantityReserved"]);
			Assert.AreEqual("Good Two", result.Rows[1]["GoodName"]);
			Assert.AreEqual((decimal)45, result.Rows[1]["QuantityLeft"]);
			Assert.AreEqual((decimal)45, result.Rows[1]["QuantityReserved"]);
		}

		private void checkGoodWriteOff()
		{
			ObjectListViewDefinition storedQueryDefinition;
			DataTable result = adapter.ObjectFactory.LoadObjectListView("MealCalc", "GoodRemains", new AttributeValueSet(), out storedQueryDefinition);
			Assert.AreEqual(2, result.Rows.Count);
			Assert.AreEqual(5, storedQueryDefinition.Attributes.Count);
			Assert.AreEqual("Good One", result.Rows[0]["GoodName"]);
			Assert.AreEqual((decimal)0, result.Rows[0]["QuantityLeft"]);
			Assert.AreEqual((decimal)0, result.Rows[0]["QuantityReserved"]);
			Assert.AreEqual("Good Two", result.Rows[1]["GoodName"]);
			Assert.AreEqual((decimal)0, result.Rows[1]["QuantityLeft"]);
			Assert.AreEqual((decimal)0, result.Rows[1]["QuantityReserved"]);
		}

		private void checkZeroGoodBalance()
		{
			ObjectListViewDefinition storedQueryDefinition;
			DataTable result = adapter.ObjectFactory.LoadObjectListView("MealCalc", "GoodRemains", new AttributeValueSet(), out storedQueryDefinition);
			Assert.AreEqual(0, result.Rows.Count);
			Assert.AreEqual(5, storedQueryDefinition.Attributes.Count);
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

			mealOneDishThree = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
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

			mealThreeDishOne = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "CalcDayMealDish",
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

		private void removeCalcDayMealDishes()
		{
			var calcDayMealDish = (PlainObjectInstance) adapter.ObjectFactory.New<PlainObjectDefinition>("MealCalc", "CalcDayMealDish");
			calcDayMealDish.Load(new AttributeValueSet { { "ID", mealOneDishOne } });
			calcDayMealDish.Delete();
			calcDayMealDish.Load(new AttributeValueSet { { "ID", mealOneDishTwo } });
			calcDayMealDish.Delete();
			calcDayMealDish.Load(new AttributeValueSet { { "ID", mealOneDishThree } });
			calcDayMealDish.Delete();
			calcDayMealDish.Load(new AttributeValueSet { { "ID", mealTwoDishOne } });
			calcDayMealDish.Delete();
			calcDayMealDish.Load(new AttributeValueSet { { "ID", mealTwoDishTwo } });
			calcDayMealDish.Delete();
			calcDayMealDish.Load(new AttributeValueSet { { "ID", mealTwoDishThree } });
			calcDayMealDish.Delete();
			calcDayMealDish.Load(new AttributeValueSet { { "ID", mealThreeDishOne } });
			calcDayMealDish.Delete();
			calcDayMealDish.Load(new AttributeValueSet { { "ID", mealThreeDishTwo } });
			calcDayMealDish.Delete();
			calcDayMealDish.Load(new AttributeValueSet { { "ID", mealThreeDishThree } });
			calcDayMealDish.Delete();
		}

		private void createGoodSpendDocument()
		{
			var parameters = new AttributeValueSet { { "ID", calcDayMenu } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMenu", "CreateGoodSpendDocument", parameters, null);
		}

		private void recomputeCalcDayMenu()
		{
			var parameters = new AttributeValueSet { { "ID", mealOneDishOne } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMealDish", "Recompute", parameters, null);
			parameters = new AttributeValueSet { { "ID", mealOneDishTwo } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMealDish", "Recompute", parameters, null);
			parameters = new AttributeValueSet { { "ID", mealOneDishThree } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMealDish", "Recompute", parameters, null);

			parameters = new AttributeValueSet { { "ID", mealTwoDishOne } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMealDish", "Recompute", parameters, null);
			parameters = new AttributeValueSet { { "ID", mealTwoDishTwo } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMealDish", "Recompute", parameters, null);
			parameters = new AttributeValueSet { { "ID", mealTwoDishThree } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMealDish", "Recompute", parameters, null);

			parameters = new AttributeValueSet { { "ID", mealThreeDishOne } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMealDish", "Recompute", parameters, null);
			parameters = new AttributeValueSet { { "ID", mealThreeDishTwo } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMealDish", "Recompute", parameters, null);
			parameters = new AttributeValueSet { { "ID", mealThreeDishThree } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMealDish", "Recompute", parameters, null);
		}

		private void approveCalcDayMenu()
		{
			var parameters = new AttributeValueSet { { "ID", calcDayMenu } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMenu", "Approve", parameters, null);
		}

		private void unapproveCalcDayMenu()
		{
			var parameters = new AttributeValueSet { { "ID", calcDayMenu } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDayMenu", "Unapprove", parameters, null);
		}

		private void commitGoodSpendDocument()
		{
			var parameters = new AttributeValueSet
				{
					{"StartDateTime", DateTime.Today},
					{"EndDateTime", DateTime.Today.AddDays(1)}
				};
			var result = adapter.ObjectFactory.LoadObjectListView("MealCalc", "GoodSpendDocumentList", parameters);
			Assert.AreEqual(1, result.Rows.Count);
			int documentID = (int) result.Rows[0]["ID"];

			var document = (DocumentInstance) adapter.ObjectFactory.New<DocumentDefinition>("MealCalc", "GoodSpendDocument");
			document.Load(documentID);
			document["DocumentNumber"] = "000-001";
			document["Comment"] = "Comment";
			document.UpdateVersion();
			document.ConfirmEdit();
			document.Commit();
		}

		private void rollbackGoodSpendDocument()
		{
			var parameters = new AttributeValueSet
				{
					{"StartDateTime", DateTime.Today},
					{"EndDateTime", DateTime.Today.AddDays(1)}
				};
			var result = adapter.ObjectFactory.LoadObjectListView("MealCalc", "GoodSpendDocumentList", parameters);
			Assert.AreEqual(1, result.Rows.Count);
			var documentID = (int)result.Rows[0]["ID"];

			var document = (DocumentInstance)adapter.ObjectFactory.New<DocumentDefinition>("MealCalc", "GoodSpendDocument");
			document.Load(documentID);
			document.Rollback();
		}

		private void deleteGoodSpendDocument()
		{
			var parameters = new AttributeValueSet
				{
					{"StartDateTime", DateTime.Today},
					{"EndDateTime", DateTime.Today.AddDays(1)}
				};
			var result = adapter.ObjectFactory.LoadObjectListView("MealCalc", "GoodSpendDocumentList", parameters);
			Assert.AreEqual(1, result.Rows.Count);
			var documentID = (int)result.Rows[0]["ID"];

			var document = (DocumentInstance)adapter.ObjectFactory.New<DocumentDefinition>("MealCalc", "GoodSpendDocument");
			document.Load(documentID);
			document.Delete();
		}

		private void rollbackGoodIncomeDocument()
		{
			var parameters = new AttributeValueSet
				{
					{"StartDateTime", DateTime.Today},
					{"EndDateTime", DateTime.Today.AddDays(1)}
				};
			var result = adapter.ObjectFactory.LoadObjectListView("MealCalc", "GoodIncomeList", parameters);
			Assert.AreEqual(1, result.Rows.Count);
			var documentID = (int)result.Rows[0]["ID"];

			var document = (DocumentInstance)adapter.ObjectFactory.New<DocumentDefinition>("MealCalc", "GoodIncomeDocument");
			document.Load(documentID);
			document.Rollback();
		}


		public void Test()
		{
			prepare();
			composeDishes();
			createMenu();
			openCalcDay();
			createCalcDayMenu();
			addCalcDayMealDishes();
			makeIncome();
			checkGoodBalance();
			recomputeCalcDayMenu();
			checkGoodReserve();
			approveCalcDayMenu();
			createGoodSpendDocument();
			commitGoodSpendDocument();
			//checkGoodWriteOff();
			rollbackGoodSpendDocument();
			deleteGoodSpendDocument();
			//checkGoodReserve();
			unapproveCalcDayMenu();
			removeCalcDayMealDishes();
			//checkGoodBalance();
			rollbackGoodIncomeDocument();
			//checkZeroGoodBalance();
		}
	}
}