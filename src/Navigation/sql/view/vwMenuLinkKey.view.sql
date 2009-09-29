if exists (select * from sysobjects where id = object_id(N'dbo.[vwMenuLinkKey]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwMenuLinkKey]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwMenuLinkKey]

AS

SELECT
    MenuLinkKey.MenuLinkId,
    MenuLinkKey.[Key]
FROM
    MenuLinkKey
GO
