SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodSpendDocument_OnRollback]
	@DocumentID int
AS

DECLARE @WriteOff TABLE (GoodIncomeID INT, Quantity DECIMAL (18, 3))
INSERT INTO @WriteOff (GoodIncomeID, Quantity)
SELECT
	dbo.MealCalc_GoodIncome.ID,
	SUM(dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity)
FROM dbo.MealCalc_GoodIncome
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpendWriteOff ON dbo.MealCalc_GoodIncome.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = dbo.MealCalc_CalcDayMenuGoodSpend.ID
	INNER JOIN dbo.MealCalc_GoodSpendDocument ON dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID = dbo.MealCalc_GoodSpendDocument.CalcDayMenuID
WHERE dbo.MealCalc_GoodSpendDocument.ID = @DocumentID
GROUP BY dbo.MealCalc_GoodIncome.ID

UPDATE dbo.MealCalc_GoodIncome SET
	dbo.MealCalc_GoodIncome.QuantityReserved = dbo.MealCalc_GoodIncome.QuantityReserved + WriteOffTable.Quantity,
	dbo.MealCalc_GoodIncome.QuantityWrittenOff = dbo.MealCalc_GoodIncome.QuantityWrittenOff - WriteOffTable.Quantity
FROM dbo.MealCalc_GoodIncome
	INNER JOIN @WriteOff AS WriteOffTable ON dbo.MealCalc_GoodIncome.ID = WriteOffTable.GoodIncomeID

UPDATE dbo.MealCalc_CalcDayMenu	SET
	dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = 2
FROM dbo.MealCalc_CalcDayMenu
	INNER JOIN dbo.MealCalc_GoodSpendDocument ON dbo.MealCalc_CalcDayMenu.ID = dbo.MealCalc_GoodSpendDocument.CalcDayMenuID
WHERE dbo.MealCalc_GoodSpendDocument.ID = @DocumentID
GO