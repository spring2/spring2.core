if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAddressCache_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAddressCache_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spAddressCache_Update

	@AddressId	Int = null,
	@Address1	VarChar(80) = null,
	@City	VarChar(40) = null,
	@Region	Char(2) = null,
	@PostalCode	VarChar(10) = null,
	@Latitude	Decimal(18, 8) = null,
	@Longitude	Decimal(18, 8) = null,
	@Result	VarChar(1000) = null,
	@Status	VarChar(20) = null

AS


UPDATE
	AddressCache
SET
	Address1 = @Address1,
	City = @City,
	Region = @Region,
	PostalCode = @PostalCode,
	Latitude = @Latitude,
	Longitude = @Longitude,
	Result = @Result,
	Status = @Status
WHERE
AddressId = @AddressId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spAddressCache_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

