if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLocalizedResource_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLocalizedResource_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spLocalizedResource_Delete

@LocalizedResourceId	Int

AS

if not exists(SELECT * FROM LocalizedResource WHERE (	LocalizedResourceId = @LocalizedResourceId
))
    BEGIN
        RAISERROR  ('spLocalizedResource_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	LocalizedResource
WHERE 
	LocalizedResourceId = @LocalizedResourceId
GO

