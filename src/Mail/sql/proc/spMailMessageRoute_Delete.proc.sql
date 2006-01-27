if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMailMessageRoute_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMailMessageRoute_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spMailMessageRoute_Delete

@MailMessageRouteId	Int

AS

if not exists(SELECT * FROM MailMessageRoute WHERE (	MailMessageRouteId = @MailMessageRouteId
))
    BEGIN
        RAISERROR  ('spMailMessageRoute_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	MailMessageRoute
WHERE 
	MailMessageRouteId = @MailMessageRouteId
GO

