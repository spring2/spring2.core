using System;

using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.Currency.Dao;
using Spring2.Core.Currency.DataObject;

namespace Spring2.Core.Currency.BusinessLogic {

    public class CurrencyExchange : Spring2.Core.BusinessEntity.BusinessEntity, ICurrencyExchange {

	[Generate()]
	private IdType currencyExchangeId = IdType.DEFAULT;
	[Generate()]
	private StringType currencyCode = StringType.DEFAULT;
	[Generate()]
	private DateTimeType effectiveDate = DateTimeType.DEFAULT;
	[Generate()]
	private DecimalType rate = DecimalType.DEFAULT;

	[Generate()]
	internal CurrencyExchange() {
	}

	[Generate()]
	internal CurrencyExchange(Boolean isNew) {
	    this.isNew = isNew;
	}

	[Generate()]
	public static CurrencyExchange NewInstance() {
	    return new CurrencyExchange();
	}

	[Generate()]
	public void Update(CurrencyExchangeData data) {
	    currencyExchangeId = data.CurrencyExchangeId.IsDefault ? currencyExchangeId : data.CurrencyExchangeId;
	    currencyCode = data.CurrencyCode.IsDefault ? currencyCode : data.CurrencyCode;
	    effectiveDate = data.EffectiveDate.IsDefault ? effectiveDate : data.EffectiveDate;
	    rate = data.Rate.IsDefault ? rate : data.Rate;
	    Store();
	}

	[Generate()]
	public void Store() {
	    MessageList errors = Validate();

	    if (errors.Count > 0) {
		if (!isNew) {
		    this.Reload();
		}
		throw new MessageListException(errors);
	    }

	    if (isNew) {
		this.CurrencyExchangeId = CurrencyExchangeDAO.DAO.Insert(this);
		isNew = false;
	    } else {
		CurrencyExchangeDAO.DAO.Update(this);
	    }
	}

	[Generate()]
	public virtual MessageList Validate() {

	    MessageList errors = new MessageList();
	    return errors;
	}


	[Generate()]
	public IdType CurrencyExchangeId {
	    get { return this.currencyExchangeId; }
	    set { this.currencyExchangeId = value; }
	}

	[Generate()]
	public StringType CurrencyCode {
	    get { return this.currencyCode; }
	    set { this.currencyCode = value; }
	}

	[Generate()]
	public DateTimeType EffectiveDate {
	    get { return this.effectiveDate; }
	    set { this.effectiveDate = value; }
	}

	[Generate()]
	public DecimalType Rate {
	    get { return this.rate; }
	    set { this.rate = value; }
	}

	[Generate()]
	public void Reload() {
	    CurrencyExchangeDAO.DAO.Reload(this);
	}



	[Generate()]
	public override String ToString() {
	    return GetType().ToString() + "@" + CurrencyExchangeId.ToString();
	}

	[Generate()]
	public static CurrencyExchange GetInstance(IdType currencyExchangeId) {
	    return CurrencyExchangeDAO.DAO.Load(currencyExchangeId);
	}

	private static readonly string defaultCurrency = "USD";

	public static ICurrencyExchange GetCurrentRate(StringType currencyCode) {
	    ICurrencyExchange line = null;
	    try {
		line = CurrencyExchangeDAO.DAO.FindEffectiveRateByCode(currencyCode);
	    } catch {
		line = CurrencyExchange.NewInstance();
	    }
	    return line;
	}

	public static double GetRate(string toCurrency) {
	    return GetRate(defaultCurrency, toCurrency);
	}

	public static double GetRate(string fromCurrency, string toCurrency) {
	    return CurrencyProvider.Instance.GetConversionRate(fromCurrency, toCurrency);
	}

	public static ICurrencyExchange CheckForNewRate(StringType currencyCode) {
	    ICurrencyExchange line = GetCurrentRate(currencyCode);
	    double rate = 0;

	    // if it doesn't exist or it's a different date create one
	    if ((line.IsNew) || (line.EffectiveDate.ToDate().CompareTo(DateType.Now.ToDate()) != 0)) {
		CurrencyExchangeData data = new CurrencyExchangeData();
		data.CurrencyCode = currencyCode;
		rate = GetRate(currencyCode); // calls the provider
		data.Rate = new DecimalType(rate);
		data.EffectiveDate = DateTimeType.Now;
		
		CurrencyExchange line2 = CurrencyExchange.NewInstance();
		line2.Update(data);
		
		return line2;
	    }

	    rate = GetRate(currencyCode);

	    // always insert a new rate when the recently retrieved and the most recent in the db are different
	    if (line.Rate != rate && rate > 0) {
		CurrencyExchangeData data = new CurrencyExchangeData();
		data.Rate = rate;
		data.EffectiveDate = DateTimeType.Now;
		data.CurrencyCode = currencyCode;

		CurrencyExchange line2 = CurrencyExchange.NewInstance();
		line2.Update(data);

		return line2;
	    }
	    return line;
	}
    }
}
