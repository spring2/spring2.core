using System;

namespace Spring2.Core.Types {

    public class IdType : DataType {

	public static readonly new IdType DEFAULT = new IdType();
	public static readonly new IdType UNSET = new IdType();

	public static IdType NewInstance(Object value) {

	    if (value is Int32) {
		return new IdType((Int32)value);
	    } else {
		return UNSET;
	    }
	}

	public static IdType NewInstance(Int32 value) {
	    return new IdType(value);
	}

	public static IdType NewInstance(String value) {
	    if (value==null) {
		return UNSET;
	    }

	    try {
		return new IdType(Int32.Parse(value));
	    } catch (FormatException) {
		return UNSET;
	    }
	}

	private Int32 value;

	private IdType() {}

	public IdType(Int32 value) {
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
		throw new InvalidCastException("UNSET and DEFAULT IdTypes have no integer value.");
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
		return value.Equals(((IdType)o).value);
	    }
	}

	public override int GetHashCode() {
	    return value.GetHashCode();
	}


    }
}
