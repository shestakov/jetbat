SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodCategory_BeforeUpdate]
	@ID int OUTPUT,
	@Name nvarchar(200) OUTPUT,
	@ErrorMessages XML OUTPUT
AS
DECLARE @ErrorList TABLE
(
	Message VARCHAR(2000),
	AttributeName VARCHAR(128) NULL,
	AttributeFriendlyName VARCHAR(200) NULL,
	Severity INT
)

IF EXISTS (SELECT 0 FROM dbo.MealCalc_GoodCategory WHERE dbo.MealCalc_GoodCategory.[Name] = @Name AND dbo.MealCalc_GoodCategory.ID != @ID)
BEGIN
	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
	SELECT
		'Категория продуктов с таким именем уже существует',
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