SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMeal_BeforeInsert]
	@ID int OUTPUT,
	@CalcDayMenuID int OUTPUT,
	@MealTimeID int OUTPUT,
	@Comment nvarchar(2000) OUTPUT,
	@ErrorMessages XML OUTPUT
AS
BEGIN
	DECLARE @ErrorList TABLE
	(
		Message VARCHAR(2000),
		AttributeName VARCHAR(128) NULL,
		AttributeFriendlyName VARCHAR(200) NULL,
		Severity INT
	)

	IF EXISTS
	(
		SELECT 0 FROM MealCalc_CalcDayMeal
		WHERE CalcDayMenuID = @CalcDayMenuID
			AND MealTimeID = @MealTimeID 
	)
	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
	SELECT
		'Указанный прием пищи уже добавлен в меню',
		'MealTimeID',
		'Прием пищи',
		2

SET @ErrorMessages =
(
	SELECT Message, AttributeName, AttributeFriendlyName, Severity
	FROM @ErrorList AS EL
	ORDER BY EL.Severity DESC, EL.AttributeName
	FOR XML RAW('row'), ROOT ('root')
)

	RETURN 0
END
GO