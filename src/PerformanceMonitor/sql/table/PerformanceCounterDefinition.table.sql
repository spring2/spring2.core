SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[PerformanceCounterDefinition]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE PerformanceCounterDefinition (
	PerformanceCounterDefinitionId Int IDENTITY(1,1) NOT NULL,
	PerformanceMachineDefinitionId Int NOT NULL,
	CategoryName Varchar(50) NOT NULL,
	CounterName Varchar(50) NOT NULL,
	InstanceName Varchar(50) NOT NULL,
	CalculationType Varchar(50) NOT NULL
)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_PerformanceCounterDefinition') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE PerformanceCounterDefinition WITH NOCHECK ADD
	CONSTRAINT PK_PerformanceCounterDefinition PRIMARY KEY NONCLUSTERED
	(
		PerformanceCounterDefinitionId
	)
GO

if not exists (select * from sysindexes where name='ixPerformanceCounterDefinition_PerformanceMachineDefinitionId' and id=object_id(N'[PerformanceCounterDefinition]'))
	CREATE CLUSTERED INDEX ixPerformanceCounterDefinition_MachineName ON PerformanceCounterDefinition
	(
        	PerformanceMachineDefinitionId
	)
GO
