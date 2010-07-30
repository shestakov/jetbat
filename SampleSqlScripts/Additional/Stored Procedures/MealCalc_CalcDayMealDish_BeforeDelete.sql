SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMealDish_BeforeDelete]
	@ID int OUTPUT,
	@ErrorMessages XML OUTPUT
AS
SET NOCOUNT ON
	
--�������� ��������������
UPDATE dbo.MealCalc_GoodIncome
	SET dbo.MealCalc_GoodIncome.QuantityReserved = dbo.MealCalc_GoodIncome.QuantityReserved -
		ISNULL
		(
			(
				SELECT SUM(dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity)
				FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
					INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
				WHERE dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID = @ID
					AND dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
			),
			0
		)

--������� ������ � ��������
DELETE FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
WHERE dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID = @ID

--������� ������ � �������
DELETE FROM dbo.MealCalc_CalcDayMenuGoodSpend
WHERE CalcDayMealDishID = @ID
GO