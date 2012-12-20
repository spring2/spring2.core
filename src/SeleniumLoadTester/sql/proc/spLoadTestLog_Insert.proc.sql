if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoadTestLog_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLoadTestLog_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spLoadTestLog_Insert
	@DllPath Varchar(200) = null,
	@TestClass Varchar(200) = null,
	@TestMethod Varchar(200) = null,
	@StartTime DateTime = null,
	@EndTime DateTime = null,
	@ElapsedInMicroseconds int = null,
	@Success bit = null,
	@Exception VarChar(4000) = null

AS


INSERT INTO LoadTestLog
(	DllPath,
	TestClass,
	TestMethod,
	StartTime,
	EndTime,
	ElapsedInMicroseconds,
	Success,
	Exception)
VALUES (
	@DllPath,
	@TestClass,
	@TestMethod,
	@StartTime,
	@EndTime,
	@ElapsedInMicroseconds,
	@Success,
	@Exception)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  ('spLoadTestLog_Insert: Unable to insert new row into LoadTestLog table ', 16, 1, @@rowcount)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

