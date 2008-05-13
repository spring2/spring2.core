if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spConfigurationSetting_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spConfigurationSetting_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spConfigurationSetting_Update

	@ConfigurationSettingId	Int = null,
	@Key	VarChar(100) = null,
	@Value	VarChar(1024) = null,
	@LastModifiedDate	DateTime = null,
	@LastModifiedUserId	Int = null,
	@EffectiveDate DateTime = null

AS


UPDATE
	ConfigurationSetting
SET
	[Key] = @Key,
	Value = @Value,
	LastModifiedDate = @LastModifiedDate,
	LastModifiedUserId = @LastModifiedUserId,
	EffectiveDate = @EffectiveDate
WHERE
ConfigurationSettingId = @ConfigurationSettingId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spConfigurationSetting_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

