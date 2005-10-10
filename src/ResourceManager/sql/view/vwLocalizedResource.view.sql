if exists (select * from sysobjects where id = object_id(N'[vwLocalizedResource]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwLocalizedResource]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [vwLocalizedResource]

AS

SELECT
    LocalizedResource.LocalizedResourceId,
    LocalizedResource.ResourceId,
    LocalizedResource.Locale,
    LocalizedResource.Language,
    LocalizedResource.Content
FROM
    LocalizedResource
GO
