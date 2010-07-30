SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_BalanceIncome_Load]
	@AccountingPeriodID INT
AS
--DECLARE @AccountingPeriodID INT
--SET @AccountingPeriodID = 1
SELECT
	MealCalc_Good.Name [Good_Name],
	ISNULL(PreviousPeriodGoodBalance.Balance, 0) AS [BalanceStart],
	MealCalc_GoodIncome.InvoiceID,
	MealCalc_GoodIncome.SupplyDateTime,
	SUM(MealCalc_GoodIncomeGood.Price * MealCalc_GoodIncomeGood.Quantity)
FROM MealCalc_AccountingPeriodGoodBalance [CurrentPeriodGoodBalance]
	INNER JOIN MealCalc_GoodIncomeGood
		ON MealCalc_GoodIncomeGood.GoodID = CurrentPeriodGoodBalance.GoodID
	INNER JOIN MealCalc_AccountingPeriod [CurrentPeriod]
		ON CurrentPeriod.ID = @AccountingPeriodID
	INNER JOIN MultiversionDocument_Document
		ON
			MultiversionDocument_Document.CurrentVersionID = MealCalc_GoodIncomeGood.DocumentVersionID AND
			MultiversionDocument_Document.DocumentStateID = 2
	INNER JOIN MealCalc_GoodIncome
		ON
			MealCalc_GoodIncome.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID AND
			MealCalc_GoodIncome.SupplyDateTime BETWEEN CurrentPeriod.StartDateTime AND CurrentPeriod.EndDateTime
	INNER JOIN MealCalc_Good
		ON MealCalc_Good.ID = CurrentPeriodGoodBalance.GoodID
	LEFT JOIN MealCalc_AccountingPeriod [PreviousPeriod]
		ON PreviousPeriod.ID = (SELECT TOP(1) ID FROM MealCalc_AccountingPeriod WHERE MealCalc_AccountingPeriod.StartDateTime < CurrentPeriod.StartDateTime ORDER BY StartDateTime DESC)
	LEFT JOIN MealCalc_AccountingPeriodGoodBalance [PreviousPeriodGoodBalance]
		ON
			PreviousPeriodGoodBalance.AccountingPeriodID = PreviousPeriod.ID AND
			PreviousPeriodGoodBalance.GoodID = CurrentPeriodGoodBalance.GoodID AND
			PreviousPeriodGoodBalance.Price = CurrentPeriodGoodBalance.Price
WHERE CurrentPeriodGoodBalance.AccountingPeriodID = @AccountingPeriodID
GROUP BY MealCalc_Good.NAME, MealCalc_GoodIncome.InvoiceID, ISNULL(PreviousPeriodGoodBalance.Balance, 0), MealCalc_GoodIncome.SupplyDateTime
GO