using System;
using System.Globalization;

namespace Spring2.Core.Types {

    public class NumberType : DataType	{

	public static readonly new NumberType DEFAULT = new NumberType();
	public static readonly new NumberType UNSET = new NumberType();

	public static NumberType NewInstance(Int32 value) {
	    return new NumberType(value);
	}

	public static NumberType Parse(String value) {
	    return new NumberType(Int32.Parse(value, NumberStyles.Number));
	}

	public static NumberType Parse(String value, IFormatProvider provider) {
	    return new NumberType(Int32.Parse(value, NumberStyles.Integer,  provider));
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

	public String ToString(String format) {
	    return value.ToString(format);
	}

	public String ToString(IFormatProvider provider) {
	    return value.ToString(provider);
	}

	public String ToString(String format, IFormatProvider provider) {
	    return value.ToString(format, provider);
	}
    }
}
