if exists (select * from sysobjects where id = object_id(N'dbo.[vwResource]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwResource]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwResource]

AS

SELECT
    Resource.ResourceId,
    Resource.Context,
    Resource.Field,
    Resource.ContextIdentity
FROM
    Resource
GO
