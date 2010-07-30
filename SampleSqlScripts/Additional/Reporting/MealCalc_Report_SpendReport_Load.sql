SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_SpendReport_Load]
	@GoodSpendByCalcDayID INT
AS
--DECLARE @GoodSpendByCalcDayID INT
--SET @GoodSpendByCalcDayID = 67
SELECT
	MealCalc_CalcDay.CalcDate [CalcDaty_CalcDate],
	ROW_NUMBER() OVER (ORDER BY MealCalc_Good.Name) [OrderIndex],
	MealCalc_Good.Name [Good_Name],
	MealCalc_MeasureUnit.Name [MeasureUnit_Name],
	ROUND(MealCalc_GoodSpendByCalcDayGood.Price, 2) [GoodSpendByCalcDayGood_Price],
	ROUND(SUM(MealCalc_GoodSpendByCalcDayGood.Quantity), 2) [GoodSpendByCalcDayGood_Quantity],
	ROUND(SUM(MealCalc_GoodSpendByCalcDayGood.Quantity) * MealCalc_GoodSpendByCalcDayGood.Price, 2) [GoodSpendByCalcDayGood_Total]
FROM MultiversionDocument_Document
	INNER JOIN MealCalc_GoodSpendByCalcDay
		ON MealCalc_GoodSpendByCalcDay.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN MealCalc_GoodSpendByCalcDayGood
		ON MealCalc_GoodSpendByCalcDayGood.DocumentVersionID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN MealCalc_Good
		ON MealCalc_Good.ID = MealCalc_GoodSpendByCalcDayGood.GoodID
	INNER JOIN MealCalc_CalcDay
		ON MealCalc_CalcDay.ID = MealCalc_GoodSpendByCalcDay.CalcDayID
	INNER JOIN MealCalc_MeasureUnit
		ON MealCalc_MeasureUnit.ID = MealCalc_Good.MeasureUnitID
WHERE MultiversionDocument_Document.ID = @GoodSpendByCalcDayID
GROUP BY MealCalc_CalcDay.CalcDate, MealCalc_Good.Name, MealCalc_MeasureUnit.Name, MealCalc_GoodSpendByCalcDayGood.Price
ORDER BY MealCalc_Good.NAME
GO