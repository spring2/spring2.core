using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Spring2.Types {

    [System.Serializable, System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct CurrencyType : IFormattable, IComparable, IConvertible {

	private Decimal   myValue;
	private TypeState myState;

	public static readonly CurrencyType Zero = new CurrencyType(Decimal.Zero, TypeState.VALID);
    
	public static readonly CurrencyType One = new CurrencyType(Decimal.One, TypeState.VALID);
    
	public static readonly CurrencyType MinusOne = new CurrencyType(Decimal.MinusOne, TypeState.VALID);
    
	public static readonly CurrencyType MaxValue = new CurrencyType(Decimal.MaxValue, TypeState.VALID);

	public static readonly CurrencyType MinValue = new CurrencyType(Decimal.MinValue, TypeState.VALID);

	public static readonly CurrencyType DEFAULT = new CurrencyType(0, TypeState.DEFAULT);
	public static readonly CurrencyType UNSET   = new CurrencyType(0, TypeState.UNSET);

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
	private CurrencyType(decimal value, TypeState state) {
	    myValue = value;
	    myState = state;
	}

	public CurrencyType(char value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}

	public CurrencyType(byte value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}

	public CurrencyType(sbyte value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}

	public CurrencyType(short value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}
    
	[CLSCompliant(false)]
	public CurrencyType(ushort value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}

	public CurrencyType(int value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}

	[CLSCompliant(false)]
	public CurrencyType(uint value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}

	// Constructs a Decimal from a long value.
	//
	public CurrencyType(long value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}
    
	// Constructs a Decimal from an unsigned long value.
	//
	[CLSCompliant(false)]
	public CurrencyType(ulong value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}
    
	// Constructs a Decimal from a float value.
	//
	public CurrencyType(float value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}

	// Constructs a Decimal from a double value.
	//
	public CurrencyType(double value) {
	    myValue = new decimal(value);
	    myState = TypeState.VALID;
	}        
        
	public CurrencyType(Decimal value) {
	    myValue = value;
	    myState = TypeState.VALID;
	}

	public CurrencyType(int[] bits) {
	    myValue = new decimal(bits);
	    myState = TypeState.VALID;
	}
    
	public CurrencyType(int lo, int mid, int hi, bool isNegative, byte scale) {
	    myValue = new decimal(lo, mid, hi, isNegative, scale);
	    myState = TypeState.VALID;
	}

	// Constructs a Decimal from a Currency value.
	//
#if notsure
        internal CurrencyType(Currency value) 
	{
            Decimal temp = Currency.ToDecimal(value);
            this.lo = temp.lo;
            this.mid = temp.mid;
            this.hi = temp.hi;
            this.flags = temp.flags;
        }
#endif
	#endregion

	#region Cast operators
	public static explicit operator Decimal(CurrencyType castFrom) {
	    if (!castFrom.IsValid) {
		throw new InvalidStateException(castFrom.myState);
	    }

	    return castFrom.myValue;
	}

	public static implicit operator CurrencyType(Decimal castFrom) {
	    return new DecimalType(castFrom);
	}

	public static explicit operator DecimalType(CurrencyType castFrom) {
	    if (!castFrom.IsValid) {
		throw new InvalidStateException(castFrom.myState);
	    }

	    return new DecimalType(castFrom.myValue);
	}

	public static implicit operator CurrencyType(DecimalType castFrom) {
	    return new CurrencyType((decimal) castFrom);
	}
	#endregion

	#region General math methods

	public static CurrencyType Negate(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    CurrencyType result = new CurrencyType(Decimal.Negate(value.myValue), TypeState.VALID);
	    return result;
	}


	//min/max aren't decimal methods but might be handy
	public static CurrencyType Max(CurrencyType leftHand, CurrencyType rightHand) {
	    if (Compare(leftHand, rightHand) >= 0) {
		return leftHand;
	    }

	    return rightHand;
	}

	public static CurrencyType Min(CurrencyType leftHand, CurrencyType rightHand) {
	    if (Compare(leftHand, rightHand) < 0) {
		return leftHand;
	    }

	    return rightHand;
	}

	public static CurrencyType Round(CurrencyType value, int decimals) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    CurrencyType result = new CurrencyType(Decimal.Round(value.myValue, decimals), TypeState.VALID);
	    return result;
	}

	public static CurrencyType Floor(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    CurrencyType result = new CurrencyType(Decimal.Floor(value.myValue), TypeState.VALID);
	    return result;
	}

	public static CurrencyType Truncate(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    CurrencyType result = new CurrencyType(Decimal.Truncate(value.myValue), TypeState.VALID);
	    return result;
	}

	#endregion
    
	#region ToString and Parsing

	public override String ToString() {
	    return myValue.ToString(null, null);
	}


	public String ToString(String format) {
	    return ToString(format, null);
	}

	public String ToString(IFormatProvider provider) {
	    return ToString(null, provider);
	}    

	public String ToString(String format, IFormatProvider provider) {
	    if (myState != TypeState.VALID) {
		throw new InvalidValueException(myState);
	    }

	    return myValue.ToString(format, provider);
	}


	public static CurrencyType Parse(String from) {
	    return Parse(from, NumberStyles.Number, null);
	}
    
	public static CurrencyType Parse(String from, NumberStyles style) {
	    return Parse(from, style, null);
	}

	public static CurrencyType Parse(String from, IFormatProvider provider) {
	    CurrencyType result;
	    result.myValue = Decimal.Parse(from, provider);
	    result.myState = TypeState.VALID;

	    return result;
	}
    
	public static CurrencyType Parse(String from, NumberStyles style, IFormatProvider provider) {
	    CurrencyType result;
	    result.myValue = Decimal.Parse(from, style, provider);
	    result.myState = TypeState.VALID;

	    return result;
	}

	#endregion
   
	#region To<type> conversion methods
    
	public static byte ToByte(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToByte(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "byte");
	    }
	}
    
	[CLSCompliant(false)]
	public static sbyte ToSByte(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToSByte(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "sbyte");
	    }
	}

	public static short ToInt16(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToInt16(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "short");
	    }
	}

	[CLSCompliant(false)]
	public static ushort ToUInt16(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToUInt16(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "ushort");
	    }
	}

	public static int ToInt32(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToInt32(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "int");
	    }
	}
    
	[CLSCompliant(false)]
	public static uint ToUInt32(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToUInt32(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "uint");
	    }
	}

	public static long ToInt64(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToInt64(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "long");
	    }
	}

	[CLSCompliant(false)]
	public static ulong ToUInt64(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToUInt64(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "ulong");
	    }
	}
    
#if no
        public static Currency ToCurrency(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToCurrency(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "Currency");
	    }
        }
#endif

	public static double ToDouble(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToDouble(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "double");
	    }
	}
    
	public static float ToSingle(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    try {
		return Decimal.ToSingle(value.myValue);
	    } catch (OverflowException) {
		throw new ValueOverflowException("CurrencyType", "float");
	    }
	}

	public static decimal ToDecimal(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    return value.myValue;
	}
    
	#endregion	      	    	

	#region Conversion Operators

	public static implicit operator CurrencyType(byte value) {
	    return new CurrencyType(value);
	}
    
	[CLSCompliant(false)]
	public static implicit operator CurrencyType(sbyte value) {
	    return new CurrencyType(value);
	}
    
	public static implicit operator CurrencyType(short value) {
	    return new CurrencyType(value);
	}
    
	[CLSCompliant(false)]
	public static implicit operator CurrencyType(ushort value) {
	    return new CurrencyType(value);
	}

	public static implicit operator CurrencyType(char value) {
	    return new CurrencyType(value);
	}
    
	public static implicit operator CurrencyType(int value) {
	    return new CurrencyType(value);
	}
    
	[CLSCompliant(false)]
	public static implicit operator CurrencyType(uint value) {
	    return new CurrencyType(value);
	}
    
	public static implicit operator CurrencyType(long value) {
	    return new CurrencyType(value);
	}
    
	[CLSCompliant(false)]
	public static implicit operator CurrencyType(ulong value) {
	    return new CurrencyType(value);
	}
        
    
	public static explicit operator CurrencyType(float value) {
	    return new CurrencyType(value);
	}
    
	public static explicit operator CurrencyType(double value) {
	    return new CurrencyType(value);
	}
    
	public static explicit operator byte(CurrencyType value) {
	    return ToByte(value);
	}
    
	[CLSCompliant(false)]
	public static explicit operator sbyte(CurrencyType value) {
	    return ToSByte(value);
	}
    
	public static explicit operator char(CurrencyType value) {
	    return (char) ToUInt16(value);
	}

	public static explicit operator short(CurrencyType value) {
	    return ToInt16(value);
	}
    
	[CLSCompliant(false)]
	public static explicit operator ushort(CurrencyType value) {
	    return ToUInt16(value);
	}
    
	public static explicit operator int(CurrencyType value) {
	    return ToInt32(value);
	}
		
	[CLSCompliant(false)]
	public static explicit operator uint(CurrencyType value) {
	    return ToUInt32(value);
	}
    
	public static explicit operator long(CurrencyType value) {
	    return ToInt64(value);
	}
    
	[CLSCompliant(false)]
	public static explicit operator ulong(CurrencyType value) {
	    return ToUInt64(value);
	}
    
	public static explicit operator float(CurrencyType value) {
	    return ToSingle(value);
	}
    
	public static explicit operator double(CurrencyType value) {
	    return ToDouble(value);
	}
    
	#endregion

	#region Addition operators and methods

	public static CurrencyType Add(CurrencyType augend, CurrencyType addend) {
	    if (augend.myState != TypeState.VALID || addend.myState != TypeState.VALID) {
		throw new InvalidValueException(augend.myState, addend.myState);
	    }

	    CurrencyType result;
	    
	    result.myValue = decimal.Add(augend.myValue, addend.myValue);
	    result.myState = TypeState.VALID;
	    return result;
	}

	public static CurrencyType operator ++(CurrencyType augend) {
	    return Add(augend, One);
	}

	public static CurrencyType operator +(CurrencyType augend, CurrencyType addend) {
	    return Add(augend, addend);
	}
	
	public static CurrencyType operator +(CurrencyType augend) {
	    return augend;
	}

	#endregion

	#region Subtraction operators and methods

	public static CurrencyType Subtract(CurrencyType minuend, CurrencyType subtrahend) {
	    if (minuend.myState != TypeState.VALID || subtrahend.myState != TypeState.VALID) {
		throw new InvalidValueException(minuend.myState, subtrahend.myState);
	    }

	    CurrencyType result = new CurrencyType(Decimal.Subtract(minuend.myValue, subtrahend.myValue), TypeState.VALID);

	    return result;
	}

	public static CurrencyType operator --(CurrencyType minuend) {
	    return Subtract(minuend, One);
	}
    
    
	public static CurrencyType operator -(CurrencyType minuend, CurrencyType subtrahend) {
	    return Subtract(minuend, subtrahend);
	}

	#endregion

	#region Multiplication operators and methods
	
	public static CurrencyType Multiply(CurrencyType multiplier, CurrencyType multiplicand) {
	    if (multiplier.myState != TypeState.VALID || multiplicand.myState != TypeState.VALID) {
		throw new InvalidValueException(multiplier.myState, multiplicand.myState);
	    }

	    CurrencyType quotient = new CurrencyType(Decimal.Multiply(multiplier.myValue, multiplicand.myValue), TypeState.VALID);

	    return quotient;
	}

	public static CurrencyType operator *(CurrencyType multiplier, CurrencyType multiplicand) {
	    return Multiply(multiplier, multiplicand);
	}

	#endregion

	#region Division operators and methods

	public static CurrencyType Divide(CurrencyType dividend, CurrencyType divisor) {
	    if (dividend.myState != TypeState.VALID || divisor.myState != TypeState.VALID) {
		throw new InvalidValueException(dividend.myState, divisor.myState);
	    }

	    CurrencyType quotient = new CurrencyType(Decimal.Divide(dividend.myValue, divisor.myValue), TypeState.VALID);

	    return quotient;
	}

	public static CurrencyType operator/ (CurrencyType dividend, CurrencyType divisor) {
	    return Divide(dividend, divisor);
	}

	#endregion

	#region Modulus operators and methods

	public static CurrencyType Remainder(CurrencyType dividend, CurrencyType divisor) {
	    if (dividend.myState != TypeState.VALID || divisor.myState != TypeState.VALID) {
		throw new InvalidValueException(dividend.myState, divisor.myState);
	    }

	    CurrencyType remainder;
	    remainder.myValue = decimal.Remainder(dividend.myValue, divisor.myValue);
	    remainder.myState = TypeState.VALID;

	    return remainder;
	}

	public static CurrencyType operator %(CurrencyType dividend, CurrencyType divisor) {
	    return Remainder(dividend, divisor);
	}
    
	#endregion

	#region Equality operators and methods
	public static int Compare(CurrencyType leftHand, CurrencyType rightHand) {
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

		if (leftHand.myState == TypeState.UNSET) {
		    return 1;
		}

		return -1;
	    }

	    //should this throw an exception?
	    return 0;
	}

	public static bool operator ==(CurrencyType leftHand, CurrencyType rightHand) {
	    return Compare(leftHand, rightHand) == 0;
	}

	public static bool operator !=(CurrencyType leftHand, CurrencyType rightHand) {
	    return Compare(leftHand, rightHand) != 0;
	}

	public static bool operator <(CurrencyType leftHand, CurrencyType rightHand) {
	    return Compare(leftHand, rightHand) < 0;
	}

	public static bool operator <=(CurrencyType leftHand, CurrencyType rightHand) {
	    return Compare(leftHand, rightHand) <= 0;
	}

	public static bool operator >(CurrencyType leftHand, CurrencyType rightHand) {
	    return Compare(leftHand, rightHand) > 0;
	}

	public static bool operator >=(CurrencyType leftHand, CurrencyType rightHand) {
	    return Compare(leftHand, rightHand) >= 0;
	}

	#endregion

	#region IConvertible and other conversions

	bool IConvertible.ToBoolean(IFormatProvider provider) {
	    return Convert.ToBoolean(this);
	}


	char IConvertible.ToChar(IFormatProvider provider) {
	    throw new InvalidCastException("Decimal", "Char");
	}

	[CLSCompliant(false)]
	sbyte IConvertible.ToSByte(IFormatProvider provider) {
	    return Convert.ToSByte(this);
	}

	byte IConvertible.ToByte(IFormatProvider provider) {
	    return Convert.ToByte(this);
	}

	short IConvertible.ToInt16(IFormatProvider provider) {
	    return Convert.ToInt16(this);
	}

	[CLSCompliant(false)]
	ushort IConvertible.ToUInt16(IFormatProvider provider) {
	    return Convert.ToUInt16(this);
	}

	int IConvertible.ToInt32(IFormatProvider provider) {
	    return Convert.ToInt32(this);
	}

	[CLSCompliant(false)]
	uint IConvertible.ToUInt32(IFormatProvider provider) {
	    return Convert.ToUInt32(this);
	}

	long IConvertible.ToInt64(IFormatProvider provider) {
	    return Convert.ToInt64(this);
	}

	[CLSCompliant(false)]
	ulong IConvertible.ToUInt64(IFormatProvider provider) {
	    return Convert.ToUInt64(this);
	}

	float IConvertible.ToSingle(IFormatProvider provider) {
	    return Convert.ToSingle(this);
	}

	double IConvertible.ToDouble(IFormatProvider provider) {
	    return Convert.ToDouble(this);
	}

	Decimal IConvertible.ToDecimal(IFormatProvider provider) {
	    Decimal d = Convert.ToDecimal(this.myValue);
	    return d;
	}

	DateTime IConvertible.ToDateTime(IFormatProvider provider) {
	    throw new InvalidCastException("Decimal", "DateTime");
	}

	Object IConvertible.ToType(Type type, IFormatProvider provider) {
	    return null;
	    //            return Convert.DefaultToType((IConvertible)this, type, provider);
	}

	#endregion

	#region Object Support and other stuff
    
	int IComparable.CompareTo(Object value) {
	    if (!(value is CurrencyType)) {
		throw new InvalidTypeException("CurrencyType");
	    }

	    if (value == null) {
		throw new InvalidArgumentException("value");
	    }

	    CurrencyType compareTo = (CurrencyType) value;

	    return Compare(this, compareTo);
	}
    	
	public int CompareTo(CurrencyType value) {
	    return Compare(this, value);
	}

	public override bool Equals(Object value) {
	    if (value is CurrencyType) {
		return Compare(this, (CurrencyType) value) == 0;
	    }

	    return false;
	}

	public static bool Equals(CurrencyType leftHand, CurrencyType rightHand) {
	    return Compare(leftHand, rightHand) == 0;
	}
	public override int GetHashCode() {
	    return myValue.GetHashCode();
	}
	
    
	public static int[] GetBits(CurrencyType value) {
	    if (value.myState != TypeState.VALID) {
		throw new InvalidValueException(value.myState);
	    }

	    return decimal.GetBits(value.myValue);
	    //	    return new int[] {value.myValue.lo, value.myValue.mid, value.myValue.hi, value.myValue.flags};
	}

	public TypeCode GetTypeCode() {
	    return TypeCode.Decimal;
	}

	#endregion
    }
}
