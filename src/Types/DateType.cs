using System;

namespace Spring2.Core.Types {

    public class DateType : DataType, IComparable {

	public static readonly new DateType DEFAULT = new DateType();
	public static readonly new DateType UNSET = new DateType();

	[Obsolete("Use appropriate constructor instead.")]
	public static DateType NewInstance(DateTime value) {
	    return new DateType(value);
	}

	public static DateType Parse(String value) {
	    return value == null ? UNSET : new DateType(DateTime.Parse(value));
	}

	public static DateType Today {
	    get {
		return new DateType(DateTime.Today);
	    }
	}

	public static DateType Now {
	    get {
		return new DateType();
	    }
	}

	private DateTime value;

	/// <summary>
	/// Creates a DateType populated with current time
	/// </summary>
	public DateType() {
	    this.value = DateTime.Now;
	}

	/// <summary>
	/// Contructs a new instance of a DateType with
	/// the given date as its internal value.
	/// </summary>
	/// <param name="value">a DateTime object for the internal 
	/// value of the date type.</param>
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

	public DateTime ToDateTime() {
	    if (IsValid) {
		return value;
	    } else {
		throw new InvalidCastException("UNSET and DEFAULT DateTypes have no decimal value.");
	    }
	}

	public override String ToString(String format) {
	    return IsValid ? value.ToString(format) : base.ToString();
	}

	public String ToShortDateString() {
	    return IsValid ? value.ToShortDateString() : base.ToString();
	}

	public String ToShortTimeString() {
	    return IsValid ? value.ToShortTimeString() : base.ToString();
	}

	public override Boolean Equals(Object o) {
	    DateType that = o as DateType;
	    if (that == null) {
		return false;
	    }
	    if (ReferenceEquals(this, that)) {
		return true;
	    }
	    if (this.IsDefault || that.IsDefault) {
		return this.IsDefault && that.IsDefault;
	    }
	    if (this.IsUnset || that.IsUnset) {
		return this.IsUnset && that.IsUnset;
	    }
	    // it appears that loading a DateTime for Sql Server will not get you the last 4 significant digits
	    // this is in an attempt to say that those last 4 digits do not matter
	    // the last 4 digits represent the portion of a 1000th of a second (ie. 5h 3m 4s 456ms xxxx)
	    long difference = this.value.Ticks - that.value.Ticks;
	    return (difference < 10000 && difference > -10000);
	}

	public override int GetHashCode() {
	    return value.GetHashCode();
	}

	public Int32 CompareTo(Object o) {
	    DateType that = o as DateType;
	    if (that == null) {
		throw new ArgumentException("Argument must be an instance of DateType");
	    }

	    if (this.IsValid && that.IsValid) {
		return value.CompareTo(that.Value);
	    }
	    
	    return Compare(that);
	}

	public DateType EndOfCurrentQuarter {
	    get {
		DateTime result = EndOfPreviousQuarter.ToDateTime().AddMonths(3);
		result = result.AddDays(DateTime.DaysInMonth(result.Year, result.Month) - result.Day);
		return new DateType(result);
	    }
	}

	public DateType EndOfPreviousQuarter {
	    get {
		// Get to the correct month.
		DateTime result = ToDateTime().AddMonths(-1);
		result = result.AddMonths(-(result.Month % 3));

		// Go to the end of the month.
		result = result.AddDays(DateTime.DaysInMonth(result.Year, result.Month) - result.Day);

		return new DateType(result);
	    }
	}

	public DateType FirstOfMonth {
	    get {
		DateTime result = ToDateTime();
		return new DateType(new DateTime(result.Year, result.Month, 1));
	    }
	}

	public DateType FirstOfYear {
	    get {
		DateTime result = ToDateTime();
		return new DateType(new DateTime(result.Year, 1, 1));
	    }
	}

	public DateType OneYearAgo {
	    get {
		DateTime result = ToDateTime().AddMonths(-12);
		return new DateType(result);
	    }
	}

	/// <summary>
	/// Get the date part only
	/// </summary>
	public DateType Date {
	    get {
		return new DateType(new DateTime(ToDateTime().Year, ToDateTime().Month, ToDateTime().Day));
	    }
	}

	/// <summary>
	/// Get the date part only
	/// </summary>
	public DateTime ToDate() {
	    return new DateTime(ToDateTime().Year, ToDateTime().Month, ToDateTime().Day);
	}

	public DateType AddDays(Double days) {
	    return new DateType(ToDateTime().AddDays(days));
	}

	/// <summary>
	/// Returns an instance that represents the current date and time
	/// </summary>
	public static DateType Now {
	    get {
		return new DateType();
	    }
	}

	/// <summary>
	/// Returns and instance that represents the current date
	/// </summary>
	public static DateType Today {
	    get {
		return new DateType(DateTime.Today);
	    }
	}
    }
}
