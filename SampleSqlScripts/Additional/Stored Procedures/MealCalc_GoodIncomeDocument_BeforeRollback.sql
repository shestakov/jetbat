SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_GoodIncomeDocument_BeforeRollback]
	@DocumentID int,
	@ErrorMessages xml OUTPUT
AS
begin

SET NOCOUNT ON;

declare @ErrorList table(Message varchar(2000),AttributeName varchar(128),AttributeFriendlyName varchar(200),Severity int);
declare @StartTransaction bit;
declare @ErrorMessage nvarchar(4000), @ErrorSeverity int, @ErrorState int;
declare @КодОшибки int;

SET @КодОшибки = 0;

--Проверяем, что товар не был частично или полностью списан
IF EXISTS 
(
	SELECT 0
	FROM dbo.MealCalc_GoodIncome
		INNER JOIN dbo.MealCalc_GoodIncomeDocumentDetail ON dbo.MealCalc_GoodIncomeDocumentDetail.ID = dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID
		INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodIncomeDocument.ID
		INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MultiversionDocument_Document.CurrentVersionID
	WHERE dbo.MultiversionDocument_Document.ID = @DocumentID
		AND ISNULL(dbo.MealCalc_GoodIncome.QuantityWrittenOff, 0) != 0
)
BEGIN
	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
	SELECT
		'Документ не может быть отменен, поскольку оприходованные продукты были частично списаны',
		NULL,
		NULL,
		2
END

--Проверяем, что товар не был частично или полностью зарезервирован
IF EXISTS 
(
	SELECT 0
	FROM dbo.MealCalc_GoodIncome
		INNER JOIN dbo.MealCalc_GoodIncomeDocumentDetail ON dbo.MealCalc_GoodIncomeDocumentDetail.ID = dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID
		INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodIncomeDocument.ID
		INNER JOIN dbo.MultiversionDocument_Document ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MultiversionDocument_Document.CurrentVersionID
	WHERE dbo.MultiversionDocument_Document.ID = @DocumentID
		AND ISNULL(dbo.MealCalc_GoodIncome.QuantityReserved, 0) != 0
)
BEGIN
	INSERT INTO @ErrorList (Message, AttributeName, AttributeFriendlyName, Severity)
	SELECT
		'Документ не может быть отменен, поскольку оприходованные продукты были зарезервированы',
		NULL,
		NULL,
		2
END

IF not exists(SELECT 1 FROM @ErrorList as q1 WHERE q1.Severity >= 2)
begin
	SET @StartTransaction = 0;

	begin try
		IF @@trancount = 0
		begin
			BEGIN TRAN;
			SET @StartTransaction = 1;
		end

		IF @StartTransaction = 1
		begin
			COMMIT TRAN;
		end
	end try
	begin catch
		SELECT @ErrorMessage = error_message(), @ErrorSeverity = error_severity(), @ErrorState = error_state();
		SET @КодОшибки = -1;

		IF xact_state() <> 0 and @StartTransaction = 1
		begin
			ROLLBACK TRAN;
		end

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
	end catch
end

IF @КодОшибки <> -1
begin
	SELECT TOP 1 @КодОшибки = EL.Severity
	FROM @ErrorList as EL
	ORDER BY EL.Severity desc;
end

ENDPROC:
	SET @ErrorMessages =
				(
					SELECT Message, AttributeName, AttributeFriendlyName, Severity
					FROM @ErrorList AS EL
					ORDER BY EL.Severity DESC, EL.AttributeName
					FOR XML RAW('row'), ROOT ('root')
				);
	return (@КодОшибки);
end
