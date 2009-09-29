if exists (select * from sysobjects where id = object_id(N'dbo.[vwMailMessageRoute]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwMailMessageRoute]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwMailMessageRoute]

AS

SELECT
    MailMessageRoute.MailMessageRouteId,
    MailMessageRoute.MailMessage,
    MailMessageRoute.RoutingType,
    MailMessageRoute.Status,
    MailMessageRoute.EmailAddress
FROM
    MailMessageRoute
GO
