if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCurrencyExchange_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCurrencyExchange_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spCurrencyExchange_Insert
	@CurrencyCode	VarChar(3) = null,
	@EffectiveDate	DateTime = null,
	@Rate	Decimal(9, 5) = null

AS

if @EffectiveDate is null set @EffectiveDate=getdate()

INSERT INTO CurrencyExchange
(	CurrencyCode,
	EffectiveDate,
	Rate)
VALUES (
	@CurrencyCode,
	@EffectiveDate,
	@Rate)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spCurrencyExchange_Insert: Unable to insert new row into CurrencyExchange table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO

