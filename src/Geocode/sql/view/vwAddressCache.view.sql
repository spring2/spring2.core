if exists (select * from sysobjects where id = object_id(N'[vwAddressCache]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwAddressCache]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [vwAddressCache]

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
    AddressCache.Status,
    AddressCache.StdAddress1,
    AddressCache.StdCity,
    AddressCache.StdRegion,
    AddressCache.StdPostalCode,
    AddressCache.StdPlus4,
    AddressCache.MatAddress1,
    AddressCache.MatCity,
    AddressCache.MatRegion,
    AddressCache.MatPostalCode,
    AddressCache.MatchType
FROM
    AddressCache
GO
