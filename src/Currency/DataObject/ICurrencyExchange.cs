using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.Currency.DataObject;
using Spring2.Core.DAO;
using Spring2.Core.Types;


using Spring2.Core.BusinessEntity;

namespace Spring2.Core.Currency.DataObject {

    public class CurrencyExchangeFields {
	private CurrencyExchangeFields() {}
	public static readonly String ENTITY_NAME = "CurrencyExchange";
	
	public static readonly ColumnMetaData CURRENCYEXCHANGEID = new ColumnMetaData("CurrencyExchangeId", "CurrencyExchangeId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData CURRENCYCODE = new ColumnMetaData("CurrencyCode", "CurrencyCode", DbType.AnsiString, SqlDbType.VarChar, 3, 0, 0);
	public static readonly ColumnMetaData EFFECTIVEDATE = new ColumnMetaData("EffectiveDate", "EffectiveDate", DbType.DateTime, SqlDbType.DateTime, 40, 0, 0);
	public static readonly ColumnMetaData RATE = new ColumnMetaData("Rate", "Rate", DbType.Decimal, SqlDbType.Decimal, 0, 9, 5);
    }

    public interface ICurrencyExchange : IBusinessEntity {
	IdType CurrencyExchangeId {
	    get;
	}
	StringType CurrencyCode {
	    get;
	}
	DateTimeType EffectiveDate {
	    get;
	}
	DecimalType Rate {
	    get;
	}
    }
}
