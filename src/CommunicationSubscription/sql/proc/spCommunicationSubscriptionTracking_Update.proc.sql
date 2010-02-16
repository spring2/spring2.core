if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCommunicationSubscriptionTracking_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCommunicationSubscriptionTracking_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spCommunicationSubscriptionTracking_Update

	@CommunicationPrimaryKeyId	Int = null,
	@CommunicationSubscriptionTypeId	Int = null,
	@CreateDate	DateTime = null,
	@CreateUserId	Int = null,
	@LastModifiedDate	DateTime = null,
	@LastModifiedUserId	Int = null

AS


UPDATE
	CommunicationSubscriptionTracking
SET
	CommunicationSubscriptionTypeId = @CommunicationSubscriptionTypeId,
	CreateDate = @CreateDate,
	CreateUserId = @CreateUserId,
	LastModifiedDate = @LastModifiedDate,
	LastModifiedUserId = @LastModifiedUserId
WHERE
CommunicationPrimaryKeyId = @CommunicationPrimaryKeyId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spCommunicationSubscriptionTracking_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

