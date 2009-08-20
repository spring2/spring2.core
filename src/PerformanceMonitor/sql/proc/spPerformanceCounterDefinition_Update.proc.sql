if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPerformanceCounterDefinition_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPerformanceCounterDefinition_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPerformanceCounterDefinition_Update

	@PerformanceCounterDefinitionId	Int = null,
	@PerformanceMachineDefinitionId Int = null,
	@CategoryName Varchar(50) = null,
	@CounterName Varchar(50) = null,
	@InstanceName Varchar(50) = null,
	@InstanceMatchName Varchar(100) = null,
	@CalculationType Varchar(50) = null

AS


UPDATE
	PerformanceCounterDefinition
SET
	PerformanceMachineDefinitionId = @PerformanceMachineDefinitionId,
	CategoryName = @CategoryName,
	CounterName = @CounterName,
	InstanceName = @InstanceName,
	InstanceMatchName = @InstanceMatchName,
	CalculationType = @CalculationType
WHERE
PerformanceCounterDefinitionId = @PerformanceCounterDefinitionId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spPerformanceCounterDefinition_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

