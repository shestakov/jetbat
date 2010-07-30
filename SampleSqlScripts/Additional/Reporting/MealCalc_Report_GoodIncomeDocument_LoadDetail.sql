SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_GoodIncomeDocument_LoadDetail]
	@GoodIncomeDocumentID INT
AS
SELECT
	MealCalc_GoodIncomeDocumentDetail.OrderNumber AS OrderIndex,
	MealCalc_Good.Name AS GoodName,
	MealCalc_GoodIncomeDocumentDetail.GoodCommodityName AS GoodCommodityName,
	ROUND(dbo.MealCalc_GoodIncomeDocumentDetail.Price, 2) AS Price,
	ROUND(dbo.MealCalc_GoodIncomeDocumentDetail.Quantity, 2) AS Quantity
FROM MultiversionDocument_Document
	INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocument.ID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN dbo.MealCalc_GoodIncomeDocumentDetail ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MultiversionDocument_Document.CurrentVersionID
	INNER JOIN MealCalc_Good
		ON MealCalc_Good.ID = MealCalc_GoodIncomeDocumentDetail.GoodID
WHERE MultiversionDocument_Document.ID = @GoodIncomeDocumentID
ORDER BY dbo.MealCalc_GoodIncomeDocumentDetail.OrderNumber
GO