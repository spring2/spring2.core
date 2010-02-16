if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCommunicationSubscriptionType_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCommunicationSubscriptionType_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spCommunicationSubscriptionType_Update

	@CommunicationSubscriptionTypeId	Int = null,
	@Name	VarChar(50) = null,
	@EmailSubject	VarChar(100) = null,
	@EmailBody	Text = null,
	@EmailBodyType	VarChar(100) = null,
	@MailMessageType	VarChar(100) = null,
	@LastSentDate	DateTime = null,
	@FrequencyInMinutes	Int = null,
	@ProviderName	VarChar(100) = null,
	@AllowSubscription	Bit = null,
	@AutoSubscribe	Bit = null,
	@EffectiveDate	DateTime = null,
	@ExpirationDate	DateTime = null,
	@CreateDate	DateTime = null,
	@CreateUserId	Int = null,
	@LastModifiedDate	DateTime = null,
	@LastModifiedUserId	Int = null

AS

if @LastSentDate is null set @LastSentDate=getdate()
if @EffectiveDate is null set @EffectiveDate=getdate()

UPDATE
	CommunicationSubscriptionType
SET
	Name = @Name,
	EmailSubject = @EmailSubject,
	EmailBody = @EmailBody,
	EmailBodyType = @EmailBodyType,
	MailMessageType = @MailMessageType,
	LastSentDate = @LastSentDate,
	FrequencyInMinutes = @FrequencyInMinutes,
	ProviderName = @ProviderName,
	AllowSubscription = @AllowSubscription,
	AutoSubscribe = @AutoSubscribe,
	EffectiveDate = @EffectiveDate,
	ExpirationDate = @ExpirationDate,
	CreateDate = @CreateDate,
	CreateUserId = @CreateUserId,
	LastModifiedDate = @LastModifiedDate,
	LastModifiedUserId = @LastModifiedUserId
WHERE
CommunicationSubscriptionTypeId = @CommunicationSubscriptionTypeId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spCommunicationSubscriptionType_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

