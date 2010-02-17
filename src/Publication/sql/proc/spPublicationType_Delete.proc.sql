if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPublicationType_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPublicationType_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spPublicationType_Delete

@PublicationTypeId	Int

AS

if not exists(SELECT * FROM PublicationType WHERE (	PublicationTypeId = @PublicationTypeId
))
    BEGIN
        RAISERROR  ('spPublicationType_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	PublicationType
WHERE 
	PublicationTypeId = @PublicationTypeId
GO

