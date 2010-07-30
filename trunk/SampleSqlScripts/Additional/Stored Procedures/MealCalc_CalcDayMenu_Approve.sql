SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenu_Approve]
	@ID INT
AS

IF EXISTS
(
	SELECT 0 FROM dbo.MealCalc_CalcDayMenuGoodSpend
		INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpendWriteOff ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
		INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
	WHERE dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID = @ID
		AND dbo.MealCalc_GoodIncome.Deleted = 1
		
)
BEGIN
	RAISERROR('Закрыть меню невозможно: некоторые приходные позиции, по которым осуществлено резервирование, были удалены. Необходимо пересчитать калькуляцию', 11, 1)
	RETURN
END

IF EXISTS
(
	SELECT 0 FROM dbo.MealCalc_CalcDayMenuGoodSpend
	WHERE dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID = @ID
		AND dbo.MealCalc_CalcDayMenuGoodSpend.QuantityFact != ISNULL((SELECT SUM(Quantity) FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff WHERE dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = dbo.MealCalc_CalcDayMenuGoodSpend.ID), 0)
)
BEGIN
	RAISERROR('Закрыть меню невозможно: по некоторым позициям количество списанных продуктов не равно запланированному количеству', 11, 2)
	RETURN
END

UPDATE dbo.MealCalc_CalcDayMenu SET
	dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = 2
WHERE dbo.MealCalc_CalcDayMenu.ID = @ID
GO