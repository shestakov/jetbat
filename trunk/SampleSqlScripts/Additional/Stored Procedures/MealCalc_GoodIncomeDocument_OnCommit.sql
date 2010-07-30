SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodIncomeDocument_OnCommit]
	@DocumentID int
AS
BEGIN
	INSERT INTO dbo.MealCalc_GoodIncome
	(
		dbo.MealCalc_GoodIncome.CalcDayID,
		dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID,
		dbo.MealCalc_GoodIncome.GoodID,
		dbo.MealCalc_GoodIncome.GoodCommodityName,
		dbo.MealCalc_GoodIncome.Price,
		dbo.MealCalc_GoodIncome.Quantity,
		dbo.MealCalc_GoodIncome.Comment,
		dbo.MealCalc_GoodIncome.QuantityWrittenOff
	)
	SELECT
		dbo.MealCalc_GoodIncomeDocument.IncomeCalcDayID,
		dbo.MealCalc_GoodIncomeDocumentDetail.ID,
		dbo.MealCalc_GoodIncomeDocumentDetail.GoodID,
		dbo.MealCalc_GoodIncomeDocumentDetail.GoodCommodityName,
		dbo.MealCalc_GoodIncomeDocumentDetail.Price / dbo.MealCalc_GoodPackingUnit.KgPerPackingUnit,
		dbo.MealCalc_GoodIncomeDocumentDetail.Quantity * dbo.MealCalc_GoodPackingUnit.KgPerPackingUnit,
		dbo.MealCalc_GoodIncomeDocumentDetail.Comment,
		0
	FROM dbo.MealCalc_GoodIncomeDocumentDetail
		INNER JOIN dbo.MealCalc_GoodPackingUnit ON dbo.MealCalc_GoodIncomeDocumentDetail.GoodPackingUnitID = dbo.MealCalc_GoodPackingUnit.ID
		INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodIncomeDocument.ID
		INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MultiversionDocument_Document.CurrentVersionID
	WHERE dbo.MultiversionDocument_Document.ID = @DocumentID
END
GO