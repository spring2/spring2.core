if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPerformanceMachineDefinition_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPerformanceMachineDefinition_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPerformanceMachineDefinition_Update

	@PerformanceMachineDefinitionId	Int = null,
	@MachineName VarChar(50) = null,
	@IntervalInSeconds Int = null,
	@NumberOfSamples Int = null

AS


UPDATE
	PerformanceMachineDefinition
SET
	MachineName = @MachineName,
	IntervalInSeconds = @IntervalInSeconds,
	NumberOfSamples = @NumberOfSamples
WHERE
PerformanceMachineDefinitionId = @PerformanceMachineDefinitionId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spPerformanceMachineDefinition_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

