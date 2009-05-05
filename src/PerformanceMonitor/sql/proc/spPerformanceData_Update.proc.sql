if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPerformanceData_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPerformanceData_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPerformanceData_Update

	@PerformanceDataId	Int = null,
	@MachineName Varchar(50) = null,
	@CategoryName Varchar(50) = null,
	@CounterName Varchar(50) = null,
	@InstanceName Varchar(50) = null,
	@CalculationType Varchar(50) = null,
	@IntervalInSeconds int = null,
	@NumberOfSamples int = null,
	@TimeOfSample datetime = null,
	@SampleValue float = null

AS


UPDATE
	PerformanceData
SET
	MachineName = @MachineName,
	CategoryName = @CategoryName,
	CounterName = @CounterName,
	InstanceName = @InstanceName,
	CalculationType = @CalculationType,
	IntervalInSeconds = @IntervalInSeconds,
	NumberOfSamples = @NumberOfSamples,
	TimeOfSample = @TimeOfSample,
	SampleValue = @SampleValue
WHERE
PerformanceDataId = @PerformanceDataId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spPerformanceData_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

