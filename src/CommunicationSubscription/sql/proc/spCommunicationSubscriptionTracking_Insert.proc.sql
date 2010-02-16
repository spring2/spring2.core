if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCommunicationSubscriptionTracking_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCommunicationSubscriptionTracking_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spCommunicationSubscriptionTracking_Insert
	@CommunicationSubscriptionTypeId	Int = null,
	@CreateDate	DateTime = null,
	@CreateUserId	Int = null,
	@LastModifiedDate	DateTime = null,
	@LastModifiedUserId	Int = null

AS


INSERT INTO CommunicationSubscriptionTracking
(	CommunicationSubscriptionTypeId,
	CreateDate,
	CreateUserId,
	LastModifiedDate,
	LastModifiedUserId)
VALUES (
	@CommunicationSubscriptionTypeId,
	@CreateDate,
	@CreateUserId,
	@LastModifiedDate,
	@LastModifiedUserId)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spCommunicationSubscriptionTracking_Insert: Unable to insert new row into CommunicationSubscriptionTracking table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

