SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_GoodIncomeDocument_Load]
	@GoodIncomeDocumentID INT
AS
SELECT
	MealCalc_Supplier.Name AS Supplier_Name,
	MealCalc_GoodIncomeDocument.InvoiceNumber,
	dbo.MealCalc_CalcDay.CalcDate AS SupplyDate
FROM MultiversionDocument_Document
	INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocument.ID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_GoodIncomeDocument.IncomeCalcDayID = dbo.MealCalc_CalcDay.ID
	INNER JOIN MealCalc_Supplier ON dbo.MealCalc_Supplier.ID = dbo.MealCalc_GoodIncomeDocument.SupplierID
WHERE MultiversionDocument_Document.ID = @GoodIncomeDocumentID
GO