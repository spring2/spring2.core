using System;
using Spring2.Core.Types;

namespace Spring2.Core.Tax {

    public class TaxResult : TaxRateInfo {
	private StringType taxTransactionId = StringType.DEFAULT;
	private CurrencyType totalTax = CurrencyType.DEFAULT;
    	private TaxResultLineList lines = new TaxResultLineList();
	private BooleanType addressValidated = BooleanType.FALSE;

	public StringType TaxTransactionId {
	    get { return taxTransactionId; }
	    set { taxTransactionId = value; }
	}

	public CurrencyType TotalTax {
	    get { return totalTax; }
	    set { totalTax = value; }
	}

	public TaxResultLineList Lines {
	    get { return lines; }
	    set { lines = value; }
	}

	public BooleanType AddressValidated {
	    get { return addressValidated; }
	    set { addressValidated = value; }
	}
    }
}