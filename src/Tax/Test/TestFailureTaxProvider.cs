using Spring2.Core.Types;
using Spring2.Core.Tax.Vertex;
using NotImplementedException = System.NotImplementedException;

namespace Spring2.Core.Tax.Test {
    /// <summary>
    /// Summary description for TestFailTaxProvider.
    /// </summary>
    public class TestFailureTaxProvider : ITaxProvider {

	public TestFailureTaxProvider(StringType profileKey) {
	}

	public TaxRateInfo GetTaxRateForArea(Spring2.Core.Types.IdType id, Spring2.Core.Types.DateType dateType) {
	    throw new NotImplementedException();
	}

	public TaxResult Commit(TaxOrder order, Spring2.Core.Types.StringType currencyCode) {
	    throw new NotImplementedException();
	}

	public TaxResult GetQuoteTaxTotal(TaxOrder order, Spring2.Core.Types.StringType currencyCode) {
	    throw new NotImplementedException();
	}

	public TaxAreaData GetTaxAreaForAddress(Spring2.Core.Types.StringType street, Spring2.Core.Types.StringType city, Spring2.Core.Types.StringType county, Spring2.Core.Types.StringType state, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType country, Spring2.Core.Types.BooleanType limits) {
	    throw new NotImplementedException();
	}

	public TaxAreaList LookupTaxArea(Spring2.Core.Types.StringType street, Spring2.Core.Types.StringType city, Spring2.Core.Types.StringType county, Spring2.Core.Types.StringType state, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType country, Spring2.Core.Types.DateType date, Spring2.Core.Types.BooleanType booleanType) {
	    throw new NotImplementedException();
	}

	public TaxResult Commit(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    throw new NotImplementedException();
	}

	public TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    throw new NotImplementedException();
	}

	public TaxResult Calculate(StringType transactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order) {
	    throw new NotImplementedException();
	}


    }
}
