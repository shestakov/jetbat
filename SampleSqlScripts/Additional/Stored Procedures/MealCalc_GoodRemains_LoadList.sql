CREATE PROCEDURE MealCalc_GoodRemains_LoadList
AS
SELECT
	GoodID,
	dbo.MealCalc_Good.GoodCategoryID AS [GoodCategoryID],
	dbo.MealCalc_Good.Name AS [GoodName],
	SUM(Quantity) - SUM(QuantityWrittenOff) AS [QuantityLeft],
	SUM(QuantityReserved) AS [QuantityReserved]
FROM dbo.MealCalc_GoodIncome
	INNER JOIN MealCalc_Good ON dbo.MealCalc_GoodIncome.GoodID = dbo.MealCalc_Good.ID
WHERE Deleted = 0
GROUP BY GoodID, dbo.MealCalc_Good.GoodCategoryID, dbo.MealCalc_Good.Name