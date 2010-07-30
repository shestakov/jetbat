SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_GoodIncome_Load]
	@GoodIncomeDocumentID INT
AS
SELECT
	MealCalc_Supplier.Name [Supplier_Name],
	MealCalc_GoodIncomeDocument.InvoiceNumber,
	dbo.MealCalc_CalcDay.CalcDate AS SupplyDate,
	MealCalc_GoodIncomeDocumentDetail.OrderNumber AS OrderIndex,
	MealCalc_Good.Name AS Good_Name,
	ROUND(dbo.MealCalc_GoodIncomeDocumentDetail.Price, 2) AS Price,
	ROUND(dbo.MealCalc_GoodIncomeDocumentDetail.Quantity, 2) AS Quantity
FROM MultiversionDocument_Document
	INNER JOIN dbo.MealCalc_GoodIncomeDocument
		ON dbo.MealCalc_GoodIncomeDocument.ID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_GoodIncomeDocument.IncomeCalcDayID = dbo.MealCalc_CalcDay.ID
	INNER JOIN dbo.MealCalc_GoodIncomeDocumentDetail
		ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MultiversionDocument_Document.CurrentVersionID
	INNER JOIN MealCalc_Good
		ON MealCalc_Good.ID = MealCalc_GoodIncomeDocumentDetail.GoodID
	INNER JOIN MealCalc_Supplier
		ON dbo.MealCalc_Supplier.ID = dbo.MealCalc_GoodIncomeDocument.SupplierID
WHERE MultiversionDocument_Document.ID = @GoodIncomeDocumentID
ORDER BY dbo.MealCalc_GoodIncomeDocumentDetail.OrderNumber
GO