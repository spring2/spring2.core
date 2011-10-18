using System;
using Spring2.Core.Types;
using Spring2.Core.Tax.Vertex;
using NotImplementedException = System.NotImplementedException;

namespace Spring2.Core.Tax.Test {
    /// <summary>
    /// Null object provider so that there can be something to lock in the tax manager
    /// </summary>
    public class NullTaxProvider : ITaxProvider {

	public NullTaxProvider(StringType profileKey) {
	}

	public TaxRateInfo GetTaxRateForArea(Spring2.Core.Types.IdType id, Spring2.Core.Types.DateType dateType) {
	    // TODO:  Add NullTaxProvider.GetTaxRateForArea implementation
	    return null;
	}

	public TaxResult Commit(TaxOrder order, Spring2.Core.Types.StringType currencyCode) {
	    // TODO:  Add NullTaxProvider.Commit implementation
	    return new TaxResult ();
	}

	public TaxResult GetQuoteTaxTotal(TaxOrder order, Spring2.Core.Types.StringType currencyCode) {
	    // TODO:  Add NullTaxProvider.GetQuoteTaxTotal implementation
	    return new TaxResult ();
	}

	public TaxAreaData GetTaxAreaForAddress(Spring2.Core.Types.StringType street, Spring2.Core.Types.StringType city, Spring2.Core.Types.StringType county, Spring2.Core.Types.StringType state, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType country, Spring2.Core.Types.BooleanType limits) {
	    // TODO:  Add NullTaxProvider.GetTaxAreaForAddress implementation
	    return null;
	}

	public TaxAreaList LookupTaxArea(Spring2.Core.Types.StringType street, Spring2.Core.Types.StringType city, Spring2.Core.Types.StringType county, Spring2.Core.Types.StringType state, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType country, Spring2.Core.Types.DateType date, Spring2.Core.Types.BooleanType booleanType) {
	    // TODO:  Add NullTaxProvider.LookupTaxArea implementation
	    return null;
	}

	public TaxResult Commit(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    // TODO:  Add NullTaxProvider.LookupTaxArea implementation
	    return null;
	}

	public TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    // TODO:  Add NullTaxProvider.LookupTaxArea implementation
	    return null;
	}

	public TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order, StringType location) {
	    // TODO:  Add NullTaxProvider.LookupTaxArea implementation
	    return null;
	}

	public TaxResult Calculate(StringType transactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    StringType location = StringType.DEFAULT;
	    return Calculate(transactionId, street, city, county, region, postalCode, country, date, order, location);
	}

	public TaxResult Calculate(StringType transactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order, StringType location) {
	    return null;
	}
    }
}