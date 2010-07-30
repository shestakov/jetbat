SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodWriteOffDocument_OnRollback]
	@DocumentID int
AS
UPDATE dbo.MealCalc_GoodIncome SET
	dbo.MealCalc_GoodIncome.QuantityWrittenOff = dbo.MealCalc_GoodIncome.QuantityWrittenOff - dbo.MealCalc_GoodWriteOffDocumentDetail.Quantity
FROM dbo.MealCalc_GoodIncome
	INNER JOIN dbo.MealCalc_GoodWriteOffDocumentDetail ON dbo.MealCalc_GoodIncome.ID = dbo.MealCalc_GoodWriteOffDocumentDetail.GoodIncomeID
	INNER JOIN dbo.MealCalc_GoodWriteOffDocument ON dbo.MealCalc_GoodWriteOffDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodWriteOffDocument.ID
	INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodWriteOffDocument.ID = dbo.MultiversionDocument_Document.CurrentVersionID
WHERE dbo.MultiversionDocument_Document.ID = @DocumentID
GO