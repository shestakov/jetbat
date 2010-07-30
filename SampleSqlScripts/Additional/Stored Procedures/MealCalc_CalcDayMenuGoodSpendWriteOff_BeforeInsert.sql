SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff_BeforeInsert]
	@ID int OUTPUT,
	@CalcDayMenuGoodSpendID int OUTPUT,
	@GoodIncomeID int OUTPUT,
	@Quantity decimal(18, 3) OUTPUT,
	@ErrorMessages XML OUTPUT
AS
SET NOCOUNT ON
	
DECLARE @ErrorList TABLE
(
	Message VARCHAR(2000),
	AttributeName VARCHAR(128) NULL,
	AttributeFriendlyName VARCHAR(200) NULL,
	Severity INT
)

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

IF (SELECT dbo.MealCalc_GoodIncome.Deleted FROM dbo.MealCalc_GoodIncome WHERE dbo.MealCalc_GoodIncome.ID = @GoodIncomeID) = 1
BEGIN
	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
	SELECT
		'Указанная приходная позиция удалена',
		NULL,
		NULL,
		2
END

IF ISNULL
(
	(
		SELECT dbo.MealCalc_GoodIncome.Quantity - dbo.MealCalc_GoodIncome.QuantityWrittenOff - dbo.MealCalc_GoodIncome.QuantityWrittenOff
		FROM dbo.MealCalc_GoodIncome
		WHERE dbo.MealCalc_GoodIncome.ID = @GoodIncomeID
	),
	0
) < @Quantity
BEGIN
	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
	SELECT
		'Указанная позиция содержит недостаточное количество товара для списания',
		NULL,
		NULL,
		2
END


SET @ErrorMessages =
(
	SELECT Message, AttributeName, AttributeFriendlyName, Severity
	FROM @ErrorList AS EL
	ORDER BY EL.Severity DESC, EL.AttributeName
	FOR XML RAW('row'), ROOT ('root')
)
GO