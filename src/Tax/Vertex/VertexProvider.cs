using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml;
using log4net;
using Spring2.Core.Configuration;
using Spring2.Core.Message;
using Spring2.Core.Types;
using NotImplementedException = System.NotImplementedException;

namespace Spring2.Core.Tax.Vertex {

    /// <summary>
    /// Tax provider using Vertex O Series via Web Services.
    /// This class is must be stateless in order to be thread safe.
    /// </summary>
    public class VertexProvider : ITaxProvider {
	#region Private declarations
	private static Hashtable regionHash;
	private static Hashtable countryHash;
	private static StringType calcType = StringType.DEFAULT;

	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	private static readonly string userKey = "Vertex.User";
	private static readonly string passwordKey = "Vertex.PW";
	private static readonly string lookupUrlKey = "Vertex.LookupWSUrl";
	private static readonly string calcUrlKey = "Vertex.CalcWSUrl";
	private static readonly string companyKey = "Vertex.Company";
	private string profileKey = null;

	private string User {
	    get { 
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? userKey : string.Format("{0}.{1}", userKey, profileKey)];
		if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[userKey];
		}
		return key;
	    }
	}

	private string Password {
	    get { 
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? passwordKey : string.Format("{0}.{1}", passwordKey, profileKey)];
		if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[passwordKey];
		}
		return key;
	    }
	}

	private string LookupUrl {
	    get { 
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? lookupUrlKey : string.Format("{0}.{1}", lookupUrlKey, profileKey)];
		if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[lookupUrlKey];
		}
		return key;
	    }
	}

	private string CalcUrl {
	    get { 
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? calcUrlKey : string.Format("{0}.{1}", calcUrlKey, profileKey)];
		if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[calcUrlKey];
		}
		return key;
	    }
	}

	private string Company {
	    get { 
		string key = ConfigurationProvider.Instance.Settings[profileKey == null ? companyKey : string.Format("{0}.{1}", companyKey, profileKey)];
		if (key == null) {
		    key = ConfigurationProvider.Instance.Settings[companyKey];
		}
		return key;
	    }
	}
	#endregion	

	#region XML Strings
	// XML String for tax area lookup
	private String GetTaxAreaLookupXml(string asOfDate, string city, string mainDivision, string subDivision, string postalCode, string country) { 
	    StringBuilder xml = new StringBuilder();

	    xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
	    xml.Append("<VertexEnvelope xmlns=\"urn:vertexinc:o-series:tps:2:2\" ");
	    xml.Append(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
	    xml.Append("xsi:schemaLocation=\"VertexEnvelope_vertex.xsd\"> ");
	    xml.Append("<Login> ");
	    xml.Append("<UserName> ");
	    xml.Append(User);
	    xml.Append("</UserName> ");
	    xml.Append("<Password> ");
	    xml.Append(Password);
	    xml.Append("</Password> ");
	    xml.Append("</Login> ");
	    xml.Append("<TaxAreaRequest> ");
	    xml.AppendFormat("<TaxArea asOfDate=\"{0}\"> ", asOfDate);
	    xml.AppendFormat("<City>{0}</City> ", city);
	    xml.AppendFormat("<MainDivision>{0}</MainDivision> ", mainDivision);
	    xml.AppendFormat("<SubDivision>{0}</SubDivision> ", subDivision);
	    xml.AppendFormat("<PostalCode>{0}</PostalCode> ", postalCode);
	    xml.AppendFormat("<Country>{0}</Country> ", country);
	    xml.Append("</TaxArea> ");
	    xml.Append("</TaxAreaRequest> ");
	    xml.Append("</VertexEnvelope>");

	    return xml.ToString();
	}

	// XML String for rate lookup
	private String GetRateLookupXml(string taxAreaId, string taxDate) {
	    StringBuilder xml = new StringBuilder();

	    xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
	    xml.Append("<VertexEnvelope xmlns=\"urn:vertexinc:o-series:tps:2:2\" ");
	    xml.Append("     xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
	    xml.Append("xsi:schemaLocation=\"VertexEnvelope_vertex.xsd\"> ");
	    xml.Append("<Login> ");
	    xml.Append("<UserName> ");
	    xml.Append(User);
	    xml.Append("</UserName> ");
	    xml.Append("<Password> ");
	    xml.Append(Password);
	    xml.Append("</Password> ");
	    xml.Append("</Login> ");
	    xml.AppendFormat("<QuotationSaleRequest documentDate=\"{0}\"> ", taxDate);
	    xml.Append("    <Currency/> ");
	    xml.Append("    <Seller> ");
	    xml.AppendFormat("      <Company>{0}</Company> ", Company);
	    xml.Append("    </Seller> ");
	    xml.Append("<Customer> ");
	    xml.AppendFormat("     <Destination taxAreaId=\"{0}\"> ", taxAreaId);
	    xml.Append("</Destination> ");
	    xml.Append("</Customer> ");
	    xml.Append("<LineItem isMulticomponent=\"false\" lineItemId=\"1\" lineItemNumber=\"1\" ");
	    xml.AppendFormat("taxDate=\"{0}\"> ", taxDate);
	    xml.Append("<Quantity>1.0</Quantity> ");
	    xml.Append("<Freight>0</Freight> ");
	    xml.Append("<UnitPrice>1000</UnitPrice> ");
	    xml.Append("<ExtendedPrice>1000</ExtendedPrice> ");
	    xml.Append("</LineItem> ");
	    xml.Append("</QuotationSaleRequest> ");
	    xml.Append("</VertexEnvelope>");

	    return xml.ToString();
	}

	private String GetTaxAreaXml(string taxAreaId, string asOfDate) {
	    StringBuilder xml = new StringBuilder();
	    
	    xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
	    xml.Append("<VertexEnvelope xmlns=\"urn:vertexinc:o-series:tps:2:2\" ");
	    xml.Append("     xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
	    xml.Append("xsi:schemaLocation=\"VertexEnvelope_vertex.xsd\"> ");
	    xml.Append("<Login> ");
	    xml.Append("<TrustedId>su-trusted-id</TrustedId> ");
	    xml.Append("</Login> ");
	    xml.Append("<TaxAreaRequest> ");
	    xml.AppendFormat("<TaxArea taxAreaId=\"{0}\" asOfDate=\"{1}\"> ", taxAreaId, asOfDate);
	    xml.Append("</TaxArea> ");
	    xml.Append("</TaxAreaRequest> ");
	    xml.Append("</VertexEnvelope>");

	    return xml.ToString();
	}

	// XML String for order calculation
	private String GetExemptCalcXml(string taxAreaId, string documentDate, string documentNumber, string lines, string isTaxExempt, string classCode, string currency) {
	    StringBuilder xml = new StringBuilder();

	    xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
	    xml.Append("<VertexEnvelope xmlns=\"urn:vertexinc:o-series:tps:2:2\" ");
	    xml.Append("     xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
	    xml.Append("xsi:schemaLocation=\"VertexEnvelope_vertex.xsd\"> ");
	    xml.Append("<Login> ");
	    xml.Append("<UserName> ");
	    xml.Append(User);
	    xml.Append("</UserName> ");
	    xml.Append("<Password> ");
	    xml.Append(Password);
	    xml.Append("</Password> ");
	    xml.Append("</Login> ");
	    xml.AppendFormat("<QuotationSaleRequest documentDate=\"{0}\" documentNumber=\"{1}\"> ", documentDate, documentNumber);
	    xml.AppendFormat("     <Currency>{0}</Currency> ", currency);
	    xml.Append("    <Seller> ");
	    xml.Append("      <Company>");
	    xml.Append(Company);
	    xml.Append("</Company> ");
	    xml.Append("    </Seller> ");
	    xml.AppendFormat("    <Customer classCode=\"{0}\" isTaxExempt=\"{1}\"> ", classCode, isTaxExempt);
	    xml.AppendFormat("         <Destination taxAreaId=\"{0}\"> ", taxAreaId);
	    xml.Append("         </Destination> ");
	    xml.Append("    </Customer> ");
	    xml.Append(lines);
	    xml.Append("</QuotationSaleRequest> ");
	    xml.Append("</VertexEnvelope>");

	    return xml.ToString();
	}

	// XML String for order calculation
	private String GetCalcTaxXml(string taxAreaId, string documentDate, string documentNumber, string lines, string currency) {
	    StringBuilder xml = new StringBuilder();

	    xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
	    xml.Append("<VertexEnvelope xmlns=\"urn:vertexinc:o-series:tps:2:2\" ");
	    xml.Append("     xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
	    xml.Append("xsi:schemaLocation=\"VertexEnvelope_vertex.xsd\"> ");
	    xml.Append("<Login> ");
	    xml.Append("<UserName> ");
	    xml.Append(User);
	    xml.Append("</UserName> ");
	    xml.Append("<Password> ");
	    xml.Append(Password);
	    xml.Append("</Password> ");
	    xml.Append("</Login> ");
	    xml.AppendFormat("<QuotationSaleRequest documentDate=\"{0}\" documentNumber=\"{1}\"> ", documentDate, documentNumber);
	    xml.AppendFormat("     <Currency>{0}</Currency> ", currency);
	    xml.Append("    <Seller> ");
	    xml.Append("      <Company>");
	    xml.Append(Company);
	    xml.Append("</Company> ");
	    xml.Append("    </Seller> ");
	    xml.Append("    <Customer> ");
	    xml.AppendFormat("         <Destination taxAreaId=\"{0}\"> ", taxAreaId);
	    xml.Append("         </Destination> ");
	    xml.Append("    </Customer> ");
	    xml.Append(lines);
	    xml.Append("</QuotationSaleRequest> ");
	    xml.Append("</VertexEnvelope>");

	    return xml.ToString();
	}

	// XML String for Tax Exempt journal write
	private String GetExemptJournalXml(string taxAreaId, string documentDate, string documentNumber, string lines, string isTaxExempt, string classCode, string currency) {
	    StringBuilder xml = new StringBuilder();

	    xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
	    xml.Append("<VertexEnvelope xmlns=\"urn:vertexinc:o-series:tps:2:2\" ");
	    xml.Append("     xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
	    xml.Append("xsi:schemaLocation=\"VertexEnvelope_vertex.xsd\"> ");
	    xml.Append("<Login> ");
	    xml.Append("<UserName> ");
	    xml.Append(User);
	    xml.Append("</UserName> ");
	    xml.Append("<Password> ");
	    xml.Append(Password);
	    xml.Append("</Password> ");
	    xml.Append("</Login> ");
	    xml.AppendFormat("<InvoiceSaleRequest documentDate=\"{0}\" documentNumber=\"{1}\"> ", documentDate, documentNumber);
	    xml.AppendFormat("     <Currency>{0}</Currency> ", currency);
	    xml.Append("    <Seller> ");
	    xml.AppendFormat("      <Company>{0}</Company> ", Company);
	    xml.Append("    </Seller> ");
	    xml.AppendFormat("    <Customer classCode=\"{0}\" isTaxExempt=\"{1}\"> ", classCode, isTaxExempt);
	    xml.AppendFormat("         <Destination taxAreaId=\"{0}\"> ", taxAreaId);
	    xml.Append("         </Destination> ");
	    xml.Append("    </Customer> ");
	    xml.Append(lines);
	    xml.Append("</InvoiceSaleRequest> ");
	    xml.Append("</VertexEnvelope>");

	    return xml.ToString();
	}

	// XML String for journal write
	private String GetJournalXml(string taxAreaId, string documentDate, string documentNumber, string lines, string currency) {
	    StringBuilder xml = new StringBuilder();

	    xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
	    xml.Append("<VertexEnvelope xmlns=\"urn:vertexinc:o-series:tps:2:2\" ");
	    xml.Append("     xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
	    xml.Append("xsi:schemaLocation=\"VertexEnvelope_vertex.xsd\"> ");
	    xml.Append("<Login> ");
	    xml.Append("<UserName> ");
	    xml.Append(User);
	    xml.Append("</UserName> ");
	    xml.Append("<Password> ");
	    xml.Append(Password);
	    xml.Append("</Password> ");
	    xml.Append("</Login> ");
	    xml.AppendFormat("<InvoiceSaleRequest documentDate=\"{0}\" documentNumber=\"{1}\"> ", documentDate, documentNumber);
	    xml.AppendFormat("     <Currency>{0}</Currency> ", currency);
	    xml.Append("    <Seller> ");
	    xml.AppendFormat("      <Company>{0}</Company> ", Company);
	    xml.Append("    </Seller> ");
	    xml.Append("    <Customer> ");
	    xml.AppendFormat("         <Destination taxAreaId=\"{0}\"> ", taxAreaId);
	    xml.Append("         </Destination> ");
	    xml.Append("    </Customer> ");
	    xml.Append(lines);
	    xml.Append("</InvoiceSaleRequest> ");
	    xml.Append("</VertexEnvelope>");

	    return xml.ToString();
	}

	// XML String for journal write with Customer Order
	private String GetCustomerOrderJournalXml(string taxAreaId, string documentDate, string documentNumber, string lines, string company, string currency) {
	    StringBuilder xml = new StringBuilder();

		xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
		xml.Append("<VertexEnvelope xmlns=\"urn:vertexinc:o-series:tps:2:2\" ");
		xml.Append("     xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
		xml.Append("xsi:schemaLocation=\"VertexEnvelope_vertex.xsd\"> ");
		xml.Append("<Login> ");
		xml.Append("<UserName> ");
		xml.Append(User);
		xml.Append("</UserName> ");
		xml.Append("<Password> ");
		xml.Append(Password);
		xml.Append("</Password> ");
		xml.Append("</Login> ");
		xml.AppendFormat("<InvoiceSaleRequest documentDate=\"{0}\" documentNumber=\"{1}\"> ", documentDate, documentNumber);
		xml.AppendFormat("     <Currency>{0}</Currency> ", currency);
		xml.Append("    <Seller> ");
		xml.AppendFormat("      <Company>{0}</Company> ", Company);
		xml.Append("    </Seller> ");
		xml.Append("    <Customer> ");
		xml.AppendFormat("      <Company>{0}</Company> ", company);
		xml.AppendFormat("         <Destination taxAreaId=\"{0}\"> ", taxAreaId);
		xml.Append("         </Destination> ");
		xml.Append("    </Customer> ");
		xml.Append(lines);
		xml.Append("</InvoiceSaleRequest> ");
		xml.Append("</VertexEnvelope>");

	    return xml.ToString();

	}

	// XML String for a single line item
	private String GetLineItemXml(string taxDate, string quantity, string unitPrice, string extendedPrice, string lineItemId, string productClass, string discountAmount) {
	    StringBuilder xml = new StringBuilder();

	    xml.AppendFormat("<LineItem lineItemId=\"{0}\" taxDate=\"{1}\"> ", lineItemId, taxDate);
	    xml.AppendFormat("    <Product productClass=\"\">{0}</Product> ", productClass);
	    xml.AppendFormat("    <Quantity>{0}</Quantity> ", quantity);
	    xml.AppendFormat("    <UnitPrice>{0}</UnitPrice> ", unitPrice);
	    xml.AppendFormat("    <ExtendedPrice>{0}</ExtendedPrice> ", extendedPrice);
	    xml.AppendFormat("    <Discount DiscountAmount=\"{0}\"> ", discountAmount);
	    xml.Append("    </Discount> ");
	    xml.Append("</LineItem> ");

	    return xml.ToString();
	}

	// XML String for a single shipping line item
	private String GetShippingLineItemXml(string taxDate, string price, string lineItemId) {
	    StringBuilder xml = new StringBuilder();

	    xml.AppendFormat("<LineItem lineItemId=\"{0}\" taxDate=\"{1}\"> ", lineItemId, taxDate);
	    xml.Append("    <Product productClass=\"\">SHIPPING</Product> ");
	    xml.Append("    <Quantity>1</Quantity> ");
	    xml.AppendFormat("    <UnitPrice>{0}</UnitPrice> ", price);
	    xml.AppendFormat("    <ExtendedPrice>{0}</ExtendedPrice> ", price);
	    xml.Append("</LineItem> ");

	    return xml.ToString();
	}

	#endregion

	#region Public methods that call Vertex
	public VertexProvider(StringType profileKey) {
	    if (profileKey.IsValid) {
		this.profileKey = profileKey.ToString();
	    }
	}

	public TaxAreaList LookupTaxArea(StringType street, StringType city, StringType county, StringType region, StringType postalCode,
	     StringType country, DateType date, BooleanType replaceHTMLEntities) {
		 StringType location = StringType.DEFAULT;
		 return LookupTaxArea(street, city, county, region, postalCode, country, date, replaceHTMLEntities, location);
	}

	/// <summary>
	/// Look up tax area using the passed-in parameters.
	/// </summary>
	/// <param name="country">country</param>
	/// <param name="postalCode">postal code</param>
	/// <param name="city">city</param>
	/// <param name="region">state/provice</param>
	/// <param name="county">county</param>
	/// <param name="date">date to get tax areas as of</param>
	/// <param name="replaceHTMLEntities">should HTML entities be replaced with literals?</param>
	/// <returns>list of matching tax areas for the passed-in parameters</returns>
	public TaxAreaList LookupTaxArea(StringType street, StringType city, StringType county, StringType region, StringType postalCode, 
	     StringType country, DateType date, BooleanType replaceHTMLEntities, StringType location) {
	    if (regionHash == null) {
		PopulateRegionHash();
	    }
	    if (countryHash == null) {
		PopulateCountryHash();
	    }

	    String dateString = date.ToDateTime().ToString("yyyy-MM-dd");
	    LookupTaxAreasWSService lta = new LookupTaxAreasWSService();

	    String url = LookupUrl;
	    if (url != null) {
		lta.Url = url;
	    }

	    try {
		String xmlToSend = GetTaxAreaLookupXml(dateString, city.ToString().Trim(), region.ToString(),
		    county.ToString().Trim(), postalCode.ToString().Trim(), country.ToString());
		String xmlResponse;

		xmlResponse = lta.lookupTaxAreasString(xmlToSend);


		TaxAreaList areas = ParseTaxAreaResponse(xmlResponse, country, county, city, replaceHTMLEntities);

		// if user entered a 9-digit zip code, and we got a single match with no city, try
		// again with the 5 digit zip code.
		if (areas.Count == 1 && country.Equals("United States") && areas[0].City.IsEmpty && postalCode.Length == 9) {
		    xmlToSend = GetTaxAreaLookupXml(dateString, city.ToString().Trim(), region.ToString(),
			county.ToString().Trim(), postalCode.ToString().Substring(0, 5), country.ToString().Trim());

		    xmlResponse = lta.lookupTaxAreasString(xmlToSend);

		    areas = ParseTaxAreaResponse(xmlResponse, country, county, city, replaceHTMLEntities);
		}

		if (!postalCode.IsEmpty) {
		    foreach (TaxAreaData area in areas) {
			area.PostalCode = postalCode;
		    }
		}

		return areas;
	   } catch (SoapException ex) {
		MessageList errors = new MessageList();
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		throw new MessageListException(errors);
	    }
	}

    	public TaxResult Commit(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order)
    	{
    		throw new NotImplementedException();
    	}

    	public TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order)
    	{
    		throw new NotImplementedException();
    	}

    	/// <summary>
	/// Get the tax rates for the passed-in tax area ID and date.
	/// </summary>
	/// <param name="taxAreaID">tax area ID</param>
	/// <param name="date">date</param>
	/// <returns>tax rate info</returns>
	public TaxRateInfo GetTaxRateForArea(IdType taxAreaID, DateType date) {
	    if (regionHash == null) {
		PopulateRegionHash();
	    }
	    if (countryHash == null) {
		PopulateCountryHash();
	    }

	    String dateString = date.ToDateTime().ToString("yyyy-MM-dd");
	    CalculateTaxWSService ct = new CalculateTaxWSService();

	    String url = CalcUrl;
	    if (url != null) {
		ct.Url = url;
	    }


	    String xmlToSend = GetRateLookupXml(taxAreaID.ToString(), dateString);

	    String xmlResponse = ct.calculateTaxString(xmlToSend);

	    return ParseQuoteResponse(xmlResponse);
	}

	/// <summary>
	/// Get a tax area based on the tax area ID previously returned from Vertex.
	/// </summary>
	/// <param name="taxAreaID">tax area ID from Vertex</param>
	/// <param name="date">as of date</param>
	/// <param name="replaceHTMLEntities">should HTML entities be replaced with literals?</param>
	/// <returns>matching tax area, or null if not found</returns>
	public TaxAreaData GetTaxAreaByID(IdType taxAreaID, DateType date, BooleanType replaceHTMLEntities) {
	    if (regionHash == null) {
		PopulateRegionHash();
	    }
	    if (countryHash == null) {
		PopulateCountryHash();
	    }

	    String dateString = date.ToDateTime().ToString("yyyy-MM-dd");
	    LookupTaxAreasWSService lta = new LookupTaxAreasWSService();

	    String url = LookupUrl;
	    if (url != null) {
		lta.Url = url;
	    }

	    try {
		String xmlToSend = GetTaxAreaXml(taxAreaID.ToString(), dateString);

		String xmlResponse = lta.lookupTaxAreasString(xmlToSend);

		TaxAreaList areas = ParseTaxAreaResponse(xmlResponse, StringType.EMPTY, StringType.EMPTY, StringType.EMPTY, replaceHTMLEntities);

		if (areas.Count == 1) {
		    return (TaxAreaData) areas[0];
		} else {
		    return null;
		}
	    } catch (SoapException ex) {
		MessageList errors = new MessageList();
		//errors.Add(new TaxAddressNotFoundException());
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		throw new MessageListException(errors);
	    }
	}

	/// <summary>
	/// Calculate the sales tax for the passed in order.
	/// </summary>
	/// <param name="order">order to calculate tax for</param>
	public TaxResult GetQuoteTaxTotal(TaxOrder order, StringType currencyCode) {
	    if (order.MerchandiseTotal + order.ShipTotal <= CurrencyType.ZERO) {
		return new TaxResult();
	    } else {
		String dateString = order.CompleteDate.ToDateTime().ToString("yyyy-MM-dd");
		CalculateTaxWSService ct = new CalculateTaxWSService();

		String url = CalcUrl;
		if (url != null) {
		    ct.Url = url;
		}

		String xmlToSend = "";
		StringBuilder xmlLines = GetXmlLines(order);


		if (order.OrderType.Equals("Fundraiser") || order.OrderType.Equals("Retail") || order.OrderType.Equals("International") || order.OrderType.Equals("Shopping Cart")) {
		    if (order.IsTaxExempt.IsTrue) {
			xmlToSend = GetExemptCalcXml(order.TaxAreaId.ToString(),
			    dateString, order.OrderId.ToString(), xmlLines.ToString(), "true", order.OrderType.ToString(), currencyCode);
		    } else {
			xmlToSend = GetCalcTaxXml(order.TaxAreaId.ToString(),
			    dateString, order.OrderId.ToString(), xmlLines.ToString(), currencyCode);
		    }
		} else {
		    xmlToSend = GetCalcTaxXml(order.TaxAreaId.ToString(),
			dateString, order.OrderId.ToString(), xmlLines.ToString(), currencyCode);
		}

		String xmlResponse;
		try {
		    xmlResponse = ct.calculateTaxString(xmlToSend);
		} catch (SoapException ex) {
		    log.Error("Tax amount was not determined with calcType for an QUOTE Tax Total.  ERROR: " + ex.Message.ToString());
		    MessageList errors = new MessageList();
		    errors.Add(new TaxSoapException(ex.Message.ToString()));
		    throw new MessageListException(errors);
		}

		TaxResult result = null;
		result = GetTaxResultForQuoteResponse(xmlResponse);

		return result;
	    }
	}


	public StringBuilder GetXmlLines(TaxOrder order) {
	    String dateString = order.CompleteDate.ToDateTime().ToString("yyyy-MM-dd");

	    StringBuilder xmlLines = new StringBuilder(5000);
	    String xmlLine;
	    DecimalType discAmt = order.DiscountRate;

	    int lineCount = 0;
	    foreach (TaxOrderLine dtl in order.Lines) {
		if (dtl.Quantity > 0) {
		    xmlLine = GetLineItemXml(dateString, dtl.Quantity.ToString(),
			dtl.Price.ToString("F"), dtl.ExtendedPrice.ToString("F"),
			dtl.OrderLineId.ToString(), dtl.ItemNumber.ToString(), "0");
		    xmlLines.Append(xmlLine);
		    lineCount++;
		}
	    }

	    if (order.ShipTotal > 0) {
		xmlLine = GetShippingLineItemXml(dateString, order.ShipTotal.ToString("F"),
		    Int32.MaxValue.ToString());
		xmlLines.Append(xmlLine);
	    } else {
		if (order.Lines.Count == 0 || lineCount == 0) {
		    xmlLine = GetShippingLineItemXml(dateString, ".01",
			Int32.MaxValue.ToString());
		    xmlLines.Append(xmlLine);
		}
	    }
	    return xmlLines;
	}

	/// <summary>
	/// Calculate the sales tax for the passed in order.
	/// </summary>
	/// <param name="order">order to calculate tax for</param>
	/// <param name="writeToJournal">Should this be an invoice call that writes to the tax journal?</param>
	public TaxResult Commit(TaxOrder order, StringType currencyCode) {
	    String dateString = order.CompleteDate.ToDateTime().ToString("yyyy-MM-dd");

	    CalculateTaxWSService ct = new CalculateTaxWSService();

	    String url = CalcUrl;
	    if (url != null) {
		ct.Url = url;
	    }

	    String xmlToSend = "";
	    StringBuilder xmlLines = GetXmlLines(order);


	    if (order.OrderType.Equals("Customer") || order.OrderType.Equals("Shopping Cart")) {
		xmlToSend = GetCustomerOrderJournalXml(order.TaxAreaId.ToString(),
		    dateString, order.OrderId.ToString(), xmlLines.ToString(), order.CustomerId.ToString(), currencyCode);
	    } else {
		if (order.IsTaxExempt.IsTrue) {
		    xmlToSend = GetExemptJournalXml(order.TaxAreaId.ToString(),
			dateString, order.OrderId.ToString(), xmlLines.ToString(), "true", order.OrderType.ToString(),
			currencyCode);
		} else {
		    xmlToSend = GetJournalXml(order.TaxAreaId.ToString(),
			dateString, order.OrderId.ToString(), xmlLines.ToString(), currencyCode);
		}
	    }

	    String xmlResponse;
	    try {
		xmlResponse = ct.calculateTaxString(xmlToSend);
	    } catch (SoapException ex) {
		log.Error("Tax amount was not determined with calcType for an INVOICE Tax Total.  ERROR: " + ex.Message.ToString());
		MessageList errors = new MessageList();
		errors.Add(new TaxSoapException(ex.Message.ToString()));
		throw new MessageListException(errors);
	    }

	    TaxResult result = null;
	    result = GetTaxResultForInvoiceResponse(xmlResponse);

	    return result;
	}

	public TaxAreaData GetTaxAreaForAddress(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, BooleanType outsideCityLimits) {
	    StringType location = StringType.DEFAULT;
	    return GetTaxAreaForAddress(street, city, county, region, postalCode, country, outsideCityLimits, location);
	}

	public TaxAreaData GetTaxAreaForAddress(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, BooleanType outsideCityLimits, StringType location) {
	    //need to figure out why country is sometimes invalid
	    if (!country.IsValid) {
		country = "United States";
	    }
	    TaxAreaList list = new TaxAreaList();
	    DateType date = DateType.Now;
	    if (!postalCode.IsEmpty && !region.IsEmpty && !city.IsEmpty && !country.IsEmpty) {
		list = LookupTaxArea(street, city, county, region, postalCode, country, date, BooleanType.TRUE);

		if (list.Count == 1) {
		    return list[0];
		} else {
		    foreach (TaxAreaData data in list) {
			if (!data.TaxAreaID.IsDefault) {
			    if (outsideCityLimits.IsTrue) {
				if (!data.County.IsDefault) {
				    return data;
				}
			    } else {
				if (!data.City.IsDefault) {
				    if (data.City.ToUpper().Trim() == city.ToUpper().Trim()) {
					return data;
				    }
				}
			    }
			}
		    }
		}
	    }
	    throw new TaxAddressNotFoundException();
	}

	public TaxAreaList GetTaxAreaListForAddress(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, BooleanType outsideCityLimits) {
	    if (!country.IsValid) {
		country = "United States";
	    }
	    TaxAreaList list = new TaxAreaList();
	    DateType date = DateType.Now;
	    if (!postalCode.IsEmpty && !region.IsEmpty && !city.IsEmpty && !country.IsEmpty) {
		list = LookupTaxArea(street, city, county, region, postalCode, country, date, BooleanType.TRUE);

		return list;
	    }
	    throw new TaxAddressNotFoundException();
	}

	#endregion

	#region Private methods to parse result strings

	/// <summary>
	/// 
	/// </summary>
	/// <param name="xml"></param>
	/// <param name="country"></param>
	/// <param name="county"></param>
	/// <param name="city"></param>
	/// <param name="replaceHTMLEntities"></param>
	/// <returns></returns>
	private TaxAreaList ParseTaxAreaResponse(String xml, StringType country, StringType county, StringType city, BooleanType replaceHTMLEntities) {
	    TaxAreaList taxAreas = new TaxAreaList();

	    XmlDocument doc = new XmlDocument();
	    doc.LoadXml(xml);

	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("TaxArea");

	    foreach (XmlNode node in elements) {
		TaxAreaData t = new TaxAreaData();
		t.TaxAreaID = IdType.Parse(node.Attributes["taxAreaId"].Value);

		foreach (XmlNode jur in node.ChildNodes) {
		    if (jur.Attributes["jurisdictionLevel"] != null) {
			if (jur.Attributes["jurisdictionLevel"].Value == "STATE"
			    || jur.Attributes["jurisdictionLevel"].Value == "PROVINCE"
			    || jur.Attributes["jurisdictionLevel"].Value == "TERRITORY") {
			    t.Region = regionHash[jur.InnerText].ToString();
			} else if (jur.Attributes["jurisdictionLevel"].Value == "COUNTRY") {
			    t.Country = countryHash[jur.InnerText].ToString();
			} else if (jur.Attributes["jurisdictionLevel"].Value == "COUNTY"
			    || jur.Attributes["jurisdictionLevel"].Value == "PARISH") {
			    t.County = StringType.Parse(jur.InnerText);
			    t.TaxArea = StringType.Parse(jur.InnerText + " COUNTY");
			} else if (jur.Attributes["jurisdictionLevel"].Value == "CITY"
			    || jur.Attributes["jurisdictionLevel"].Value == "BOROUGH"
			    || jur.Attributes["jurisdictionLevel"].Value == "TOWNSHIP"
			    || jur.Attributes["jurisdictionLevel"].Value == "APO"
			    || jur.Attributes["jurisdictionLevel"].Value == "FPO") {
			    t.City = StringType.Parse(jur.InnerText);
			    t.TaxArea = StringType.Parse(jur.InnerText);
			} else if (jur.Attributes["jurisdictionLevel"].Value == "DISTRICT"
			    || jur.Attributes["jurisdictionLevel"].Value == "TRANSIT_DISTRICT"
			    || jur.Attributes["jurisdictionLevel"].Value == "SPECIAL_PURPOSE_DISTRICT"
			    || jur.Attributes["jurisdictionLevel"].Value == "LOCAL_IMPROVEMENT_DISTRICT") {
			    String dist = jur.InnerText;

			    if (replaceHTMLEntities.IsTrue) {
				dist = dist.Replace("&amp;", "&");
			    }

//			    if (t.LocalDistrict1.IsEmpty) {
//				t.LocalDistrict1 = StringType.Parse(dist);
//			    } else if (t.LocalDistrict2.IsEmpty) {
//				t.LocalDistrict2 = StringType.Parse(dist);
//			    } else if (t.LocalDistrict3.IsEmpty) {
//				t.LocalDistrict3 = StringType.Parse(dist);
//			    }
			}
		    }
		}


		taxAreas.Add(t);

	    }
	    return taxAreas;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="xml"></param>
	/// <returns></returns>
	private TaxRateInfo ParseQuoteResponse(String xml) {
		TaxRateInfo tri = new TaxRateInfo();
	    Decimal totalRate = 0.0M;

	    XmlDocument doc = new XmlDocument();
	    doc.LoadXml(xml);
	    String rate, jur;
	    XmlNodeList elements = doc.GetElementsByTagName("Taxes");
	    foreach (XmlNode node in elements) {
		rate = "";
		jur = "";

		foreach (XmlNode child in node.ChildNodes) {
		    if (child.Name == "EffectiveRate") {
			rate = child.InnerText;
			totalRate += Decimal.Parse(rate);
		    }
		    if (child.Name == "Jurisdiction") {
			jur = child.Attributes["jurisdictionLevel"].Value;
		    }
		}
		if (jur == "COUNTRY") {
		    tri.CountryTaxRate = DecimalType.Parse(rate);
		} else if (jur == "COUNTY" || jur == "PARISH") {
		    tri.CountyTaxRate = DecimalType.Parse(rate);
		} else if (jur == "CITY" || jur == "TOWNSHIP" || jur == "BOROUGH" || jur == "APO" || jur == "FPO") {
		    tri.CityTaxRate = DecimalType.Parse(rate);
		} else if (jur == "STATE" || jur == "PROVINCE" || jur == "TERRITORY") {
		    tri.RegionTaxRate = DecimalType.Parse(rate);
		} else if (jur == "DISTRICT" || jur == "TRANSIT_DISTRICT"
		    || jur == "SPECIAL_PURPOSE_DISTRICT" || jur == "LOCAL_IMPROVEMENT_DISTRICT") {
		    if (!tri.LocalDistrict1TaxRate.IsValid) {
			tri.LocalDistrict1TaxRate = DecimalType.Parse(rate);
		    } else if (!tri.LocalDistrict2TaxRate.IsValid) {
			tri.LocalDistrict2TaxRate = DecimalType.Parse(rate);
		    } else if (!tri.LocalDistrict3TaxRate.IsValid) {
			tri.LocalDistrict3TaxRate = DecimalType.Parse(rate);
		    }
		}
	    }

	    tri.TotalTaxRate = new DecimalType(totalRate*100);

	    return tri;
	}

	private CurrencyType ParseInvoiceTaxTotalResponse(String xml) {
	    XmlDocument doc = new XmlDocument();
	    doc.LoadXml(xml);

	    XmlNodeList elements = null;

	    elements = doc.GetElementsByTagName("InvoiceSaleResponse");

	    CurrencyType detailTaxAmount = CurrencyType.DEFAULT;
	    foreach (XmlNode node in elements) {
		foreach (XmlNode child in node.ChildNodes) {
		    if (child.Name == "TotalTax") {
			detailTaxAmount = Decimal.Parse(child.InnerText);
		    }
		}
	    }
	    if (!detailTaxAmount.IsValid) {
		log.Error("Tax amount was not determined with calcType for an INVOICE Tax Total: " + xml);
	    }
	    return detailTaxAmount;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="xml"></param>
	private CurrencyType ParseQuoteTaxTotalResponse(String xml) {
	    XmlDocument doc = new XmlDocument();
	    doc.LoadXml(xml);

	    XmlNodeList elements = null;

	    elements = doc.GetElementsByTagName("QuotationSaleResponse");


	    CurrencyType detailTaxAmount = CurrencyType.DEFAULT;
	    foreach (XmlNode node in elements) {
		foreach (XmlNode child in node.ChildNodes) {
		    if (child.Name == "TotalTax") {
			detailTaxAmount = Decimal.Parse(child.InnerText);
		    }
		}
	    }
	    if (!detailTaxAmount.IsValid) {
		log.Error("Tax amount was not determined for a QUOTE Tax Total: " + xml);
	    }
	    return detailTaxAmount;
	}

	/// <summary>
	/// Parses an InvoiceSaleResponse into a TaxResult object.
	/// </summary>
	/// <param name="xml"></param>
	/// <returns></returns>
	private TaxResult GetTaxResultForInvoiceResponse(String xml) {
	    XmlDocument doc = new XmlDocument();
	    doc.LoadXml(xml);

	    XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
	    manager.AddNamespace("ns", "urn:vertexinc:o-series:tps:2:2");
	    manager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

	    XmlNodeList elements = doc.GetElementsByTagName("InvoiceSaleResponse");
	    XmlNodeList lines = doc.GetElementsByTagName("LineItem");

	    TaxResult result = new TaxResult();
	    CurrencyType totalTax = CurrencyType.ZERO;


	    foreach (XmlNode node in elements) {
		foreach (XmlNode child in node.ChildNodes) {
		    if (child.Name == "TotalTax") {
			totalTax = Decimal.Parse(child.InnerText);
		    }
		}
	    }

	    result.TotalTax = totalTax;

	    foreach (XmlNode child in lines) {
		TaxResultLine line = new TaxResultLine();
		XmlAttribute atrLineId = child.Attributes["lineItemId"];
		line.OrderLineId = IdType.Parse(atrLineId.InnerText);

		if (child.SelectSingleNode("ns:Quantity", manager) == null) {
		    line.Quantity = IntegerType.ZERO;
		} else {
		    IntegerType qty = IntegerType.Parse(child.SelectSingleNode("ns:Quantity", manager).InnerText);
		    line.Quantity = qty;
		}

		if (child.SelectSingleNode("ns:UnitPrice", manager) == null) {
		    line.Price = CurrencyType.ZERO;
		} else {
		    CurrencyType price = CurrencyType.Parse(child.SelectSingleNode("ns:UnitPrice", manager).InnerText);
		    line.Price = price;
		}

		if (child.SelectSingleNode("ns:ExtendedPrice", manager) == null) {
		    line.ExtendedPrice = CurrencyType.ZERO;
		} else {
		    CurrencyType extPrice = CurrencyType.Parse(child.SelectSingleNode("ns:ExtendedPrice", manager).InnerText);
		    line.ExtendedPrice = extPrice;
		}

		if (child.SelectSingleNode("ns:TotalTax", manager) == null) {
		    line.TaxAmount = CurrencyType.ZERO;
		} else {
		    CurrencyType taxAmt = CurrencyType.Parse(child.SelectSingleNode("ns:TotalTax", manager).InnerText);
		    line.TaxAmount = taxAmt;
		}

		result.Lines.Add(line);
	    }

	    return result;
	}

	/// <summary>
	/// Parses a QuotationSaleResponse into a TaxResult object.
	/// </summary>
	/// <param name="xml"></param>
	/// <returns></returns>
	private TaxResult GetTaxResultForQuoteResponse(String xml) {
	    XmlDocument doc = new XmlDocument();
	    doc.LoadXml(xml);
	    XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
	    manager.AddNamespace("ns", "urn:vertexinc:o-series:tps:2:2");

	    XmlNodeList elements = null;
	    XmlNodeList lines = null;

	    elements = doc.GetElementsByTagName("QuotationSaleResponse");
	    lines = doc.GetElementsByTagName("LineItem");

	    TaxResult result = new TaxResult();
	    CurrencyType totalTax = CurrencyType.DEFAULT;

	    foreach (XmlNode node in elements) {
		foreach (XmlNode child in node.ChildNodes) {
		    if (child.Name == "TotalTax") {
			totalTax = Decimal.Parse(child.InnerText);
		    }
		}
	    }

	    result.TotalTax = totalTax;

	    foreach (XmlNode child in lines) {
		TaxResultLine line = new TaxResultLine();
		XmlAttribute atrLineId = child.Attributes["lineItemId"];
		line.OrderLineId = IdType.Parse(atrLineId.InnerText);

		if (child.SelectSingleNode("ns:Quantity", manager) == null) {
		    line.Quantity = IntegerType.ZERO;
		} else {
		    IntegerType qty = IntegerType.Parse(child.SelectSingleNode("ns:Quantity", manager).InnerText);
		    line.Quantity = qty;
		}

		if (child.SelectSingleNode("ns:UnitPrice", manager) == null) {
		    line.Price = CurrencyType.ZERO;
		} else {
		    CurrencyType price = CurrencyType.Parse(child.SelectSingleNode("ns:UnitPrice", manager).InnerText);
		    line.Price = price;
		}

		if (child.SelectSingleNode("ns:ExtendedPrice", manager) == null) {
		    line.ExtendedPrice = CurrencyType.ZERO;
		} else {
		    CurrencyType extPrice = CurrencyType.Parse(child.SelectSingleNode("ns:ExtendedPrice", manager).InnerText);
		    line.ExtendedPrice = extPrice;
		}

		if (child.SelectSingleNode("ns:TotalTax", manager) == null) {
		    line.TaxAmount = CurrencyType.ZERO;
		} else {
		    CurrencyType taxAmt = CurrencyType.Parse(child.SelectSingleNode("ns:TotalTax", manager).InnerText);
		    line.TaxAmount = taxAmt;
		}

		result.Lines.Add(line);
	    }

	    return result;
	}

	#endregion

	#region Setup Hash tables

	private static void PopulateRegionHash() {
	    regionHash = new Hashtable();

	    regionHash.Add("ALABAMA", "AL");
	    regionHash.Add("ALASKA", "AK");
	    regionHash.Add("ALBERTA", "AB");
	    regionHash.Add("AMERICAN SAMOA", "AS");
	    regionHash.Add("ARIZONA", "AZ");
	    regionHash.Add("ARKANSAS", "AR");
	    regionHash.Add("BRITISH COLUMBIA", "BC");
	    regionHash.Add("CALIFORNIA", "CA");
	    regionHash.Add("COLORADO", "CO");
	    regionHash.Add("CONNECTICUT", "CT");
	    regionHash.Add("DELAWARE", "DE");
	    regionHash.Add("DISTRICT OF COLUMBIA", "DC");
	    regionHash.Add("F.S. OF MICRONESIA", "FM");
	    regionHash.Add("FLORIDA", "FL");
	    regionHash.Add("GEORGIA", "GA");
	    regionHash.Add("GUAM", "GU");
	    regionHash.Add("HAWAII", "HI");
	    regionHash.Add("IDAHO", "ID");
	    regionHash.Add("ILLINOIS", "IL");
	    regionHash.Add("INDIANA", "IN");
	    regionHash.Add("IOWA", "IA");
	    regionHash.Add("KANSAS", "KS");
	    regionHash.Add("KENTUCKY", "KY");
	    regionHash.Add("LOUISIANA", "LA");
	    regionHash.Add("MAINE", "ME");
	    regionHash.Add("MANITOBA", "MB");
	    regionHash.Add("MARSHALL ISLANDS", "MH");
	    regionHash.Add("MARYLAND", "MD");
	    regionHash.Add("MASSACHUSETTS", "MA");
	    regionHash.Add("MICHIGAN", "MI");
	    regionHash.Add("MINNESOTA", "MN");
	    regionHash.Add("MISSISSIPPI", "MS");
	    regionHash.Add("MISSOURI", "MO");
	    regionHash.Add("MONTANA", "MT");
	    regionHash.Add("N. MARIANA ISLANDS", "MP");
	    regionHash.Add("NEBRASKA", "NE");
	    regionHash.Add("NEVADA", "NV");
	    regionHash.Add("NEW BRUNSWICK", "NB");
	    regionHash.Add("NEW HAMPSHIRE", "NH");
	    regionHash.Add("NEW JERSEY", "NJ");
	    regionHash.Add("NEW MEXICO", "NM");
	    regionHash.Add("NEW YORK", "NY");
	    regionHash.Add("NEWFOUNDLAND", "NF");
	    regionHash.Add("NEWFOUNDLAND AND LABRADOR", "NF");
	    regionHash.Add("NORTH CAROLINA", "NC");
	    regionHash.Add("NORTH DAKOTA", "ND");
	    regionHash.Add("NORTHWEST TERRITORIES", "NT");
	    regionHash.Add("NOVA SCOTIA", "NS");
	    regionHash.Add("NUNAVUT", "NU");
	    regionHash.Add("OHIO", "OH");
	    regionHash.Add("OKLAHOMA", "OK");
	    regionHash.Add("ONTARIO", "ON");
	    regionHash.Add("OREGON", "OR");
	    regionHash.Add("PALAU", "PW");
	    regionHash.Add("PENNSYLVANIA", "PA");
	    regionHash.Add("PRINCE EDWARD ISLAND", "PE");
	    regionHash.Add("PUERTO RICO", "PR");
	    regionHash.Add("QUEBEC", "QC");
	    regionHash.Add("RHODE ISLAND", "RI");
	    regionHash.Add("SASKATCHEWAN", "SK");
	    regionHash.Add("SOUTH CAROLINA", "SC");
	    regionHash.Add("SOUTH DAKOTA", "SD");
	    regionHash.Add("TENNESSEE", "TN");
	    regionHash.Add("TEXAS", "TX");
	    regionHash.Add("UTAH", "UT");
	    regionHash.Add("VERMONT", "VT");
	    regionHash.Add("VIRGIN ISLANDS", "VI");
	    regionHash.Add("VIRGINIA", "VA");
	    regionHash.Add("WASHINGTON", "WA");
	    regionHash.Add("WEST VIRGINIA", "WV");
	    regionHash.Add("WISCONSIN", "WI");
	    regionHash.Add("WYOMING", "WY");
	    regionHash.Add("YUKON TERRITORY", "YT");
	}

	private static void PopulateCountryHash() {
	    countryHash = new Hashtable();

	    countryHash.Add("UNITED STATES", "US");
	    countryHash.Add("CANADA", "CA");
	}

	#endregion

	public TaxResult Calculate(StringType transactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    StringType location = StringType.DEFAULT;
	    return Calculate(transactionId, street, city, county, region, postalCode, country, date, order, location);
	}

	public TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order, StringType location) {
	    throw new NotImplementedException();
	}

	public TaxResult Calculate(StringType transactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order, StringType location) {
	    throw new NotImplementedException();
	}

    }
}