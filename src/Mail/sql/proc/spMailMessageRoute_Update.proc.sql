if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMailMessageRoute_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMailMessageRoute_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMailMessageRoute_Update

	@MailMessageRouteId	Int = null,
	@MailMessage	VarChar(50) = null,
	@RoutingType	VarChar(10) = null,
	@Status	VarChar(1) = null,
	@EmailAddress	VarChar(200) = null

AS

if @Status is null set @Status='Y'

UPDATE
	MailMessageRoute
SET
	MailMessage = @MailMessage,
	RoutingType = @RoutingType,
	Status = @Status,
	EmailAddress = @EmailAddress
WHERE
MailMessageRouteId = @MailMessageRouteId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spMailMessageRoute_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

