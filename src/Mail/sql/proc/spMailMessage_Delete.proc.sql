if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMailMessage_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMailMessage_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMailMessage_Delete

@MailMessageId	Int

AS

if not exists(SELECT * FROM MailMessage WHERE (	MailMessageId = @MailMessageId
))
    BEGIN
        RAISERROR  ('spMailMessage_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	MailMessage
WHERE 
	MailMessageId = @MailMessageId
GO

