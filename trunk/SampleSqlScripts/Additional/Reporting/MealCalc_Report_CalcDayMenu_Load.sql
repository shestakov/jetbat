SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_CalcDayMenu_Load]
	@CalcDayMenuID INT
AS
SELECT
	dbo.MealCalc_CalcDay.CalcDate AS CalcDayMenuDate,
	dbo.MealCalc_Menu.Name AS MenuName
FROM dbo.MealCalc_CalcDayMenu
	INNER JOIN dbo.MealCalc_Menu ON dbo.MealCalc_CalcDayMenu.MenuID = dbo.MealCalc_Menu.ID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDayMenu.CalcDayID = dbo.MealCalc_CalcDay.ID
WHERE dbo.MealCalc_CalcDayMenu.ID = @CalcDayMenuID
GO