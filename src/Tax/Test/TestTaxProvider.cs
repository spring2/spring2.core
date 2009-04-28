using System;
using Spring2.Core.Types;
using Spring2.Core.Tax.Vertex;
using NotImplementedException = System.NotImplementedException;
using Spring2.Core.Message;

namespace Spring2.Core.Tax.Test {
    /// <summary>
    /// Summary description for TestTaxProvider.
    /// </summary>
    public class TestTaxProvider : ITaxProvider {

	public TestTaxProvider(StringType profileKey) {
	}

	public TaxRateInfo GetTaxRateForArea(IdType id, DateType dateType) {
		if(!id.IsValid)
		{
			MessageList errors = new MessageList();
			errors.Add(new InvalidTaxAreaIdException());
			throw new MessageListException(errors);
		}

	    TaxRateInfo taxRateInfo = new TaxRateInfo();
	    taxRateInfo.CityTaxRate = 1.0M;
	    taxRateInfo.CountryTaxRate = 1.0M;
	    taxRateInfo.CountyTaxRate = 1;
	    taxRateInfo.LocalDistrict1TaxRate = 0;
	    taxRateInfo.LocalDistrict2TaxRate = 0;
	    taxRateInfo.LocalDistrict3TaxRate = 0;
	    taxRateInfo.RegionTaxRate = 0;
	    taxRateInfo.TotalTaxRate = 6.6M;
	    taxRateInfo.TaxAreaID = new IdType(1);

	    return taxRateInfo;
	}

	public TaxResult Commit(TaxOrder order, StringType currencyCode) {
	    CurrencyType total = CurrencyType.ZERO;
	    foreach(TaxOrderLine line in order.Lines) {
		total += line.ExtendedPrice;
	    }

	    TaxResult result = new TaxResult();
	    result.TotalTax = CurrencyType.Round(total * 0.066M,2);
	    return result;
	}

	public TaxResult GetQuoteTaxTotal(TaxOrder order, StringType currencyCode) {
	    CurrencyType total = CurrencyType.ZERO;
	    foreach(TaxOrderLine line in order.Lines) {
		total += line.ExtendedPrice;
	    }

	    TaxResult result = new TaxResult();
	    result.TotalTax = CurrencyType.Round(total * 0.066M,2);
	    return result;
	}

	public TaxAreaData GetTaxAreaForAddress(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, BooleanType limits) {
	    TaxAreaData taxArea = new TaxAreaData();
	    taxArea.City = city;
	    taxArea.CityTaxRate = 0;
	    taxArea.Country = country;
	    taxArea.CountyTaxRate = 0;
	    taxArea.PostalCode = postalCode;
	    taxArea.Region = region;
	    taxArea.TaxArea = "Test";
	    taxArea.TaxAreaID = new IdType(1);
	    taxArea.TotalTaxRate = 6.6M;
	    taxArea.IsShippingTaxable = BooleanType.TRUE;
	    taxArea.AddressValidated = BooleanType.TRUE;
	    return taxArea;
	}

	public TaxAreaList LookupTaxArea(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, BooleanType replaceHTMLEntities) {
	    TaxAreaList list = new TaxAreaList();
	    list.Add(GetTaxAreaForAddress(street, city, county, region, postalCode, country, BooleanType.FALSE));
	    return list;
	}

	public TaxResult Commit(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    CurrencyType total = CurrencyType.ZERO;
	    foreach(TaxOrderLine line in order.Lines) {
		total += line.ExtendedPrice;
	    }

	    TaxResult result = new TaxResult();
	    result.City = city;
	    result.CityTaxRate = 0;
	    result.Country = country;
	    result.CountyTaxRate = 0;
	    result.PostalCode = postalCode;
	    result.Region = region;
	    result.TaxAreaID = new IdType(1);
	    result.TotalTaxRate = 6.6M;
	    result.TotalTax = CurrencyType.Round(total * 0.066M,2);

		result.TaxJurisdictions.AddRange(GetTestTaxJurisdictions(city, county, region, total));
	    
	    return result;
	}

	public TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    CurrencyType total = CurrencyType.ZERO;
	    foreach(TaxOrderLine line in order.Lines) {
		total += line.Price * line.Quantity.ToInt32() - line.DiscountAmount;
	    }

	    TaxResult result = new TaxResult();
	    result.City = city;
	    result.CityTaxRate = 0;
	    result.Country = country;
	    result.CountyTaxRate = 0;
	    result.PostalCode = postalCode;
	    result.Region = region;
	    result.TaxAreaID = new IdType(1);
	    result.TotalTaxRate = 6.6M;
	    result.TotalTax = CurrencyType.Round(total * 0.066M,2);
	    result.AddressValidated = BooleanType.TRUE;
	    
		result.TaxJurisdictions.AddRange(GetTestTaxJurisdictions(city, county, region, total));
		
	    return result;
	}

	public TaxResult Calculate(StringType transactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    CurrencyType total = CurrencyType.ZERO;
	    foreach(TaxOrderLine line in order.Lines) {
		total += line.Price * line.Quantity.ToInt32() - line.DiscountAmount;
	    }

	    TaxResult result = new TaxResult();
	    result.City = city;
	    result.CityTaxRate = 0;
	    result.Country = country;
	    result.CountyTaxRate = 0;
	    result.PostalCode = postalCode;
	    result.Region = region;
	    result.TaxAreaID = new IdType(1);
	    result.TotalTaxRate = 6.6M;
	    result.TotalTax = CurrencyType.Round(total * 0.066M,2);
	    result.AddressValidated = BooleanType.TRUE;

		result.TaxJurisdictions.AddRange(GetTestTaxJurisdictions(city, county, region, total));
	    
	    return result;
	}

	private System.Collections.ArrayList GetTestTaxJurisdictions(StringType county, StringType city, StringType region, CurrencyType total)
	{
		System.Collections.ArrayList results = new System.Collections.ArrayList();

		DecimalType countyRate = new DecimalType(1.5);
		DecimalType cityRate = new DecimalType(.35);
		DecimalType stateRate = new DecimalType(4.75);

		TaxJurisdiction tj = null;

		tj = new TaxJurisdiction();
		tj.Amount = total * countyRate;
		tj.Description = county;
		tj.JurisdictionType = TaxJurisdictionTypeEnum.COUNTY;
		tj.Rate = countyRate;
		results.Add(tj);

		tj = new TaxJurisdiction();
		tj.Amount = total * cityRate;
		tj.Description = city;
		tj.JurisdictionType = TaxJurisdictionTypeEnum.CITY;
		tj.Rate = cityRate;
		results.Add(tj);

		tj = new TaxJurisdiction();
		tj.Amount = total * stateRate;
		tj.Description = region;
		tj.JurisdictionType = TaxJurisdictionTypeEnum.STATE;
		tj.Rate = stateRate;
		results.Add(tj);

		return results;
	}

    }
}
