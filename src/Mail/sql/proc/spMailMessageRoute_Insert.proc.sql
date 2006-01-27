if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMailMessageRoute_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMailMessageRoute_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spMailMessageRoute_Insert
	@MailMessage	VarChar(50) = null,
	@RoutingType	VarChar(10) = null,
	@Status	VarChar(1) = null,
	@EmailAddress	VarChar(200) = null

AS

if @Status is null set @Status='Y'

INSERT INTO MailMessageRoute
(	MailMessage,
	RoutingType,
	Status,
	EmailAddress)
VALUES (
	@MailMessage,
	@RoutingType,
	@Status,
	@EmailAddress)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spMailMessageRoute_Insert: Unable to insert new row into MailMessageRoute table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

