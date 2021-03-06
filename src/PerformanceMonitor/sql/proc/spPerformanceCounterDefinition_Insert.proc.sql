if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPerformanceCounterDefinition_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPerformanceCounterDefinition_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPerformanceCounterDefinition_Insert
	@PerformanceMachineDefinitionId Int = null,
	@CategoryName Varchar(50) = null,
	@CounterName Varchar(50) = null,
	@InstanceName Varchar(50) = null,
	@InstanceMatchName Varchar(100) = null,
	@CalculationType Varchar(50) = null

AS


INSERT INTO PerformanceCounterDefinition
(	PerformanceMachineDefinitionId,
	CategoryName,
	CounterName,
	InstanceName,
	InstanceMatchName,
	CalculationType)
VALUES (
	@PerformanceMachineDefinitionId,
	@CategoryName,
	@CounterName,
	@InstanceName,
	@InstanceMatchName,
	@CalculationType)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  ('spPerformanceCounterDefinition_Insert: Unable to insert new row into PerformanceCounterDefinition table ', 16, 1, @@ROWCOUNT)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

