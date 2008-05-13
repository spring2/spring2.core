if exists (select * from sysobjects where id = object_id(N'dbo.[vwConfigurationSetting]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwConfigurationSetting]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwConfigurationSetting]

AS

SELECT
    ConfigurationSetting.ConfigurationSettingId,
    ConfigurationSetting.[Key],
    ConfigurationSetting.Value,
    ConfigurationSetting.LastModifiedDate,
    ConfigurationSetting.LastModifiedUserId,
    ConfigurationSetting.EffectiveDate
FROM
    ConfigurationSetting
GO
