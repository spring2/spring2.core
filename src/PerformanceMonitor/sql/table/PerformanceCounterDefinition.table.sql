SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[PerformanceCounterDefinition]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE PerformanceCounterDefinition (
	PerformanceCounterDefinitionId Int IDENTITY(1,1) NOT NULL,
	PerformanceMachineDefinitionId Int NOT NULL,
	CategoryName Varchar(50) NOT NULL,
	CounterName Varchar(50) NOT NULL,
	InstanceName Varchar(200) NOT NULL,
	InstanceMatchName Varchar(100) NULL,
	CalculationType Varchar(50) NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('[PerformanceCounterDefinition]') and name = 'InstanceMatchName')
  BEGIN
	ALTER TABLE [PerformanceCounterDefinition] ADD
	    InstanceMatchName VarChar(100) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('[PerformanceCounterDefinition]') and name = 'InstanceName')
	if not exists(select * from syscolumns where id=object_id('[PerformanceCounterDefinition]') and name = 'InstanceName' and length=200)
		alter table [PerformanceCounterDefinition] alter column InstanceName Varchar(200) NOT NULL
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_PerformanceCounterDefinition') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE PerformanceCounterDefinition WITH NOCHECK ADD
	CONSTRAINT PK_PerformanceCounterDefinition PRIMARY KEY NONCLUSTERED
	(
		PerformanceCounterDefinitionId
	)
GO

if exists (select * from sysindexes where name='ixPerformanceCounterDefinition_MachineName' and id=object_id(N'[PerformanceCounterDefinition]'))
	Drop Index PerformanceCounterDefinition.ixPerformanceCounterDefinition_MachineName
GO

if not exists (select * from sysindexes where name='ixPerformanceCounterDefinition_PerformanceMachineDefinitionId' and id=object_id(N'[PerformanceCounterDefinition]'))
	CREATE CLUSTERED INDEX ixPerformanceCounterDefinition_PerformanceMachineDefinitionId ON PerformanceCounterDefinition
	(
        	PerformanceMachineDefinitionId
	)
