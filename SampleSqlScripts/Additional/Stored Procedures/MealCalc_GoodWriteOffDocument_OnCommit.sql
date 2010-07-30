SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodWriteOffDocument_OnCommit]
	@DocumentID int
AS

IF EXISTS
(
	SELECT 0 FROM dbo.MealCalc_GoodIncome
		INNER JOIN dbo.MealCalc_GoodWriteOffDocumentDetail ON dbo.MealCalc_GoodIncome.ID = dbo.MealCalc_GoodWriteOffDocumentDetail.GoodIncomeID
		INNER JOIN dbo.MealCalc_GoodWriteOffDocument ON dbo.MealCalc_GoodWriteOffDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodWriteOffDocument.ID
		INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodWriteOffDocument.ID = dbo.MultiversionDocument_Document.CurrentVersionID
	WHERE dbo.MultiversionDocument_Document.ID = @DocumentID
		AND dbo.MealCalc_GoodIncome.Deleted = 1
		AND dbo.MealCalc_GoodIncome.Quantity - dbo.MealCalc_GoodIncome.QuantityReserved - dbo.MealCalc_GoodIncome.QuantityWrittenOff - MealCalc_GoodWriteOffDocumentDetail.Quantity < 0
)
BEGIN
	RAISERROR('Ќекоторые приходные позиции, по которым проводитс€ списание, были отменены', 11, 1)
	RETURN
END

IF EXISTS
(
	SELECT 0 FROM dbo.MealCalc_GoodIncome
		INNER JOIN dbo.MealCalc_GoodWriteOffDocumentDetail ON dbo.MealCalc_GoodIncome.ID = dbo.MealCalc_GoodWriteOffDocumentDetail.GoodIncomeID
		INNER JOIN dbo.MealCalc_GoodWriteOffDocument ON dbo.MealCalc_GoodWriteOffDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodWriteOffDocument.ID
		INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodWriteOffDocument.ID = dbo.MultiversionDocument_Document.CurrentVersionID
	WHERE dbo.MultiversionDocument_Document.ID = @DocumentID
		AND dbo.MealCalc_GoodIncome.Quantity - dbo.MealCalc_GoodIncome.QuantityWrittenOff - dbo.MealCalc_GoodIncome.QuantityReserved < dbo.MealCalc_GoodWriteOffDocumentDetail.Quantity
)
BEGIN
	RAISERROR('ѕо некоторым позици€м недостаточно доступных продуктов дл€ списани€', 11, 2)
	RETURN
END

UPDATE dbo.MealCalc_GoodIncome SET
	dbo.MealCalc_GoodIncome.QuantityWrittenOff = dbo.MealCalc_GoodIncome.QuantityWrittenOff + dbo.MealCalc_GoodWriteOffDocumentDetail.Quantity
FROM dbo.MealCalc_GoodIncome
	INNER JOIN dbo.MealCalc_GoodWriteOffDocumentDetail ON dbo.MealCalc_GoodIncome.ID = dbo.MealCalc_GoodWriteOffDocumentDetail.GoodIncomeID
	INNER JOIN dbo.MealCalc_GoodWriteOffDocument ON dbo.MealCalc_GoodWriteOffDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodWriteOffDocument.ID
	INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodWriteOffDocument.ID = dbo.MultiversionDocument_Document.CurrentVersionID
WHERE dbo.MultiversionDocument_Document.ID = @DocumentID
GO