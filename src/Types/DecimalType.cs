using System;
using System.Globalization;

namespace Spring2.Core.Types {

    public class DecimalType : DataType, IComparable {

	public static readonly new DecimalType DEFAULT = new DecimalType();
	public static readonly new DecimalType UNSET = new DecimalType();

	protected Decimal value;

	[Obsolete("Use appropriate constructor instead.")]
	public static DecimalType NewInstance(Decimal value) {
	    return new DecimalType(value);
	}

	public static DecimalType Parse(String value) {
	    return value == null ? UNSET : new DecimalType(Decimal.Parse(value));
	}

	public static DecimalType Parse(String value, NumberStyles style) {
	    return value == null ? UNSET : new DecimalType(Decimal.Parse(value, style));
	}

	public static DecimalType Parse(String value, IFormatProvider provider) {
	    return value == null ? UNSET : new DecimalType(Decimal.Parse(value, provider));
	}

	public static DecimalType Parse(String value, NumberStyles style, IFormatProvider provider) {
	    return value == null ? UNSET : new DecimalType(Decimal.Parse(value, style, provider));
	}

	protected DecimalType() {}

	public DecimalType(Decimal value) {
	    this.value = value;
	}

	public DecimalType(Double value) {
	    this.value = new Decimal(value);
	}

	public DecimalType(Int32 value) {
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
	    return IsValid ? value.ToString() : base.ToString();
	}

	public virtual String ToString(String format) {
	    return IsValid ? value.ToString(format) : base.ToString();
	}

	public Int32 CompareTo(Object o) {

	    DecimalType that = o as DecimalType;
	    if (that == null) {
		throw new ArgumentException("Argument must be an instance of DecimalType");
	    }

	    if (this.IsValid && that.IsValid) {
		return value.CompareTo(that.Value);
	    }
	    
	    return Compare(that);
	}
    }
}
