using System;
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using CultureInfo = System.Globalization.CultureInfo;
using Calendar = System.Globalization.Calendar;

namespace Spring2.Types {

    [Serializable(), StructLayout(LayoutKind.Auto)]
    public struct DateTimeType : IComparable, IFormattable, IConvertible
    {
	private DateTime  myValue;
	private TypeState myState;

	public static readonly DateTimeType MinValue = new DateTimeType(DateTime.MinValue);
        public static readonly DateTimeType MaxValue = new DateTimeType(DateTime.MaxValue);

	public static readonly DateTimeType DEFAULT = new DateTimeType(TypeState.DEFAULT);
	public static readonly DateTimeType UNSET   = new DateTimeType(TypeState.UNSET);
            
	#region State management
	public bool IsValid {
	    get {return myState == TypeState.VALID;}
	}

	public void SetValid() {
	    myState = TypeState.VALID;
	}

	public bool IsDefault {
	    get {return myState == TypeState.DEFAULT;}
	}

	public void SetDefault() {
	    myState = TypeState.DEFAULT;
	}

	public bool IsUnset {
	    get {return myState == TypeState.UNSET;}
	}

	public void SetUnset() {
	    myState = TypeState.UNSET;
	}

	public TypeState State {
	    get {return myState;}
	    set {myState = value;}
	}
       #endregion

	#region Constructors
	private DateTimeType(TypeState state) {
	    myState = state;
	    myValue = new DateTime();
	}

	public DateTimeType(DateTime newTime) {
	    myValue = newTime;
	    myState = TypeState.VALID;
	}

	public DateTimeType(long ticks) {
	    myValue = new DateTime(ticks);
	    myState = TypeState.VALID;
        }

        public DateTimeType(int year, int month, int day) {
	    myValue = new DateTime(year, month, day);
	    myState = TypeState.VALID;
        }
    
        public DateTimeType(int year, int month, int day, Calendar calendar) {
	    myValue = new DateTime(year, month, day, calendar);
	    myState = TypeState.VALID;
        }
    
        public DateTimeType(int year, int month, int day, int hour, int minute, int second) {
	    myValue = new DateTime(year, month, day, hour, second, minute, second);
	    myState = TypeState.VALID;
        }
    
        public DateTimeType(int year, int month, int day, int hour, int minute, int second, Calendar calendar) {
	    myValue = new DateTime(year, month, day, hour, minute, second, calendar);
	    myState = TypeState.VALID;
        }
    
        public DateTimeType(int year, int month, int day, int hour, int minute, int second, int millisecond) {
	    myValue = new DateTime(year, month, day, hour, minute, second, millisecond);
	    myState = TypeState.VALID;
        }
        
        public DateTimeType(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar) {
	    myValue = new DateTime(year, month, day, hour, minute, second, millisecond, calendar);
	    myState = TypeState.VALID;
        }    
	#endregion 

	#region Cast operators
	public static explicit operator DateType(DateTimeType value) {
	    return new DateType(value.Date);
	}

	public static explicit operator DateTimeType(DateType value) {
	    return value.ToDateTimeType();
	}

	public static explicit operator DateTime(DateTimeType value) {
	    //should we state check this?
	    return value.myValue;
	}

	public static explicit operator DateTimeType(DateTime value) {
	    return new DateTimeType(value);
	}
	#endregion

	#region To/From DateType methods
	public DateType ToDateType() {
	    return new DateType(myValue.Date);
	}

	public static DateTimeType FromDateType(DateType value) {
	    return value.ToDateTimeType();
	}
	#endregion 

	#region Comparison methods
	public static int Compare(DateTimeType leftHand, DateTimeType rightHand) 
	{
	    if (leftHand.myState == TypeState.VALID && rightHand.myState == TypeState.VALID) {
		if (leftHand.myValue < rightHand.myValue) {
		    return -1;
		}

		if (leftHand.myValue == rightHand.myValue) {
		    return 0;
		}

		if (leftHand.myValue > rightHand.myValue) {
		    return 1;
		}
	    }

	    if (leftHand.myState == TypeState.UNSET) {
		if (rightHand.myState == TypeState.DEFAULT || rightHand.myState == TypeState.VALID) {
		    return -1;
		}

		if (rightHand.myState == TypeState.UNSET) {
		    return 0;
		}
	    }

	    if (rightHand.myState == TypeState.DEFAULT) {
		if (leftHand.myState == TypeState.DEFAULT) {
		    return 0;
		}

		if (leftHand.myState == TypeState.UNSET)
		{
		    return 1;
		}

		return -1;
	    }

	    //should this throw an exception?
	    return 0;
	}

    
        public int CompareTo(Object value) {
            if (value == null) {
		return 1;
	    }

	    if (!(value is DateTimeType)) {
		throw new InvalidArgumentException("DateTimeType.CompareTo(object) - object must be of type DateTimeType");
	    }

	    DateTimeType dtValue = (DateTimeType) value;

	    if (myValue.Ticks > dtValue.myValue.Ticks) {
		return 1;
	    }

	    if (myValue.Ticks < dtValue.myValue.Ticks) {
		return -1;
	    }

            return 0;
        }
	#endregion
   
	#region Various methods for extracting parts of a DateTimeType (Date, Now, etc..)
        public static int DaysInMonth(int year, int month) {
	    return DateTime.DaysInMonth(year, month);
        }

	public DateTimeType Date {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return new DateTimeType(myValue.Date);
	    }
	}
    
        public int Day {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Day;
	    }
        }
    
        public DayOfWeek DayOfWeek {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.DayOfWeek;
	    }
        }
    
        public int DayOfYear {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.DayOfYear;
	    }
	}
       
        public int Hour {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Hour;
	    }
	}
    
        public int Millisecond {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Millisecond;
	    }
        }
    
        public int Minute {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Minute;
	    }
        }
    
        public int Month {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Month;
	    }
        }
    
        public static DateTimeType Now {
	    get {
		DateTimeType now = new DateTimeType(DateTime.Now);
		return now;
	    }
	}

        public static DateTimeType UtcNow {
	    get {
		DateTimeType now = new DateTimeType(DateTime.Now.ToUniversalTime());
		return now;
	    }
	}
    
        public int Second {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Second;
	    }
        }
    
        public long Ticks {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Ticks;
	    }
        }
    
        public TimeSpan TimeOfDay {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.TimeOfDay;
	    }
        }
    
        public static DateTimeType Today {
	    get {
		return new DateTimeType(DateTime.Today);
	    }
        }
    
        public int Year {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Year;
	    }
        }
    
        public static bool IsLeapYear(int year) {
	    return DateTime.IsLeapYear(year);
        }
	#endregion

	#region ParseMethods
        public static DateTimeType Parse(String s) 
	{
	    return new DateTimeType(DateTime.Parse(s));
        }
    
        public static DateTimeType Parse(String s, IFormatProvider provider) {
	    return new DateTimeType(DateTime.Parse(s, provider));
        }

        public static DateTimeType Parse(String s, IFormatProvider provider, DateTimeStyles styles) {
	    return new DateTimeType(DateTime.Parse(s, provider, styles));
        }
        
        public static DateTimeType ParseExact(String s, String format, IFormatProvider provider) {
	    return new DateTimeType(DateTime.ParseExact(s, format, provider));
        }

        public static DateTimeType ParseExact(String s, String format, IFormatProvider provider, DateTimeStyles style) {
	    return new DateTimeType(DateTime.ParseExact(s, format, provider, style));
        }    

        public static DateTimeType ParseExact(String s, String[] formats, IFormatProvider provider, DateTimeStyles style) {
	    return new DateTimeType(DateTime.ParseExact(s, formats, provider, style));
        }
	#endregion

	#region To<OADate, FileTime, LocalTime, UniversalTime> and From<FileTime, OADate> methods
        public double ToOADate() 
	{
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToOADate();
        }

        public long ToFileTime() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToFileTime();
        }
    
        public DateTimeType ToLocalTime() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return new DateTimeType(TimeZone.CurrentTimeZone.ToLocalTime(myValue));
        }
    
        public DateTimeType ToUniversalTime() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return new DateTimeType(myValue.ToUniversalTime());
        }

	public static DateTimeType FromFileTime(long fileTime) {
            if (fileTime < 0) {
		throw new InvalidArgumentException("DateTimeType.FromFileTime() - fileTime argument cannot be < 0");
	    }

	    DateTimeType result = new DateTimeType(DateTime.FromFileTime(fileTime));
	    return result;
        }
    
        public static DateTimeType FromOADate(double value) {
	    DateTimeType dt = new DateTimeType(DateTime.FromOADate(value));
	    return dt;
        }

	#endregion

	#region ToStringXX methods
        public String ToLongDateString() 
	{
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToLongDateString();
        }
    
        public String ToLongTimeString() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToLongTimeString();
        }
    
        public String ToShortDateString() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToShortDateString();
        }
    
        public String ToShortTimeString() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToShortTimeString();
        }
    
        public override String ToString() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToString();
        }

        public String ToString(String format) {
 	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToString(format);
        }

        public String ToString(IFormatProvider provider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return myValue.ToString(provider);
        }
         
        public String ToString(String format, IFormatProvider provider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToString(format, provider);
        }
	#endregion   

	#region Addition and Subtraction operators and methods
        public TimeSpan Subtract(DateTimeType value) 
	{
	    if (!IsValid || !value.IsValid) {
		throw new InvalidStateException(myState, value.myState);
	    }
	    
	    return myValue.Subtract(value.myValue);
        }
    
        public DateTimeType Subtract(TimeSpan value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return new DateTimeType(myValue.Ticks - value.Ticks);
        }
    
	#region Addition operators and methods
        public static DateTimeType operator +(DateTimeType dateToAddTo, TimeSpan timeToAdd) 
	{
	    if (!dateToAddTo.IsValid) {
		throw new InvalidStateException(dateToAddTo.myState);
	    }

            return new DateTimeType(dateToAddTo.myValue.Ticks + timeToAdd.Ticks);
        }

	public DateTimeType Add(TimeSpan value) 
	{
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    long newTicks = myValue.Ticks + value.Ticks;
            return new DateTimeType(newTicks);
        }
    
   
        public DateTimeType AddDays(double value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    DateTimeType newValue = new DateTimeType(myValue.AddDays(value));
            return newValue;
        }
    
        public DateTimeType AddHours(double value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    DateTimeType newValue = new DateTimeType(myValue.AddHours(value).Ticks);
            return newValue;
        }
    
        public DateTimeType AddMilliseconds(double value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    DateTimeType newValue = new DateTimeType(myValue.AddMilliseconds(value).Ticks);
            return newValue;
        }
    
        public DateTimeType AddMinutes(double value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    DateTimeType newValue = new DateTimeType(myValue.AddMinutes(value).Ticks);
            return newValue;
        }
    
        public DateTimeType AddMonths(int months) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    DateTimeType newValue = new DateTimeType(myValue.AddMonths(months));
            return newValue;
        }
    
        public DateTimeType AddSeconds(double value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    DateTimeType newValue = new DateTimeType(myValue.AddSeconds(value));
            return newValue;
        }
    
        public DateTimeType AddTicks(long value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    DateTimeType newValue = new DateTimeType(myValue.AddTicks(value).Ticks);
            return newValue;
        }
    
        public DateTimeType AddYears(int value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    DateTimeType newValue = new DateTimeType(myValue.AddYears(value).Ticks);
            return newValue;
        }
	#endregion 
    
        public static DateTimeType operator -(DateTimeType dateToSubtractFrom, TimeSpan timeToSubtract) {
	    if (!dateToSubtractFrom.IsValid) {
		throw new InvalidStateException(dateToSubtractFrom.myState);
	    }

            return new DateTimeType(dateToSubtractFrom.myValue.Ticks - timeToSubtract.Ticks);
        }
    
        public static TimeSpan operator -(DateTimeType leftHand, DateTimeType rightHand) {
	    if (!leftHand.IsValid || !rightHand.IsValid) {
		throw new InvalidStateException(leftHand.myState, rightHand.myState);
	    }

	    return new TimeSpan(leftHand.Ticks - rightHand.Ticks);
        }
	#endregion

	#region Equality operators and methods

	//what should we do here?
	//if params not valid do we fail?
	//or return false?
        public static bool Equals(DateTimeType leftHand, DateTimeType rightHand) {
	    if (!leftHand.IsValid || !rightHand.IsValid) {
		throw new InvalidStateException(leftHand.myState, rightHand.myState);
	    }

	    return leftHand.myValue.Ticks == rightHand.myValue.Ticks;
        }

        public static bool operator ==(DateTimeType leftHand, DateTimeType rightHand) 
	{
	    return Compare(leftHand, rightHand) == 0;
        }

        public static bool operator !=(DateTimeType leftHand, DateTimeType rightHand) {
	    return Compare(leftHand, rightHand) != 0;
        }
        
        public static bool operator <(DateTimeType leftHand, DateTimeType rightHand) {
	    return Compare(leftHand, rightHand) < 0;
        }

        public static bool operator <=(DateTimeType leftHand, DateTimeType rightHand) {
	    return Compare(leftHand, rightHand) <= 0;
        }

        public static bool operator >(DateTimeType leftHand, DateTimeType rightHand) {
	    return Compare(leftHand, rightHand) > 0;
        }

        public static bool operator >=(DateTimeType leftHand, DateTimeType rightHand) {
	    return Compare(leftHand, rightHand) >= 0;
        }
	#endregion 

	#region GetDateTimeFormats methods
        public String[] GetDateTimeFormats() 
	{
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.GetDateTimeFormats();
        }

        public String[] GetDateTimeFormats(IFormatProvider provider)
        {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return myValue.GetDateTimeFormats(provider);
        }
        
        public String[] GetDateTimeFormats(char format)
        {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return myValue.GetDateTimeFormats(format);
        }
        
        public String[] GetDateTimeFormats(char format, IFormatProvider provider)
        {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return myValue.GetDateTimeFormats(format, provider);
        }
        
        public TypeCode GetTypeCode() {
            return TypeCode.DateTime;
        }
	#endregion

	#region IConvertible methods
        bool IConvertible.ToBoolean(IFormatProvider provider) 
	{
            throw new InvalidCastException("DateTime", "Boolean");
        }

        char IConvertible.ToChar(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "Char");
        }

        [CLSCompliant(false)]
        sbyte IConvertible.ToSByte(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "SByte");
        }

        byte IConvertible.ToByte(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "Byte");
        }

        short IConvertible.ToInt16(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "Int16");
        }

        [CLSCompliant(false)]
        ushort IConvertible.ToUInt16(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "UInt16");
        }

        int IConvertible.ToInt32(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "Int32");
        }

        [CLSCompliant(false)]
        uint IConvertible.ToUInt32(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "UInt32");
        }

        long IConvertible.ToInt64(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "Int64");
        }

        [CLSCompliant(false)]
        ulong IConvertible.ToUInt64(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "UInt64");
        }

        float IConvertible.ToSingle(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "Single");
        }

        double IConvertible.ToDouble(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "Double");
        }

        Decimal IConvertible.ToDecimal(IFormatProvider provider) {
            throw new InvalidCastException("DateTime", "Decimal");
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return myValue;
        }

	//what do we do here?
	Object IConvertible.ToType(Type type, IFormatProvider provider) {
	    return null;
//            return Convert.DefaultToType((IConvertible)this, type, provider);
        }
	#endregion

	#region Object support methods
        public override int GetHashCode() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.GetHashCode();
        }

        public override bool Equals(Object value) {
            if (value is DateTimeType) {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

                return myValue.Ticks == ((DateTimeType)value).myValue.Ticks;
            }

	    return false;
        }

	#endregion
    }
}
