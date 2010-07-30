SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenu_ComputeWriteOff]
	@ID INT
AS
DECLARE @calcDayID INT
DECLARE @calcDayDate DATETIME
DECLARE @error_message varchar(1000)

SELECT
	@calcDayID = dbo.MealCalc_CalcDayMenu.CalcDayID,
	@calcDayDate = dbo.MealCalc_CalcDay.CalcDate
FROM MealCalc_CalcDayMenu
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDayMenu.CalcDayID = dbo.MealCalc_CalcDay.ID
WHERE dbo.MealCalc_CalcDayMenu.ID = @ID

--3. Попытка списания по каждой позиции
DECLARE @QuantityToSpend DECIMAL(18, 3)
DECLARE @SpendTable TABLE (ID INT IDENTITY, CalcDayMenuGoodSpendID INT, GoodIncomeID INT, Quantity DECIMAL(18, 3))

DECLARE @goodSpendCusor_ID INT
DECLARE @goodSpendCusor_GoodID INT
DECLARE @goodSpendCusor_Quantity DECIMAL(18, 3)

DECLARE @goodIncomeCursor_ID INT
DECLARE @goodIncomeCursor_GoodID INT
DECLARE @goodIncomeCursor_Quantity DECIMAL(18, 3)
DECLARE @goodIncomeCursor_Price DECIMAL(18, 3)
DECLARE @goodIncomeCursor_WrittenOffQuantity DECIMAL(18, 3)

--курсор для таблицы расхода
DECLARE goodSpendCusor CURSOR FOR SELECT
	MealCalc_CalcDayMenuGoodSpend.ID,
	MealCalc_CalcDayMenuGoodSpend.GoodID,
	MealCalc_CalcDayMenuGoodSpend.QuantityFact
FROM dbo.MealCalc_CalcDayMenuGoodSpend
	INNER JOIN dbo.MealCalc_CalcDayMenu ON dbo.MealCalc_CalcDayMenu.ID = dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID
	WHERE dbo.MealCalc_CalcDayMenu.CalcDayID = @calcDayID

OPEN goodSpendCusor
FETCH NEXT FROM goodSpendCusor
INTO
	@goodSpendCusor_ID,
	@goodSpendCusor_GoodID,
	@goodSpendCusor_Quantity

WHILE @@FETCH_STATUS = 0
BEGIN
	--курсор для таблицы приходования	
	DECLARE goodIncomeCursor CURSOR FOR
	SELECT 
		MealCalc_GoodIncome.ID,
		MealCalc_GoodIncome.GoodID,
		MealCalc_GoodIncome.Quantity,
		MealCalc_GoodIncome.Price,
		MealCalc_GoodIncome.QuantityWrittenOff
	FROM dbo.MealCalc_GoodIncome
		INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDay.ID = dbo.MealCalc_GoodIncome.CalcDayID
	WHERE
		dbo.MealCalc_CalcDay.CalcDate <= @calcDayDate AND
		dbo.MealCalc_GoodIncome.GoodID = @goodSpendCusor_GoodID AND
		dbo.MealCalc_GoodIncome.Quantity > ISNULL(MealCalc_GoodIncome.QuantityWrittenOff, 0) AND
		dbo.MealCalc_GoodIncome.Deleted = 0
	ORDER BY dbo.MealCalc_CalcDay.CalcDate DESC
	FOR UPDATE OF QuantityWrittenOff

	OPEN goodIncomeCursor
	FETCH NEXT FROM goodIncomeCursor
	INTO
		@goodIncomeCursor_ID,
		@goodIncomeCursor_GoodID,
		@goodIncomeCursor_Quantity,
		@goodIncomeCursor_Price,
		@goodIncomeCursor_WrittenOffQuantity

	WHILE @@FETCH_STATUS = 0 AND @goodSpendCusor_Quantity > 0
	BEGIN
		IF @goodIncomeCursor_Quantity - ISNULL(@goodIncomeCursor_WrittenOffQuantity, 0) > @goodSpendCusor_Quantity
			SET @QuantityToSpend = @goodSpendCusor_Quantity
		ELSE
			SET @QuantityToSpend = @goodIncomeCursor_Quantity - ISNULL(@goodIncomeCursor_WrittenOffQuantity, 0)
		
		INSERT INTO @SpendTable (CalcDayMenuGoodSpendID, GoodIncomeID, Quantity)
		VALUES (@goodSpendCusor_ID, @goodIncomeCursor_ID, @QuantityToSpend)

		UPDATE MealCalc_GoodIncome SET QuantityWrittenOff = ISNULL(QuantityWrittenOff, 0) + @QuantityToSpend
		WHERE CURRENT OF goodIncomeCursor

		SET @goodSpendCusor_Quantity = @goodSpendCusor_Quantity - @QuantityToSpend

		FETCH NEXT FROM goodIncomeCursor INTO
			@goodIncomeCursor_ID,
			@goodIncomeCursor_GoodID,
			@goodIncomeCursor_Quantity,
			@goodIncomeCursor_Price,
			@goodIncomeCursor_WrittenOffQuantity
	END

	IF (@goodSpendCusor_Quantity != 0)
	BEGIN
		CLOSE goodIncomeCursor
		DEALLOCATE goodIncomeCursor
		
		CLOSE goodSpendCusor
		DEALLOCATE goodSpendCusor
		
		SET @error_message = 'Недостаток товара ' + (SELECT Name FROM MealCalc_Good WHERE ID = @goodSpendCusor_GoodID) + ', недостача ' + CAST(@goodSpendCusor_Quantity AS varchar(50))
		RAISERROR (@error_message, 14, 1)
		--ROLLBACK TRANSACTION
		RETURN -2
	END	

	CLOSE goodIncomeCursor
	DEALLOCATE goodIncomeCursor

	FETCH NEXT FROM goodSpendCusor
	INTO
		@goodSpendCusor_ID,
		@goodSpendCusor_GoodID,
		@goodSpendCusor_Quantity
END

CLOSE goodSpendCusor
DEALLOCATE goodSpendCusor

DELETE FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
	FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
		INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = dbo.MealCalc_CalcDayMenuGoodSpend.ID
	WHERE dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID = @ID

INSERT INTO dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
(
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID,
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID,
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity
)
SELECT
	CalcDayMenuGoodSpendID,
	GoodIncomeID,
	Quantity
FROM @SpendTable

UPDATE dbo.MealCalc_CalcDayMenu SET
	dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = 3
WHERE dbo.MealCalc_CalcDayMenu.ID = @ID
GO