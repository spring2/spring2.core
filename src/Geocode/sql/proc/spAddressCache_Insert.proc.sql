if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAddressCache_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAddressCache_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spAddressCache_Insert
	@Address1	VarChar(80) = null,
	@City	VarChar(40) = null,
	@Region	Char(2) = null,
	@PostalCode	VarChar(10) = null,
	@Latitude	Decimal(18, 8) = null,
	@Longitude	Decimal(18, 8) = null,
	@Result	VarChar(1000) = null,
	@Status	VarChar(20) = null

AS


INSERT INTO AddressCache
(	Address1,
	City,
	Region,
	PostalCode,
	Latitude,
	Longitude,
	Result,
	Status)
VALUES (
	@Address1,
	@City,
	@Region,
	@PostalCode,
	@Latitude,
	@Longitude,
	@Result,
	@Status)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spAddressCache_Insert: Unable to insert new row into AddressCache table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

