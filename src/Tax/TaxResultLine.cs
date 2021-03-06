using System;
using Spring2.Core.Types;

namespace Spring2.Core.Tax {
    
    public class TaxResultLine {

    	public static readonly TaxResultLine DEFAULT = new TaxResultLine();

	private IdType orderlineId = IdType.DEFAULT;
	private StringType itemNumber = StringType.DEFAULT;
	private IntegerType quantity = IntegerType.DEFAULT;
	private CurrencyType price = CurrencyType.DEFAULT;
	private CurrencyType extendedPrice = CurrencyType.DEFAULT;
	private CurrencyType discountAmount = CurrencyType.DEFAULT;
    	private CurrencyType taxAmount = CurrencyType.DEFAULT;

	public IdType OrderLineId {
	    get { return this.orderlineId; }
	    set { this.orderlineId = value; }
	}

	public IntegerType Quantity {
	    get { return this.quantity; }
	    set { this.quantity = value; }
	}

	public CurrencyType Price {
	    get { return this.price; }
	    set { this.price = value; }
	}

	public CurrencyType ExtendedPrice {
	    get { return this.extendedPrice; }
	    set { this.extendedPrice = value; }
	}

	public CurrencyType DiscountAmount {
	    get { return this.discountAmount; }
	    set { this.discountAmount = value; }
	}

	public StringType ItemNumber {
	    get { return this.itemNumber; }
	    set { this.itemNumber = value; }
	}

	public CurrencyType TaxAmount {
	    get { return this.taxAmount; }
	    set { this.taxAmount = value; }
	}

    	public Boolean IsDefault {
	    get { return Object.ReferenceEquals(DEFAULT, this); }
	}

    }
}