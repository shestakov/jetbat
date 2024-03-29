SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Good_BeforeDelete]
	@ID int OUTPUT,
	@ErrorMessages XML OUTPUT
AS
UPDATE dbo.MealCalc_Good SET DefaultGoodPackingUnitID = NULL WHERE ID = @ID

DELETE FROM dbo.MealCalc_GoodPackingUnit WHERE GoodID = @ID