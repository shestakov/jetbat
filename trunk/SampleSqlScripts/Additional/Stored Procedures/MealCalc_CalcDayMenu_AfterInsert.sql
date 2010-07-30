SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenu_AfterInsert]
	@ID int,
	@CalcDayID int,
	@MenuID int,
	@Comment nvarchar(2000)
AS
BEGIN
	INSERT INTO dbo.MealCalc_CalcDayMeal
	(
		dbo.MealCalc_CalcDayMeal.CalcDayMenuID,
		dbo.MealCalc_CalcDayMeal.MealTimeID
	)
	SELECT
		@ID,
		ID
	FROM MealCalc_MealTime
	WHERE dbo.MealCalc_MealTime.MenuID = @MenuID
END

GO