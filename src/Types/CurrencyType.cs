using System;

namespace Spring2.Core.Types {

    public class CurrencyType : DataType	{

	public static readonly new CurrencyType DEFAULT = new CurrencyType();
	public static readonly new CurrencyType UNSET = new CurrencyType();

	public static CurrencyType NewInstance(Object value) {

	    if (value is Decimal) {
		return new CurrencyType((Decimal)value);
	    } else {
		return UNSET;
	    }
	}

	private Decimal value;

	private CurrencyType() {}

	public CurrencyType(Decimal value) {
	    this.value = value;
	}

	protected override Object Value {
	    get {
		return value;
	    }
	}

	public override Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(this, DEFAULT);
	    }
	}

	public override Boolean IsUnset {
	    get {
		return Object.ReferenceEquals(this, UNSET);
	    }
	}
    }
}
