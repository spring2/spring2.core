using System;

namespace Spring2.Core.Types {

    public class QuantityType : DataType {

	public static readonly new QuantityType DEFAULT = new QuantityType();
	public static readonly new QuantityType UNSET = new QuantityType();

	public static QuantityType NewInstance(Object value) {

	    if (value is Decimal) {
		return new QuantityType((Decimal)value);
	    } else {
		return UNSET;
	    }
	}

	private Decimal value;

	private QuantityType() {}

	public QuantityType(Decimal value) {
	    this.value = value;
	}

	public QuantityType(Double value) {
	    this.value = (Decimal)value;
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

	public Decimal ToDecimal() {
	    if (IsUnset || IsDefault) {
		throw new InvalidCastException("UNSET and DEFAULT QuantityTypes have no decimal value.");
	    } else {
		return value;
	    }
	}

	public String ToString(String format) {
	    return value.ToString(format);
	}

	public override Boolean Equals(Object o) {
	    if (this == o) {
		return true;
	    } else if (!(o is IdType)) {
		return false;
	    } else {
		return value.Equals(((QuantityType)o).value);
	    }
	}

	public override int GetHashCode() {
	    return value.GetHashCode();
	}


    }
}
