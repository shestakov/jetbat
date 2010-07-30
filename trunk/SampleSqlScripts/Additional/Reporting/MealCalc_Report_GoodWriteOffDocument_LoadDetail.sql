SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_GoodWriteOffDocument_LoadDetail]
	@DocumentID INT
AS
SELECT
	MealCalc_GoodWriteOffDocumentDetail.SequenceNumber AS SequenceNumber,
	MealCalc_Good.Name AS GoodName,
	dbo.MealCalc_GoodIncome.GoodCommodityName AS GoodCommodityName,
	ROUND(dbo.MealCalc_GoodIncome.Price, 2) AS Price,
	ROUND(dbo.MealCalc_GoodWriteOffDocumentDetail.Quantity, 2) AS Quantity
FROM MultiversionDocument_Document
	INNER JOIN dbo.MealCalc_GoodWriteOffDocumentDetail ON dbo.MealCalc_GoodWriteOffDocumentDetail.DocumentVersionID = dbo.MultiversionDocument_Document.CurrentVersionID
	INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_GoodWriteOffDocumentDetail.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
	INNER JOIN MealCalc_Good ON MealCalc_Good.ID = dbo.MealCalc_GoodIncome.GoodID
WHERE MultiversionDocument_Document.ID = @DocumentID
ORDER BY MealCalc_GoodWriteOffDocumentDetail.SequenceNumber
GO