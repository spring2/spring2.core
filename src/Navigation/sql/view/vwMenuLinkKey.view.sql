if exists (select * from sysobjects where id = object_id(N'[vwMenuLinkKey]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwMenuLinkKey]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [vwMenuLinkKey]

AS

SELECT
    MenuLinkKey.MenuLinkId,
    MenuLinkKey.[Key]
FROM
    MenuLinkKey
GO
