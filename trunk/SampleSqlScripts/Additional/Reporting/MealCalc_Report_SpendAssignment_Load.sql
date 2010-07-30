SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_SpendAssignment_Load]
	@GoodSpendByCalcDayID INT
AS
--DECLARE @GoodSpendByCalcDayID INT
--SET @GoodSpendByCalcDayID = 67
SELECT
	MealCalc_CalcDay.CalcDate [CalcDaty_CalcDate],
	MealCalc_MealTime.Name [MealTime_Name],
	MealCalc_Dish.Name [Dish_Name],
	ROUND
	(
		(
			SELECT SUM(0.001 * MealCalc_DishGood2.Quantity * MealCalc_CalcDayMealDish2.PortionCount * MealCalc_GoodSpendByCalcDayGood2.Price)
			FROM MealCalc_CalcDayMealDish [MealCalc_CalcDayMealDish2]
				LEFT JOIN MealCalc_Dish [MealCalc_Dish2]
					ON MealCalc_Dish2.ID = MealCalc_CalcDayMealDish2.DishID
				LEFT JOIN MealCalc_DishGood [MealCalc_DishGood2]
					ON MealCalc_DishGood2.DishID = MealCalc_Dish2.ID
				LEFT JOIN MealCalc_GoodSpendByCalcDayGood [MealCalc_GoodSpendByCalcDayGood2]
					ON
						MealCalc_GoodSpendByCalcDayGood2.GoodID = MealCalc_DishGood2.GoodID AND
						MealCalc_GoodSpendByCalcDayGood2.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID
			WHERE MealCalc_CalcDayMealDish2.ID = MealCalc_CalcDayMealDish.ID
		) / MealCalc_CalcDayMealDish.PortionCount, 2
	) [Dish_Price],
	MealCalc_CalcDayMealDish.PortionCount [CalcDayMealDish_PortionCount],
	MealCalc_Dish.WorkOut [Dish_WorkOut],
	MealCalc_Good.Name [Good_Name],
	ROUND(MealCalc_GoodSpendByCalcDayGood.Price, 2) [GoodSpendByCalcDayGood_Price],
	ROUND(MealCalc_DishGood.Quantity, 2) [DishGood.Quantity],
	ROUND(MealCalc_GoodSpendByCalcDayGood.Quantity, 2) [GoodSpendByCalcDayGood.Quantity],
	ROUND(MealCalc_GoodSpendByCalcDayGood.Quantity * MealCalc_GoodSpendByCalcDayGood.Price, 2) [GoodSpendByCalcDayGood_Total],
	ROUND((
		SELECT SUM(0.001 * MealCalc_DishGood2.Quantity * MealCalc_CalcDayMealDish2.PortionCount * MealCalc_GoodSpendByCalcDayGood2.Price)
		FROM MealCalc_CalcDayMealDish [MealCalc_CalcDayMealDish2]
			LEFT JOIN MealCalc_Dish [MealCalc_Dish2]
				ON MealCalc_Dish2.ID = MealCalc_CalcDayMealDish2.DishID
			LEFT JOIN MealCalc_DishGood [MealCalc_DishGood2]
				ON MealCalc_DishGood2.DishID = MealCalc_Dish2.ID
			LEFT JOIN MealCalc_GoodSpendByCalcDayGood [MealCalc_GoodSpendByCalcDayGood2]
				ON
					MealCalc_GoodSpendByCalcDayGood2.GoodID = MealCalc_DishGood2.GoodID AND
					MealCalc_GoodSpendByCalcDayGood2.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID
		WHERE MealCalc_CalcDayMealDish2.ID = MealCalc_CalcDayMealDish.ID
	), 2) [MealCalc_CalcDayMealDish_TotalGoods],
	ROUND(ROUND
	(
		(
			SELECT SUM(0.001 * MealCalc_DishGood2.Quantity * MealCalc_CalcDayMealDish2.PortionCount * MealCalc_GoodSpendByCalcDayGood2.Price)
			FROM MealCalc_CalcDayMealDish [MealCalc_CalcDayMealDish2]
				LEFT JOIN MealCalc_Dish [MealCalc_Dish2]
					ON MealCalc_Dish2.ID = MealCalc_CalcDayMealDish2.DishID
				LEFT JOIN MealCalc_DishGood [MealCalc_DishGood2]
					ON MealCalc_DishGood2.DishID = MealCalc_Dish2.ID
				LEFT JOIN MealCalc_GoodSpendByCalcDayGood [MealCalc_GoodSpendByCalcDayGood2]
					ON
						MealCalc_GoodSpendByCalcDayGood2.GoodID = MealCalc_DishGood2.GoodID AND
						MealCalc_GoodSpendByCalcDayGood2.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID
			WHERE MealCalc_CalcDayMealDish2.ID = MealCalc_CalcDayMealDish.ID
		) / MealCalc_CalcDayMealDish.PortionCount, 2
	) * MealCalc_CalcDayMealDish.PortionCount, 2) [MealCalc_CalcDayMealDish_TotalDishes]
FROM MultiversionDocument_Document
	INNER JOIN MealCalc_GoodSpendByCalcDay
		ON MealCalc_GoodSpendByCalcDay.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN MealCalc_GoodSpendByCalcDayGood
		ON MealCalc_GoodSpendByCalcDayGood.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN MealCalc_CalcDayMealDish
		ON
			MealCalc_CalcDayMealDish.ID = MealCalc_GoodSpendByCalcDayGood.CalcDayMealDishID AND
			MealCalc_CalcDayMealDish.Deleted = 0
	INNER JOIN MealCalc_Dish
		ON MealCalc_Dish.ID = MealCalc_CalcDayMealDish.DishID
	INNER JOIN MealCalc_Good
		ON MealCalc_Good.ID = MealCalc_GoodSpendByCalcDayGood.GoodID
	INNER JOIN MealCalc_DishGood
		ON 
			MealCalc_DishGood.GoodID = MealCalc_GoodSpendByCalcDayGood.GoodID AND
			MealCalc_DishGood.DishID = MealCalc_CalcDayMealDish.DishID
	INNER JOIN MealCalc_CalcDayMeal
		ON
			MealCalc_CalcDayMeal.ID = MealCalc_CalcDayMealDish.CalcDayMealID AND
			MealCalc_CalcDayMeal.Deleted = 0
	INNER JOIN MealCalc_MealTime
		ON MealCalc_MealTime.ID = MealCalc_CalcDayMeal.MealTimeID
	INNER JOIN MealCalc_CalcDay
		ON MealCalc_CalcDay.ID = MealCalc_GoodSpendByCalcDay.CalcDayID
WHERE MultiversionDocument_Document.ID = @GoodSpendByCalcDayID
ORDER BY MealCalc_MealTime.OrderIndex, MealCalc_Dish.Name, MealCalc_Good.Name
GO