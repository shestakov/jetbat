SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodPackingUnit_BeforeInsert]
	@ID int OUTPUT,
	@GoodID int OUTPUT,
	@KgPerPackingUnit decimal(18, 3) OUTPUT,
	@Name nvarchar(100) OUTPUT,
	@ErrorMessages XML OUTPUT
AS
DECLARE @ErrorList TABLE
(
	Message VARCHAR(2000),
	AttributeName VARCHAR(128) NULL,
	AttributeFriendlyName VARCHAR(200) NULL,
	Severity INT
)

IF EXISTS
(
	SELECT 0 FROM dbo.MealCalc_GoodPackingUnit
	WHERE dbo.MealCalc_GoodPackingUnit.[Name] = @Name
		AND dbo.MealCalc_GoodPackingUnit.GoodID = @GoodID
)
BEGIN
	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
	SELECT
		'Упаковочная единица такого продукта с таким именем уже существует',
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