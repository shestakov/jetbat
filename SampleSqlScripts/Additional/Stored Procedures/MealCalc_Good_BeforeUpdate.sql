SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Good_BeforeUpdate]
	@ID int OUTPUT,
	@Carbohydrates decimal(18, 3) OUTPUT,
	@DefaultGoodPackingUnitID int OUTPUT,
	@Fat decimal(18, 3) OUTPUT,
	@FoodValue decimal(18, 3) OUTPUT,
	@GoodCategoryID int OUTPUT,
	@Name nvarchar(400) OUTPUT,
	@Proteins decimal(18, 3) OUTPUT,
	@ErrorMessages XML OUTPUT
AS
DECLARE @ErrorList TABLE
(
	Message VARCHAR(2000),
	AttributeName VARCHAR(128) NULL,
	AttributeFriendlyName VARCHAR(200) NULL,
	Severity INT
)

IF EXISTS (SELECT 0 FROM dbo.MealCalc_Good WHERE dbo.MealCalc_Good.[Name] = @Name AND dbo.MealCalc_Good.ID != @ID)
BEGIN
	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
	SELECT
		'Продукт с таким именем уже существует',
		'Name',
		'Наименование',
		2
END

SET @ErrorMessages =
(
	SELECT Message, AttributeName, AttributeFriendlyName, Severity
	FROM @ErrorList AS EL
	ORDER BY EL.Severity DESC, EL.AttributeName
	FOR XML RAW('row'), ROOT ('root')
)