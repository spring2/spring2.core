if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCommunicationSubscriptionType_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCommunicationSubscriptionType_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spCommunicationSubscriptionType_Delete

@CommunicationSubscriptionTypeId	Int

AS

if not exists(SELECT * FROM CommunicationSubscriptionType WHERE (	CommunicationSubscriptionTypeId = @CommunicationSubscriptionTypeId
))
    BEGIN
        RAISERROR  ('spCommunicationSubscriptionType_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	CommunicationSubscriptionType
WHERE 
	CommunicationSubscriptionTypeId = @CommunicationSubscriptionTypeId
GO

