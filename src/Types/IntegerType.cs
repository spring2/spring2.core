using System;
using System.Globalization;

namespace Spring2.Core.Types {

    /// <summary>
    /// Data type for wrapping integers.  This class is to replace the soon to be 
    /// obsolete NumberType class.
    /// </summary>
    public class IntegerType : DataType	{

	public static readonly new IntegerType DEFAULT = new IntegerType();
	public static readonly new IntegerType UNSET = new IntegerType();

	public static IntegerType Parse(String value) {
	    return value == null ? UNSET : new IntegerType(Int32.Parse(value, NumberStyles.Number));
	}

	public static IntegerType Parse(String value, IFormatProvider provider) {
	    return value == null ? UNSET : new IntegerType(Int32.Parse(value, NumberStyles.Integer,  provider));
	}

	private Int32 value;

	protected IntegerType() {}

	public IntegerType(Int32 value) {
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
		throw new InvalidCastException("UNSET and DEFAULT IntegerTypes have no integer value.");
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
