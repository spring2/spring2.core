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
using Avalara.AvaTax.Adapter.TaxService;
using Avalara.AvaTax.Adapter.AddressService;
using Avalara.AvaTax.Adapter;
using Spring2.Core.Util;

using Address = Avalara.AvaTax.Adapter.AddressService.Address;
using System.Collections.Generic;

namespace Spring2.Core.Tax.AvalaraTax {
    /// <summary>
    /// Tax provider using AvalaraTax Web Service.
    /// This class is must be stateless in order to be thread safe.
    /// </summary>
    public class AvalaraProvider : ITaxProvider {
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	private static readonly string urlKey = "Avalara.Url";
	private static readonly string accountKey = "Avalara.Account";
	private static readonly string licenseKey = "Avalara.License";
	private static readonly string companyCodeKey = "Avalara.CompanyCode";
	private string profileKey = null;

	private string Url {
	    get { 
	        string key = ConfigurationProvider.Instance.Settings[profileKey == null ? urlKey : string.Format("{0}.{1}", urlKey, profileKey)];
	        if (key == null) {
	            key = ConfigurationProvider.Instance.Settings[urlKey];
	        }
	        return key;
	    }
	}

	private string Account {
	    get { 
	        string key = ConfigurationProvider.Instance.Settings[profileKey == null ? accountKey : string.Format("{0}.{1}", accountKey, profileKey)];
	        if (key == null) {
	            key = ConfigurationProvider.Instance.Settings[accountKey];
	        }
	        return key;
	    }
	}

	private string License {
	    get {
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? licenseKey : string.Format("{0}.{1}", licenseKey, profileKey)];
	        if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[licenseKey];
	        }
	        return key;
	    }
	}

	private string CompanyCode {
	    get {
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? companyCodeKey : string.Format("{0}.{1}", companyCodeKey, profileKey)];
	        if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[companyCodeKey];
	        }
	        return key;
	    }
	}

	public AvalaraProvider(StringType profileKey) {
	    if (profileKey.IsValid) {
		this.profileKey = profileKey.ToString();
	    }
	}

	// Country name has to be put in configuration settings.
	public TaxRateInfo GetTaxRateForArea(IdType id, DateType dateType) {
	    //TaxOrder order = GetDummyOrder();
	    //TaxResult result = Calculate(StringType.DEFAULT, StringType.DEFAULT, StringType.DEFAULT, StringType.DEFAULT, id.ToString(), "USA", DateType.Now, order);

	    //return result;

	    throw new System.NotImplementedException("can't work with just a zip code");
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

	public TaxAreaData GetTaxAreaForAddress(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, BooleanType limits, StringType location) {
	    return GetTaxAreaForAddress(street, city, county, region, postalCode, country, limits);
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

	public TaxAreaList LookupTaxArea(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, BooleanType booleanType, StringType location) {
	    return LookupTaxArea(street, city, county, region, postalCode, country, date, booleanType);
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

	public TaxAreaData Validate(StringType street, StringType city, StringType region, StringType postalCode, StringType country) {
	    AddressSvc addressSvc = GetAddressSvc();

	    ValidateRequest req = new ValidateRequest();
	    Address a = new Address();
	    a.Line1 = street;
	    a.City = city;
	    a.Region = region;
	    a.PostalCode = postalCode;
	    a.Country = country;
	    req.Address = a;
	    req.Taxability = true;	    

	    ValidateResult result;
	    try {
		result = addressSvc.Validate(req);
	    } catch (SoapException ex) {
		MessageList errors = new MessageList();
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		throw new MessageListException(errors);
	    }

	    TaxAreaData data = new TaxAreaData();

	    if (result.Messages.Count > 0) {
		data.AddressValidated = false;
	    } else {
		data.AddressValidated = true;
	    }

	    if (result.Addresses.Count > 0) {
		data.Street = result.Addresses[0].Line1;
		data.City = result.Addresses[0].City;
		data.Region = result.Addresses[0].Region;
		data.PostalCode = result.Addresses[0].PostalCode;
		data.Country = result.Addresses[0].Country;
		data.TaxAreaID = result.Addresses[0].TaxRegionId;
	    }

	    return data;
	}



	public void Cancel(TaxOrder order) {
	    TaxSvc taxSvc = GetTaxSvc();

	    CancelTaxRequest req = new CancelTaxRequest();
	    req.CompanyCode = CompanyCode;
	    req.DocType = DocumentType.SalesInvoice;
	    req.DocCode = GetDocCode(order.OrderId);
	    req.CancelCode = CancelCode.DocDeleted;

	    CancelTaxResult result;
	    try {
		result = taxSvc.CancelTax(req);
	    } catch (SoapException ex) {
		MessageList errors = new MessageList();
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		throw new MessageListException(errors);
	    }

	    if (result.Messages.Count > 0) {
		throw new Exception("cancel result has exceptions");
	    }

	}

	public TaxResult Commit(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order, StringType location) {
	    return Commit(street, city, county, region, postalCode, country, date, order);
	}

	public TaxResult Commit(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    try {
		TaxResult result = Calculate(null, street, city, county, region, postalCode, country, date, order, DocumentType.SalesInvoice);

		//##############################################################################
		//### 1st WE CREATE THE REQUEST OBJECT FOR DOCUMENT THAT SHOULD BE COMMITTED ###
		//##############################################################################
		CommitTaxRequest commitTaxRequest = new CommitTaxRequest();

		//###########################################################
		//### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
		//###########################################################
		commitTaxRequest.CompanyCode = CompanyCode;
		commitTaxRequest.DocType = DocumentType.SalesInvoice;
		commitTaxRequest.DocCode = GetDocCode(order.OrderId);

		//##############################################################################################
		//### 3rd WE INVOKE THE COMMITTAX() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
		TaxSvc taxSvc = GetTaxSvc();

		PostTaxRequest postTaxRequest = new PostTaxRequest();
		postTaxRequest.CompanyCode = CompanyCode;
		postTaxRequest.DocType = DocumentType.SalesInvoice;
		postTaxRequest.DocCode = GetDocCode(order.OrderId);
		postTaxRequest.Commit = false;
		postTaxRequest.DocDate = DateTime.Now;
		postTaxRequest.TotalAmount = order.MerchandiseTotal.ToDecimal();
		postTaxRequest.TotalTax = result.TotalTax.ToDecimal();

		PostTaxResult postTaxResult = taxSvc.PostTax(postTaxRequest);

		if (postTaxResult.Messages.Count > 0) {
		    throw new Exception("post result has exceptions");
		}

		CommitTaxResult commitTaxResult = taxSvc.CommitTax(commitTaxRequest);
		
		result.TaxTransactionId = commitTaxResult.TransactionId;

		if (commitTaxResult.Messages.Count > 0) {
		    throw new Exception("commit result has exceptions");
		}

		commitTaxRequest = null;
		taxSvc = null;				

		return result;
	    } catch (SoapException ex) {
		MessageList errors = new MessageList();
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		throw new MessageListException(errors);
	    }
	}

	private TaxSvc GetTaxSvc() {
	    TaxSvc taxSvc = new TaxSvc();
	    taxSvc.Configuration.Url = Url;
	    taxSvc.Configuration.Security.Account = Account;
	    taxSvc.Configuration.Security.License = License;
	    return taxSvc;
	}

	private AddressSvc GetAddressSvc() {
	    AddressSvc taxSvc = new AddressSvc();
	    taxSvc.Configuration.Url = Url;
	    taxSvc.Configuration.Security.Account = Account;
	    taxSvc.Configuration.Security.License = License;
	    return taxSvc;
	}


	public TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    StringType taxTransactionId = "";
	    return Calculate(taxTransactionId, street, city, county, region, postalCode, country, date, order);
	}

	//TODO: need to figure out what to do with the location
	public TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order, StringType location) {
	    StringType taxTransactionId = "";
	    return Calculate(taxTransactionId, street, city, county, region, postalCode, country, date, order);
	}

	//TODO: need to figure out what to do with the location
	public TaxResult Calculate(StringType taxTransactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order, StringType location) {
	    return Calculate(taxTransactionId, street, city, county, region, postalCode, country, date, order);
	}

	public TaxResult Calculate(StringType taxTransactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    return Calculate(taxTransactionId, street, city, county, region, postalCode, country, date, order, DocumentType.SalesOrder);
	}

	public TaxResult Calculate(StringType taxTransactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order, DocumentType documentType) {
	    TaxSvc taxSvc = GetTaxSvc();
	    GetTaxRequest gTaxReq = new GetTaxRequest();
	    GetTaxResult gTaxRes;
	    Address documentOrigAddress = new Address();
	    Address documentDestAddress = new Address();
	    Line lItem;

	    TaxResult result = new TaxResult();

	    TaxAreaData data = Validate(street, city, region, postalCode, country);
	    result.AddressValidated = data.AddressValidated;
	    result.TaxAreaID = data.TaxAreaID;
	    result.Street = data.Street;
	    result.City = data.City;
	    result.Region = data.Region;
	    result.PostalCode = data.PostalCode;
	    result.Country = data.Country;

	    gTaxReq.CompanyCode = CompanyCode;

	    // Profile.client is required - pattern is ERPName,majver,minver[,connectorname,majver,minver]
	    Assembly assembly = Assembly.GetExecutingAssembly();
	    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
	    string version = fvi.ProductMajorPart + "," + fvi.ProductMinorPart;

	    Type t = MethodBase.GetCurrentMethod().DeclaringType;
	    taxSvc.Profile.Client = t.ToString() + "," + version;

	    // Default (header) Origin Address for GetTax
	    documentOrigAddress.Line1 = "10150 Centennial Parkway";
	    documentOrigAddress.Line2 = "Suite 210";
	    documentOrigAddress.City = "Sandy";
	    documentOrigAddress.Region = "UT";
	    documentOrigAddress.PostalCode = "84070";
	    documentOrigAddress.Country = "US";

	    // Default (header) Destination Address for GetTax
	    documentDestAddress.Line1 = street;
	    documentDestAddress.City = city;
	    documentDestAddress.Region = region;
	    documentDestAddress.PostalCode = postalCode;
	    documentDestAddress.Country = country;

	    // intended to be your internal cust #.  also cross for ECMS
	    gTaxReq.CustomerCode = order.CustomerId.ToString();

	    gTaxReq.DestinationAddress = documentDestAddress;
	    gTaxReq.OriginAddress = documentOrigAddress;

	    // detaillevel tax means give us all the jurisdictional breakout
	    // detaillevel line means give us just the tax amount for each line, but no juridistional info
	    // detaillevel document (default) means just give back the total tax for the whole invoice
	    gTaxReq.DetailLevel = DetailLevel.Tax;

	    // Salesorder is for quotes (no record saved to dashboard)
	    // SalesInvoice is for invoices (invoice is saved to dashboard to be posted later)
	    gTaxReq.DocType = documentType;
	    gTaxReq.DocDate = DateTime.Now;

	    gTaxReq.DocCode = GetDocCode(order.OrderId);

	    for (Int32 i = 0; i < order.Lines.Count; i++) {
		TaxOrderLine line = order.Lines[i];

		lItem = new Line();
		lItem.No = i.ToString();
		lItem.Qty = line.Quantity;
		lItem.Amount = (line.Price.ToDecimal() * line.Quantity.ToInt32());
		if (line.DiscountAmount.IsValid) {
		    lItem.Amount -= line.DiscountAmount.ToDecimal();
		}
		// ItemCode and description are required for all lines
		lItem.ItemCode = line.ItemNumber;
		//lItem.Description = "Description of ItemSkuA";

		gTaxReq.Lines.Add(lItem);
	    }

	    try {
		gTaxRes = taxSvc.GetTax(gTaxReq);
	    } catch (Exception ex) {
		MessageList errors = new MessageList();
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		Debug.WriteLine(ex.Message.ToString());
		throw new MessageListException(errors);
	    }

	    if (gTaxRes.ResultCode != SeverityLevel.Success) {
		MessageList errors = new MessageList();

		//Console.WriteLine("GetTax Returned Other Than Success:");
		foreach (Avalara.AvaTax.Adapter.Message msg in gTaxRes.Messages) {
		    String m = msg.Name + Environment.NewLine + msg.Summary + Environment.NewLine + msg.Details;
		    errors.Add(new TaxSoapException(m));
		    Debug.WriteLine(m);
		}

		throw new MessageListException(errors);
	    } else {
		result.TotalTax = gTaxRes.TotalTax;

		foreach (TaxLine taxline in gTaxRes.TaxLines) {
		    result.TotalTaxRate = 0;
		    foreach (TaxDetail taxdetail in taxline.TaxDetails) {
			result.TotalTaxRate += taxdetail.Rate * 100.0;
		    }
		}

		result.Street = street;
		result.City = city;
		result.Region = region;
		result.PostalCode = postalCode;

		Dictionary<String, TaxJurisdiction> jurisdictions = new Dictionary<String, TaxJurisdiction>();

		foreach (TaxLine taxline in gTaxRes.TaxLines) {
		    TaxResultLine line = new TaxResultLine();
		    line.TaxAmount = taxline.Tax;
		    result.Lines.Add(line);

		    foreach (TaxDetail taxdetail in taxline.TaxDetails) {
			String key = taxdetail.JurisType.ToString() + ":" + taxdetail.JurisName;
			if (!jurisdictions.ContainsKey(key)) {
			    TaxJurisdiction jurisdiction = new TaxJurisdiction();
			    jurisdiction.JurisdictionType = GetJurisdictionType(taxdetail.JurisType);
			    jurisdiction.Description = taxdetail.JurisName;
			    jurisdiction.Rate = taxdetail.Rate * 100.0;
			    jurisdiction.Amount = taxdetail.Tax;

			    jurisdictions.Add(key, jurisdiction);
			} else {
			    TaxJurisdiction jurisdiction = jurisdictions[key];
			    //jurisdiction.Rate += taxdetail.Rate;
			    jurisdiction.Amount += taxdetail.Tax;
			}
		    }

		}

		foreach (TaxJurisdiction jurisdiction in jurisdictions.Values) {
		    result.TaxJurisdictions.Add(jurisdiction);
		}
	    }

	    return result;
	}

	private TaxJurisdictionTypeEnum GetJurisdictionType(JurisdictionType jurisdictionType) {
	    if (jurisdictionType.Equals(JurisdictionType.Country)) {
		return TaxJurisdictionTypeEnum.COUNTRY;
	    } else if (jurisdictionType.Equals(JurisdictionType.State)) {
		return TaxJurisdictionTypeEnum.STATE;
	    } else if (jurisdictionType.Equals(JurisdictionType.County)) {
		return TaxJurisdictionTypeEnum.COUNTY;
	    } else if (jurisdictionType.Equals(JurisdictionType.City)) {
		return TaxJurisdictionTypeEnum.CITY;
	    } else if (jurisdictionType.Equals(JurisdictionType.Composite)) {
		return TaxJurisdictionTypeEnum.OTHER;
	    } else if (jurisdictionType.Equals(JurisdictionType.Special)) {
		return TaxJurisdictionTypeEnum.OTHER;
	    } else {
		return TaxJurisdictionTypeEnum.OTHER;
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

	private void AddTaxJurisidctionsToResult(TaxTransaction taxTrans, Order order, TaxResult result) {
	    if (taxTrans.CityTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.CITY] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.CITY, taxTrans.CityTaxAuthority, (taxTrans.CityTax / order.Total) * 100, 0);
	    }
	    if (taxTrans.CountyTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTY] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.COUNTY, taxTrans.CountyTaxAuthority, (taxTrans.CountyTax / order.Total) * 100, 0);
	    }
	    if (taxTrans.NationalTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTRY] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.COUNTRY, taxTrans.NationalTaxAuthority, (taxTrans.NationalTax / order.Total) * 100, 0);
	    }
	    if (taxTrans.StateTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.STATE] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.STATE, taxTrans.StateTaxAuthority, (taxTrans.StateTax / order.Total) * 100, 0);
	    }
	    if (taxTrans.OtherTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.OTHER] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.OTHER, taxTrans.OtherTaxAuthority, (taxTrans.OtherTax / order.Total) * 100, 0);
	    }
	    if (taxTrans.LocalTax > 0 && result.TaxJurisdictions[TaxJurisdictionTypeEnum.LOCAL] == null) {
		result.AddTaxJurisdiction(TaxJurisdictionTypeEnum.LOCAL, taxTrans.LocalTaxAuthority, (taxTrans.LocalTax / order.Total) * 100, 0);
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

	private String GetDocCode(IdType orderId) {
	    String s;
	    if (orderId.IsValid) {
		s = orderId.ToString();
	    } else {
		s = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Right(12);
	    }

	    Console.Out.WriteLine("DocCode: " + s);
	    return s;
	}

    }
}