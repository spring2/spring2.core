if exists (select * from sysobjects where id = object_id(N'dbo.[vwMenuLinkGroup]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwMenuLinkGroup]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwMenuLinkGroup]

AS

SELECT
    MenuLinkGroup.MenuLinkGroupId,
    MenuLinkGroup.Name
FROM
    MenuLinkGroup
GO
