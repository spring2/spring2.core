if exists (select * from sysobjects where id = object_id(N'[vwMailMessageRoute]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwMailMessageRoute]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [vwMailMessageRoute]

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
