if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPublicationTracking_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPublicationTracking_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spPublicationTracking_Delete

@PublicationPrimaryKeyId	Int

AS

if not exists(SELECT * FROM PublicationTracking WHERE (	PublicationPrimaryKeyId = @PublicationPrimaryKeyId
))
    BEGIN
        RAISERROR  ('spPublicationTracking_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	PublicationTracking
WHERE 
	PublicationPrimaryKeyId = @PublicationPrimaryKeyId
GO

