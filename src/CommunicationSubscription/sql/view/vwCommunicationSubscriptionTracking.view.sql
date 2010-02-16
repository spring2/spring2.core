if exists (select * from sysobjects where id = object_id(N'dbo.[vwCommunicationSubscriptionTracking]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwCommunicationSubscriptionTracking]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwCommunicationSubscriptionTracking]

AS

SELECT
    CommunicationSubscriptionTracking.CommunicationPrimaryKeyId,
    CommunicationSubscriptionTracking.CommunicationSubscriptionTypeId,
    CommunicationSubscriptionTracking.CreateDate,
    CommunicationSubscriptionTracking.CreateUserId,
    CommunicationSubscriptionTracking.LastModifiedDate,
    CommunicationSubscriptionTracking.LastModifiedUserId
FROM
    CommunicationSubscriptionTracking
GO
