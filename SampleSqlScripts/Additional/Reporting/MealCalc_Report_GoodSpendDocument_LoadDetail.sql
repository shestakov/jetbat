SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_GoodSpendDocument_LoadDetail]
	@DocumentID INT
AS
SELECT
	dbo.MealCalc_GoodSpendDocumentDetail.SequenceNumber AS SequenceNumber,
	dbo.MealCalc_Good.Name AS GoodName,
	dbo.MealCalc_GoodSpendDocumentDetail.GoodCommodityName AS GoodCommodityName,
	ROUND(dbo.MealCalc_GoodSpendDocumentDetail.Price, 2) AS Price,
	ROUND(dbo.MealCalc_GoodSpendDocumentDetail.Quantity, 2) AS Quantity
FROM MultiversionDocument_Document
	INNER JOIN dbo.MealCalc_GoodSpendDocumentDetail ON dbo.MealCalc_GoodSpendDocumentDetail.DocumentVersionID = dbo.MultiversionDocument_Document.CurrentVersionID
	INNER JOIN MealCalc_Good ON MealCalc_Good.ID = dbo.MealCalc_GoodSpendDocumentDetail.GoodID
WHERE MultiversionDocument_Document.ID = @DocumentID
ORDER BY dbo.MealCalc_GoodSpendDocumentDetail.SequenceNumber
GO