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

	public Decimal ToDecimal() {
	    if (IsUnset || IsDefault) {
		throw new InvalidCastException("UNSET and DEFAULT DecimalTypes have no decimal value.");
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
	    } else if (!(o is CurrencyType)) {
		return false;
	    } else {
		return value.Equals(((CurrencyType)o).value);
	    }
	}

	public override int GetHashCode() {
	    return value.GetHashCode();
	}

    }
}
