if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCommunicationSubscriptionTracking_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCommunicationSubscriptionTracking_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spCommunicationSubscriptionTracking_Delete

@CommunicationPrimaryKeyId	Int

AS

if not exists(SELECT * FROM CommunicationSubscriptionTracking WHERE (	CommunicationPrimaryKeyId = @CommunicationPrimaryKeyId
))
    BEGIN
        RAISERROR  ('spCommunicationSubscriptionTracking_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	CommunicationSubscriptionTracking
WHERE 
	CommunicationPrimaryKeyId = @CommunicationPrimaryKeyId
GO

