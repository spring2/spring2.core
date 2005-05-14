using System;

namespace Spring2.Core.Types 
{

    /// <summary>
    /// This class is identical to DateType and only exists for forward compatability with the new value types.
    /// Inheritance didn't work because we had to return DateTimeType from the methods instead of DateType.
    /// </summary>
    public class DateTimeType : DataType, IComparable 
    {

	public static readonly new DateTimeType DEFAULT = new DateTimeType();
	public static readonly new DateTimeType UNSET = new DateTimeType();

	[Obsolete("Use appropriate constructor instead.")]
	public static DateTimeType NewInstance(DateTime value) 
	{
	    return new DateTimeType(value);
	}

	public static DateTimeType Parse(String value) 
	{
	    return value == null ? UNSET : new DateTimeType(DateTime.Parse(value));
	}

	public static DateTimeType Today 
	{
	    get 
	    {
		return new DateTimeType(DateTime.Today);
	    }
	}

	public static DateTimeType Now 
	{
	    get 
	    {
		return new DateTimeType();
	    }
	}

	//this is used to indicate a default date value based on 1/1/1900
	private static DateTimeType theDefault1900 = DateTimeType.Parse("01/01/1900");

	public static DateTimeType Default1900 
	{
	    get {return theDefault1900;}
	}       	

	private DateTime value;

	/// <summary>
	/// Creates a DateTimeType populated with current time
	/// </summary>
	public DateTimeType() 
	{
	    this.value = DateTime.Now;
	}

	/// <summary>
	/// Contructs a new instance of a DateTimeType with
	/// the given date as its internal value.
	/// </summary>
	/// <param name="value">a DateTime object for the internal 
	/// value of the date type.</param>
	public DateTimeType(DateTime value) 
	{
	    this.value = value;
	}

	protected override Object Value 
	{
	    get 
	    {
		return value;
	    }
	}

	public override Boolean IsDefault 
	{
	    get 
	    {
		return Object.ReferenceEquals(this, DEFAULT);
	    }
	}

	public override Boolean IsUnset 
	{
	    get 
	    {
		return Object.ReferenceEquals(this, UNSET);
	    }
	}

	public DateTime ToDateTime() 
	{
	    if (IsValid) 
	    {
		return value;
	    } 
	    else 
	    {
		throw new InvalidCastException("UNSET and DEFAULT DateTimeTypes have no decimal value.");
	    }
	}

	public override String ToString(String format) 
	{
	    return IsValid ? value.ToString(format) : base.ToString();
	}

	public String ToShortDateString() 
	{
	    return IsValid ? value.ToShortDateString() : base.ToString();
	}

	public String ToShortTimeString() 
	{
	    return IsValid ? value.ToShortTimeString() : base.ToString();
	}

	public override Boolean Equals(Object o) 
	{
	    DateTimeType that = o as DateTimeType;
	    if (that == null) 
	    {
		return false;
	    }
	    if (ReferenceEquals(this, that)) 
	    {
		return true;
	    }
	    if (this.IsDefault || that.IsDefault) 
	    {
		return this.IsDefault && that.IsDefault;
	    }
	    if (this.IsUnset || that.IsUnset) 
	    {
		return this.IsUnset && that.IsUnset;
	    }
	    // it appears that loading a DateTime for Sql Server will not get you the last 4 significant digits
	    // this is in an attempt to say that those last 4 digits do not matter
	    // the last 4 digits represent the portion of a 1000th of a second (ie. 5h 3m 4s 456ms xxxx)
	    long difference = this.value.Ticks - that.value.Ticks;
	    return (difference < 10000 && difference > -10000);
	}

	public override int GetHashCode() 
	{
	    return value.GetHashCode();
	}

	public Int32 CompareTo(Object o) 
	{
	    DateTimeType that = o as DateTimeType;
	    if (that == null) 
	    {
		throw new ArgumentException("Argument must be an instance of DateTimeType");
	    }

	    if (this.IsValid && that.IsValid) 
	    {
		return value.CompareTo(that.Value);
	    }
	    
	    return Compare(that);
	}

	public DateTimeType EndOfCurrentQuarter 
	{
	    get 
	    {
		DateTime result = EndOfPreviousQuarter.ToDateTime().AddMonths(3);
		result = result.AddDays(DateTime.DaysInMonth(result.Year, result.Month) - result.Day);
		return new DateTimeType(result);
	    }
	}

	public DateTimeType EndOfPreviousQuarter 
	{
	    get 
	    {
		// Get to the correct month.
		DateTime result = ToDateTime().AddMonths(-1);
		result = result.AddMonths(-(result.Month % 3));

		// Go to the end of the month.
		result = result.AddDays(DateTime.DaysInMonth(result.Year, result.Month) - result.Day);

		return new DateTimeType(result);
	    }
	}

	public DateTimeType FirstOfMonth 
	{
	    get 
	    {
		DateTime result = ToDateTime();
		return new DateTimeType(new DateTime(result.Year, result.Month, 1));
	    }
	}

	public DateTimeType FirstOfYear 
	{
	    get 
	    {
		DateTime result = ToDateTime();
		return new DateTimeType(new DateTime(result.Year, 1, 1));
	    }
	}

	public DateTimeType OneYearAgo 
	{
	    get 
	    {
		DateTime result = ToDateTime().AddMonths(-12);
		return new DateTimeType(result);
	    }
	}

	/// <summary>
	/// Get the date part only
	/// </summary>
	public DateTimeType Date 
	{
	    get 
	    {
		if (this.IsValid) 
		{
		    return new DateTimeType(new DateTime(ToDateTime().Year, ToDateTime().Month, ToDateTime().Day));
		} 
		else 
		{
		    return this;
		}
	    }
	}

	/// <summary>
	/// Get the date part only
	/// </summary>
	public DateTime ToDate() 
	{
	    return new DateTime(ToDateTime().Year, ToDateTime().Month, ToDateTime().Day);
	}

	public DateTimeType AddDays(Double days) 
	{
	    return new DateTimeType(ToDateTime().AddDays(days));
	}

	public Boolean SameDayAs(DateTimeType that) 
	{
	    return this.Date.Equals(that.Date);
	}
    }
}

