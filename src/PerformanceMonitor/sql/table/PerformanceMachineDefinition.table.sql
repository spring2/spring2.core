SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[PerformanceMachineDefinition]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE PerformanceMachineDefinition (
	PerformanceMachineDefinitionId Int IDENTITY(1,1) NOT NULL,
	MachineName Varchar(50) NOT NULL,
	IntervalInSeconds Int NOT NULL,
	NumberOfSamples Int NULL
)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_PerformanceMachineDefinition') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE PerformanceMachineDefinition WITH NOCHECK ADD
	CONSTRAINT PK_PerformanceMachineDefinition PRIMARY KEY NONCLUSTERED
	(
		PerformanceMachineDefinitionId
	)
GO

if not exists (select * from sysindexes where name='ixPerformanceMachineDefinition_MachineName' and id=object_id(N'[PerformanceMachineDefinition]'))
	CREATE UNIQUE CLUSTERED INDEX ixPerformanceMachineDefinition_MachineName ON PerformanceMachineDefinition
	(
        	MachineName
	)
GO
