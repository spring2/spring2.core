using System;

namespace Spring2.Core.Types {

    public class QuantityType : DecimalType {

	public static readonly new QuantityType DEFAULT = new QuantityType();
	public static readonly new QuantityType UNSET = new QuantityType();

	[Obsolete("Use appropriate constructor instead.")]
	public static new QuantityType NewInstance(Decimal value) {
	    return new QuantityType(value);
	}

	public static new QuantityType Parse(String value) {
	    return new QuantityType(Decimal.Parse(value));
	}

	private QuantityType() {}

	public QuantityType(Decimal value) : base(value) {}

	public QuantityType(Double value) : base(value) {}

	public QuantityType(Int32 value) : base(value) {}

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
