if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMailMessage_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMailMessage_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spMailMessage_Update

	@MailMessageId	Int = null,
	@ScheduleTime	DateTime = null,
	@ProcessedTime	DateTime = null,
	@Priority	VarChar(10) = null,
	@From	VarChar(250) = null,
	@To	VarChar(6000) = null,
	@Cc	VarChar(250) = null,
	@Bcc	VarChar(250) = null,
	@Subject	VarChar(80) = null,
	@BodyFormat	VarChar(10) = null,
	@Body	Text = null,
	@MailMessageStatus	VarChar(30) = null,
	@ReleasedByUserId	Int = null,
	@MailMessageType	VarChar(80) = null,
	@NumberOfAttempts	Int = null,
	@MessageQueueDate	DateTime = null

AS

if @MailMessageType is null set @MailMessageType=''

UPDATE
	MailMessage
SET
	ScheduleTime = @ScheduleTime,
	ProcessedTime = @ProcessedTime,
	Priority = @Priority,
	[From] = @From,
	[To] = @To,
	Cc = @Cc,
	Bcc = @Bcc,
	Subject = @Subject,
	BodyFormat = @BodyFormat,
	Body = @Body,
	MailMessageStatus = @MailMessageStatus,
	ReleasedByUserId = @ReleasedByUserId,
	MailMessageType = @MailMessageType,
	NumberOfAttempts = @NumberOfAttempts,
	MessageQueueDate = @MessageQueueDate
WHERE
MailMessageId = @MailMessageId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spMailMessage_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

