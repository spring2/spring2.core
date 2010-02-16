if exists (select * from sysobjects where id = object_id(N'dbo.[vwCommunicationSubscriptionType]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwCommunicationSubscriptionType]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwCommunicationSubscriptionType]

AS

SELECT
    CommunicationSubscriptionType.CommunicationSubscriptionTypeId,
    CommunicationSubscriptionType.Name,
    CommunicationSubscriptionType.EmailSubject,
    CommunicationSubscriptionType.EmailBody,
    CommunicationSubscriptionType.EmailBodyType,
    CommunicationSubscriptionType.MailMessageType,
    CommunicationSubscriptionType.LastSentDate,
    CommunicationSubscriptionType.FrequencyInMinutes,
    CommunicationSubscriptionType.ProviderName,
    CommunicationSubscriptionType.AllowSubscription,
    CommunicationSubscriptionType.AutoSubscribe,
    CommunicationSubscriptionType.EffectiveDate,
    CommunicationSubscriptionType.ExpirationDate,
    CommunicationSubscriptionType.CreateDate,
    CommunicationSubscriptionType.CreateUserId,
    CommunicationSubscriptionType.LastModifiedDate,
    CommunicationSubscriptionType.LastModifiedUserId
FROM
    CommunicationSubscriptionType
GO
