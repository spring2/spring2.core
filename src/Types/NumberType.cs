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
    }
}
