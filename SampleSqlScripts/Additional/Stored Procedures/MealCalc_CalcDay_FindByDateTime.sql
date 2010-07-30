SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MealCalc_CalcDay_FindByDateTime]
	@CalcDate datetime,
	@ID int OUTPUT
AS
SELECT @ID = ID FROM MealCalc_CalcDay WHERE CalcDate = @CalcDate
GO