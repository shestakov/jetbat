SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodWriteOffDocumentDetail_BeforeInsert]
	@ID int OUTPUT,
	@DocumentVersionID int OUTPUT,
	@SequenceNumber int OUTPUT,
	@GoodIncomeID int OUTPUT,
	@Quantity decimal(18, 3) OUTPUT,
	@ErrorMessages XML OUTPUT
AS

DECLARE @ErrorList TABLE
(
	Message VARCHAR(2000),
	AttributeName VARCHAR(128) NULL,
	AttributeFriendlyName VARCHAR(200) NULL,
	Severity INT
)

--IF @Quantity > (SELECT dbo.MealCalc_GoodIncome.Quantity - dbo.MealCalc_GoodIncome.QuantityWrittenOff - dbo.MealCalc_GoodIncome.QuantityReserved FROM dbo.MealCalc_GoodIncome WHERE ID = @GoodIncomeID)
--BEGIN
--	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
--	SELECT
--		'Недостаточно свободного количества товара для списания',
--		NULL,
--		NULL,
--		2
--END

SET @ErrorMessages =
(
	SELECT Message, AttributeName, AttributeFriendlyName, Severity
	FROM @ErrorList AS EL
	ORDER BY EL.Severity DESC, EL.AttributeName
	FOR XML RAW('row'), ROOT ('root')
)
GO