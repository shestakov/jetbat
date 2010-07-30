SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_GoodSpendDocument_Load]
	@DocumentID INT
AS
SELECT
	dbo.MealCalc_CalcDay.CalcDate AS SpendDate,
	dbo.MealCalc_Menu.Name AS MenuName,
	dbo.MealCalc_GoodSpendDocument.DocumentDateTime AS DocumentDate,
	dbo.MealCalc_GoodSpendDocument.DocumentNumber AS DocumentNumber
FROM MultiversionDocument_Document
	INNER JOIN dbo.MealCalc_GoodSpendDocument ON dbo.MealCalc_GoodSpendDocument.ID = MultiversionDocument_Document.CurrentVersionID
	LEFT JOIN dbo.MealCalc_CalcDayMenu ON dbo.MealCalc_GoodSpendDocument.CalcDayMenuID = dbo.MealCalc_CalcDayMenu.ID
	INNER JOIN dbo.MealCalc_Menu ON dbo.MealCalc_CalcDayMenu.MenuID = dbo.MealCalc_Menu.ID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDayMenu.CalcDayID = dbo.MealCalc_CalcDay.ID
WHERE MultiversionDocument_Document.ID = @DocumentID
GO