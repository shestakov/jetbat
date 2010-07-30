SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodIncomeDocument_OnRollback]
	@DocumentID int
AS
UPDATE dbo.MealCalc_GoodIncome SET
	dbo.MealCalc_GoodIncome.Deleted = 1,
	dbo.MealCalc_GoodIncome.Quantity = 0
FROM dbo.MealCalc_GoodIncome
	INNER JOIN dbo.MealCalc_GoodIncomeDocumentDetail ON dbo.MealCalc_GoodIncomeDocumentDetail.ID = dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID
	INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodIncomeDocument.ID
	INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MultiversionDocument_Document.CurrentVersionID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDay.ID = dbo.MealCalc_GoodIncomeDocument.IncomeCalcDayID
WHERE dbo.MultiversionDocument_Document.ID = @DocumentID
GO