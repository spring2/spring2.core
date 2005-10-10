if exists (select * from sysobjects where id = object_id(N'[vwResource]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwResource]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [vwResource]

AS

SELECT
    Resource.ResourceId,
    Resource.EntityName,
    Resource.PropertyName,
    Resource.[Identity]
FROM
    Resource
GO
