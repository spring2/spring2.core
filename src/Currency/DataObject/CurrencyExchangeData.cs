using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.Currency.DataObject;
using Spring2.Core.DAO;
using Spring2.Core.Types;


using Spring2.Core.Currency.Types;


namespace Spring2.Core.Currency.DataObject {

    public class CurrencyExchangeData : Spring2.Core.DataObject.DataObject {

	public static readonly CurrencyExchangeData DEFAULT = new CurrencyExchangeData();

	private IdType currencyExchangeId = IdType.DEFAULT;
	private StringType currencyCode = StringType.DEFAULT;
	private DateTimeType effectiveDate = DateTimeType.DEFAULT;
	private DecimalType rate = DecimalType.DEFAULT;

	public IdType CurrencyExchangeId {
	    get { return this.currencyExchangeId; }
	    set { this.currencyExchangeId = value; }
	}

	public StringType CurrencyCode {
	    get { return this.currencyCode; }
	    set { this.currencyCode = value; }
	}

	public DateTimeType EffectiveDate {
	    get { return this.effectiveDate; }
	    set { this.effectiveDate = value; }
	}

	public DecimalType Rate {
	    get { return this.rate; }
	    set { this.rate = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}
