if exists (select * from sysobjects where id = object_id(N'dbo.[vwAddressCache]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwAddressCache]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwAddressCache]

AS

SELECT
    AddressCache.AddressId,
    AddressCache.Address1,
    AddressCache.City,
    AddressCache.Region,
    AddressCache.PostalCode,
    AddressCache.Latitude,
    AddressCache.Longitude,
    AddressCache.Result,
    AddressCache.Status
FROM
    AddressCache
GO
