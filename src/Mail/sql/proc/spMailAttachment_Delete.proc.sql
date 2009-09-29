if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMailAttachment_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMailAttachment_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMailAttachment_Delete

@MailAttachmentId	Int

AS

if not exists(SELECT * FROM MailAttachment WHERE (	MailAttachmentId = @MailAttachmentId
))
    BEGIN
        RAISERROR  ('spMailAttachment_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	MailAttachment
WHERE 
	MailAttachmentId = @MailAttachmentId
GO

