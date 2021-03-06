using System;
using System.Collections;

namespace Spring2.Core.Types {

    public class BooleanType : EnumDataType {

	private static readonly IList OPTIONS = new ArrayList();

	public static readonly new BooleanType DEFAULT = new BooleanType();
	public static readonly new BooleanType UNSET = new BooleanType();

	public static readonly BooleanType TRUE = new BooleanType("Y");
	public static readonly BooleanType FALSE = new BooleanType("N");

	public static BooleanType GetInstance(Boolean value) {
	    return value ? TRUE : FALSE;
	}

	public static BooleanType GetInstance(Int32 value) {
	    if (value == 1) {
		return TRUE;
	    }
	    if (value == 0) {
		return FALSE;
	    }
	    return UNSET;
	}

	public static BooleanType GetInstance(Object value) {
	    if (value is String) {
		foreach (BooleanType b in OPTIONS) {
		    if (b.Value.Equals(value)) {
			return b;
		    }
		}
	    }

	    return UNSET;
	}

	private BooleanType() {
	}

	private BooleanType(String code) {
	    this.code = code;
	    OPTIONS.Add(this);
	}

	protected override Object Value {
	    get {
		return code;
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

	public Boolean IsTrue {
	    get {
		return Object.ReferenceEquals(this, TRUE);
	    }
	}

	public Boolean IsFalse {
	    get {
		return Object.ReferenceEquals(this, FALSE);
	    }
	}

	public Boolean ToBoolean() {
	    if (Object.ReferenceEquals(this, TRUE)) {
		return true;
	    }
	    if (Object.ReferenceEquals(this, FALSE)) {
		return false;
	    }
	    throw new InvalidCastException("UNSET and DEFAULT cannot be cast to a Boolean.");
	}
    }
}
