SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenu_Unapprove]
	@ID INT
AS

IF EXISTS
(
	SELECT 0 FROM dbo.MealCalc_GoodSpendDocument
		INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodSpendDocument.ID = dbo.MultiversionDocument_Document.CurrentVersionID
	WHERE dbo.MealCalc_GoodSpendDocument.CalcDayMenuID = @ID
		AND NOT dbo.MultiversionDocument_Document.DocumentStateID IN (100)
)
BEGIN
	RAISERROR('Для этого меню учетного дня уже был создан расходный документ. Пожалуйста, удалите его', 11, 1)
	RETURN
END

UPDATE dbo.MealCalc_CalcDayMenu SET
	dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = 1
WHERE dbo.MealCalc_CalcDayMenu.ID = @ID
GO