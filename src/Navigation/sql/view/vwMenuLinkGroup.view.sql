if exists (select * from sysobjects where id = object_id(N'[vwMenuLinkGroup]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwMenuLinkGroup]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [vwMenuLinkGroup]

AS

SELECT
    MenuLinkGroup.MenuLinkGroupId,
    MenuLinkGroup.Name
FROM
    MenuLinkGroup
GO
