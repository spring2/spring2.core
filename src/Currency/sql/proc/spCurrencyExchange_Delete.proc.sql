if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCurrencyExchange_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCurrencyExchange_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spCurrencyExchange_Delete

@CurrencyExchangeId	Int

AS

if not exists(SELECT * FROM CurrencyExchange WHERE (	CurrencyExchangeId = @CurrencyExchangeId
))
    BEGIN
        RAISERROR  ('spCurrencyExchange_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	CurrencyExchange
WHERE 
	CurrencyExchangeId = @CurrencyExchangeId
GO

