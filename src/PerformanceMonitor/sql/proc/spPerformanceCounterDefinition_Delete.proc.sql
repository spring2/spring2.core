if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPerformanceCounterDefinition_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPerformanceCounterDefinition_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPerformanceCounterDefinition_Delete

@PerformanceCounterDefinitionId	Int

AS

if not exists(SELECT * FROM PerformanceCounterDefinition WHERE (	PerformanceCounterDefinitionId = @PerformanceCounterDefinitionId
))
    BEGIN
        RAISERROR  ('spPerformanceCounterDefinition_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	PerformanceCounterDefinition
WHERE 
	PerformanceCounterDefinitionId = @PerformanceCounterDefinitionId
GO

