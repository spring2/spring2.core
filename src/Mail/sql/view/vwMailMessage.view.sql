if exists (select * from sysobjects where id = object_id(N'dbo.[vwMailMessage]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwMailMessage]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwMailMessage]

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
    MailMessage.MessageQueueDate,
    MailMessage.ReferenceKey,
    MailMessage.UniqueKey,
    MailMessage.Checksum,
    MailMessage.OpenCount,
    MailMessage.Bounces,
    MailMessage.LastOpenDate,
    MailMessage.SmtpServer
FROM
    MailMessage
GO
