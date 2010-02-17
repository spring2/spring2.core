if exists (select * from sysobjects where id = object_id(N'dbo.[vwPublicationTracking]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwPublicationTracking]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwPublicationTracking]

AS

SELECT
    PublicationTracking.PublicationTrackingId,
    PublicationTracking.PublicationPrimaryKeyId,
    PublicationTracking.PublicationTypeId,
    PublicationTracking.CreateDate,
    PublicationTracking.CreateUserId,
    PublicationTracking.LastModifiedDate,
    PublicationTracking.LastModifiedUserId
FROM
    PublicationTracking
GO
