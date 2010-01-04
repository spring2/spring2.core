using System;
using Spring2.Core.Types;

namespace Spring2.Core.Tax {
    
    public class TaxJurisdiction : DataObject.DataObject {

	public static readonly TaxJurisdiction DEFAULT = new TaxJurisdiction();

	private TaxJurisdictionTypeEnum jurisdictionType = TaxJurisdictionTypeEnum.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private DecimalType rate = DecimalType.DEFAULT;
	private CurrencyType amount = CurrencyType.DEFAULT;

	public TaxJurisdictionTypeEnum JurisdictionType {
	    get { return this.jurisdictionType; }
	    set { this.jurisdictionType = value; }
	}

	public StringType Description {
	    get { return this.description; }
	    set { this.description = value; }
	}

	public DecimalType Rate {
	    get { return this.rate; }
	    set { this.rate = value; }
	}

	public CurrencyType Amount {
	    get { return this.amount; }
	    set { this.amount = value; }
	}

    }
}