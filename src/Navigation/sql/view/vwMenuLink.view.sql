if exists (select * from sysobjects where id = object_id(N'dbo.[vwMenuLink]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwMenuLink]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwMenuLink]

AS

SELECT
    MenuLink.MenuLinkId,
    MenuLink.Name,
    MenuLink.Target,
    MenuLink.Active,
    MenuLink.MenuLinkGroupId,
    MenuLink.ParentMenuLinkId,
    MenuLink.EffectiveDate,
    MenuLink.ExpirationDate
FROM
    MenuLink
GO
