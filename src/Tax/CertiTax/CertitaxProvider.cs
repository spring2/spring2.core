using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Web.Services.Protocols;
using log4net;
using Spring2.Core.Configuration;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.Dss.net.esalestax.webservices1;
using Spring2.Core.Tax.Vertex;

namespace Spring2.Core.Tax.CertiTax {
    /// <summary>
    /// Tax provider using CertiTax Web Service.
    /// This class is must be stateless in order to be thread safe.
    /// </summary>
    public class CertiTaxProvider : ITaxProvider {
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	private static readonly string serialNumberKey = "CertiTax.SerialNum";
	private static readonly string transactionIdKey = "CertiTax.TransactionId";
	private static readonly string nexusKey = "CertiTax.Nexus";
	private static readonly string merchantIdKey = "CertiTax.MerchantId";
	private string profileKey = null;

	private string SerialNumber {
	    get { 
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? serialNumberKey : string.Format("{0}.{1}", serialNumberKey, profileKey)];
		if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[serialNumberKey];
		}
		return key;
	    }
	}

	private string TransactionId {
	    get { 
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? transactionIdKey : string.Format("{0}.{1}", transactionIdKey, profileKey)];
		if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[transactionIdKey];
		}
		return key;
	    }
	}

	private string Nexus {
	    get { 
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? nexusKey : string.Format("{0}.{1}", nexusKey, profileKey)];
		if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[nexusKey];
		}
		return key;
	    }
	}

	private string MerchantId {
	    get { 
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? merchantIdKey : string.Format("{0}.{1}", merchantIdKey, profileKey)];
		if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[merchantIdKey];
		}
		return key;
	    }
	}

	public CertiTaxProvider(StringType profileKey) {
	    if (profileKey.IsValid) {
		this.profileKey = profileKey.ToString();
	    }
	}

	// Country name has to be put in configuration settings.
	public TaxRateInfo GetTaxRateForArea(IdType id, DateType dateType) {
	    TaxOrder order = GetDummyOrder();
	    TaxResult result = Calculate(StringType.DEFAULT, StringType.DEFAULT, StringType.DEFAULT, StringType.DEFAULT, id.ToString(), "USA", DateType.Now, order);

	    return result;
	}

	public TaxResult Commit(TaxOrder order, StringType currencyCode) {
	    throw new System.NotImplementedException();
	}

	public TaxResult GetQuoteTaxTotal(TaxOrder order, StringType currencyCode) {
	    throw new System.NotImplementedException();
	}

	public TaxResult GetQuoteTaxTotal(ref TaxTransaction taxTrans, TaxOrder order, StringType currencyCode) {
	    throw new System.NotImplementedException();
	}

	//street address, county, & company name need to be passed too. The interface needs to be modified.
	public TaxAreaData GetTaxAreaForAddress(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, BooleanType limits) {
	    TaxOrder order = GetDummyOrder();
	    TaxResult result = Calculate(street, city, county, region, postalCode, country, DateType.Now, order);
	    CurrencyType productTaxAmount = result.TotalTax;

	    TaxAreaData area = new TaxAreaData();
	    area.City = result.City;
	    area.CityTaxRate = result.CityTaxRate;
	    area.Country = result.Country;
	    area.CountryTaxRate = result.CountryTaxRate;
	    area.County = result.County;
	    area.CountyTaxRate = result.CountyTaxRate;
	    area.PostalCode = result.PostalCode;
	    area.Region = result.Region;
	    area.RegionTaxRate = result.RegionTaxRate;
	    area.Street = result.Street;
	    area.TotalTaxRate = result.TotalTaxRate;
	    area.TaxAreaID = result.TaxAreaID;
	    area.AddressValidated = result.AddressValidated;
	    area.TaxJurisdictions = result.TaxJurisdictions;

	    order.Lines.Add(GetDummyShippingLine());
	    result = Calculate(street, city, county, region, postalCode, country, DateType.Now, order);

	    if (result.TotalTax - productTaxAmount > 0) {
	    	area.IsShippingTaxable = BooleanType.TRUE;
	    } else {
	    	area.IsShippingTaxable = BooleanType.FALSE;
	    }

	    return area;
	}

	public TaxAreaList LookupTaxArea(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, BooleanType booleanType) {
	    try {
		TaxAreaList taxAreaList = new TaxAreaList();
		taxAreaList.Add(GetTaxAreaForAddress(street, city, county, region, postalCode, country, BooleanType.FALSE));

		return taxAreaList;
	    } catch (SoapException ex) {
		MessageList errors = new MessageList();
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		throw new MessageListException(errors);
	    }
	}

	public TaxResult Commit(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    try {
		TaxResult result = Calculate(null, street, city, county, region, postalCode, country, date, order);

		CertiCalc ws = new CertiCalc();
		log.Info("Committing tax transaction: " + result.TaxTransactionId);
		ws.Commit(result.TaxTransactionId, SerialNumber);

		return result;
	    } catch (SoapException ex) {
		MessageList errors = new MessageList();
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		throw new MessageListException(errors);
	    }
	}

	public TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    StringType taxTransactionId = TransactionId;
	    return Calculate(taxTransactionId, street, city, county, region, postalCode, country, date, order);
	}

	public TaxResult Calculate(StringType taxTransactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    StringType location = StringType.DEFAULT;
	    return Calculate(taxTransactionId, street, city, county, region, postalCode, country, date, order, location);
	}

	public TaxResult Calculate(StringType taxTransactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order, StringType location) {
	    //Create Order object and populate it.
	    Order certiTaxOrder = new Order();

	    //Populate main fields
	    certiTaxOrder.SerialNumber = SerialNumber;
	    certiTaxOrder.Nexus = Nexus;
	    certiTaxOrder.MerchantTransactionId = MerchantId;
	    certiTaxOrder.CalculateTax = true;
	    if(street.IsValid || city.IsValid || county.IsValid || region.IsValid) {
		certiTaxOrder.ConfirmAddress = true;
	    }
	    if(taxTransactionId != null && taxTransactionId.IsValid) {
		certiTaxOrder.CertiTAXTransactionId = taxTransactionId;
	    }

	    // If a location is supplied, use it.
	    if(!location.IsDefault) {
		certiTaxOrder.Location = location;
	    } else if(region.Equals("UT") || 
		(region.IsDefault && postalCode.StartsWith("8"))) {

		// TODO: hack to make the tax rates within Utah correct
		certiTaxOrder.Location = "TX";
	    }

	    //Populate address fields for the order
	    certiTaxOrder.Address = new Address();
	    certiTaxOrder.Address.Street1 = street;
	    certiTaxOrder.Address.City = city;
	    certiTaxOrder.Address.County = county;
	    certiTaxOrder.Address.State = region;
	    certiTaxOrder.Address.PostalCode = postalCode;
	    certiTaxOrder.Address.Nation = country;

	    certiTaxOrder.LineItems = GetDummyOrderLines();
	    certiTaxOrder.Total = certiTaxOrder.LineItems[0].ExtendedPrice;

	    CertiCalc ws = new CertiCalc();
	    log.Info("Calculating tax rate for transaction: " + certiTaxOrder.CertiTAXTransactionId);
	    TaxTransaction taxTrans = null;
	    bool addressValidated = false;
	    try {
		taxTrans = ws.Calculate(certiTaxOrder);
		addressValidated = true;
	    } catch (SoapException) {
		//address could not be validated, get tax based on the valid data
		certiTaxOrder.ConfirmAddress = false;
		taxTrans = ws.Calculate(certiTaxOrder);
		addressValidated = false;
	    }

	    // get tax rate information
	    TaxResult result = new TaxResult();
	    result.AddressValidated = addressValidated;
	    result.TaxTransactionId = taxTrans.CertiTAXTransactionId;
	    Debug.WriteLine(taxTrans.CertiTAXTransactionId);
	    result.TotalTax = taxTrans.TotalTax;

//	    result.CityTaxRate = (DecimalType) ((taxTrans.CityTax/certiTaxOrder.Total)*100);
//	    result.CountyTaxRate = (DecimalType) ((taxTrans.CountyTax/certiTaxOrder.Total)*100);
//	    result.RegionTaxRate = (DecimalType) ((taxTrans.StateTax/certiTaxOrder.Total)*100);
//	    result.CountryTaxRate = (DecimalType) ((taxTrans.NationalTax/certiTaxOrder.Total)*100);
	    result.TotalTaxRate = (DecimalType) ((taxTrans.TotalTax/certiTaxOrder.Total)*100);	    

	    result.Street = taxTrans.CorrectedAddress.Street1;
	    result.City = taxTrans.CorrectedAddress.City;
	    result.Region = taxTrans.CorrectedAddress.State;
	    result.PostalCode = taxTrans.CorrectedAddress.PostalCode;

	    String pc = taxTrans.CorrectedAddress.PostalCode.Replace("-", "").Replace(" ", "");
	    Int32 pci;
	    if (Int32.TryParse(pc, out pci)) {
		result.TaxAreaID = new IdType(pci);
	    } else {
		//  TODO: hack to put something in tax area
		result.TaxAreaID = ConvertToId(taxTrans.CorrectedAddress.PostalCode);
	    }

	    AddTaxJurisidctionsToResult(taxTrans, certiTaxOrder, result);

	    // calculate the actual tax
	    try {
		//populate orderline items
		//OrderLineItem orderLineItem = new OrderLineItem();
		OrderLineItem[] certiTaxOrderLineItemArray = new OrderLineItem[order.Lines.Count];
		certiTaxOrder.Total = 0;
		certiTaxOrder.LineItems = new OrderLineItem[order.Lines.Count];

	    	ArrayList orderLines = new ArrayList();
		for (Int32 i = 0; i < order.Lines.Count; i ++) {
		    TaxOrderLine line = order.Lines[i];
		    //Determine if the order line item is a shipping cost related one. If it is pass build it as a "SHIPPING" SKU
		    OrderLineItem orderLine;
		    if (line.ItemNumber.ToString() == "SHIPPING") {
			orderLine = BuildOrderLineItem(new IdType(i), line.Price, line.Quantity, line.ItemNumber, line.DiscountAmount);
		    } else {
			orderLine = BuildOrderLineItem(new IdType(i), line.Price, line.Quantity, line.ItemNumber, line.DiscountAmount);
		    }

		    certiTaxOrder.Total = certiTaxOrder.Total + orderLine.ExtendedPrice;
		    orderLines.Add(orderLine);
		}
	    	
	    	certiTaxOrder.LineItems = orderLines.ToArray(typeof(OrderLineItem)) as OrderLineItem[];
	    	
		log.Info("Calculating tax for transaction: " + certiTaxOrder.CertiTAXTransactionId);
		taxTrans = ws.Calculate(certiTaxOrder);

		result.TaxTransactionId = taxTrans.CertiTAXTransactionId;
		Debug.WriteLine(taxTrans.CertiTAXTransactionId);
		result.TotalTax = taxTrans.TotalTax;

	    	AddTaxJurisidctionsToResult(taxTrans, certiTaxOrder, result);
		UpdateTaxJurisdictionAmounts(taxTrans, result);

		return result;
	    } catch (SoapException ex) {
		MessageList errors = new MessageList();
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		Debug.WriteLine(ex.Message.ToString());
		throw new MessageListException(errors);
	    }
	}

	private IdType ConvertToId(String s) {
	    String code = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	    Int32 result = 0;
	    Int32 power = 1;
	    for (Int32 i = s.Length; i > 0; i--) {
		Char c = s[i-1];
		if (c != ' ') {
		    Int32 v = code.IndexOf(c) * power;
		    result += v;
		    power *= 10;
		}
	    }

	    return new IdType(result);
	}

	private void AddTaxJurisidctionsToResult(TaxTransaction taxTrans, Order certiTaxOrder, TaxResult result) {
	    if (taxTrans.CityTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.CITY] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.CITY, taxTrans.CityTaxAuthority, (taxTrans.CityTax / certiTaxOrder.Total) * 100, 0);
	    }
	    if (taxTrans.CountyTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTY] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.COUNTY, taxTrans.CountyTaxAuthority, (taxTrans.CountyTax / certiTaxOrder.Total) * 100, 0);
	    }
	    if (taxTrans.NationalTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTRY] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.COUNTRY, taxTrans.NationalTaxAuthority, (taxTrans.NationalTax / certiTaxOrder.Total) * 100, 0);
	    }
	    if (taxTrans.StateTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.STATE] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.STATE, taxTrans.StateTaxAuthority, (taxTrans.StateTax / certiTaxOrder.Total) * 100, 0);
	    }
	    if (taxTrans.OtherTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.OTHER] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.OTHER, taxTrans.OtherTaxAuthority, (taxTrans.OtherTax / certiTaxOrder.Total) * 100, 0);
	    }
	    if (taxTrans.LocalTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.LOCAL] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.LOCAL, taxTrans.LocalTaxAuthority, (taxTrans.LocalTax / certiTaxOrder.Total) * 100, 0);
	    }
	}

	private void UpdateTaxJurisdictionAmounts(TaxTransaction taxTrans, TaxResult result) {
	    foreach (TaxJurisdiction jurisdiction in result.TaxJurisdictions) {
		if (jurisdiction.JurisdictionType.Equals(TaxJurisdictionTypeEnum.CITY)) {
		    jurisdiction.Amount = taxTrans.CityTax;
		} else if (jurisdiction.JurisdictionType.Equals(TaxJurisdictionTypeEnum.COUNTRY)) {
		    jurisdiction.Amount = taxTrans.NationalTax;
		} else if (jurisdiction.JurisdictionType.Equals(TaxJurisdictionTypeEnum.COUNTY)) {
		    jurisdiction.Amount = taxTrans.CountyTax;
		} else if (jurisdiction.JurisdictionType.Equals(TaxJurisdictionTypeEnum.LOCAL)) {
		    jurisdiction.Amount = taxTrans.LocalTax;
		} else if (jurisdiction.JurisdictionType.Equals(TaxJurisdictionTypeEnum.OTHER)) {
		    jurisdiction.Amount = taxTrans.OtherTax;
		} else if (jurisdiction.JurisdictionType.Equals(TaxJurisdictionTypeEnum.STATE)) {
		    jurisdiction.Amount = taxTrans.StateTax;
		}
	    }
	}

	private OrderLineItem BuildOrderLineItem(IdType lineId, CurrencyType price, IntegerType quantity, StringType itemNumber, CurrencyType discountAmount) {
	    OrderLineItem line = new OrderLineItem();
	    if (quantity == 0) {
	    	line.Quantity = 1;
	    	line.ExtendedPrice = 0;
	    } else {
	    	line.Quantity = (int)quantity;
	    	line.ExtendedPrice = ((decimal)price * quantity) - (discountAmount.IsValid ? discountAmount.ToDecimal() : 0);
	    }
	    line.ItemId = lineId.ToString();
	    line.ProductCode = 0;
	    line.StockingUnit = itemNumber;

	    return line;
	}

	private TaxOrder GetDummyOrder() {
	    TaxOrder order = new TaxOrder();

	    TaxOrderLine taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 1000;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    return order;
	}

	private OrderLineItem[] GetDummyOrderLines() {
	    OrderLineItem[] lines = new OrderLineItem[1];
	    lines[0] = BuildOrderLineItem(0, 1000, 1, "CalcTaxRate", 0);
	    return lines;
	}

	private TaxOrderLine GetDummyShippingLine() {
	    TaxOrderLine taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 1000;
	    taxOrderLine.ItemNumber = "SHIPPING";

	    return taxOrderLine;
	}

    }
}