using System;

namespace Spring2.Core.Types {

    public class NumberType : DataType	{

	public static readonly new NumberType DEFAULT = new NumberType();
	public static readonly new NumberType UNSET = new NumberType();

	public static NumberType NewInstance(Object value) {

	    if (value is Int32) {
		return new NumberType((Int32)value);
	    } else {
		return UNSET;
	    }
	}

	private Int32 value;

	private NumberType() {}

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

	public override Boolean Equals(Object o) {
	    if (this == o) {
		return true;
	    } else if (!(o is IdType)) {
		return false;
	    } else {
		return value.Equals(((NumberType)o).value);
	    }
	}

	public override int GetHashCode() {
	    return value.GetHashCode();
	}


    }
}
