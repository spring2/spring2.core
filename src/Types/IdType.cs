using System;
using System.Globalization;

namespace Spring2.Core.Types {

    public class IdType : NumberType {

	public static readonly new IdType DEFAULT = new IdType();
	public static readonly new IdType UNSET = new IdType();
	
	public static new IdType NewInstance(Int32 value) {
	    return new IdType(value);
	}

	public static IdType NewInstance(String value) {
	    if (value == null) {
		return UNSET;
	    }
	    try {
		return new IdType(Int32.Parse(value));
	    } catch (FormatException) {
		return UNSET;
	    }
	}

	public static new IdType Parse(String value) {
	    return new IdType(Int32.Parse(value));
	}

	private IdType() {}

	public IdType(Int32 value) : base(value) {
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
    }
}
