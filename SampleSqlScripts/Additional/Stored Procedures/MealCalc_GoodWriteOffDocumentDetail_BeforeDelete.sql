SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodWriteOffDocumentDetail_BeforeDelete]
	@ID int OUTPUT,
	@ErrorMessages XML OUTPUT
AS
UPDATE dbo.MealCalc_GoodIncome SET
	dbo.MealCalc_GoodIncome.QuantityReserved = dbo.MealCalc_GoodIncome.QuantityReserved - dbo.MealCalc_GoodWriteOffDocumentDetail.Quantity
FROM dbo.MealCalc_GoodIncome
	INNER JOIN dbo.MealCalc_GoodWriteOffDocumentDetail ON dbo.MealCalc_GoodIncome.ID = dbo.MealCalc_GoodWriteOffDocumentDetail.GoodIncomeID
WHERE dbo.MealCalc_GoodWriteOffDocumentDetail.ID = @ID
GO