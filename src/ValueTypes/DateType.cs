using System;
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using CultureInfo = System.Globalization.CultureInfo;
using Calendar = System.Globalization.Calendar;

namespace Spring2.Types {

    [Serializable(), StructLayout(LayoutKind.Auto)]
    public struct DateType : IComparable, IFormattable, IConvertible
    {
	private DateTime  myValue;
	private TypeState myState;

	public static readonly DateType MinValue = new DateType(DateTime.MinValue.Date);
        public static readonly DateType MaxValue = new DateType(DateTime.MaxValue.Date);
        
	public static readonly DateType DEFAULT = new DateType(TypeState.DEFAULT);
	public static readonly DateType UNSET   = new DateType(TypeState.UNSET);
	
	#region State management
      
	public bool IsValid 
	{
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
	private DateType(TypeState state) {
	    myState = state;
	    myValue = new DateTime();
	}

	public DateType(DateTime newTime) {
	    myValue = newTime.Date;
	    myState = TypeState.VALID;
	}

	public DateType(DateTimeType newTime) {
	    myValue = new DateTime(newTime.Date.Ticks);
	    myState = TypeState.VALID;
	}

	public DateType(long ticks) {
	    myValue = new DateTime(ticks);
	    myState = TypeState.VALID;
        }

        public DateType(int year, int month, int day) {
	    myValue = new DateTime(year, month, day);
	    myState = TypeState.VALID;
        }
    
        public DateType(int year, int month, int day, Calendar calendar) {
	    myValue = new DateTime(year, month, day, calendar);
	    myState = TypeState.VALID;
        }
       	#endregion 

	#region Cast operators
	public static explicit operator DateType(DateTimeType value) 
	{
	    return new DateType(value);
	}

	public static explicit operator DateTimeType(DateType value) {
	    return new DateTimeType(value.myValue.Date);
	}

	public static explicit operator DateType(DateTime value) 
	{
	    return new DateType(value);
	}

	public static explicit operator DateTime(DateType value) {
	    return value.myValue.Date;
	}
	#endregion
        
	#region Comparison methods
	public static int Compare(DateType leftHand, DateType rightHand) 
	{
	    if (leftHand.myState == TypeState.VALID && rightHand.myState == TypeState.VALID) {
		if (leftHand.myValue.Ticks < rightHand.myValue.Ticks) {
		    return -1;
		}

		if (leftHand.myValue.Ticks == rightHand.myValue.Ticks) {
		    return 0;
		}

		if (leftHand.myValue.Ticks > rightHand.myValue.Ticks) {
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

	    if (!(value is DateType)) {
		throw new InvalidArgumentException("DateType.CompareTo(object) - object must be of type DateType");
	    }

	    DateType dtValue = (DateType) value;

	    if (myValue.Ticks > dtValue.myValue.Ticks) {
		return 1;
	    }

	    if (myValue.Ticks < dtValue.myValue.Ticks) {
		return -1;
	    }

            return 0;
        }
	#endregion
   
	#region Various methods for extracting parts of a DateType (Date, Now, etc..)
        public static int DaysInMonth(int year, int month) {
	    return DateTime.DaysInMonth(year, month);
        }

	public DateType Date {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return new DateType(myValue.Date);
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
          
        public int Month {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Month;
	    }
        }
    
        public static DateType Now {
	    get {
		return new DateType(DateTime.Now.Date);
	    }
	}

        public static DateType UtcNow {
	    get {
		return new DateType(DateTime.Now.ToUniversalTime().Date);
	    }
	}
    
        public long Ticks {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		return myValue.Date.Ticks;
	    }
        }
       
        public static DateType Today {
	    get {
		return new DateType(DateTime.Today.Date);
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
        public static DateType Parse(String s) 
	{
	    return new DateType(DateTime.Parse(s).Date);
        }
    
        public static DateType Parse(String s, IFormatProvider provider) {
	    return new DateType(DateTime.Parse(s, provider).Date);
        }

        public static DateType Parse(String s, IFormatProvider provider, DateTimeStyles styles) {
	    return new DateType(DateTime.Parse(s, provider, styles).Date);
        }
        
        public static DateType ParseExact(String s, String format, IFormatProvider provider) {
	    return new DateType(DateTime.ParseExact(s, format, provider).Date);
        }

        public static DateType ParseExact(String s, String format, IFormatProvider provider, DateTimeStyles style) {
	    return new DateType(DateTime.ParseExact(s, format, provider, style).Date);
        }    

        public static DateType ParseExact(String s, String[] formats, IFormatProvider provider, DateTimeStyles style) {
	    return new DateType(DateTime.ParseExact(s, formats, provider, style).Date);
        }
	#endregion

	#region To/From DateTimeType methods
	public DateTimeType ToDateTimeType() 
	{
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return new DateTimeType(myValue.Date);
	}

	public static DateType FromDateTimeType(DateTimeType value) {
	    if (!value.IsValid) {
		throw new InvalidStateException(value.State);
	    }

	    return new DateType(value.Date);
	}
	#endregion

	#region ToOADate and FromOADate methods
        public double ToOADate() 
	{
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToOADate();
        }
    
        public static DateType FromOADate(double value) {
	    return new DateType(DateTime.FromOADate(value).Date);
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
       
        public String ToShortDateString() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToShortDateString();
        }
        
        public override String ToString() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToString("d");
        }

        public String ToString(String format) {
 	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    if (format == null) {
	       return myValue.ToString("d");
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
	#region Subtraction operators and methods
        public TimeSpan Subtract(DateType value) 
	{
	    if (!IsValid || !value.IsValid) {
		throw new InvalidStateException(myState, value.myState);
	    }
	    
	    return myValue.Subtract(value.myValue);
        }
    
        public DateType Subtract(TimeSpan value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return new DateType(myValue - value);
        }

        public static DateType operator -(DateType dateToSubtractFrom, TimeSpan timeToSubtract) {
	    if (!dateToSubtractFrom.IsValid) {
		throw new InvalidStateException(dateToSubtractFrom.myState);
	    }

            return new DateType(dateToSubtractFrom.myValue.Ticks - timeToSubtract.Ticks);
        }
    
        public static TimeSpan operator -(DateType leftHand, DateType rightHand) {
	    if (!leftHand.IsValid || !rightHand.IsValid) {
		throw new InvalidStateException(leftHand.myState, rightHand.myState);
	    }

	    return new TimeSpan(leftHand.Ticks - rightHand.Ticks);
        }
	#endregion 

	#region Addition operators and methods
        public static DateType operator +(DateType dateToAddTo, TimeSpan timeToAdd) 
	{
	    if (!dateToAddTo.IsValid) {
		throw new InvalidStateException(dateToAddTo.myState);
	    }

	    DateTime result = new DateTime(dateToAddTo.Ticks + timeToAdd.Ticks);
            return new DateType(result.Date);
        }

	public DateType Add(TimeSpan value) 
	{
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    DateTime result = new DateTime(Ticks + value.Ticks);
            return new DateType(result.Date);
        }
    
   
        public DateType AddDays(double value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return new DateType(myValue.AddDays(value).Date);
        }
    
        public DateType AddMonths(int months) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return new DateType(myValue.AddMonths(months).Date);
        }
    
        public DateType AddTicks(long value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return new DateType(myValue.AddTicks(value).Date);
        }
    
        public DateType AddYears(int value) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

            return new DateType(myValue.AddYears(value).Date);
        }
	#endregion 
    
	#endregion

	#region Equality operators and methods

	//what should we do here?
	//if params not valid do we fail?
	//or return false?
        public static bool Equals(DateType leftHand, DateType rightHand) {
	    if (!leftHand.IsValid || !rightHand.IsValid) {
		throw new InvalidStateException(leftHand.myState, rightHand.myState);
	    }

	    return leftHand.myValue.Date.Ticks == rightHand.myValue.Date.Ticks;
        }

        public static bool operator ==(DateType leftHand, DateType rightHand) 
	{
	    return Compare(leftHand, rightHand) == 0;
        }

        public static bool operator !=(DateType leftHand, DateType rightHand) {
	    return Compare(leftHand, rightHand) != 0;
        }
        
        public static bool operator <(DateType leftHand, DateType rightHand) {
	    return Compare(leftHand, rightHand) < 0;
        }

        public static bool operator <=(DateType leftHand, DateType rightHand) {
	    return Compare(leftHand, rightHand) <= 0;
        }

        public static bool operator >(DateType leftHand, DateType rightHand) {
	    return Compare(leftHand, rightHand) > 0;
        }

        public static bool operator >=(DateType leftHand, DateType rightHand) {
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
            throw new InvalidCastException("DateType", "Boolean");
        }

        char IConvertible.ToChar(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "Char");
        }

        [CLSCompliant(false)]
        sbyte IConvertible.ToSByte(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "SByte");
        }

        byte IConvertible.ToByte(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "Byte");
        }

        short IConvertible.ToInt16(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "Int16");
        }

        [CLSCompliant(false)]
        ushort IConvertible.ToUInt16(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "UInt16");
        }

        int IConvertible.ToInt32(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "Int32");
        }

        [CLSCompliant(false)]
        uint IConvertible.ToUInt32(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "UInt32");
        }

        long IConvertible.ToInt64(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "Int64");
        }

        [CLSCompliant(false)]
        ulong IConvertible.ToUInt64(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "UInt64");
        }

        float IConvertible.ToSingle(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "Single");
        }

        double IConvertible.ToDouble(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "Double");
        }

        Decimal IConvertible.ToDecimal(IFormatProvider provider) {
            throw new InvalidCastException("DateType", "Decimal");
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
            if (value is DateType) {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

                return myValue.Ticks == ((DateType)value).myValue.Ticks;
            }

	    return false;
        }

	#endregion
    }
}
