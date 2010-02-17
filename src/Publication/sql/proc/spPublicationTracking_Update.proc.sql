if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPublicationTracking_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPublicationTracking_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spPublicationTracking_Update

	@PublicationTrackingId	Int = null,
	@PublicationPrimaryKeyId	Int = null,
	@PublicationTypeId	Int = null,
	@CreateDate	DateTime = null,
	@CreateUserId	Int = null,
	@LastModifiedDate	DateTime = null,
	@LastModifiedUserId	Int = null

AS


UPDATE
	PublicationTracking
SET
	PublicationPrimaryKeyId = @PublicationPrimaryKeyId,
	PublicationTypeId = @PublicationTypeId,
	CreateDate = @CreateDate,
	CreateUserId = @CreateUserId,
	LastModifiedDate = @LastModifiedDate,
	LastModifiedUserId = @LastModifiedUserId
WHERE
PublicationTrackingId = @PublicationTrackingId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spPublicationTracking_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

