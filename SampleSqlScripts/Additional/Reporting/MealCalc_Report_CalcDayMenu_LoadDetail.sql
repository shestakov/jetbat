SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_CalcDayMenu_LoadDetail]
	@CalcDayMenuID INT
AS
SELECT
	dbo.MealCalc_MealTime.Name AS MealTimeName,
	dbo.MealCalc_Dish.Name AS DishName,
	CAST
	(
		(
			SELECT
				SUM(dbo.MealCalc_GoodIncome.Price)
			FROM dbo.MealCalc_CalcDayMealDish CDMD
				INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON CDMD.ID = dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID
				INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpendWriteOff ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
				INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
			WHERE CDMD.ID = MealCalc_CalcDayMealDish.ID
		) / dbo.MealCalc_CalcDayMealDish.PortionCount
		AS DECIMAL(18, 2)
	) AS PortionPrice,
	dbo.MealCalc_CalcDayMealDish.PortionCount AS PortionCount,
	dbo.MealCalc_Dish.WorkOut AS WorkOut,
	dbo.MealCalc_Good.Name AS GoodName,
	dbo.MealCalc_GoodIncome.Price AS Price,
	dbo.MealCalc_DishGood.Quantity AS QuantityPerPortion,
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity AS QuantityTotal,
	CAST((dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity * dbo.MealCalc_GoodIncome.Price) AS DECIMAL(13, 2)) AS PriceTotal
FROM dbo.MealCalc_CalcDayMenu
	INNER JOIN dbo.MealCalc_CalcDayMeal ON dbo.MealCalc_CalcDayMenu.ID = dbo.MealCalc_CalcDayMeal.CalcDayMenuID
	INNER JOIN dbo.MealCalc_CalcDayMealDish ON dbo.MealCalc_CalcDayMeal.ID = dbo.MealCalc_CalcDayMealDish.CalcDayMealID
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMealDish.ID = dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID
	LEFT JOIN dbo.MealCalc_DishGood ON
		dbo.MealCalc_CalcDayMealDish.DishID = dbo.MealCalc_DishGood.DishID AND
		dbo.MealCalc_CalcDayMenuGoodSpend.GoodID = dbo.MealCalc_DishGood.GoodID
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpendWriteOff ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
	INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
	INNER JOIN dbo.MealCalc_Good ON dbo.MealCalc_CalcDayMenuGoodSpend.GoodID = dbo.MealCalc_Good.ID
	INNER JOIN dbo.MealCalc_Dish ON dbo.MealCalc_CalcDayMealDish.DishID = dbo.MealCalc_Dish.ID
	INNER JOIN dbo.MealCalc_MealTime ON dbo.MealCalc_CalcDayMeal.MealTimeID = dbo.MealCalc_MealTime.ID
WHERE dbo.MealCalc_CalcDayMenu.ID = @CalcDayMenuID
GO