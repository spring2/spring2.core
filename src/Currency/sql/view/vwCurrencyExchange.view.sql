if exists (select * from sysobjects where id = object_id(N'dbo.[vwCurrencyExchange]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.[vwCurrencyExchange]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.[vwCurrencyExchange]

AS

SELECT
    CurrencyExchange.CurrencyExchangeId,
    CurrencyExchange.CurrencyCode,
    CurrencyExchange.EffectiveDate,
    CurrencyExchange.Rate
FROM
    CurrencyExchange
GO
