if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPublicationTracking_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPublicationTracking_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spPublicationTracking_Insert
	@PublicationPrimaryKeyId	Int = null,
	@PublicationTypeId	Int = null,
	@CreateDate	DateTime = null,
	@CreateUserId	Int = null,
	@LastModifiedDate	DateTime = null,
	@LastModifiedUserId	Int = null

AS


INSERT INTO PublicationTracking
(	PublicationPrimaryKeyId,
	PublicationTypeId,
	CreateDate,
	CreateUserId,
	LastModifiedDate,
	LastModifiedUserId)
VALUES (
	@PublicationPrimaryKeyId,
	@PublicationTypeId,
	@CreateDate,
	@CreateUserId,
	@LastModifiedDate,
	@LastModifiedUserId)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spPublicationTracking_Insert: Unable to insert new row into PublicationTracking table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

