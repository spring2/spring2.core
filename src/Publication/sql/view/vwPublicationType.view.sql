if exists (select * from sysobjects where id = object_id(N'dbo.[vwPublicationType]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwPublicationType]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwPublicationType]

AS

SELECT
    PublicationType.PublicationTypeId,
    PublicationType.Name,
    PublicationType.EmailSubject,
    PublicationType.EmailBody,
    PublicationType.EmailBodyType,
    PublicationType.Description,
    PublicationType.MailMessageType,
    PublicationType.LastSentDate,
    PublicationType.FrequencyInMinutes,
    PublicationType.ProviderName,
    PublicationType.AllowSubscription,
    PublicationType.AutoSubscribe,
    PublicationType.EffectiveDate,
    PublicationType.ExpirationDate,
    PublicationType.CreateDate,
    PublicationType.CreateUserId,
    PublicationType.LastModifiedDate,
    PublicationType.LastModifiedUserId
FROM
    PublicationType
GO
