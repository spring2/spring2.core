using System;
using System.Globalization;

namespace Spring2.Core.Types {

    public class DecimalType : DataType {

	public static readonly new DecimalType DEFAULT = new DecimalType();
	public static readonly new DecimalType UNSET = new DecimalType();

	protected Decimal value;

	public static DecimalType NewInstance(Decimal value) {
	    return new DecimalType(value);
	}

	public static DecimalType Parse(String value) {
	    return new DecimalType(Decimal.Parse(value));
	}

	public static DecimalType Parse(String value, NumberStyles style) {
	    return new DecimalType(Decimal.Parse(value, style));
	}

	public static DecimalType Parse(String value, IFormatProvider provider) {
	    return new DecimalType(Decimal.Parse(value, provider));
	}

	public static DecimalType Parse(String value, NumberStyles style, IFormatProvider provider) {
	    return new DecimalType(Decimal.Parse(value, style, provider));
	}

	protected DecimalType() {}

	public DecimalType(Decimal value) {
	    this.value = value;
	}

	public DecimalType(Double value) {
	    this.value = new Decimal(value);
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

	public override String ToString() {
	    return value.ToString();
	}

	public virtual String ToString(String format) {
	    return value.ToString(format);
	}
    }
}
