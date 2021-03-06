using System;
using System.Globalization;

namespace Spring2.Core.Types {

    [Obsolete("Use IntegerType instead.")]
    public class NumberType : DataType	{

	public static readonly new NumberType DEFAULT = new NumberType();
	public static readonly new NumberType UNSET = new NumberType();

	[Obsolete("Use appropriate constructor instead.")]
	public static NumberType NewInstance(Int32 value) {
	    return new NumberType(value);
	}

	public static NumberType Parse(String value) {
	    return value == null ? UNSET : new NumberType(Int32.Parse(value, NumberStyles.Number));
	}

	public static NumberType Parse(String value, IFormatProvider provider) {
	    return value == null ? UNSET : new NumberType(Int32.Parse(value, NumberStyles.Integer,  provider));
	}

	private Int32 value;

	protected NumberType() {}

	public NumberType(Int32 value) {
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

	public Int32 ToInt32() {
	    if (IsUnset || IsDefault) {
		throw new InvalidCastException("UNSET and DEFAULT NumberTypes have no integer value.");
	    } else {
		return value;
	    }
	}

	public override String ToString(String format) {
	    return IsValid ? value.ToString(format) : base.ToString();
	}

	public String ToString(IFormatProvider provider) {
	    return IsValid ? value.ToString(provider) : base.ToString();
	}

	public String ToString(String format, IFormatProvider provider) {
	    return IsValid ? value.ToString(format, provider) : base.ToString();
	}
    }
}
