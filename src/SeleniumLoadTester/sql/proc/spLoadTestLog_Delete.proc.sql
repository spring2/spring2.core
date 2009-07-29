if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoadTestLog_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLoadTestLog_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spLoadTestLog_Delete

@LoadTestLogId	Int

AS

if not exists(SELECT * FROM LoadTestLog WHERE (	LoadTestLogId = @LoadTestLogId
))
    BEGIN
        RAISERROR  ('spLoadTestLog_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	LoadTestLog
WHERE 
	LoadTestLogId = @LoadTestLogId
GO

