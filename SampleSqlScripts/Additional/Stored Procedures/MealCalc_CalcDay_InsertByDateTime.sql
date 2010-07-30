SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDay_InsertByDateTime]
	@CalcDate datetime,
	@ID int OUTPUT
AS
IF 
	DATEPART(hour, @CalcDate) != 0 OR
	DATEPART(minute, @CalcDate) != 0 OR
	DATEPART(second, @CalcDate) != 0 OR
	DATEPART(millisecond, @CalcDate) != 0
BEGIN
	RAISERROR('Ошибка при открытии рассчетного дня. Параметр должен указывать на начало суток', 16, 1)
	RETURN -1
END

IF EXISTS (SELECT 0 FROM MealCalc_CalcDay WHERE MealCalc_CalcDay.CalcDate = @CalcDate)
BEGIN
	RAISERROR('Ошибка при открытии рассчетного дня. Рассчетный день на указанную дату уже открыт', 16, 2)
	RETURN -1
END

INSERT INTO MealCalc_CalcDay (CalcDate)
VALUES (@CalcDate)
SET @ID = SCOPE_IDENTITY()
GO