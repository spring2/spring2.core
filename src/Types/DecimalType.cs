using System;

namespace Spring2.Core.Types {

    public class DecimalType : DataType {

	public static readonly new DecimalType DEFAULT = new DecimalType();
	public static readonly new DecimalType UNSET = new DecimalType();

	public static DecimalType NewInstance(Object value) {

	    if (value is Decimal) {
		return new DecimalType((Decimal)value);
	    } else {
		return UNSET;
	    }
	}

	private Decimal value;

	private DecimalType() {}

	public DecimalType(Decimal value) {
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
