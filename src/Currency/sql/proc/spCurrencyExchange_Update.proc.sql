if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCurrencyExchange_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCurrencyExchange_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spCurrencyExchange_Update

	@CurrencyExchangeId	Int = null,
	@CurrencyCode	VarChar(3) = null,
	@EffectiveDate	DateTime = null,
	@Rate	Decimal(9, 5) = null

AS

if @EffectiveDate is null set @EffectiveDate=getdate()

UPDATE
	CurrencyExchange
SET
	CurrencyCode = @CurrencyCode,
	EffectiveDate = @EffectiveDate,
	Rate = @Rate
WHERE
CurrencyExchangeId = @CurrencyExchangeId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spCurrencyExchange_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

