SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[LoadTestLog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE LoadTestLog (
	LoadTestLogId Int IDENTITY(1,1) NOT NULL,
	DllPath Varchar(200) NOT NULL,
	TestClass Varchar(200) NOT NULL,
	TestMethod Varchar(200) NOT NULL,
	StartTime DateTime NOT NULL,
	EndTime DateTime NOT NULL,
	ElapsedInMicroseconds int NOT NULL,
	Success bit NOT NULL,
	Exception VarChar(4000) NULL
)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_LoadTestLog') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE LoadTestLog WITH NOCHECK ADD
	CONSTRAINT PK_LoadTestLog PRIMARY KEY NONCLUSTERED
	(
		LoadTestLogId
	)
GO
