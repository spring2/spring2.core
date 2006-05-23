if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMailAttachment_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMailAttachment_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spMailAttachment_Update

	@MailAttachmentId	Int = null,
	@MailMessageId	Int = null,
	@Filename	VarChar(50) = null,
	@Text	VarBinary(MAX) = null

AS


UPDATE
	MailAttachment
SET
	MailMessageId = @MailMessageId,
	Filename = @Filename,
	Text = @Text
WHERE
MailAttachmentId = @MailAttachmentId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spMailAttachment_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

