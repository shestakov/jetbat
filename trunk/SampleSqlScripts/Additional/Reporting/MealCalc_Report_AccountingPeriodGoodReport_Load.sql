SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_AccountingPeriodGoodReport_Load]
	@AccountingPeriodGoodReportID INT
AS
--DECLARE @AccountingPeriodGoodReportID INT
--SET @AccountingPeriodGoodReportID = 127
SELECT
	MealCalc_AccountingPeriodGoodReport.ReportNumber,
	MealCalc_AccountingPeriodGoodReport.ReportDateTime,
	MealCalc_AccountingPeriod.StartDateTime,
	MealCalc_AccountingPeriod.EndDateTime,
	MealCalc_AccountingPeriodGoodReport.ReportAuthorName,
	MealCalc_AccountingPeriodGoodReport.ReportAuthorTableNumber
FROM MultiversionDocument_Document
	INNER JOIN MealCalc_AccountingPeriodGoodReport
		ON MealCalc_AccountingPeriodGoodReport.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN MealCalc_AccountingPeriod
		ON MealCalc_AccountingPeriod.ID = MealCalc_AccountingPeriodGoodReport.AccountingPeriodID
WHERE MultiversionDocument_Document.ID = @AccountingPeriodGoodReportID
GO