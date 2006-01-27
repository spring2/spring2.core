if exists (select * from sysobjects where id = object_id(N'[vwMailMessage]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwMailMessage]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [vwMailMessage]

AS

SELECT
    MailMessage.MailMessageId,
    MailMessage.ScheduleTime,
    MailMessage.ProcessedTime,
    MailMessage.Priority,
    MailMessage.[From],
    MailMessage.[To],
    MailMessage.Cc,
    MailMessage.Bcc,
    MailMessage.Subject,
    MailMessage.BodyFormat,
    MailMessage.Body,
    MailMessage.MailMessageStatus,
    MailMessage.ReleasedByUserId,
    MailMessage.MailMessageType,
    MailMessage.NumberOfAttempts,
    MailMessage.MessageQueueDate
FROM
    MailMessage
GO
