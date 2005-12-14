if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAddressCache_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAddressCache_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spAddressCache_Delete

@AddressId	Int

AS

if not exists(SELECT * FROM AddressCache WHERE (	AddressId = @AddressId
))
    BEGIN
        RAISERROR  ('spAddressCache_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	AddressCache
WHERE 
	AddressId = @AddressId
GO

