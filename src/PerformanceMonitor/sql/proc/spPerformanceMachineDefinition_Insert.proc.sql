if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPerformanceMachineDefinition_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPerformanceMachineDefinition_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPerformanceMachineDefinition_Insert
	@MachineName VarChar(50) = null,
	@IntervalInSeconds Int = null,
	@NumberOfSamples Int = null

AS


INSERT INTO PerformanceMachineDefinition
(	MachineName,
	IntervalInSeconds,
	NumberOfSamples)
VALUES (
	@MachineName,
	@IntervalInSeconds,
	@NumberOfSamples)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spPerformanceMachineDefinition_Insert: Unable to insert new row into PerformanceMachineDefinition table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

