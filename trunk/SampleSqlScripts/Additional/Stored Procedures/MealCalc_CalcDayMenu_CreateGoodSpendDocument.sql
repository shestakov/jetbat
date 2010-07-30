SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDayMenu_CreateGoodSpendDocument]
	@ID INT
AS
SET XACT_ABORT ON

DECLARE @CalcDayMenuStatusID INT

SELECT @CalcDayMenuStatusID = CalcDayMenuStatusID FROM dbo.MealCalc_CalcDayMenu WHERE ID = @ID

IF (@CalcDayMenuStatusID IS NULL)
BEGIN
	RAISERROR('“ребуемое меню учетного дн€ не найдено', 11, 1)
	RETURN
END
ELSE IF (@CalcDayMenuStatusID != 2)
BEGIN
	RAISERROR('—татус меню учетного дн€ не позвол€ет создать расходный документ', 11, 2)
	RETURN
END

IF EXISTS
(
	SELECT 0 FROM dbo.MealCalc_GoodSpendDocument
		INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodSpendDocument.ID = dbo.MultiversionDocument_Document.CurrentVersionID
	WHERE dbo.MealCalc_GoodSpendDocument.CalcDayMenuID = @ID
		AND NOT dbo.MultiversionDocument_Document.DocumentStateID IN (100)
)
BEGIN
	RAISERROR('ƒл€ этого меню учетного дн€ уже был создан расходный документ. ѕожалуйста, проведите или удалите его', 11, 3)
	RETURN
END

BEGIN TRAN

DECLARE @DocumentID INT
DECLARE @VersionID INT

EXEC dbo.MealCalc_GoodSpendDocument_Create
	@DocumentID = @DocumentID OUTPUT,
	@VersionID = @VersionID OUTPUT

DECLARE @DocumentDateTime DATETIME
SET @DocumentDateTime = CAST(FLOOR(CAST(GETDATE() AS FLOAT)) AS DATETIME)

EXEC dbo.MealCalc_GoodSpendDocument_UpdateVersion
	@VersionID = @VersionID,
	@CalcDayMenuID = @ID,
	@DocumentDateTime = @DocumentDateTime,
	@DocumentNumber = NULL,
	@Comment = NULL,
	@ErrorMessages = NULL


INSERT INTO dbo.MealCalc_GoodSpendDocumentDetail
(
	dbo.MealCalc_GoodSpendDocumentDetail.DocumentVersionID,
	dbo.MealCalc_GoodSpendDocumentDetail.GoodID,
	dbo.MealCalc_GoodSpendDocumentDetail.GoodCommodityName,
	dbo.MealCalc_GoodSpendDocumentDetail.Price,
	dbo.MealCalc_GoodSpendDocumentDetail.Quantity,
	dbo.MealCalc_GoodSpendDocumentDetail.SequenceNumber
)
SELECT
	@VersionID,
	dbo.MealCalc_CalcDayMenuGoodSpend.GoodID,
	dbo.MealCalc_GoodIncome.GoodCommodityName,
	dbo.MealCalc_GoodIncome.Price,
	SUM(dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity),
	ROW_NUMBER() OVER (ORDER BY dbo.MealCalc_Good.Name, dbo.MealCalc_GoodIncome.GoodCommodityName, dbo.MealCalc_GoodIncome.Price)
FROM dbo.MealCalc_CalcDayMenu
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenu.ID = dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpendWriteOff ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
	INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
	INNER JOIN dbo.MealCalc_Good ON dbo.MealCalc_GoodIncome.GoodID = dbo.MealCalc_Good.ID
GROUP BY dbo.MealCalc_CalcDayMenuGoodSpend.GoodID, dbo.MealCalc_Good.Name, dbo.MealCalc_GoodIncome.GoodCommodityName, dbo.MealCalc_GoodIncome.Price
ORDER BY dbo.MealCalc_Good.Name, dbo.MealCalc_GoodIncome.GoodCommodityName, dbo.MealCalc_GoodIncome.Price

EXEC dbo.MealCalc_GoodSpendDocument_ConfirmEdit
	@VersionID = @VersionID,
	@ErrorMessages = NULL

--UPDATE MealCalc_CalcDayMenu SET dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = 3 WHERE ID = @ID

COMMIT TRAN
GO