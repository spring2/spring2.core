if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPerformanceData_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPerformanceData_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPerformanceData_Delete

@PerformanceDataId	Int

AS

if not exists(SELECT * FROM PerformanceData WHERE (	PerformanceDataId = @PerformanceDataId
))
    BEGIN
        RAISERROR  ('spPerformanceData_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	PerformanceData
WHERE 
	PerformanceDataId = @PerformanceDataId
GO

