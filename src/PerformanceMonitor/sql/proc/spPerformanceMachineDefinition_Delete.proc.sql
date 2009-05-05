if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPerformanceMachineDefinition_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPerformanceMachineDefinition_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPerformanceMachineDefinition_Delete

@PerformanceMachineDefinitionId	Int

AS

if not exists(SELECT * FROM PerformanceMachineDefinition WHERE (	PerformanceMachineDefinitionId = @PerformanceMachineDefinitionId
))
    BEGIN
        RAISERROR  ('spPerformanceMachineDefinition_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	PerformanceMachineDefinition
WHERE 
	PerformanceMachineDefinitionId = @PerformanceMachineDefinitionId
GO

