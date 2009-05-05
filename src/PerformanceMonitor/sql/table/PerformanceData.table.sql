SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[PerformanceData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE PerformanceData (
	PerformanceDataId Int IDENTITY(1,1) NOT NULL,
	MachineName Varchar(50) NOT NULL,
	CategoryName Varchar(50) NOT NULL,
	CounterName Varchar(50) NOT NULL,
	InstanceName Varchar(50) NOT NULL,
	CalculationType Varchar(50) NOT NULL,
	IntervalInSeconds int NOT NULL,
	NumberOfSamples int NOT NULL,
	TimeOfSample DateTime NOT NULL,
	SampleValue float NOT NULL
)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_PerformanceData') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE PerformanceData WITH NOCHECK ADD
	CONSTRAINT PK_PerformanceData PRIMARY KEY NONCLUSTERED
	(
		PerformanceDataId
	)
GO

if not exists (select * from sysindexes where name='ixPerformanceData_MachineName' and id=object_id(N'[PerformanceData]'))
	CREATE CLUSTERED INDEX ixPerformanceData_MachineName ON PerformanceData
	(
        	MachineName,
		CategoryName,
		CounterName,
		InstanceName,
		CalculationType
	)
GO
