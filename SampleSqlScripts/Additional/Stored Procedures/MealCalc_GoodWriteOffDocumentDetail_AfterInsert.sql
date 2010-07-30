SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodWriteOffDocumentDetail_AfterInsert]
	@ID int,
	@DocumentVersionID int,
	@SequenceNumber int,
	@GoodIncomeID int,
	@Quantity decimal(18, 3)
AS
UPDATE dbo.MealCalc_GoodIncome SET
	dbo.MealCalc_GoodIncome.QuantityReserved = dbo.MealCalc_GoodIncome.QuantityReserved + @Quantity
WHERE dbo.MealCalc_GoodIncome.ID = @GoodIncomeID
GO