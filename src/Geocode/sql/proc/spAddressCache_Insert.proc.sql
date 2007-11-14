if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAddressCache_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAddressCache_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spAddressCache_Insert
	@Address1	VarChar(80) = null,
	@City	VarChar(40) = null,
	@Region	Char(2) = null,
	@PostalCode	VarChar(10) = null,
	@Latitude	Decimal(18, 8) = null,
	@Longitude	Decimal(18, 8) = null,
	@Result	VarChar(1000) = null,
	@Status	VarChar(20) = null,
	@StdAddress1	VarChar(80) = null,
	@StdCity	VarChar(40) = null,
	@StdRegion	Char(2) = null,
	@StdPostalCode	VarChar(10) = null,
	@StdPlus4	Char(4) = null,
	@MatAddress1	VarChar(80) = null,
	@MatCity	VarChar(40) = null,
	@MatRegion	Char(2) = null,
	@MatPostalCode	VarChar(10) = null,
	@MatchType	Int = null

AS


INSERT INTO AddressCache
(	Address1,
	City,
	Region,
	PostalCode,
	Latitude,
	Longitude,
	Result,
	Status,
	StdAddress1,
	StdCity,
	StdRegion,
	StdPostalCode,
	StdPlus4,
	MatAddress1,
	MatCity,
	MatRegion,
	MatPostalCode,
	MatchType)
VALUES (
	@Address1,
	@City,
	@Region,
	@PostalCode,
	@Latitude,
	@Longitude,
	@Result,
	@Status,
	@StdAddress1,
	@StdCity,
	@StdRegion,
	@StdPostalCode,
	@StdPlus4,
	@MatAddress1,
	@MatCity,
	@MatRegion,
	@MatPostalCode,
	@MatchType)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spAddressCache_Insert: Unable to insert new row into AddressCache table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

