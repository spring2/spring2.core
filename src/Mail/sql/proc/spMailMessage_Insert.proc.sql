if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMailMessage_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMailMessage_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMailMessage_Insert
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
	@MessageQueueDate	DateTime = null,
	@ReferenceKey	VarChar(50) = null,
	@UniqueKey	VarChar(50) = null,
	@Checksum	VarChar(50) = null,
	@OpenCount	Int = null,
	@Bounces	Int = null,
	@LastOpenDate	DateTime = null,
	@SmtpServer	VarChar(50) = null

AS

if @MailMessageType is null set @MailMessageType=''

INSERT INTO MailMessage
(	ScheduleTime,
	ProcessedTime,
	Priority,
	[From],
	[To],
	Cc,
	Bcc,
	Subject,
	BodyFormat,
	Body,
	MailMessageStatus,
	ReleasedByUserId,
	MailMessageType,
	NumberOfAttempts,
	MessageQueueDate,
	ReferenceKey,
	UniqueKey,
	Checksum,
	OpenCount,
	Bounces,
	LastOpenDate,
	SmtpServer)
VALUES (
	@ScheduleTime,
	@ProcessedTime,
	@Priority,
	@From,
	@To,
	@Cc,
	@Bcc,
	@Subject,
	@BodyFormat,
	@Body,
	@MailMessageStatus,
	@ReleasedByUserId,
	@MailMessageType,
	@NumberOfAttempts,
	@MessageQueueDate,
	@ReferenceKey,
	@UniqueKey,
	@Checksum,
	@OpenCount,
	@Bounces,
	@LastOpenDate,
	@SmtpServer)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR ('spMailMessage_Insert: Unable to insert new row into MailMessage table ', 16, 1, @@ROWCOUNT)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

