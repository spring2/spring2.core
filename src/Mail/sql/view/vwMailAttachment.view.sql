if exists (select * from sysobjects where id = object_id(N'dbo.[vwMailAttachment]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwMailAttachment]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwMailAttachment]

AS

SELECT
    MailAttachment.MailAttachmentId,
    MailAttachment.MailMessageId,
    MailAttachment.Filename,
    MailAttachment.Text
FROM
    MailAttachment
GO
