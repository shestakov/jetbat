SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff_BeforeDelete]
	@ID int OUTPUT,
	@ErrorMessages XML OUTPUT
AS
SET NOCOUNT ON

IF 
	(
		SELECT
			dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID
		FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
			INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = dbo.MealCalc_CalcDayMenuGoodSpend.ID
			INNER JOIN dbo.MealCalc_CalcDayMenu ON dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID = dbo.MealCalc_CalcDayMenu.ID
		WHERE dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.ID = @ID
	)
	!= 1
BEGIN
	RAISERROR ('»зменение меню невозможно, посколько калькул€ци€ уже утверждена', 11, 1)
	RETURN -1
END

--отменить резервирование
UPDATE dbo.MealCalc_GoodIncome SET
	dbo.MealCalc_GoodIncome.QuantityReserved = dbo.MealCalc_GoodIncome.QuantityReserved - ISNULL(dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity, 0)
FROM dbo.MealCalc_GoodIncome
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpendWriteOff ON dbo.MealCalc_GoodIncome.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID
WHERE dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.ID = @ID
GO