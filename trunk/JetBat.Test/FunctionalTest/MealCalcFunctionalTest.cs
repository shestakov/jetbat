using System;
using JetBat.Client.Entities;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Test.FunctionalTest
{
	public class MealCalcFunctionalTest
	{
		protected IAccessAdapter adapter;
		protected int supplier;
		protected int goodCategoryOne;
		protected int goodCategoryTwo;
		protected int goodOne;
		protected int goodTwo;
		protected int goodThree;
		protected int goodFour;
		protected int dishCategory;
		protected int dishOne;
		protected int dishTwo;
		protected int dishThree;
		protected int calcDay;
		protected int goodOneDefaultPackingUnit;
		protected int goodTwoDefaultPackingUnit;
		protected int goodThreeDefaultPackingUnit;
		protected int goodFourDefaultPackingUnit;
		protected int menu;
		protected int mealTimeOne;
		protected int mealTimeTwo;
		protected int mealTimeThree;

		public MealCalcFunctionalTest(IAccessAdapter adapter)
		{
			this.adapter = adapter;
		}

		protected void prepare()
		{

			#region Suppliers (1)

			supplier = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Supplier",
			                                                  delegate(ObjectInstance instance)
			                                                  	{
			                                                  		instance["Name"] = "Основной поставщик";
			                                                  		instance["Active"] = true;
			                                                  	});

			#endregion

			#region Good Categories (2)

			goodCategoryOne = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "GoodCategory",
			                                                         delegate(ObjectInstance instance)
			                                                         	{
			                                                         		instance["Name"] = "Good Category One";
			                                                         	});
			goodCategoryTwo = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "GoodCategory",
			                                                         delegate(ObjectInstance instance)
			                                                         	{
			                                                         		instance["Name"] = "Good Category Two";
			                                                         	});

			#endregion

			#region Goods (4)

			var good = (PlainObjectInstance)adapter.ObjectFactory.New<PlainObjectDefinition>("MealCalc", "Good");

			goodOne = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Good",
			                                                 delegate(ObjectInstance instance)
			                                                 	{
			                                                 		instance["Name"] = "Good One";
			                                                 		instance["GoodCategoryID"] = goodCategoryOne;
			                                                 	});
			good.Load(new AttributeValueSet { { "ID", goodOne } });
			goodOneDefaultPackingUnit = (int)good["DefaultGoodPackingUnitID"];

			goodTwo = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Good",
			                                                 delegate(ObjectInstance instance)
			                                                 	{
			                                                 		instance["Name"] = "Good Two";
			                                                 		instance["GoodCategoryID"] = goodCategoryOne;
			                                                 	});
			good.Load(new AttributeValueSet { { "ID", goodTwo } });
			goodTwoDefaultPackingUnit = (int)good["DefaultGoodPackingUnitID"];

			goodThree = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Good",
			                                                   delegate(ObjectInstance instance)
			                                                   	{
			                                                   		instance["Name"] = "Good Three";
			                                                   		instance["GoodCategoryID"] = goodCategoryTwo;
			                                                   	});
			good.Load(new AttributeValueSet { { "ID", goodThree } });
			goodThreeDefaultPackingUnit = (int)good["DefaultGoodPackingUnitID"];

			goodFour = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Good",
			                                                  delegate(ObjectInstance instance)
			                                                  	{
			                                                  		instance["Name"] = "Good Four";
			                                                  		instance["GoodCategoryID"] = goodCategoryTwo;
			                                                  	});
			good.Load(new AttributeValueSet { { "ID", goodFour } });
			goodFourDefaultPackingUnit = (int)good["DefaultGoodPackingUnitID"];

			#endregion

			#region Dish categories (1)

			dishCategory = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishCategory",
			                                                      delegate(ObjectInstance instance)
			                                                      	{
			                                                      		instance["Name"] = "Dish Category";
			                                                      		instance["IndexNumber"] = 1;
			                                                      	});

			#endregion

			#region Dishes (3)

			dishOne = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Dish",
			                                                 delegate(ObjectInstance instance)
			                                                 	{
			                                                 		instance["Name"] = "Dish One";
			                                                 		instance["DishCategoryID"] = dishCategory;
			                                                 		instance["ReceiptCode"] = "001";
			                                                 		instance["PortionCount"] = 1;
			                                                 		instance["WorkOut"] = "100/100";
			                                                 		instance["PortionWeight"] = 250;
			                                                 	});

			dishTwo = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Dish",
			                                                 delegate(ObjectInstance instance)
			                                                 	{
			                                                 		instance["Name"] = "Dish Two";
			                                                 		instance["DishCategoryID"] = dishCategory;
			                                                 		instance["ReceiptCode"] = "002";
			                                                 		instance["PortionCount"] = 1;
			                                                 		instance["WorkOut"] = "100/100";
			                                                 		instance["PortionWeight"] = 200;
			                                                 	});

			dishThree = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Dish",
			                                                   delegate(ObjectInstance instance)
			                                                   	{
			                                                   		instance["Name"] = "Dish Three";
			                                                   		instance["DishCategoryID"] = dishCategory;
			                                                   		instance["ReceiptCode"] = "003";
			                                                   		instance["PortionCount"] = 1;
			                                                   		instance["WorkOut"] = "100/100";
			                                                   		instance["PortionWeight"] = 150;
			                                                   	});

			#endregion
		}

		protected void composeDishes()
		{
			#region DishOne

			BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishGood",
			                                       delegate(ObjectInstance instance)
			                                       	{
			                                       		instance["DishID"] = dishOne;
			                                       		instance["GoodID"] = goodOne;
			                                       		instance["Quantity"] = (decimal)30;
			                                       	});

			BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishGood",
			                                       delegate(ObjectInstance instance)
			                                       	{
			                                       		instance["DishID"] = dishOne;
			                                       		instance["GoodID"] = goodTwo;
			                                       		instance["Quantity"] = (decimal)300;
			                                       	});

			//BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishGood",
			//                                   delegate(ObjectInstance instance)
			//                                   {
			//                                       instance["DishID"] = dishOne;
			//                                       instance["GoodID"] = goodThree;
			//                                       instance["Quantity"] = (decimal)200;
			//                                   });

			#endregion

			#region DishTwo

			BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishGood",
			                                       delegate(ObjectInstance instance)
			                                       	{
			                                       		instance["DishID"] = dishTwo;
			                                       		instance["GoodID"] = goodOne;
			                                       		instance["Quantity"] = (decimal)50;
			                                       	});

			BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishGood",
			                                       delegate(ObjectInstance instance)
			                                       	{
			                                       		instance["DishID"] = dishTwo;
			                                       		instance["GoodID"] = goodTwo;
			                                       		instance["Quantity"] = (decimal)500;
			                                       	});

			//BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishGood",
			//                                   delegate(ObjectInstance instance)
			//                                   {
			//                                       instance["DishID"] = dishTwo;
			//                                       instance["GoodID"] = goodThree;
			//                                       instance["Quantity"] = (decimal)100;
			//                                   });

			#endregion

			#region DishThree

			BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishGood",
			                                       delegate(ObjectInstance instance)
			                                       	{
			                                       		instance["DishID"] = dishThree;
			                                       		instance["GoodID"] = goodOne;
			                                       		instance["Quantity"] = (decimal)70;
			                                       	});

			BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishGood",
			                                       delegate(ObjectInstance instance)
			                                       	{
			                                       		instance["DishID"] = dishThree;
			                                       		instance["GoodID"] = goodTwo;
			                                       		instance["Quantity"] = (decimal)700;
			                                       	});

			//BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "DishGood",
			//                                   delegate(ObjectInstance instance)
			//                                   {
			//                                       instance["DishID"] = dishThree;
			//                                       instance["GoodID"] = goodThree;
			//                                       instance["Quantity"] = (decimal)100;
			//                                   });

			#endregion
		}

		protected void createMenu()
		{
			#region Menus (1)

			menu = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "Menu",
			                                              delegate(ObjectInstance instance)
			                                              	{
			                                              		instance["Name"] = "Основное меню";
			                                              		instance["Active"] = true;
			                                              	});

			#endregion

			#region Meal times (3)

			mealTimeOne = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "MealTime",
			                                                     delegate(ObjectInstance instance)
			                                                     	{
			                                                     		instance["MenuID"] = menu;
			                                                     		instance["Name"] = "Завтрак";
			                                                     		instance["OrderIndex"] = 1;
			                                                     		instance["Active"] = true;
			                                                     	});

			mealTimeTwo = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "MealTime",
			                                                     delegate(ObjectInstance instance)
			                                                     	{
			                                                     		instance["MenuID"] = menu;
			                                                     		instance["Name"] = "Обед";
			                                                     		instance["OrderIndex"] = 2;
			                                                     		instance["Active"] = true;
			                                                     	});

			mealTimeThree = BusinessObjectHelper.CreatePlainObject(adapter, "MealCalc", "MealTime",
			                                                       delegate(ObjectInstance instance)
			                                                       	{
			                                                       		instance["MenuID"] = menu;
			                                                       		instance["Name"] = "Ужин";
			                                                       		instance["OrderIndex"] = 3;
			                                                       		instance["Active"] = true;
			                                                       	});

			#endregion
		}

		protected void openCalcDay()
		{
			DateTime calcDate = DateTime.Today;
			var method_parameters = new AttributeValueSet { { "CalcDate", calcDate }, { "ID", DBNull.Value } };
			adapter.AccessProvider.ExecuteProcedure("MealCalc", "CalcDay", "InsertByDateTime", method_parameters, null);
			calcDay = (int)method_parameters["ID"];
		}
	}
}