using System;

namespace Spring2.Core.Types {

    public class DateType : DataType {

	public static readonly new DateType DEFAULT = new DateType();
	public static readonly new DateType UNSET = new DateType();

	private DateTime value;

	public DateType() {
	    this.value = DateTime.Now;
	}

	public DateType(DateTime value) {
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
