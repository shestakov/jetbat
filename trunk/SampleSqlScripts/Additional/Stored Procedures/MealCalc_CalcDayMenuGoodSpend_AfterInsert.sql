SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenuGoodSpend_AfterInsert]
	@ID int,
	@CalcDayMenuID int,
	@CalcDayMealDishID int,
	@GoodID int,
	@QuantityFact decimal(18, 3)
AS
SET NOCOUNT ON

DECLARE @QuantityLeft DECIMAL(18, 3)
DECLARE @QuantityToSpend DECIMAL(18, 3)

--Получить идентификатор учетного дня
DECLARE @calcDayID INT
DECLARE @calcDayDate DATETIME
SELECT
	@calcDayID = dbo.MealCalc_CalcDayMenu.CalcDayID,
	@calcDayDate = dbo.MealCalc_CalcDay.CalcDate
FROM dbo.MealCalc_CalcDayMenuGoodSpend
	INNER JOIN MealCalc_CalcDayMenu ON dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID = dbo.MealCalc_CalcDayMenu.ID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDayMenu.CalcDayID = dbo.MealCalc_CalcDay.ID
WHERE dbo.MealCalc_CalcDayMenuGoodSpend.ID = @ID

--добавить записи о списании
DECLARE @goodIncomeCursor_ID INT
DECLARE @goodIncomeCursor_GoodID INT
DECLARE @goodIncomeCursor_Quantity DECIMAL(18, 3)
DECLARE @goodIncomeCursor_Price DECIMAL(18, 3)
DECLARE @goodIncomeCursor_QantityAvailable DECIMAL(18, 3)

SET @QuantityLeft = @QuantityFact

--курсор для таблицы приходования	
DECLARE goodIncomeCursor CURSOR FOR
SELECT 
	MealCalc_GoodIncome.ID,
	MealCalc_GoodIncome.GoodID,
	MealCalc_GoodIncome.Quantity,
	MealCalc_GoodIncome.Price,
	ISNULL(MealCalc_GoodIncome.Quantity, 0) - ISNULL(MealCalc_GoodIncome.QuantityWrittenOff, 0) - ISNULL(MealCalc_GoodIncome.QuantityReserved, 0)
FROM dbo.MealCalc_GoodIncome
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDay.ID = dbo.MealCalc_GoodIncome.CalcDayID
WHERE
	dbo.MealCalc_CalcDay.CalcDate <= @calcDayDate AND
	dbo.MealCalc_GoodIncome.GoodID = @GoodID AND
	dbo.MealCalc_GoodIncome.Quantity > ISNULL(MealCalc_GoodIncome.QuantityWrittenOff, 0) + ISNULL(MealCalc_GoodIncome.QuantityReserved, 0)
ORDER BY dbo.MealCalc_CalcDay.CalcDate DESC
FOR UPDATE OF QuantityReserved

OPEN goodIncomeCursor
FETCH NEXT FROM goodIncomeCursor
INTO
	@goodIncomeCursor_ID,
	@goodIncomeCursor_GoodID,
	@goodIncomeCursor_Quantity,
	@goodIncomeCursor_Price,
	@goodIncomeCursor_QantityAvailable

WHILE @@FETCH_STATUS = 0 AND @QuantityLeft > 0
BEGIN

	IF @goodIncomeCursor_QantityAvailable > @QuantityLeft
		SET @QuantityToSpend = @QuantityLeft
	ELSE
		SET @QuantityToSpend = @goodIncomeCursor_QantityAvailable
	
	--Добавление записи в таблицу списания
	INSERT INTO dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
	(
		dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID,
		dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID,
		dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity
	)
	VALUES
	(
		@ID,
		@goodIncomeCursor_ID,
		@QuantityToSpend
	)

	UPDATE MealCalc_GoodIncome SET QuantityReserved = ISNULL(QuantityReserved, 0) + @QuantityToSpend
	WHERE CURRENT OF goodIncomeCursor

	SET @QuantityLeft = @QuantityLeft - @QuantityToSpend

	FETCH NEXT FROM goodIncomeCursor INTO
		@goodIncomeCursor_ID,
		@goodIncomeCursor_GoodID,
		@goodIncomeCursor_Quantity,
		@goodIncomeCursor_Price,
		@goodIncomeCursor_QantityAvailable
END

CLOSE goodIncomeCursor
DEALLOCATE goodIncomeCursor
GO