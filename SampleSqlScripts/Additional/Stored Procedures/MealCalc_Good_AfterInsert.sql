SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Good_AfterInsert]
	@ID int,
	@DefaultGoodPackingUnitID int,
	@Fat decimal(18, 3),
	@FoodValue decimal(18, 3),
	@GoodCategoryID int,
	@Name nvarchar(400),
	@Proteins decimal(18, 3),
	@Carbohydrates decimal(18, 3)
AS
SET NOCOUNT ON
DECLARE @GoodPackingUnitID INT
INSERT INTO dbo.MealCalc_GoodPackingUnit
(
	GoodID,
	Name,
	KgPerPackingUnit
)
VALUES
( 
	@ID,
	N'кг',
	1
)

SET @GoodPackingUnitID = SCOPE_IDENTITY()
UPDATE MealCalc_Good SET
	DefaultGoodPackingUnitID = @GoodPackingUnitID
WHERE ID = @ID