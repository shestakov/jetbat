SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_Report_GoodWriteOffDocument_Load]
	@DocumentID INT
AS
SELECT
	dbo.MealCalc_CalcDay.CalcDate AS WriteOffDate,
	dbo.MealCalc_GoodWriteOffDocument.DocumentDateTime AS DocumentDate,
	dbo.MealCalc_GoodWriteOffDocument.DocumentNumber AS DocumentNumber,
	dbo.MealCalc_WriteOffReason.Name AS WriteOffReasonName	
FROM MultiversionDocument_Document
	INNER JOIN dbo.MealCalc_GoodWriteOffDocument ON dbo.MealCalc_GoodWriteOffDocument.ID = MultiversionDocument_Document.CurrentVersionID
	INNER JOIN dbo.MealCalc_WriteOffReason ON dbo.MealCalc_GoodWriteOffDocument.WriteOffReasonID = dbo.MealCalc_WriteOffReason.ID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_GoodWriteOffDocument.CalcDayID = dbo.MealCalc_CalcDay.ID
WHERE MultiversionDocument_Document.ID = @DocumentID
GO