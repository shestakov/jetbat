SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_AccountingPeriodGoodReport_LoadDetail]
	@AccountingPeriodGoodReportID INT
AS
--DECLARE @AccountingPeriodGoodReportID INT
--SET @AccountingPeriodGoodReportID = 127
SELECT
	MealCalc_Good.Name [Good_Name],
	MealCalc_AccountingPeriodGoodReportGood.Price,
	MealCalc_AccountingPeriodGoodReportGood.BalanceStart,
	MealCalc_AccountingPeriodGoodReportGood.BalanceEnd
FROM MultiversionDocument_Document
	INNER JOIN MealCalc_AccountingPeriodGoodReportGood
		ON MealCalc_AccountingPeriodGoodReportGood.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN MealCalc_Good
		ON MealCalc_Good.ID = MealCalc_AccountingPeriodGoodReportGood.GoodID
WHERE MultiversionDocument_Document.ID = @AccountingPeriodGoodReportID
GO