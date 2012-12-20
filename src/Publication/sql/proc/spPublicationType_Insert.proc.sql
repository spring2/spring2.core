if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPublicationType_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPublicationType_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spPublicationType_Insert
	@Name	VarChar(50) = null,
	@EmailSubject	VarChar(500) = null,
	@EmailBody	Text = null,
	@EmailBodyType	VarChar(100) = null,
	@Description	VarChar(max) = null,
	@MailMessageType	VarChar(100) = null,
	@LastSentDate	DateTime = null,
	@FrequencyInMinutes	Int = null,
	@ProviderName	VarChar(250) = null,
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

INSERT INTO PublicationType
(	Name,
	EmailSubject,
	EmailBody,
	EmailBodyType,
	Description,
	MailMessageType,
	LastSentDate,
	FrequencyInMinutes,
	ProviderName,
	AllowSubscription,
	AutoSubscribe,
	EffectiveDate,
	ExpirationDate,
	CreateDate,
	CreateUserId,
	LastModifiedDate,
	LastModifiedUserId)
VALUES (
	@Name,
	@EmailSubject,
	@EmailBody,
	@EmailBodyType,
	@Description,
	@MailMessageType,
	@LastSentDate,
	@FrequencyInMinutes,
	@ProviderName,
	@AllowSubscription,
	@AutoSubscribe,
	@EffectiveDate,
	@ExpirationDate,
	@CreateDate,
	@CreateUserId,
	@LastModifiedDate,
	@LastModifiedUserId)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR ('spPublicationType_Insert: Unable to insert new row into PublicationType table ', 16, 1, @@ROWCOUNT)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

