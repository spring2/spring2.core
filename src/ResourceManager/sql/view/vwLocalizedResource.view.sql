if exists (select * from sysobjects where id = object_id(N'dbo.[vwLocalizedResource]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwLocalizedResource]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwLocalizedResource]

AS

SELECT
    LocalizedResource.LocalizedResourceId,
    LocalizedResource.ResourceId,
    LocalizedResource.Locale,
    LocalizedResource.Language,
    LocalizedResource.Content,
    Resource.ResourceId Resource_ResourceId,
    Resource.Context Resource_Context,
    Resource.Field Resource_Field,
    Resource.ContextIdentity Resource_ContextIdentity
FROM
    LocalizedResource
LEFT JOIN vwResource Resource on LocalizedResource.ResourceId = Resource.ResourceId
GO
