if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPerformanceData_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPerformanceData_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPerformanceData_Insert
	@MachineName Varchar(50) = null,
	@CategoryName Varchar(50) = null,
	@CounterName Varchar(50) = null,
	@InstanceName Varchar(50) = null,
	@CalculationType Varchar(50) = null,
	@IntervalInSeconds int = null,
	@NumberOfSamples int = null,
	@TimeOfSample DateTime = null,
	@SampleValue float = null

AS


INSERT INTO PerformanceData
(	MachineName,
	CategoryName,
	CounterName,
	InstanceName,
	CalculationType,
	IntervalInSeconds,
	NumberOfSamples,
	TimeOfSample,
	SampleValue)
VALUES (
	@MachineName,
	@CategoryName,
	@CounterName,
	@InstanceName,
	@CalculationType,
	@IntervalInSeconds,
	@NumberOfSamples,
	@TimeOfSample,
	@SampleValue)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  ('spPerformanceData_Insert: Unable to insert new row into PerformanceData table ', 16, 1, @@rowcount)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

