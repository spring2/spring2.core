if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoadTestLog_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLoadTestLog_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spLoadTestLog_Update

	@LoadTestLogId	Int = null,
	@DllPath Varchar(200) = null,
	@TestClass Varchar(200) = null,
	@TestMethod Varchar(200) = null,
	@StartTime DateTime = null,
	@EndTime DateTime = null,
	@ElapsedInMicroseconds int = null,
	@Success bit = null,
	@Exception VarChar(4000) = null

AS


UPDATE
	LoadTestLog
SET
	DllPath = @DllPath,
	TestClass = @TestClass,
	TestMethod = @TestMethod,
	StartTime = @StartTime,
	EndTime = @EndTime,
	ElapsedInMicroseconds = @ElapsedInMicroseconds,
	Success = @Success,
	Exception = @Exception
WHERE
LoadTestLogId = @LoadTestLogId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spLoadTestLog_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

