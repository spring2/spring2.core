using System;
using System.Globalization;

namespace Spring2.Core.Types {

    public class CurrencyType : DecimalType {

	public static readonly new CurrencyType DEFAULT = new CurrencyType();
	public static readonly new CurrencyType UNSET = new CurrencyType();

	[Obsolete("Use appropriate constructor instead.")]
	public static new CurrencyType NewInstance(Decimal value) {
	    return new CurrencyType(value);
	}

	public static new CurrencyType Parse(String value) {
	    return new CurrencyType(Decimal.Parse(value, NumberStyles.Currency));
	}

	public static new CurrencyType Parse(String value, IFormatProvider provider) {
	    return new CurrencyType(Decimal.Parse(value, NumberStyles.Currency, provider));
	}

	private CurrencyType() {}

	public CurrencyType(Decimal value) : base(value) {}

	public CurrencyType(Double value) : base(value) {}

	public CurrencyType(Int32 value) : base(value) {}

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

	public override String ToString() {
	    return value.ToString("c");
	}

	public override String ToString(String format) {
	    return value.ToString(format);
	}
    }
}
