SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMealDish_Recompute]
	@ID INT,
	@ErrorMessages XML OUTPUT
AS
SET NOCOUNT ON

DECLARE @ErrorMessage NVARCHAR(4000);
DECLARE @ErrorSeverity INT;
DECLARE @ErrorState INT;

DECLARE @CalcDayMealID INT
DECLARE @DishID INT
DECLARE @PortionCount INT

DECLARE @ErrorList TABLE
(
	Message VARCHAR(2000),
	AttributeName VARCHAR(128) NULL,
	AttributeFriendlyName VARCHAR(200) NULL,
	Severity INT
)

SELECT
	@CalcDayMealID = CalcDayMealID,
	@DishID = DishID,
	@PortionCount = PortionCount
FROM dbo.MealCalc_CalcDayMealDish
WHERE ID = @ID

IF @DishID IS NULL
BEGIN
	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
	SELECT
		'Ѕлюдо меню учетного дн€ не найдено',
		NULL,
		NULL,
		2
END

IF NOT EXISTS (SELECT 0 FROM @ErrorList WHERE Severity > 1)
BEGIN TRY
	BEGIN TRAN
	
	--отменить резервирование
	UPDATE dbo.MealCalc_GoodIncome
		SET dbo.MealCalc_GoodIncome.QuantityReserved =
			(
				ISNULL(dbo.MealCalc_GoodIncome.QuantityReserved, 0) -
				ISNULL
				(
					(
						SELECT dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity
						FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
							INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
						WHERE dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID = @ID
							AND dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
					),
					0
				 )
			)

	--удалить записи о списании
	DELETE FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
	FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
		INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
	WHERE dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID = @ID

	--удалить записи о расходе
	DELETE FROM dbo.MealCalc_CalcDayMenuGoodSpend
	WHERE CalcDayMealDishID = @ID

	--ѕолучить идентификатор учетного дн€ и меню
	DECLARE @CalcDayMenuID INT
	DECLARE @calcDayID INT
	DECLARE @calcDayDate DATETIME

	SELECT
		@CalcDayMenuID = dbo.MealCalc_CalcDayMeal.CalcDayMenuID,
		@calcDayID = dbo.MealCalc_CalcDayMenu.CalcDayID,
		@calcDayDate = dbo.MealCalc_CalcDay.CalcDate
	FROM dbo.MealCalc_CalcDayMeal
		INNER JOIN MealCalc_CalcDayMenu ON dbo.MealCalc_CalcDayMeal.CalcDayMenuID = dbo.MealCalc_CalcDayMenu.ID
		INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDayMenu.CalcDayID = dbo.MealCalc_CalcDay.ID
	WHERE dbo.MealCalc_CalcDayMeal.ID = @CalcDayMealID

	--ѕолучить список продуктов добавл€емого блюда с учетом количества порций
	DECLARE @TableGoodsToInsert TABLE (GoodID INT, Quantity DECIMAL(18, 3))
	INSERT INTO @TableGoodsToInsert
	(
		GoodID,
		Quantity
	)
	SELECT
		dbo.MealCalc_DishGood.GoodID,
		@PortionCount * (dbo.MealCalc_DishGood.Quantity / 1000)
	FROM dbo.MealCalc_DishGood
	WHERE dbo.MealCalc_DishGood.DishID = @DishID

	--«арезервировать списываемые продукты и добавить записи в таблицу расхода
	DECLARE @QuantityToSpend DECIMAL(18, 3)

	DECLARE @CalcDayMenuGoodSpendID INT
	DECLARE @goodSpendCusor_GoodID INT
	DECLARE @goodSpendCusor_Quantity DECIMAL(18, 3)

	DECLARE @goodIncomeCursor_ID INT
	DECLARE @goodIncomeCursor_GoodID INT
	DECLARE @goodIncomeCursor_Quantity DECIMAL(18, 3)
	DECLARE @goodIncomeCursor_Price DECIMAL(18, 3)
	DECLARE @goodIncomeCursor_QantityAvailable DECIMAL(18, 3)

	--курсор дл€ таблицы расхода
	DECLARE goodSpendCusor CURSOR FOR SELECT
		GoodID,
		Quantity
	FROM @TableGoodsToInsert

	OPEN goodSpendCusor
	FETCH NEXT FROM goodSpendCusor
	INTO
		@goodSpendCusor_GoodID,
		@goodSpendCusor_Quantity

	WHILE @@FETCH_STATUS = 0
	BEGIN
		--ƒобавление записи в таблицу расхода
		INSERT INTO dbo.MealCalc_CalcDayMenuGoodSpend
		(
			dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID,
			dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID,
			dbo.MealCalc_CalcDayMenuGoodSpend.GoodID,
			dbo.MealCalc_CalcDayMenuGoodSpend.QuantityFact,
			dbo.MealCalc_CalcDayMenuGoodSpend.QuantityPlan
		)
		VALUES
		(
			@CalcDayMenuID,
			@ID,
			@goodSpendCusor_GoodID,
			@goodSpendCusor_Quantity,
			@goodSpendCusor_Quantity
		)

		SET @CalcDayMenuGoodSpendID = SCOPE_IDENTITY()

		--курсор дл€ таблицы приходовани€	
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
			dbo.MealCalc_GoodIncome.GoodID = @goodSpendCusor_GoodID AND
			dbo.MealCalc_GoodIncome.Quantity > ISNULL(MealCalc_GoodIncome.QuantityWrittenOff, 0) + ISNULL(MealCalc_GoodIncome.QuantityReserved, 0) AND
			dbo.MealCalc_GoodIncome.Deleted = 0
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

		WHILE @@FETCH_STATUS = 0 AND @goodSpendCusor_Quantity > 0
		BEGIN

	--DECLARE @error_message NVARCHAR(1000)
	--SET @error_message = (SELECT COUNT(0) FROM @TableGoodsToInsert)
	--RAISERROR(@error_message, 13, 1)


			IF @goodIncomeCursor_QantityAvailable > @goodSpendCusor_Quantity
				SET @QuantityToSpend = @goodSpendCusor_Quantity
			ELSE
				SET @QuantityToSpend = @goodIncomeCursor_QantityAvailable
			
			--ƒобавление записи в таблицу списани€
			INSERT INTO dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
			(
				dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID,
				dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID,
				dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity
			)
			VALUES
			(
				@CalcDayMenuGoodSpendID,
				@goodIncomeCursor_ID,
				@QuantityToSpend
			)

			UPDATE MealCalc_GoodIncome SET QuantityReserved = ISNULL(QuantityReserved, 0) + @QuantityToSpend
			WHERE CURRENT OF goodIncomeCursor

			SET @goodSpendCusor_Quantity = @goodSpendCusor_Quantity - @QuantityToSpend

			FETCH NEXT FROM goodIncomeCursor INTO
				@goodIncomeCursor_ID,
				@goodIncomeCursor_GoodID,
				@goodIncomeCursor_Quantity,
				@goodIncomeCursor_Price,
				@goodIncomeCursor_QantityAvailable
		END

		IF (@goodSpendCusor_Quantity != 0)
		BEGIN
			SET @goodSpendCusor_Quantity = 0

			INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
			SELECT
				'Ќедостаток продукта: ' + (SELECT dbo.MealCalc_Good.Name FROM dbo.MealCalc_Good WHERE dbo.MealCalc_Good.ID = @goodSpendCusor_GoodID),
				NULL,
				NULL,
				1
		END

		CLOSE goodIncomeCursor
		DEALLOCATE goodIncomeCursor

		FETCH NEXT FROM goodSpendCusor
		INTO
			@goodSpendCusor_GoodID,
			@goodSpendCusor_Quantity
	END

	CLOSE goodSpendCusor
	DEALLOCATE goodSpendCusor
	
	COMMIT TRAN
END TRY
BEGIN CATCH
    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

	IF XACT_STATE() != 0
    BEGIN
        ROLLBACK TRANSACTION;
    END
	
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH

SET @ErrorMessages =
(
	SELECT Message, AttributeName, AttributeFriendlyName, Severity
	FROM @ErrorList as EL
	ORDER BY EL.Severity DESC, EL.AttributeName
	FOR XML RAW('row'), ROOT ('root')
)

RETURN(SELECT TOP(1) Severity FROM @ErrorList)
GO