if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAddressCache_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAddressCache_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spAddressCache_Update

	@AddressId	Int = null,
	@Address1	VarChar(80) = null,
	@City	VarChar(50) = null,
	@Region	Char(50) = null,
	@PostalCode	VarChar(10) = null,
	@Latitude	Decimal(18, 8) = null,
	@Longitude	Decimal(18, 8) = null,
	@Result	VarChar(8000) = null,
	@Status	VarChar(20) = null,
	@StdAddress1	VarChar(80) = null,
	@StdCity	VarChar(50) = null,
	@StdRegion	Char(50) = null,
	@StdPostalCode	VarChar(10) = null,
	@StdPlus4	Char(4) = null,
	@MatAddress1	VarChar(80) = null,
	@MatCity	VarChar(50) = null,
	@MatRegion	Char(50) = null,
	@MatPostalCode	VarChar(10) = null,
	@MatchType	Int = null

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
	Status = @Status,
	StdAddress1 = @StdAddress1,
	StdCity = @StdCity,
	StdRegion = @StdRegion,
	StdPostalCode = @StdPostalCode,
	StdPlus4 = @StdPlus4,
	MatAddress1 = @MatAddress1,
	MatCity = @MatCity,
	MatRegion = @MatRegion,
	MatPostalCode = @MatPostalCode,
	MatchType = @MatchType
WHERE
AddressId = @AddressId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spAddressCache_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

