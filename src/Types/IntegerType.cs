using System;
using System.Globalization;

namespace Spring2.Core.Types {

    /// <summary>
    /// Data type for wrapping integers.  This class is to replace the soon to be 
    /// obsolete NumberType class.
    /// </summary>
    public class IntegerType : DataType, IComparable {

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

	public override String ToString(String format) {
	    return IsValid ? value.ToString(format) : base.ToString();
	}

	public String ToString(IFormatProvider provider) {
	    return IsValid ? value.ToString(provider) : base.ToString();
	}

	public String ToString(String format, IFormatProvider provider) {
	    return IsValid ? value.ToString(format, provider) : base.ToString();
	}

	public Int32 CompareTo(Object o) {

	    IntegerType that = o as IntegerType;
	    if (that == null) {
		throw new ArgumentException("Argument must be an instance of IntegerType");
	    }

	    if (this.IsValid && that.IsValid) {
		return value.CompareTo(that.Value);
	    }
	    
	    return Compare(that);
	}
    }
}
