if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMailAttachment_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMailAttachment_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spMailAttachment_Insert
	@MailMessageId	Int = null,
	@Filename	VarChar(50) = null,
	@Text	VarChar(8000) = null

AS


INSERT INTO MailAttachment
(	MailMessageId,
	Filename,
	Text)
VALUES (
	@MailMessageId,
	@Filename,
	@Text)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spMailAttachment_Insert: Unable to insert new row into MailAttachment table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

