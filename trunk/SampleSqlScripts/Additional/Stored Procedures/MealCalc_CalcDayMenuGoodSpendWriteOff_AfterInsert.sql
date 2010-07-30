SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff_AfterInsert]
	@ID int,
	@CalcDayMenuGoodSpendID int,
	@GoodIncomeID int,
	@Quantity decimal(18, 3)
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
	RAISERROR ('Изменение меню невозможно, посколько калькуляция уже утверждена', 11, 1)
	RETURN -1
END

--зарезервировать
UPDATE dbo.MealCalc_GoodIncome SET
	dbo.MealCalc_GoodIncome.QuantityReserved = dbo.MealCalc_GoodIncome.QuantityReserved + ISNULL(@Quantity, 0)
WHERE dbo.MealCalc_GoodIncome.ID = @GoodIncomeID
GO