SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenuGoodSpend_BeforeDelete]
	@ID int OUTPUT,
	@ErrorMessages XML OUTPUT
AS
SET NOCOUNT ON

--отменить резервирование
UPDATE dbo.MealCalc_GoodIncome
	SET dbo.MealCalc_GoodIncome.QuantityReserved = dbo.MealCalc_GoodIncome.QuantityReserved -
		ISNULL
		(
			(
				SELECT SUM(dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity)
				FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
				WHERE dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = @ID
					AND dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
			),
			0
		)
WHERE EXISTS
(
	SELECT 0 FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff WHERE dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
)

--удалить записи о списании
DELETE FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
WHERE dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = @ID
GO