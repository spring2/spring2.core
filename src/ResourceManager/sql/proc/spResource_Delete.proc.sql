if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spResource_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spResource_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spResource_Delete

@ResourceId	Int

AS

if not exists(SELECT * FROM Resource WHERE (	ResourceId = @ResourceId
))
    BEGIN
        RAISERROR  ('spResource_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	Resource
WHERE 
	ResourceId = @ResourceId
GO

