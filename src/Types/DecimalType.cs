using System;

namespace Spring2.Core.Types {

    public class DecimalType : DataType {

	public static readonly new DecimalType DEFAULT = new DecimalType();
	public static readonly new DecimalType UNSET = new DecimalType();

	public static DecimalType NewInstance(Object value) {

	    if (value is Decimal) {
		return new DecimalType((Decimal)value);
	    } else {
		return UNSET;
	    }
	}

	protected Decimal value;

	protected DecimalType() {}

	public DecimalType(Decimal value) {
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

	public Decimal ToDecimal() {
	    if (IsUnset || IsDefault) {
		throw new InvalidCastException("UNSET and DEFAULT DecimalTypes have no decimal value.");
	    } else {
		return value;
	    }
	}

	public virtual String ToString(String format) {
	    return value.ToString(format);
	}

	public override Boolean Equals(Object o) {
	    if (this == o) {
		return true;
	    } else if (!(o is IdType)) {
		return false;
	    } else {
		return value.Equals(((DecimalType)o).value);
	    }
	}

	public override int GetHashCode() {
	    return value.GetHashCode();
	}

    }
}
