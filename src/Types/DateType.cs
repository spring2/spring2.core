using System;

namespace Spring2.Core.Types {

    public class DateType : DataType {

	public static readonly new DateType DEFAULT = new DateType();
	public static readonly new DateType UNSET = new DateType();

	public static DateType NewInstance(Object value) {

	    if (value is DateTime) {
		return new DateType((DateTime)value);
	    } else {
		return UNSET;
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
	/// the given date as its internal value.  This constructor
	/// should be private to avoid creating DateType objects with
	/// a null internal value.
	/// </summary>
	/// <param name="value">a DateTime object for the internal 
	/// value of the date type.</param>
	private DateType(DateTime value) {
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
	    if (IsUnset || IsDefault) {
		throw new InvalidCastException("UNSET and DEFAULT DateTypes have no decimal value.");
	    } else {
		return value;
	    }
	}

	public String ToString(String format) {
	    return value.ToString(format);
	}

	public String ToShortDateString() {
	    return value.ToShortDateString();
	}

	public String ToShortTimeString() {
	    return value.ToShortTimeString();
	}

	public override Boolean Equals(Object o) {
	    if (this == o) {
		return true;
	    } else if (!(o is DateType)) {
		return false;
	    } else {
		// it appears that loading a DateTime for Sql Server will not get you the last 4 significant digits
		// this is in an attempt to say that those last 4 digits do not matter
		// the last 4 digits represent the portion of a 1000th of a second (ie. 5h 3m 4s 456ms xxxx)
		long difference = value.Ticks - ((DateType)o).value.Ticks;
		return (difference < 10000 && difference > -10000);
	    }
	}

	public override int GetHashCode() {
	    return value.GetHashCode();
	}

    }
}
