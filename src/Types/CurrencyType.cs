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
	    return value == null ? UNSET : new CurrencyType(Decimal.Parse(value, NumberStyles.Currency));
	}

	public static new CurrencyType Parse(String value, IFormatProvider provider) {
	    return value == null ? UNSET : new CurrencyType(Decimal.Parse(value, NumberStyles.Currency, provider));
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
	    return IsValid ? value.ToString("c") : base.ToString();
	}

	public override String ToString(String format) {
	    return IsValid ? value.ToString(format) : base.ToString();
	}

	public String ToString(IFormatProvider provider) {
	    return IsValid ? value.ToString("c", provider) : base.ToString();
	}

	public new CurrencyType Round(Int32 decimals) {
	    return IsValid ? new CurrencyType(Decimal.Round(value, decimals)) : this;
	}

	public static CurrencyType operator + (CurrencyType c1, CurrencyType c2) {
	    return new CurrencyType(c1.ToDecimal() + c2.ToDecimal());
	}

	public static CurrencyType operator * (CurrencyType c1, CurrencyType c2) {
	    return new CurrencyType(c1.ToDecimal() * c2.ToDecimal());
	}

	public static CurrencyType operator * (CurrencyType c, DecimalType d) {
	    return new CurrencyType(c.ToDecimal() * d.ToDecimal());
	}

	public static CurrencyType operator - (CurrencyType c1, CurrencyType c2) {
	    return new CurrencyType(c1.ToDecimal() - c2.ToDecimal());
	}

	public static DecimalType operator / (CurrencyType c1, CurrencyType c2) {
	    return new DecimalType(c1.ToDecimal() / c2.ToDecimal());
	}

	public static CurrencyType operator - (CurrencyType c) {
	    return new CurrencyType(- c.ToDecimal());
	}

	public static Boolean operator > (CurrencyType d1, CurrencyType d2) {
	    return d1.CompareTo(d2) > 0;
	}

	public static Boolean operator < (CurrencyType d1, CurrencyType d2) {
	    return d1.CompareTo(d2) < 0;
	}

	public static Boolean operator >= (CurrencyType d1, CurrencyType d2) {
	    return d1.CompareTo(d2) >= 0;
	}

	public static Boolean operator <= (CurrencyType d1, CurrencyType d2) {
	    return d1.CompareTo(d2) <= 0;
	}

    }
}
