if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAddressCache_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAddressCache_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spAddressCache_Insert
	@Address1	VarChar(80) = null,
	@City	VarChar(50) = null,
	@Region	VarChar(50) = null,
	@PostalCode	VarChar(10) = null,
	@Latitude	Decimal(18, 8) = null,
	@Longitude	Decimal(18, 8) = null,
	@Result	VarChar(8000) = null,
	@Status	VarChar(20) = null,
	@StdAddress1	VarChar(80) = null,
	@StdCity	VarChar(50) = null,
	@StdRegion	VarChar(50) = null,
	@StdPostalCode	VarChar(10) = null,
	@StdPlus4	VarChar(4) = null,
	@MatAddress1	VarChar(80) = null,
	@MatCity	VarChar(50) = null,
	@MatRegion	VarChar(50) = null,
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
        RAISERROR ('spAddressCache_Insert: Unable to insert new row into AddressCache table ', 16, 1, @@ROWCOUNT)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

